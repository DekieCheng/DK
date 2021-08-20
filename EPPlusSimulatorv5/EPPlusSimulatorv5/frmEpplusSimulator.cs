using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EPPlusSimulatorv5
{
    public partial class frmEpplusSimulator : Form
    {
        public frmEpplusSimulator()
        {
            InitializeComponent();
        }

        private void cmdOpen_Click(object sender, EventArgs e)
        {
            string pfilePath = "";
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "Excel File|*.xls|Excel File|*.xlsx";
            if (ofd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                pfilePath = ofd.FileName;
            }
            if (string.IsNullOrEmpty(pfilePath))
            {
                MessageBox.Show("No chosen excel document.");
                return;
            }

            FileInfo existingFile = new FileInfo(pfilePath);
            try
            {
                ExcelPackage package = new ExcelPackage(existingFile);
                ExcelPackage.LicenseContext = OfficeOpenXml.LicenseContext.NonCommercial;

                int vSheetCount = package.Workbook.Worksheets.Count; //obtain Sheet count totally 

                ExcelWorksheet worksheet = package.Workbook.Worksheets[0];//specified the sheet, here default is 1st sheet anytime.

                SaveFileDialog sfd = new SaveFileDialog();
                sfd.Filter = "Excel File|*.xls|Excel File|*.xlsx";
                if (sfd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    FileInfo file = new FileInfo(sfd.FileName);
                    //ep.File = file;
                    package.SaveAs(file);
                    MessageBox.Show("Save as successfully");
                }

                int maxColumnNum = worksheet.Dimension.End.Column;//Max Column
                int minColumnNum = worksheet.Dimension.Start.Column;//Min Column

                int maxRowNum = worksheet.Dimension.End.Row;//max Row
                int minRowNum = worksheet.Dimension.Start.Row;//min row

                DataTable vTable = new DataTable();
                DataColumn vC;
                for (int j = 1; j <= maxColumnNum; j++)
                {
                    vC = new DataColumn("A_" + j, typeof(string));
                    vTable.Columns.Add(vC);
                }
                if (maxRowNum > 200)
                {
                    maxRowNum = 200;
                }
                for (int n = 1; n <= maxRowNum; n++)
                {
                    DataRow vRow = vTable.NewRow();
                    for (int m = 1; m <= maxColumnNum; m++)
                    {
                        vRow[m - 1] = worksheet.Cells[n, m].Value;
                    }
                    vTable.Rows.Add(vRow);
                }
                this.dataGridView1.DataSource = vTable;


            }
            catch (Exception vErr)
            {
                MessageBox.Show(vErr.Message);
            }
        }

        private void ProcessSaveExcelDocument()
        {

            OfficeOpenXml.ExcelPackage ep = new OfficeOpenXml.ExcelPackage();
            OfficeOpenXml.ExcelWorkbook wb = ep.Workbook;
            OfficeOpenXml.ExcelWorksheet ws = wb.Worksheets.Add("我的工作表");
            //配置文件属性
            wb.Properties.Category = "类别";
            wb.Properties.Author = "作者";
            wb.Properties.Comments = "备注";
            wb.Properties.Company = "公司";
            wb.Properties.Keywords = "关键字";
            wb.Properties.Manager = "管理者";
            wb.Properties.Status = "内容状态";
            wb.Properties.Subject = "主题";
            wb.Properties.Title = "标题";
            wb.Properties.LastModifiedBy = "最后一次保存者";
            //写数据
            ws.Cells[1, 1].Value = "Hello";

            ws.Column(1).Width = 40;//修改列宽
            ws.Cells["B1"].Value = "World";
            ws.Cells[3, 3, 3, 5].Merge = true;
            ws.Cells[3, 3].Value = "Cells[3, 3, 3, 5]合并";
            ws.Cells["A4:D5"].Merge = true;
            ws.Cells["A4"].Value = "Cells[\"A4:D5\"]合并";

            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Filter = "*.xls|*.xlsx";
            if (sfd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                FileInfo file = new FileInfo(sfd.FileName);
                ep.File = file;
                ep.Save();
                MessageBox.Show("保存成功");
            }
            else
            {
                MessageBox.Show("取消保存");
            }
        }
    }
}
