﻿using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SDPSubSystem.Common.Customized
{
    public class ExcelToAll
    {
        /// <summary>
        /// 把excel文件转换数据表
        /// </summary>
        /// <param name="fileName"></param>
        /// <param name="dicHeader"></param>
        /// <returns></returns>
        public static DataTable ExcelToDataTable(string fileName, Dictionary<string, object> dicHeader, List<string> columns, int sheetnum)
        {
            DataTable dt = new DataTable();
            IWorkbook book;
            try
            {
                using (FileStream stream = new FileStream(fileName, FileMode.Open, FileAccess.Read))
                {
                    if (Path.GetExtension(fileName).IndexOf("xlsx") > -1)
                    {
                        book = new XSSFWorkbook(stream);
                    }
                    else
                    {
                        book = new HSSFWorkbook(stream);
                    }
                }
            }
            catch
            {
                throw new Exception("Failed to open the file");
            }
            DataSet ds = new DataSet();

            ISheet sheet = book.GetSheetAt(sheetnum);
            IRow colRow = sheet.GetRow(0);
            StringBuilder result = new StringBuilder();
            string sb = string.Empty;
            //List<string> columns = (new string[] { "c1", "c2", "c3", "c4", "c5", "c6", "c7", "c8", "c9", "c10", "c11", "c12", "c13", "c14", "c15", "c16", "c17", "c18", "c19", "c20", "c21", "c22", "c23", "c24", "c25", "c26", "c27", "c28", "c29", "c30", "c31", "c32", "c33", "c34", "c35", "c36" }).ToList(); //new List<string>();


            foreach (var item in columns)
            {
                dt.Columns.Add(item);
            }
            string cellValue = string.Empty;
            bool isBreak = false;
            int currenti = 0;
            int currentj = 0;
            try
            {
                for (int i = 1; i <= sheet.LastRowNum; i++)
                {
                    currenti = i;
                    DataRow dr = dt.NewRow();
                    //sb = "<data ";
                    IRow row = sheet.GetRow(i);
                    for (int j = 0; j < columns.Count; j++)
                    {
                        currentj = j;
                        if (sheetnum == 3)
                        {
                            if (i == 1 || i == 2)
                            {
                                if (j == 21)
                                {
                                    ICell ilcell = row.GetCell(j);
                                }
                            }
                        }

                        ICell icell = row.GetCell(j);
                        if (icell == null)
                        {
                            cellValue = "";
                        }
                        else
                        {

                            if (icell.CellType == CellType.Numeric && DateUtil.IsCellDateFormatted(icell))
                            {
                                cellValue = icell.DateCellValue.ToString();
                            }
                            else
                            {
                                cellValue = System.Security.SecurityElement.Escape(row.GetCell(j).ToString());
                            }
                            switch (row.GetCell(j).CellType)
                            {
                                case CellType.Numeric:
                                    if (DateUtil.IsCellDateFormatted(icell))
                                    {
                                        cellValue = icell.DateCellValue.ToString();
                                    }
                                    else
                                    {
                                        cellValue = System.Security.SecurityElement.Escape(row.GetCell(j).ToString());
                                    }
                                    break;
                                case CellType.Formula:
                                    row.GetCell(j).SetCellType(CellType.String);
                                    cellValue = row.GetCell(j).ToString();
                                    break;
                                default:
                                    cellValue = row.GetCell(j).StringCellValue;
                                    break;
                            }

                        }
                        if (j == 0 && string.IsNullOrEmpty(cellValue))
                        {
                            isBreak = true; break;
                        }
                        dr[j] = cellValue;
                    }
                    if (isBreak) break;
                    dt.Rows.Add(dr);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message.ToString() + "Row is " + currenti.ToString() + "Columns is " + currentj.ToString());
            }
            return dt;
        }

        public static DataSet ExcelToDataTableByColumnsAllSheets(string fileName, Dictionary<string, object> dicHeader, List<string> columns, int sheets)
        {
            //DataTable dt = new DataTable();
            DataSet ds = new DataSet();
            IWorkbook book;
            try
            {
                using (FileStream stream = new FileStream(fileName, FileMode.Open, FileAccess.Read))
                {
                    if (Path.GetExtension(fileName).IndexOf("xlsx") > -1)
                    {
                        book = new XSSFWorkbook(stream);
                    }
                    else
                    {
                        book = new HSSFWorkbook(stream);
                    }
                }
            }
            catch
            {
                throw new Exception("Failed to open the file");
            }
            //DataSet ds = new DataSet();
            for (int k = 0; k < sheets; k++)
            {
                DataTable dt = new DataTable();
                ISheet sheet = book.GetSheetAt(k);
                IRow colRow = sheet.GetRow(0);
                StringBuilder result = new StringBuilder();
                string sb = string.Empty;
                //List<string> columns = (new string[] { "c1", "c2", "c3", "c4", "c5", "c6", "c7", "c8", "c9", "c10", "c11", "c12", "c13", "c14", "c15", "c16", "c17", "c18", "c19", "c20", "c21", "c22", "c23", "c24", "c25", "c26", "c27", "c28", "c29", "c30", "c31", "c32", "c33", "c34", "c35", "c36" }).ToList(); //new List<string>();
                //设置datatable字段
                List<string> dtcolumns = new List<string>();
                int fixi = 0;
                int fixj = 0;
                try
                {
                    for (int i = colRow.FirstCellNum, len = colRow.LastCellNum; i < len; i++)
                    {
                        fixi = i;
                        if (colRow.Cells[i].StringCellValue.Trim().Length == 0)
                        {
                            dt.Columns.Add(i.ToString());
                            dtcolumns.Add(i.ToString());
                        }
                        else
                        {
                            if (dtcolumns.Contains(colRow.Cells[i].StringCellValue))
                            {
                                dt.Columns.Add(colRow.Cells[i].StringCellValue + i.ToString());
                                dtcolumns.Add(colRow.Cells[i].StringCellValue);
                            }
                            else
                            {
                                dt.Columns.Add(colRow.Cells[i].StringCellValue);
                                dtcolumns.Add(colRow.Cells[i].StringCellValue);
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception(ex + " Error incur at  column :" + (fixi + 1).ToString());
                }

                string cellValue = string.Empty;
                bool isBreak = false;
                fixi = 0;
                fixj = 0;
                try
                {
                    for (int i = 1; i <= sheet.LastRowNum; i++)
                    {
                        fixi = i;

                        DataRow dr = dt.NewRow();
                        //sb = "<data ";
                        IRow row = sheet.GetRow(i);
                        for (int j = 0; j < dtcolumns.Count; j++)
                        {
                            fixj = j;
                            //if (fixj == 48)
                            //{
                            //    fixj = 48;
                            //}
                            //currenti = i;
                            //currentj = j;
                            //if (sheetnum == 3)
                            //{
                            //    if (i == 1 || i == 2)
                            //    {
                            //        if (j == 21)
                            //        {
                            //            ICell ilcell = row.GetCell(j);
                            //        }
                            //    }
                            //}

                            ICell icell = row.GetCell(j);
                            if (icell == null)
                            {
                                cellValue = "";
                            }
                            else
                            {
                                //if (icell.CellType == CellType.Numeric && DateUtil.IsCellDateFormatted(icell))
                                //{
                                //    cellValue = icell.DateCellValue.ToString();
                                //}
                                //else
                                //{
                                //    cellValue = System.Security.SecurityElement.Escape(row.GetCell(j).ToString());
                                //}
                                switch (row.GetCell(j).CellType)
                                {
                                    case CellType.Numeric:
                                        if (DateUtil.IsCellDateFormatted(icell))
                                        {
                                            cellValue = icell.DateCellValue.ToString();
                                        }
                                        else
                                        {
                                            cellValue = System.Security.SecurityElement.Escape(row.GetCell(j).ToString());
                                        }
                                        break;
                                    case CellType.Formula:
                                        row.GetCell(j).SetCellType(CellType.String);
                                        cellValue = row.GetCell(j).ToString();
                                        break;
                                    default:
                                        //cellValue = row.GetCell(j).StringCellValue;
                                        cellValue = row.GetCell(j).ToString();
                                        break;
                                }

                            }
                            if (j == 0 && string.IsNullOrEmpty(cellValue))
                            {
                                isBreak = true; break;
                            }
                            dr[j] = cellValue;
                        }
                        if (isBreak) break;
                        dt.Rows.Add(dr);
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception(ex + " Error incur at row: " + (fixi + 1).ToString() + " column :" + (fixj + 1).ToString());
                }
                //return dt;
                ds.Tables.Add(dt);
            }
            return ds;
        }

        /// <summary>
        /// 把excel文件转换数据表
        /// </summary>
        /// <param name="fileName"></param>
        /// <param name="dicHeader"></param>
        /// <returns></returns>
        public static DataTable ExcelToDataTableForFOL(string fileName, Dictionary<string, object> dicHeader, List<string> columns, int sheetnum)
        {
            DataTable dt = new DataTable();
            IWorkbook book;
            try
            {
                using (FileStream stream = new FileStream(fileName, FileMode.Open, FileAccess.Read))
                {
                    if (Path.GetExtension(fileName).IndexOf("xlsx") > -1)
                    {
                        book = new XSSFWorkbook(stream);
                    }
                    else
                    {
                        book = new HSSFWorkbook(stream);
                    }
                }
            }
            catch
            {
                throw new Exception("Failed to open the file");
            }
            DataSet ds = new DataSet();

            ISheet sheet = book.GetSheetAt(sheetnum);
            IRow colRow = sheet.GetRow(0);
            StringBuilder result = new StringBuilder();
            string sb = string.Empty;
            //List<string> columns = (new string[] { "c1", "c2", "c3", "c4", "c5", "c6", "c7", "c8", "c9", "c10", "c11", "c12", "c13", "c14", "c15", "c16", "c17", "c18", "c19", "c20", "c21", "c22", "c23", "c24", "c25", "c26", "c27", "c28", "c29", "c30", "c31", "c32", "c33", "c34", "c35", "c36" }).ToList(); //new List<string>();
            //设置datatable字段
            List<string> dtcolumns = new List<string>();
            int fixi = 0;
            int fixj = 0;
            try
            {
                for (int i = colRow.FirstCellNum, len = colRow.LastCellNum; i < len; i++)
                {
                    fixi = i;
                    if (colRow.Cells[i].StringCellValue.Trim().Length == 0)
                    {
                        dt.Columns.Add(i.ToString());
                        dtcolumns.Add(i.ToString());
                    }
                    else
                    {
                        if (dtcolumns.Contains(colRow.Cells[i].StringCellValue))
                        {
                            dt.Columns.Add(colRow.Cells[i].StringCellValue + i.ToString());
                            dtcolumns.Add(colRow.Cells[i].StringCellValue);
                        }
                        else
                        {
                            dt.Columns.Add(colRow.Cells[i].StringCellValue);
                            dtcolumns.Add(colRow.Cells[i].StringCellValue);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex + " Error incur at  column :" + (fixi + 1).ToString());
            }

            string cellValue = string.Empty;
            bool isBreak = false;
            fixi = 0;
            fixj = 0;
            try
            {
                for (int i = 1; i <= sheet.LastRowNum; i++)
                {
                    fixi = i;

                    DataRow dr = dt.NewRow();
                    //sb = "<data ";
                    IRow row = sheet.GetRow(i);
                    for (int j = 0; j < dtcolumns.Count; j++)
                    {
                        fixj = j;
                        //if (fixj == 48)
                        //{
                        //    fixj = 48;
                        //}
                        //currenti = i;
                        //currentj = j;
                        //if (sheetnum == 3)
                        //{
                        //    if (i == 1 || i == 2)
                        //    {
                        //        if (j == 21)
                        //        {
                        //            ICell ilcell = row.GetCell(j);
                        //        }
                        //    }
                        //}

                        ICell icell = row.GetCell(j);
                        if (icell == null)
                        {
                            cellValue = "";
                        }
                        else
                        {
                            //if (icell.CellType == CellType.Numeric && DateUtil.IsCellDateFormatted(icell))
                            //{
                            //    cellValue = icell.DateCellValue.ToString();
                            //}
                            //else
                            //{
                            //    cellValue = System.Security.SecurityElement.Escape(row.GetCell(j).ToString());
                            //}
                            switch (row.GetCell(j).CellType)
                            {
                                case CellType.Numeric:
                                    if (DateUtil.IsCellDateFormatted(icell))
                                    {
                                        cellValue = icell.DateCellValue.ToString();
                                    }
                                    else
                                    {
                                        cellValue = System.Security.SecurityElement.Escape(row.GetCell(j).ToString());
                                    }
                                    break;
                                case CellType.Formula:
                                    if (j == 1 || j==2)
                                    {
                                        row.GetCell(j).SetCellType(CellType.String);
                                        cellValue = DateTime.FromOADate(Convert.ToInt32(row.GetCell(j).ToString())).ToString();
                                    }
                                    else
                                    {
                                        row.GetCell(j).SetCellType(CellType.String);
                                        cellValue = row.GetCell(j).ToString();
                                    }

                                    break;
                                default:
                                    //cellValue = row.GetCell(j).StringCellValue;
                                    cellValue = row.GetCell(j).ToString();
                                    break;
                            }

                        }
                        if (j == 0 && string.IsNullOrEmpty(cellValue))
                        {
                            isBreak = true; break;
                        }
                        dr[j] = cellValue;
                    }
                    if (isBreak) break;
                    dt.Rows.Add(dr);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex + " Error incur at row: " + (fixi + 1).ToString() + " column :" + (fixj + 1).ToString());
            }
            return dt;
        }
        /// <summary>
        /// 把excel文件转换数据表
        /// </summary>
        /// <param name="fileName"></param>
        /// <param name="dicHeader"></param>
        /// <returns></returns>
        public static DataTable ExcelToDataTableByColumns(string fileName, Dictionary<string, object> dicHeader, List<string> columns, int sheetnum)
        {
            DataTable dt = new DataTable();
            IWorkbook book;
            try
            {
                using (FileStream stream = new FileStream(fileName, FileMode.Open, FileAccess.Read))
                {
                    if (Path.GetExtension(fileName).IndexOf("xlsx") > -1)
                    {
                        book = new XSSFWorkbook(stream);
                    }
                    else
                    {
                        book = new HSSFWorkbook(stream);
                    }
                }
            }
            catch
            {
                throw new Exception("Failed to open the file");
            }
            DataSet ds = new DataSet();

            ISheet sheet = book.GetSheetAt(sheetnum);
            IRow colRow = sheet.GetRow(0);
            StringBuilder result = new StringBuilder();
            string sb = string.Empty;
            //List<string> columns = (new string[] { "c1", "c2", "c3", "c4", "c5", "c6", "c7", "c8", "c9", "c10", "c11", "c12", "c13", "c14", "c15", "c16", "c17", "c18", "c19", "c20", "c21", "c22", "c23", "c24", "c25", "c26", "c27", "c28", "c29", "c30", "c31", "c32", "c33", "c34", "c35", "c36" }).ToList(); //new List<string>();
            //设置datatable字段
            List<string> dtcolumns = new List<string>();
            int fixi = 0;
            int fixj = 0;
            try
            {
                for (int i = colRow.FirstCellNum, len = colRow.LastCellNum; i < len; i++)
                {
                    fixi = i;
                    if (colRow.Cells[i].StringCellValue.Trim().Length == 0)
                    {
                        dt.Columns.Add(i.ToString());
                        dtcolumns.Add(i.ToString());
                    }
                    else
                    {
                        if (dtcolumns.Contains(colRow.Cells[i].StringCellValue))
                        {
                            dt.Columns.Add(colRow.Cells[i].StringCellValue + i.ToString());
                            dtcolumns.Add(colRow.Cells[i].StringCellValue);
                        }
                        else
                        {
                            dt.Columns.Add(colRow.Cells[i].StringCellValue);
                            dtcolumns.Add(colRow.Cells[i].StringCellValue);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex + " Error incur at  column :" + (fixi + 1).ToString());
            }

            string cellValue = string.Empty;
            bool isBreak = false;
            fixi = 0;
            fixj = 0;
            try
            {
                for (int i = 1; i <= sheet.LastRowNum; i++)
                {
                    fixi = i;

                    DataRow dr = dt.NewRow();
                    //sb = "<data ";
                    IRow row = sheet.GetRow(i);
                    for (int j = 0; j < dtcolumns.Count; j++)
                    {
                        fixj = j;
                        //if (fixj == 48)
                        //{
                        //    fixj = 48;
                        //}
                        //currenti = i;
                        //currentj = j;
                        //if (sheetnum == 3)
                        //{
                        //    if (i == 1 || i == 2)
                        //    {
                        //        if (j == 21)
                        //        {
                        //            ICell ilcell = row.GetCell(j);
                        //        }
                        //    }
                        //}

                        ICell icell = row.GetCell(j);
                        if (icell == null)
                        {
                            cellValue = "";
                        }
                        else
                        {
                            //if (icell.CellType == CellType.Numeric && DateUtil.IsCellDateFormatted(icell))
                            //{
                            //    cellValue = icell.DateCellValue.ToString();
                            //}
                            //else
                            //{
                            //    cellValue = System.Security.SecurityElement.Escape(row.GetCell(j).ToString());
                            //}
                            switch (row.GetCell(j).CellType)
                            {
                                case CellType.Numeric:
                                    if (DateUtil.IsCellDateFormatted(icell))
                                    {
                                        cellValue = icell.DateCellValue.ToString();
                                    }
                                    else
                                    {
                                        cellValue = System.Security.SecurityElement.Escape(row.GetCell(j).ToString());
                                    }
                                    break;
                                case CellType.Formula:
                                    row.GetCell(j).SetCellType(CellType.String);
                                    cellValue = row.GetCell(j).ToString();
                                    break;
                                default:
                                    //cellValue = row.GetCell(j).StringCellValue;
                                    cellValue = row.GetCell(j).ToString();
                                    break;
                            }

                        }
                        if (j == 0 && string.IsNullOrEmpty(cellValue))
                        {
                            isBreak = true; break;
                        }
                        dr[j] = cellValue;
                    }
                    if (isBreak) break;
                    dt.Rows.Add(dr);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex + " Error incur at row: " + (fixi + 1).ToString() + " column :" + (fixj + 1).ToString());
            }
            return dt;
        }
        protected static List<string> GetColumns(int columnQty)
        {
            string[] columnss = new string[columnQty];
            for (int i = 0; i < columnQty; i++)
            {
                columnss[i] = "c" + i.ToString();
            }
            return columnss.ToList();
        }
        /// <summary>
        /// 把Excel转为xml，第一列为空值也可以导入
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns></returns>
        public static string ExcelToXmlWithAll(string fileName, Dictionary<string, object> dicHeader)
        {
            IWorkbook book;
            string msg = "";
            try
            {
                using (FileStream stream = new FileStream(fileName, FileMode.Open, FileAccess.Read))
                {
                    msg = "进来FileStream了";
                    if (Path.GetExtension(fileName).IndexOf("xlsx") > -1)
                    {
                        msg = "进来FileName641行了";
                        book = new XSSFWorkbook(stream);
                        msg = "进来FileName642行了";
                    }
                    else
                    {
                        msg = "进来FileName647行了";
                        book = new HSSFWorkbook(stream);
                    }
                }
            }
            catch (Exception e)
            {
                throw new Exception("error msg:" + e + ".Failed to open the file.运行msg：" + msg);
            }
            ISheet sheet = book.GetSheetAt(0);
            IRow colRow = sheet.GetRow(0);
            StringBuilder result = new StringBuilder();
            string sb = string.Empty;
            //List<string> columns = (new string[] { "SCHED_SHIP_DATE", "Qty_Per_Pallet", "HAWB_EST_WEIGHT", "Pallet_Length", "Pallet_Width", "Pallet_Height", "Pallet_Qty", "HAWB_ACT_WEIGHT", "ODM08", "PACK_ID" }).ToList(); //new List<string>();

            List<string> columns = GetColumns(colRow.LastCellNum);
            string cellValue = string.Empty;
            bool isBreak = false;
            //for (int i = 0; i < colRow.LastCellNum; i++)
            //{
            //    cellValue = colRow.GetCell(i).StringCellValue;
            //    if (string.IsNullOrEmpty(cellValue)) { break; }
            //    columns.Add(cellValue);
            //}
            result.Append("<sdp ");
            foreach (var d in dicHeader)
            {
                result.AppendFormat("{0}=\"{1}\" ", d.Key, d.Value);
            }
            result.Append(">");
            int fixi = 0;
            int fixj = 0;
            try
            {
                for (int i = 1; i <= sheet.LastRowNum; i++)
                {
                    fixi = i;
                    sb = "<data ";
                    IRow row = sheet.GetRow(i);
                    for (int j = 0; j < columns.Count; j++)
                    {
                        fixj = j;
                        cellValue = row.GetCell(j) == null ? "" : System.Security.SecurityElement.Escape(row.GetCell(j).ToString());

                        ////如果是公式Cell 
                        //则仅读取其Cell单元格的显示值 而不是读取公式
                        if (row.GetCell(j) != null)
                        {
                            if (row.GetCell(j).CellType == CellType.Formula)
                            {
                                row.GetCell(j).SetCellType(CellType.String);
                                cellValue = row.GetCell(j).StringCellValue;
                            }
                            else
                            {

                                NPOI.SS.UserModel.ICell icell = row.GetCell(j);
                                if (icell == null)
                                {
                                    cellValue = "";
                                }
                                else
                                {

                                    if (icell.CellType == CellType.Numeric && DateUtil.IsCellDateFormatted(icell))
                                    {
                                        cellValue = icell.DateCellValue.ToString();
                                    }
                                    else
                                    {
                                        cellValue = System.Security.SecurityElement.Escape(row.GetCell(j).ToString());
                                    }
                                }
                                //cellValue = row.GetCell(j).ToString();
                            }
                        }


                        if (j == 6 && string.IsNullOrEmpty(cellValue))
                        {
                            isBreak = true;
                            break;
                        }
                        sb += string.Format("{0}=\"{1}\" ", columns[j], cellValue);
                    }
                    if (isBreak) break;
                    result.Append(sb + " />");
                }
            }
            catch (Exception ex)
            {

                throw new Exception(ex + ",Failed to read excel the file at the Row: " + (fixi + 1).ToString() + "  Column: " + (fixj + 1).ToString());
            }
            result.Append("</sdp>");
            return result.ToString();
        }
        /// <summary>
        /// 把Excel转为xml
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns></returns>
        public static string ExcelToXml(string fileName, Dictionary<string, object> dicHeader)
        {
            IWorkbook book;
            string msg = "";
            try
            {
                using (FileStream stream = new FileStream(fileName, FileMode.Open, FileAccess.Read))
                {
                    msg = "进来FileStream了";
                    if (Path.GetExtension(fileName).IndexOf("xlsx") > -1)
                    {
                        msg = "进来FileName641行了";
                        book = new XSSFWorkbook(stream);
                        msg = "进来FileName642行了";
                    }
                    else
                    {
                        msg = "进来FileName647行了";
                        book = new HSSFWorkbook(stream);
                    }
                }
            }
            catch(Exception e)
            {
                throw new Exception("error msg:"+e+".Failed to open the file.运行msg："+msg);
            }
            ISheet sheet = book.GetSheetAt(0);
            IRow colRow = sheet.GetRow(0);
            StringBuilder result = new StringBuilder();
            string sb = string.Empty;
            //List<string> columns = (new string[] { "SCHED_SHIP_DATE", "Qty_Per_Pallet", "HAWB_EST_WEIGHT", "Pallet_Length", "Pallet_Width", "Pallet_Height", "Pallet_Qty", "HAWB_ACT_WEIGHT", "ODM08", "PACK_ID" }).ToList(); //new List<string>();

            List<string> columns = GetColumns(colRow.LastCellNum);
            string cellValue = string.Empty;
            bool isBreak = false;
            //for (int i = 0; i < colRow.LastCellNum; i++)
            //{
            //    cellValue = colRow.GetCell(i).StringCellValue;
            //    if (string.IsNullOrEmpty(cellValue)) { break; }
            //    columns.Add(cellValue);
            //}
            result.Append("<sdp ");
            foreach (var d in dicHeader)
            {
                result.AppendFormat("{0}=\"{1}\" ", d.Key, d.Value);
            }
            result.Append(">");
            int fixi = 0;
            int fixj = 0;
            try
            {
                for (int i = 1; i <= sheet.LastRowNum; i++)
                {
                    fixi = i;
                    sb = "<data ";
                    IRow row = sheet.GetRow(i);
                    for (int j = 0; j < columns.Count; j++)
                    {
                        fixj = j;
                        cellValue = row.GetCell(j) == null ? "" : System.Security.SecurityElement.Escape(row.GetCell(j).ToString());

                        ////如果是公式Cell 
                        //则仅读取其Cell单元格的显示值 而不是读取公式
                        if (row.GetCell(j) != null)
                        {
                            if (row.GetCell(j).CellType == CellType.Formula)
                            {
                                row.GetCell(j).SetCellType(CellType.String);
                                cellValue = row.GetCell(j).StringCellValue;
                            }
                            else
                            {

                                NPOI.SS.UserModel.ICell icell = row.GetCell(j);
                                if (icell == null)
                                {
                                    cellValue = "";
                                }
                                else
                                {

                                    if (icell.CellType == CellType.Numeric && DateUtil.IsCellDateFormatted(icell))
                                    {
                                        cellValue = icell.DateCellValue.ToString();
                                    }
                                    else
                                    {
                                        cellValue = System.Security.SecurityElement.Escape(row.GetCell(j).ToString());
                                    }
                                }
                                //cellValue = row.GetCell(j).ToString();
                            }
                        }
                        

                        if (j == 0 && string.IsNullOrEmpty(cellValue))
                        {
                            isBreak = true;
                            break;
                        }
                        sb += string.Format("{0}=\"{1}\" ", columns[j], cellValue);
                    }
                    if (isBreak) break;
                    result.Append(sb + " />");
                }
            }
            catch (Exception ex)
            {

                throw new Exception(ex+",Failed to read excel the file at the Row: " + (fixi + 1).ToString() + "  Column: " + (fixj + 1).ToString());
            }
            result.Append("</sdp>");
            return result.ToString();
        }

        /// <summary>
        /// 把Excel转为xml
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns></returns>
        public static string ExcelToXmlFrom3(string fileName, Dictionary<string, object> dicHeader)
        {
            IWorkbook book;
            string msg = "";
            try
            {
                using (FileStream stream = new FileStream(fileName, FileMode.Open, FileAccess.Read))
                {
                    msg = "进来FileStream了";
                    if (Path.GetExtension(fileName).IndexOf("xlsx") > -1)
                    {
                        msg = "进来FileName641行了";
                        book = new XSSFWorkbook(stream);
                        msg = "进来FileName642行了";
                    }
                    else
                    {
                        msg = "进来FileName647行了";
                        book = new HSSFWorkbook(stream);
                    }
                }
            }
            catch (Exception e)
            {
                throw new Exception("error msg:" + e + ".Failed to open the file.运行msg：" + msg);
            }
            ISheet sheet = book.GetSheetAt(0);
            IRow colRow = sheet.GetRow(0);
            StringBuilder result = new StringBuilder();
            string sb = string.Empty;
            //List<string> columns = (new string[] { "SCHED_SHIP_DATE", "Qty_Per_Pallet", "HAWB_EST_WEIGHT", "Pallet_Length", "Pallet_Width", "Pallet_Height", "Pallet_Qty", "HAWB_ACT_WEIGHT", "ODM08", "PACK_ID" }).ToList(); //new List<string>();

            List<string> columns = GetColumns(colRow.LastCellNum);
            string cellValue = string.Empty;
            bool isBreak = false;
            //for (int i = 0; i < colRow.LastCellNum; i++)
            //{
            //    cellValue = colRow.GetCell(i).StringCellValue;
            //    if (string.IsNullOrEmpty(cellValue)) { break; }
            //    columns.Add(cellValue);
            //}
            result.Append("<sdp ");
            foreach (var d in dicHeader)
            {
                result.AppendFormat("{0}=\"{1}\" ", d.Key, d.Value);
            }
            result.Append(">");
            int fixi = 0;
            int fixj = 0;
            try
            {
                for (int i = 2; i <= sheet.LastRowNum; i++)
                {
                    fixi = i;
                    sb = "<data ";
                    IRow row = sheet.GetRow(i);
                    for (int j = 0; j < columns.Count; j++)
                    {
                        fixj = j;
                        cellValue = row.GetCell(j) == null ? "" : System.Security.SecurityElement.Escape(row.GetCell(j).ToString());

                        ////如果是公式Cell 
                        //则仅读取其Cell单元格的显示值 而不是读取公式
                        if (row.GetCell(j) != null)
                        {
                            if (row.GetCell(j).CellType == CellType.Formula)
                            {
                                row.GetCell(j).SetCellType(CellType.String);
                                cellValue = row.GetCell(j).StringCellValue;
                            }
                            else
                            {

                                NPOI.SS.UserModel.ICell icell = row.GetCell(j);
                                if (icell == null)
                                {
                                    cellValue = "";
                                }
                                else
                                {

                                    if (icell.CellType == CellType.Numeric && DateUtil.IsCellDateFormatted(icell))
                                    {
                                        cellValue = icell.DateCellValue.ToString();
                                    }
                                    else
                                    {
                                        cellValue = System.Security.SecurityElement.Escape(row.GetCell(j).ToString());
                                    }
                                }
                                //cellValue = row.GetCell(j).ToString();
                            }
                        }


                        if (j == 0 && string.IsNullOrEmpty(cellValue))
                        {
                            isBreak = true;
                            break;
                        }
                        sb += string.Format("{0}=\"{1}\" ", columns[j], cellValue);
                    }
                    if (isBreak) break;
                    result.Append(sb + " />");
                }
            }
            catch (Exception ex)
            {

                throw new Exception(ex + ",Failed to read excel the file at the Row: " + (fixi + 1).ToString() + "  Column: " + (fixj + 1).ToString());
            }
            result.Append("</sdp>");
            return result.ToString();
        }

        public static string ExcelToXmlForRegion(string fileName, Dictionary<string, object> dicHeader)
        {
            IWorkbook book;
            try
            {
                using (FileStream stream = new FileStream(fileName, FileMode.Open, FileAccess.Read))
                {
                    if (Path.GetExtension(fileName).IndexOf("xlsx") > -1)
                    {
                        book = new XSSFWorkbook(stream);
                    }
                    else
                    {
                        book = new HSSFWorkbook(stream);
                    }
                }
            }
            catch
            {
                throw new Exception("Failed to open the file");
            }
            ISheet sheet = book.GetSheetAt(0);
            IRow colRow = sheet.GetRow(0);
            StringBuilder result = new StringBuilder();
            string sb = string.Empty;
            List<string> columns = (new string[] { "SUB_REGION", "ORDER_TYPE", "PREF_GATEWAY", "AREA_GROUP_ID" }).ToList(); //new List<string>();
            string cellValue = string.Empty;
            bool isBreak = false;
            //for (int i = 0; i < colRow.LastCellNum; i++)
            //{
            //    cellValue = colRow.GetCell(i).StringCellValue;
            //    if (string.IsNullOrEmpty(cellValue)) { break; }
            //    columns.Add(cellValue);
            //}
            result.Append("<sdp ");
            foreach (var d in dicHeader)
            {
                result.AppendFormat("{0}=\"{1}\" ", d.Key, d.Value);
            }
            result.Append(">");
            for (int i = 1; i <= sheet.LastRowNum; i++)
            {
                sb = "<data ";
                IRow row = sheet.GetRow(i);
                for (int j = 0; j < columns.Count; j++)
                {
                    cellValue = row.GetCell(j) == null ? "" : System.Security.SecurityElement.Escape(row.GetCell(j).ToString());
                    if (j == 0 && string.IsNullOrEmpty(cellValue))
                    {
                        isBreak = true; break;
                    }
                    sb += string.Format("{0}=\"{1}\" ", columns[j], cellValue);
                }
                if (isBreak) break;
                result.Append(sb + " />");

            }
            result.Append("</sdp>");
            return result.ToString();
        }
        public static string ExcelToDataTableACK(string fileName, Dictionary<string, object> dicHeader)
        {
            IWorkbook book;
            try
            {
                using (FileStream stream = new FileStream(fileName, FileMode.Open, FileAccess.Read))
                {
                    if (Path.GetExtension(fileName).IndexOf("xlsx") > -1)
                    {
                        book = new XSSFWorkbook(stream);
                    }
                    else
                    {
                        book = new HSSFWorkbook(stream);
                    }
                }
            }
            catch
            {
                throw new Exception("Failed to open the file");
            }
            ISheet sheet = book.GetSheetAt(0);
            IRow colRow = sheet.GetRow(0);
            StringBuilder result = new StringBuilder();
            string sb = string.Empty;
            List<string> columns = (new string[] { "SHIPPING_SITE", "SCHED_SHIP_DATE", "PORT_OF_ORIGIN_SHIP_DATE", "REGION", "INTL_CARRIER", "SHIP_MODE", "TRANS_SERV_LEVEL", "PREF_GATEWAY", "WAYBILL_NUMBER", "HAWB_PALLET_QTY", "HAWB_BOX_QTY", "MASTER_WAYBILL_NUMBER", "MASTER_WAYBILL_CONTAINER_TYPE_QTY", "CONTAINER_ID", "SEAL_ID" }).ToList(); //new List<string>();
            string cellValue = string.Empty;
            bool isBreak = false;
            //for (int i = 0; i < colRow.LastCellNum; i++)
            //{
            //    cellValue = colRow.GetCell(i).StringCellValue;
            //    if (string.IsNullOrEmpty(cellValue)) { break; }
            //    columns.Add(cellValue);
            //}
            result.Append("<sdp ");
            foreach (var d in dicHeader)
            {
                result.AppendFormat("{0}=\"{1}\" ", d.Key, d.Value);
            }
            result.Append(">");
            for (int i = 1; i <= sheet.LastRowNum; i++)
            {
                sb = "<data ";
                IRow row = sheet.GetRow(i);
                for (int j = 0; j < columns.Count; j++)
                {
                    cellValue = row.GetCell(j) == null ? "" : System.Security.SecurityElement.Escape(row.GetCell(j).ToString());
                    if (j == 0 && string.IsNullOrEmpty(cellValue))
                    {
                        isBreak = true; break;
                    }
                    sb += string.Format("{0}=\"{1}\" ", columns[j], cellValue);
                }
                if (isBreak) break;
                result.Append(sb + " />");

            }
            result.Append("</sdp>");
            return result.ToString();
        }
        public static string ExcelToXmlForProduct(string fileName, Dictionary<string, object> dicHeader)
        {
            IWorkbook book;
            try
            {
                using (FileStream stream = new FileStream(fileName, FileMode.Open, FileAccess.Read))
                {
                    if (Path.GetExtension(fileName).IndexOf("xlsx") > -1)
                    {
                        book = new XSSFWorkbook(stream);
                    }
                    else
                    {
                        book = new HSSFWorkbook(stream);
                    }
                }
            }
            catch
            {
                throw new Exception("Failed to open the file");
            }
            ISheet sheet = book.GetSheetAt(0);
            IRow colRow = sheet.GetRow(0);
            StringBuilder result = new StringBuilder();
            string sb = string.Empty;
            List<string> columns = (new string[] { "SKU", "BoxTypeNRegion", "Ulayer", "Layer", "ULoad", "LCTN", "UCTN", "Region", "CTNdefault", "Layer20CTN", "ULoad20CTN", "L_CTN20", "UCTN20CTN", "Layer40CTN", "ULoad40CTN", "L_CTN40", "UCTN40CTN", "Withpallet", "UlayerAir", "LayersAir", "ULoadAir", "LCTNAir", "CTNAir", "Model" }).ToList(); //new List<string>();
            string cellValue = string.Empty;
            bool isBreak = false;
            //for (int i = 0; i < colRow.LastCellNum; i++)
            //{
            //    cellValue = colRow.GetCell(i).StringCellValue;
            //    if (string.IsNullOrEmpty(cellValue)) { break; }
            //    columns.Add(cellValue);
            //}
            result.Append("<sdp ");
            foreach (var d in dicHeader)
            {
                result.AppendFormat("{0}=\"{1}\" ", d.Key, d.Value);
            }
            result.Append(">");
            for (int i = 1; i <= sheet.LastRowNum; i++)
            {
                sb = "<data ";
                IRow row = sheet.GetRow(i);
                for (int j = 0; j < columns.Count; j++)
                {
                    cellValue = row.GetCell(j) == null ? "" : System.Security.SecurityElement.Escape(row.GetCell(j).ToString());
                    if (j == 0 && string.IsNullOrEmpty(cellValue))
                    {
                        isBreak = true; break;
                    }
                    sb += string.Format("{0}=\"{1}\" ", columns[j], cellValue);
                }
                if (isBreak) break;
                result.Append(sb + " />");

            }
            result.Append("</sdp>");
            return result.ToString();
        }
    }
}
