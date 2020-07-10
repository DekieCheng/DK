using Microsoft.Reporting.WinForms;
using ReportsApplication.Properties;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ReportsApplication
{
    public partial class frmRDLCReportSimulator : Form
    {
        public frmRDLCReportSimulator()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            INITForm();
        }

        private void INITForm()
        {
            this.txtDBConnection.Text = "Data Source=NSCM0295\\FFMES2K17;Initial Catalog=FF2_9_3;Integrated Security=True";
            this.txtRDLCPath.Text = @"..\..\Report1.rdlc";
            this.txtUDPName.Text = "udpGetReportDataVS2017";
        }

        private DataSet GetDataSetByUDP(string udpName)
        {
            string connectionString = this.txtDBConnection.Text;
            System.Data.SqlClient.SqlConnection conn1 = new System.Data.SqlClient.SqlConnection(connectionString);
            System.Data.SqlClient.SqlCommand command1 = new System.Data.SqlClient.SqlCommand();
            command1.CommandText = udpName;
            command1.Connection = conn1;
            System.Data.SqlClient.SqlDataAdapter ada1 = new System.Data.SqlClient.SqlDataAdapter(command1);
            DataSet ds = new DataSet();

            try
            {
                conn1.Open();
                ada1.Fill(ds);
                return ds;
            }

            finally
            {
                conn1.Close();
                command1.Dispose();
                conn1.Dispose();
            }
        }

        private void FillDataToReport()
        {
            DataSet ds = GetDataSetByUDP(this.txtUDPName.Text);

            this.rptViewer.Reset();
            this.rptViewer.LocalReport.DataSources.Clear();
            this.rptViewer.LocalReport.ReportPath = this.txtRDLCPath.Text;
            this.rptViewer.LocalReport.EnableHyperlinks = true;

            //Hardcode method to generate the data source objects
            //this.reportViewer1.LocalReport.DataSources.Add(new ReportDataSource("DS_Table1", ds.Tables[0]));
            //this.reportViewer1.LocalReport.DataSources.Add(new ReportDataSource("DS_Table2", ds.Tables[1]));
            //this.reportViewer1.LocalReport.DataSources.Add(new ReportDataSource("DS_Table3", ds.Tables[2]));

            //Auto-generate the data source objects dynamically based on returned data table count.
            int runningNo = 1;
            foreach (DataTable dt in ds.Tables)
            {
                this.rptViewer.LocalReport.DataSources.Add(new ReportDataSource("DS_Table" + runningNo.ToString(), dt));
                runningNo++;
            }
            this.rptViewer.ZoomMode = ZoomMode.Percent;
            this.rptViewer.RefreshReport();
        }

        private void btnQuery_Click(object sender, EventArgs e)
        {
            FillDataToReport();
        }
    }
}