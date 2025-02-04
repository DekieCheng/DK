﻿using NPOI.HSSF.UserModel;
using NPOI.HSSF.Util;
using NPOI.SS.UserModel;
using NPOI.SS.Util;
using NPOI.XSSF.UserModel;
using SDPSubSystem.Data;
using SDPSubSystem.Model;
using SDPSubSystem.Services.Common;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Net.Mail;
using System.Net.Mime;
using System.Text;
using System.Threading.Tasks;

namespace SDPSubSystem.Services.ESDAudit
{
    public class ESDAuditService : BaseService
    {
        private static log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        

        /// <summary>
        /// 
        /// </summary>
        /// <param name="fileName"></param>
        /// <param name="ds"></param>
        /// <returns></returns>
        public static MemoryStream GetExportFile(string fileName, DataSet ds)
        {
            MemoryStream ms = new MemoryStream();
            IWorkbook book;
            try
            {
                using (FileStream stream = new FileStream(fileName, FileMode.Open, FileAccess.Read))
                {
                    if (Path.GetExtension(fileName) == ".xlsx")
                    {
                        book = new XSSFWorkbook(stream);
                    }
                    else
                    {
                        book = new HSSFWorkbook(stream);
                    }
                }
                ISheet sheet = book.GetSheetAt(0);


                DataTable dt = ds.Tables[0];

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    //IRow colRow = sheet.GetRow(0);
                    int type = Convert.ToInt32(dt.Rows[i][4].ToString());
                    //当type=某一行的第一列的时候，循环所有行，看每一行的第一个单元格是否等于type，如果等于，就在这一行的下两行（查找到下两行的单元格内容为空的地方开始）插入数据，当typeID为混合type的时候，也是这样：先匹配type，找插入的地方，再定空白单元格的地方。第1个单元格和第10个单元格是否为空。
                    //当type=18的时候，下四行开始插入数据
                    var rows =sheet.LastRowNum;
                    for (int j = 7; j < rows; j++)
                    {
                        IRow dr = sheet.GetRow(j);
                        if (dr == null)
                        {
                            continue;
                        }

                        if (type == Convert.ToInt32(dr.GetCell(0).ToString().Trim()))
                        {
                            var insertRow=j;
                            if (type == 18)
                            {
                                insertRow = j + 4;
                            }
                            else
                            {
                                insertRow = j + 2;
                            }
                            //ICell icell = sheet.GetRow(insertRow).GetCell(0);
                            if (sheet.GetRow(insertRow).GetCell(0)==null || string.IsNullOrWhiteSpace(sheet.GetRow(insertRow).GetCell(0).StringCellValue))
                            {
                                sheet.GetRow(insertRow).Cells[0].SetCellValue(dt.Rows[i]["desc"].ToString());
                                sheet.GetRow(insertRow).Cells[2].SetCellValue(dt.Rows[i]["FlexID"].ToString());
                                sheet.GetRow(insertRow).Cells[4].SetCellValue(dt.Rows[i]["value"].ToString());
                                sheet.GetRow(insertRow).Cells[6].SetCellValue(dt.Rows[i]["Result"].ToString());
                                
                                
                            }
                            else
                            {
                                if(sheet.GetRow(insertRow).GetCell(9) == null || string.IsNullOrWhiteSpace(sheet.GetRow(insertRow).GetCell(9).StringCellValue))
                                {
                                    sheet.GetRow(insertRow).Cells[7].SetCellValue(dt.Rows[i]["desc"].ToString());
                                    sheet.GetRow(insertRow).Cells[9].SetCellValue(dt.Rows[i]["FlexID"].ToString());
                                    sheet.GetRow(insertRow).Cells[11].SetCellValue(dt.Rows[i]["value"].ToString());
                                    sheet.GetRow(insertRow).Cells[13].SetCellValue(dt.Rows[i]["Result"].ToString());
                                }
                                else
                                {
                                    var rowSource = sheet.GetRow(insertRow);
                                    var rowStyle = rowSource.RowStyle;

                                    sheet.ShiftRows(insertRow, sheet.LastRowNum, 1, true, false);

                                    IRow rowInsert = sheet.CreateRow(insertRow);


                                    if (rowStyle != null)
                                        rowInsert.RowStyle = rowStyle;
                                    rowInsert.Height = rowSource.Height;

                                    for (int col = 0; col < rowSource.LastCellNum; col++)
                                    {
                                        var cellsource = rowSource.GetCell(col);
                                        var cellInsert = rowInsert.CreateCell(col);
                                        var cellStyle = cellsource.CellStyle;

                                        //设置单元格样式　　　　
                                        if (cellStyle != null)
                                            cellInsert.CellStyle = cellsource.CellStyle;
                                    }

                                    //合并单元格
                                    CellRangeAddress region = null;
                                    for (int u = 0; u < 6; u = u + 2)
                                    {
                                        region = new CellRangeAddress(insertRow, insertRow, u, u + 1);
                                        sheet.AddMergedRegion(region);
                                    }
                                    CellRangeAddress regionr = null;
                                    for (int u = 7; u < 13; u = u + 2)
                                    {
                                        regionr = new CellRangeAddress(insertRow, insertRow, u, u + 1);
                                        sheet.AddMergedRegion(regionr);
                                    }

                                    rowInsert.Cells[0].SetCellValue(dt.Rows[i]["desc"].ToString());
                                    rowInsert.Cells[2].SetCellValue(dt.Rows[i]["FlexID"].ToString());
                                    rowInsert.Cells[4].SetCellValue(dt.Rows[i]["value"].ToString());
                                    rowInsert.Cells[6].SetCellValue(dt.Rows[i]["Result"].ToString());
                                }
                            }
                            break; 
                        }

                    }
                    
                }
                
                book.Write(ms);
                book.Close();

                ms.Seek(0, SeekOrigin.Begin);
                return ms;
            }
            catch (Exception ex)
            {
                throw new Exception("Failed to open the file");

            }
        }


        public static Boolean ESDDailyReportEmail(MemoryStream ms)
        {
            string msg = "";
            List<string> errMsg = new List<string>();
            Boolean result = true;
            try
            {
                string mailaddress = "phyllis.lv@flex.com";
                string part_mail_content = "This is ESD Audit Daily Report. Please take care this email.";

                string subjects = "ESD Audit Daily Report";
                string body = "<html><body>Hi : \r\n" + part_mail_content + " ,Click <a href='http://172.30.31.17/'>here</a></body></html> ";
                AlternateView htmlBody = AlternateView.CreateAlternateViewFromString(body, null, MediaTypeNames.Text.Html);

                string time = DateTime.Now.ToString("yyyy-MM-dd") + " " + DateTime.Now.Hour.ToString() + "-" + DateTime.Now.Minute.ToString() + "-" + DateTime.Now.Second.ToString();

                string attachment = "DMNQAI-021-F2.03 ESD System Audit Measurement Daily Report " + time + ".xlsx";
                string attachments = attachment;
                string ExcelContentType = "application/ms-excel";

                result = sdpMail.sdpSendMail(ConfigurationSettings.AppSettings["MailServer"].ToString(), ConfigurationSettings.AppSettings["MailPort"].ToString(), false, "Flex.ESDAudit", "", "Flex.ESDDailyReport@flex.com", "ESD Audit System", mailaddress, "", "", System.Net.Mail.MailPriority.High, false, subjects, htmlBody, attachments, ref msg, ms, ExcelContentType, attachment);

                log.Info("BaseController ESDDailyReportEmail   Boolean result:" + result);
                log.Info("BaseController ESDDailyReportEmail    msg:" + msg);
                //if (result == true)
                //{
                //    return Json(new JsonData(JsonData.FLAG_SUCCESS, null, "OK"));
                //}
                //else
                //{
                //    return Json(new JsonData(JsonData.FLAG_ERROR, null, msg));

                //}
                return result;
            }
            catch (Exception ex)
            {
                //errMsg.Add(ex.Message);
                log.Error(ex.Message);
                log.Info("BaseController confirmSendingNoticeEmail    msg" + msg);
            }
            return result;
            //return Json(new JsonData(JsonData.FLAG_ERROR, "Last END", errMsg));
        }

        public static Boolean ESDFailReportEmail(MemoryStream ms, DataSet ds)
        {
            string msg = "";
            List<string> errMsg = new List<string>();
            Boolean result = true;
            try
            {
                //string mailaddress = getESDFailRecieversMailAddresses(ds);
                string mailaddress = "phyllis.lv@flex.com";
                string part_mail_content = "You have Fail ESD Audit. Please Add your action.";

                string subjects = "ESD Audit Fail Notice";
                string body = "<html><body>Hi : \r\n" + part_mail_content + " ,Click <a href='http://172.30.31.17/'>here</a></body></html> ";
                AlternateView htmlBody = AlternateView.CreateAlternateViewFromString(body, null, MediaTypeNames.Text.Html);

                string time = DateTime.Now.ToString("yyyy-MM-dd") + " " + DateTime.Now.Hour.ToString() + "-" + DateTime.Now.Minute.ToString() + "-" + DateTime.Now.Second.ToString();

                string attachment = "DMNQAI-021-F2.03 ESD System Audit Measurement Fail Report " + time + ".xlsx";
                string attachments = attachment;
                string ExcelContentType = "application/ms-excel";

                result = sdpMail.sdpSendMail(ConfigurationSettings.AppSettings["MailServer"].ToString(), ConfigurationSettings.AppSettings["MailPort"].ToString(), false, "Flex.ESDAudit", "", "Flex.ESDAudit@flex.com", "ESD Audit System", mailaddress, "", "", System.Net.Mail.MailPriority.High, false, subjects, htmlBody, attachments, ref msg, ms, ExcelContentType, attachment);

                log.Info("BaseController ESDDailyReportEmail   Boolean result:" + result);
                log.Info("BaseController ESDDailyReportEmail    msg:" + msg);
                //if (result == true)
                //{
                //    return Json(new JsonData(JsonData.FLAG_SUCCESS, null, "OK"));
                //}
                //else
                //{
                //    return Json(new JsonData(JsonData.FLAG_ERROR, null, msg));

                //}
                return result;
            }
            catch (Exception ex)
            {
                //errMsg.Add(ex.Message);
                log.Error(ex.Message);
                log.Info("BaseController confirmSendingNoticeEmail    msg" + msg);
            }
            return result;
            //return Json(new JsonData(JsonData.FLAG_ERROR, "Last END", errMsg));
        }


        /// <summary>
        /// 获取收件人
        /// </summary>
        /// <param name="ds"></param>
        /// <returns></returns>
        public static string getESDFailRecieversMailAddresses(DataSet ds)
        {
            DataTable dt = ds.Tables[1];
            string addresses = "";
            int level;
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                //1.先看creationTime距今几天
                DateTime creationTime = Convert.ToDateTime(dt.Rows[i][2].ToString());
                level= 0;
                TimeSpan t3 = DateTime.Now - creationTime;
                double getHours = t3.TotalHours;
                if (getHours > 48 && getHours<=72) 
                {
                    //第3天
                    level = 1;
                }else if(getHours>72 && getHours <= 96)
                {
                    //第4天
                    level = 2;
                }
                else if (getHours > 96 && getHours <= 120)
                {
                    //第5天
                    level = 3;
                }
                else if (getHours > 120)
                {
                    //第6天以后
                    level = 4;
                }

                //2.再看邮箱地址
                if (level == 0)
                {
                    string Operator= dt.Rows[i][0].ToString();
                    addresses += GetUserInfoByEmpNO(Operator).MailAddress + ",";
                    //不用escalate上级
                    addresses += dt.Rows[i][1].ToString() + ",";
                }
                else
                {
                    string Owner = dt.Rows[i][1].ToString();
                    bool hod = false;

                    string ReportTo= GetUserInfoByEmpEmailAddress(Owner).ReportTo;

                    for (int j=0; j < level; j++)
                    {
                        addresses += GetUserInfoByEmpNO(ReportTo).MailAddress + ",";

                        ReportTo = GetUserInfoByEmpNO(ReportTo).ReportTo;
                        //3.比较上级是否是HOD
                        DataTable dt2 = ds.Tables[2];
                        for (int w = 0; w < dt2.Rows.Count; w++)
                        {
                            if (dt2.Rows[w][0].ToString().Equals(ReportTo))
                            {
                                hod = true;
                                break;
                            }
                        }
                        if (hod == true)
                        {
                            break;
                        }
                    }
                }
            }

            return addresses;
        }


        public static MemoryStream GetExportFile_WristStrap(string fileName, DataSet ds,string date)
        {
            MemoryStream ms = new MemoryStream();
            IWorkbook book;
            try
            {
                using (FileStream stream = new FileStream(fileName, FileMode.Open, FileAccess.Read))
                {
                    if (Path.GetExtension(fileName) == ".xlsx")
                    {
                        book = new XSSFWorkbook(stream);
                    }
                    else
                    {
                        book = new HSSFWorkbook(stream);
                    }
                }
                HSSFSheet sheet = (HSSFSheet)book.GetSheetAt(0);

                DataTable dt = ds.Tables[0];

                //IRow colRow = sheet.GetRow(0);
                string project = dt.Rows[0][1].ToString();
                string line = dt.Rows[0][2].ToString();
                //string s1="项目(Project):" + project;
                //string s2 = "线名(Line):" + line;
                sheet.GetRow(1).Cells[7].SetCellValue(project);
                sheet.GetRow(1).Cells[16].SetCellValue(line);

                string year = date.Substring(0, 4);
                string month = date.Substring(5);
                string time = year + "(Y)年" + month + "(M)月";
                sheet.GetRow(1).Cells[27].SetCellValue(time);

                sheet.ShiftRows(23, 27, dt.Rows.Count -20, true, false);

                string val = "";
                //string color = "";
                

                //ICellStyle RowStyle1 = sheet.GetRow(3).RowStyle;


                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    HSSFRow dr = (HSSFRow)sheet.CreateRow(i+3);

                    //dr.RowStyle = RowStyle1;
                    dr.CreateCell(0);
                    dr.CreateCell(1);

                    HSSFFont ffont = (HSSFFont)book.CreateFont();
                    HSSFCellStyle fCellStyle = (HSSFCellStyle)book.CreateCellStyle();
                    ffont.Boldweight = (short)FontBoldWeight.Bold;
                    ffont.Color = HSSFColor.Black.Index;
                    fCellStyle.SetFont(ffont);
                    fCellStyle.WrapText = true;
                    fCellStyle.BorderLeft = NPOI.SS.UserModel.BorderStyle.Thin;
                    fCellStyle.BorderTop = BorderStyle.Medium;
                    fCellStyle.BorderRight = BorderStyle.Thin;
                    fCellStyle.BorderBottom = BorderStyle.Thin;
                    fCellStyle.Alignment = NPOI.SS.UserModel.HorizontalAlignment.Center;
                    fCellStyle.VerticalAlignment = VerticalAlignment.Center;

                    ffont.Color = HSSFColor.Black.Index;
                    fCellStyle.SetFont(ffont);
                    dr.Cells[0].CellStyle = fCellStyle;
                    dr.Cells[1].CellStyle = fCellStyle;

                    dr.Cells[0].SetCellValue(i+1);
                    dr.Cells[1].SetCellValue(dt.Rows[i][0].ToString());
                    //sheet.GetRow(i + 3).Cells[0].SetCellValue(i+1);
                    //sheet.GetRow(i + 3).Cells[1].SetCellValue(dt.Rows[i][0].ToString());


                    HSSFFont ffont_r = (HSSFFont)book.CreateFont();
                    ffont_r.Boldweight = (short)FontBoldWeight.Bold;
                    for (int j = 3; j < dt.Columns.Count; j++)
                    {
                        //给字体设置颜色
                        if (dt.Rows[i][j].ToString() == "Pass")
                        {
                            val = "√";
                            //color = "green";
                            ffont_r.Color = HSSFColor.Green.Index;
                        }
                        else if (dt.Rows[i][j].ToString() == "Fail")
                        {
                            val = "x";
                            //color = "red";
                            ffont_r.Color = HSSFColor.Red.Index;
                        }

                        dr.CreateCell(2 + (j - 3));

                        dr.Cells[2 + (j - 3)].SetCellValue(val);

                        dr.Cells[2 + (j - 3)].CellStyle.SetFont(ffont_r);
                        dr.Cells[2 + (j - 3)].CellStyle.WrapText = true;
                        dr.Cells[2 + (j - 3)].CellStyle.BorderLeft = NPOI.SS.UserModel.BorderStyle.Thin;
                        dr.Cells[2 + (j - 3)].CellStyle.BorderTop = BorderStyle.Medium;
                        dr.Cells[2 + (j - 3)].CellStyle.BorderRight = BorderStyle.Thin;
                        dr.Cells[2 + (j - 3)].CellStyle.BorderBottom = BorderStyle.Thin;
                        dr.Cells[2 + (j - 3)].CellStyle.Alignment = NPOI.SS.UserModel.HorizontalAlignment.Center;
                        dr.Cells[2 + (j - 3)].CellStyle.VerticalAlignment = VerticalAlignment.Center;

                        val = "";
                    }
                }

                int count = dt.Rows.Count;
                int testerSigRowNum = count + 3;
                int engineerSigRowNum = count + 4;

                DataTable dt1 = ds.Tables[1];
                for (int j = 0; j < dt1.Columns.Count; j++)
                {
                    sheet.GetRow(testerSigRowNum).Cells[2 + j].SetCellValue(dt1.Rows[0][j].ToString());
                    sheet.GetRow(testerSigRowNum).Cells[2 + j].CellStyle.WrapText = true;
                    sheet.GetRow(testerSigRowNum).Cells[2 + j].CellStyle.Alignment = NPOI.SS.UserModel.HorizontalAlignment.Center;
                    sheet.GetRow(testerSigRowNum).Cells[2 + j].CellStyle.VerticalAlignment = VerticalAlignment.Center;
                }

                DataTable dt2 = ds.Tables[2];
                for (int j = 0; j < dt2.Columns.Count; j++)
                {
                    sheet.GetRow(engineerSigRowNum).Cells[2 + j].SetCellValue(dt2.Rows[0][j].ToString());
                    sheet.GetRow(engineerSigRowNum).Cells[2 + j].CellStyle.WrapText = true;
                    sheet.GetRow(engineerSigRowNum).Cells[2 + j].CellStyle.Alignment = NPOI.SS.UserModel.HorizontalAlignment.Center;
                    sheet.GetRow(engineerSigRowNum).Cells[2 + j].CellStyle.VerticalAlignment = VerticalAlignment.Center;
                }

                book.Write(ms);
                book.Close();

                ms.Seek(0, SeekOrigin.Begin);
                return ms;
            }
            catch (Exception ex)
            {
                throw new Exception("Failed to open the file");

            }
        }

        //public static void SetCell(IWorkbook workbook, ISheet sheet,
        //     IRow row, int createCellIndex, object cellContent, CellType cellType, HorizontalAlignment alignment)
        //{
        //    IDataFormat celldataformat = workbook.CreateDataFormat();
        //    IFont font = workbook.CreateFont();
        //    font.FontName = "Calibri";

        //    ICell cell = row.FirstOrDefault(n => n.ColumnIndex == createCellIndex);
        //    if (cell == null)
        //        cell = row.CreateCell(createCellIndex);

        //    cell.CellStyle.SetFont(font);
        //    cell.CellStyle.BorderLeft = BorderStyle.Thin;
        //    cell.CellStyle.BorderRight = BorderStyle.Thin;
        //    cell.CellStyle.BorderTop = BorderStyle.Thin;
        //    cell.CellStyle.BorderBottom = BorderStyle.Thin;

        //    //在这里设置会影响全部单元格
        //    //cell.CellStyle.Alignment = alignment;

        //    double tmp = -1;
        //    if (cellType == CellType.Numeric && double.TryParse(cellContent.ToString(), out tmp))
        //    {
        //        //必须在这里这样设置,才能对当前单元格有效
        //        ICellStyle cellstyle = workbook.CreateCellStyle();
        //        cellstyle.CloneStyleFrom(cell.CellStyle);
        //        cellstyle.DataFormat = HSSFDataFormat.GetBuiltinFormat("#,##0");
        //        cellstyle.Alignment = alignment;
        //        cell.CellStyle = cellstyle;

        //        cell.SetCellValue(tmp);
        //    }
        //    else
        //    {
        //        //必须在这里这样设置,才能对当前单元格有效
        //        ICellStyle cellstyle = workbook.CreateCellStyle();
        //        cellstyle.CloneStyleFrom(cell.CellStyle);
        //        cellstyle.Alignment = alignment;
        //        cell.CellStyle = cellstyle;
        //        cell.SetCellValue(cellContent.ToString());
        //    }
        //}

    }
}
