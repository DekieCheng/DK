using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SDPSubSystem.Common.Opt
{

    /// <summary>
    /// 与Excel相关的操作类
    /// </summary>
    public class WaterfallExcelOpt
    {
        /// <summary>
        /// Datable导出成Excel
        /// </summary>
        /// <param name="dt"></param>
        /// <param name="file">导出路径(包括文件名与扩展名)</param>
        public static MemoryStream TableToExcelXlsx(DataTable dt)
        {
            IWorkbook book;
            // string fileExt = Path.GetExtension(file).ToLower();
            book = new XSSFWorkbook();

            //ISheet sheet = string.IsNullOrEmpty(dt.TableName) ? book.CreateSheet("Sheet1") : book.CreateSheet(dt.TableName);
            ISheet sheet = book.CreateSheet("Sheet1");


            //创建单元格样式--一种颜色
            XSSFCellStyle backgroundstyle = (XSSFCellStyle)book.CreateCellStyle();
            //填充模式
            backgroundstyle.FillPattern = FillPattern.SolidForeground;
            //创建颜⾊
            XSSFColor xssfcolor = new XSSFColor();
            //rbg值，灰色
            byte[] rgb = { 211, 211, 211 };
            //写⼊rgb
            xssfcolor.SetRgb(rgb);
            //设置颜⾊值
            backgroundstyle.SetFillForegroundColor(xssfcolor);
            //sheet.getrow(⾏号).getcell（列号）.cellstyle = backgroundstyle;


            //创建单元格样式--另一种颜色
            XSSFCellStyle backgroundstyle2 = (XSSFCellStyle)book.CreateCellStyle();
            ////填充模式
            //backgroundstyle2.FillPattern = FillPattern.SolidForeground;
            ////创建颜⾊
            //XSSFColor xssfcolor2 = new XSSFColor();
            ////rbg值
            //byte[] rgb2 = { 144, 238, 144 };
            ////写⼊rgb
            //xssfcolor2.SetRgb(rgb2);
            ////设置颜⾊值
            //backgroundstyle2.SetFillForegroundColor(xssfcolor2);

            //C#使用npoi 导出excel如何设置单元格为数字格式
            backgroundstyle.DataFormat = HSSFDataFormat.GetBuiltinFormat("0");
            backgroundstyle2.DataFormat = HSSFDataFormat.GetBuiltinFormat("0");


            //表头  
            IRow row = sheet.CreateRow(0);
            string[] arr = new string[dt.Columns.Count];

            for (int i = 0; i < dt.Columns.Count; i++)
            {
                ICell cell = row.CreateCell(i);
                cell.SetCellValue(dt.Columns[i].ColumnName);
                arr[i] = dt.Columns[i].ColumnName.ToString();
            }

            //数据  
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                var demandName = "";
                IRow row1 = sheet.CreateRow(i + 1);
                for (int j = 0; j < dt.Columns.Count; j++)
                {
                    ICell cell = row1.CreateCell(j);

                    string strValue = dt.Rows[i][j].ToString();

                    
                    if (j >= 5)
                    {
                        double numDouble = 0.000000;
                        double.TryParse(strValue, out numDouble);
                        //int cnt = Convert.ToInt32(resMin);//四舍五入
                        cell.SetCellValue(numDouble);
                    }
                    else
                    {
                        cell.SetCellValue(strValue);
                    }


                    if (j == 0)
                    {
                        demandName = dt.Rows[i][0].ToString();
                    }

                    if (j >= 6)
                    {
                        DateTime columnNameMili = DateTime.Parse( arr[j].ToString());
                        DateTime demandNameMili = DateTime.Parse( demandName);
                        if (DateTime.Compare( demandNameMili , columnNameMili) > 0)
                        {
                            //出货
                            cell.CellStyle = backgroundstyle;
                        }
                        else
                        {
                            cell.CellStyle = backgroundstyle2;
                        }
                    }
                }
            }


            //设置自适应宽度
            for (int columnNum = 0; columnNum <= dt.Columns.Count; columnNum++)
            {
                int columnWidth = sheet.GetColumnWidth(columnNum) / 256;
                //5为开始修改的行数，默认为0行开始
                for (int rowNum = 0; rowNum <= sheet.LastRowNum; rowNum++)
                {
                    IRow currentRow = sheet.GetRow(rowNum);
                    if (currentRow.GetCell(columnNum) != null)
                    {
                        ICell currentCell = currentRow.GetCell(columnNum);
                        int length = Encoding.Default.GetBytes(currentCell.ToString()).Length + 1;
                        if (columnWidth < length)
                        {
                            columnWidth = length;
                        }
                    }
                }
                sheet.SetColumnWidth(columnNum, columnWidth * 256);
            }




            //转为字节数组
            NpoiMemoryStream ms = new NpoiMemoryStream();
            ms.AllowClose = false;
            book.Write(ms);
            book.Close();
            ms.Seek(0, SeekOrigin.Begin);
            ms.AllowClose = true;
            return ms;


        }

        public class NpoiMemoryStream : MemoryStream
        {
            public NpoiMemoryStream()
            {
                // We always want to close streams by default to
                // force the developer to make the conscious decision
                // to disable it.  Then, they're more apt to remember
                // to re-enable it.  The last thing you want is to
                // enable memory leaks by default.  ;-)
                AllowClose = true;
            }

            public bool AllowClose { get; set; }

            public override void Close()
            {
                if (AllowClose)
                    base.Close();
            }
        }

        /// <summary>
        /// 把DataTable 转为内存形式的Excel
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        public static MemoryStream TableToExcel(DataTable source)
        {
            MemoryStream ms = new MemoryStream();
            HSSFWorkbook book = new HSSFWorkbook();
            if (source == null) throw new ArgumentNullException("source");
            ISheet sheet = book.CreateSheet("Sheet1");
            IRow colRow = sheet.CreateRow(0);//创建标题行
            for(int i = 0; i < source.Columns.Count; i++)
            {
                colRow.CreateCell(i).SetCellValue(source.Columns[i].ColumnName);
            }
            for(int i = 0; i < source.Rows.Count; i++)
            {
                IRow dataRow = sheet.CreateRow(i + 1);
                for(int j = 0; j < source.Columns.Count; j++)
                {
                    dataRow.CreateCell(j).SetCellValue(Convert.IsDBNull(source.Rows[i][source.Columns[j]]) ? "" : source.Rows[i][source.Columns[j]].ToString());
                }
            }
            book.Write(ms);
            book.Close();
            ms.Seek(0, SeekOrigin.Begin);
            return ms;
        }
        /// <summary>
        /// 根据columns创建模板excel
        /// </summary>
        /// <param name="columns"></param>
        /// <returns></returns>
        public static MemoryStream CreateTemplate(List<string> columns)
        {
            MemoryStream ms = new MemoryStream();
            HSSFWorkbook book = new HSSFWorkbook();
            if (columns == null) throw new ArgumentNullException("columns");
            ISheet sheet = book.CreateSheet("Sheet1");
            IRow colRow = sheet.CreateRow(0);//创建标题行
            for (int i = 0; i < columns.Count; i++)
            {
                colRow.CreateCell(i).SetCellValue(columns[i]);
            }
            book.Write(ms);
            book.Close();
            ms.Seek(0, SeekOrigin.Begin);
            return ms;
        }
        /// <summary>
        /// 把DataSet 转为内存形式的Excel
        /// </summary>
        /// <param name="ds"></param>
        /// <returns></returns>
        public static MemoryStream DataSetToExcel(DataSet ds)
        {
            MemoryStream ms = new MemoryStream();
            HSSFWorkbook book = new HSSFWorkbook();
            if (ds == null) throw new ArgumentNullException("ds");
            for(int i = 0; i < ds.Tables.Count; i++)
            {
                DataTable dt = ds.Tables[i];
                ISheet sheet = book.CreateSheet("Sheet" + (i+1).ToString());
                IRow colRow = sheet.CreateRow(0);
                for(int x = 0; x < dt.Columns.Count; x++)
                {
                    colRow.CreateCell(x).SetCellValue(dt.Columns[x].ColumnName);
                }
                for (int m = 0; m < dt.Rows.Count; m++)
                {
                    IRow dataRow = sheet.CreateRow(m + 1);
                    for (int n = 0; n < dt.Columns.Count; n++)
                    {
                        dataRow.CreateCell(n).SetCellValue(Convert.IsDBNull(dt.Rows[i][dt.Columns[n]]) ? "" : dt.Rows[i][dt.Columns[n]].ToString());
                    }
                }
            }
            book.Write(ms);
            book.Close();
            ms.Seek(0, SeekOrigin.Begin);
            return ms;
        }
        /// <summary>
        /// 读取Excel第一个Sheet填充到DataTable
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns></returns>
        public static DataTable ExcelToTable(string fileName)
        {
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
            }
            catch
            {
                throw new Exception("Fialed to open the file");
            }
            ISheet sheet = book.GetSheetAt(0);
            IRow colRow = sheet.GetRow(0);
            DataTable dt = new DataTable();
            string cellValue = string.Empty;
            bool isBreak = false;
            for(int i = 0; i < colRow.LastCellNum; i++)
            {
                cellValue = colRow.GetCell(i).StringCellValue;
                if (string.IsNullOrEmpty(cellValue)) { break; }
                dt.Columns.Add(new DataColumn(cellValue.Trim().Replace(" ", ""), typeof(string)));
            }
            for (int i = 0; i < sheet.LastRowNum; i++)
            {
                DataRow dr = dt.NewRow();
                IRow row = sheet.GetRow(i + 1);
                for(int j = 0; j < dt.Columns.Count; j++)
                {
                    cellValue = row.GetCell(j) == null ? "" : System.Security.SecurityElement.Escape(row.GetCell(j).ToString());
                    if (j == 0 && string.IsNullOrEmpty(cellValue))
                    {
                        isBreak = true;break;  
                    }
                    dr[dt.Columns[j]] = cellValue;
                }
                if (isBreak) break;
                dt.Rows.Add(dr);
            }
            return dt;
        }
        /// <summary>
        /// 把Excel转为xml
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns></returns>
        public static string ExcelToXml(string fileName,Dictionary<string,object> dicHeader)
        {
            IWorkbook book;
            try
            {
                using (FileStream stream = new FileStream(fileName, FileMode.Open, FileAccess.Read))
                {
                    if (Path.GetExtension(fileName).IndexOf("xlsx")>-1)
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
                throw new Exception("Fialed to open the file");
            }
            ISheet sheet = book.GetSheetAt(0);
            IRow colRow = sheet.GetRow(0);
            StringBuilder result = new StringBuilder();
            string sb = string.Empty;
            List<string> columns = new List<string>();
            string cellValue = string.Empty;
            bool isBreak = false;
            for (int i = 0; i < colRow.LastCellNum; i++)
            {
                cellValue = colRow.GetCell(i).StringCellValue;
                if (string.IsNullOrEmpty(cellValue)) { break; }
                columns.Add(cellValue);
            }
            result.Append("<sdp ");
            foreach(var d in dicHeader)
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
