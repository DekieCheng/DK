using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Collections;
using System.Configuration;

namespace ForB17Dashboard
{
    public partial class adminDashboard : System.Web.UI.Page
    {
        public Hashtable HTSource = new Hashtable();
        public Hashtable HTTimeLine = new Hashtable();
        public string facetimes ="100";
        public string Amonthtimes = "10";
        public string Bmonthtimes = "10";
        public string B7monthtimes = "10";
        public string VIPmonthtimes = "10";
        public string Huahainumber = "0";
        public string Huayinumber = "0";
        public string Lingyinumber = "0";
        public string Sanzhounumber = "0";
        public string HuahaiRate = "0";
        public string HuayiRate = "0";
        public string TotalRate = "0";
        protected void Page_Load(object sender, EventArgs e)
        {
            HTTimeLine.Add("KWHDay", GetKWHDay());
            HTSource.Add("KWH", GetKWHByDay());
            HTTimeLine.Add("KongtiaoDay", GetKongtiaoDay());
            HTSource.Add("Kongtiao", GetKongtiaoByDay());
            HTTimeLine.Add("AirDay", GetAirDay());
            HTSource.Add("Air", GetAirByDay());
            HTTimeLine.Add("WaterDay", GetWaterDay());
            HTSource.Add("Water", GetWaterByDay());
        }




        private string GetWaterByDay()
        {
            string sql = "Select B.GroupName,m.CurrDay,sum (CAST(m.TPAEConsumption AS REAL)) as TPAEConsumption from udtLNMeterGroupCategory a inner join udtLNMeterGroup b on a.ID = b.MeterGroupCategoryID inner join udtLNMeterMap c on c.MeterGroupID = b.ID inner join udtLNElectricityMeter em on c.MeterID = em.ID inner join udtLNSerialPortServer ss on em.SerialPortServerID = ss.ID inner join udtLNElectricityMeterMaster m on m.ElectricityMeterID = em.ID Where a.GroupCategoryName = N'电' and b.GroupName = N'B17厂房总用电量' and m.CurrDay > DATEADD(day, -7, GETDATE()) group by B.GroupName,m.CurrDay Order by m.CurrDay";
            DataSet ds = new DataSet();
            DataSet dsSort = new DataSet();
            string tmpSource = string.Empty;
            DataTable dtSort = new DataTable();
            System.Text.StringBuilder sb = new System.Text.StringBuilder(" ");
            dtSort.Columns.Add("CurrDay", typeof(string));
            dtSort.Columns.Add("TPAEConsumption", typeof(string));
            ds = SQLHelper.ExecuteDataset(ConfigurationManager.ConnectionStrings["B17AMS"].ConnectionString, CommandType.Text, sql);

            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                tmpSource = tmpSource + ",\"" + ds.Tables[0].Rows[i]["TPAEConsumption"].ToString() + "\"";
            }
            if (tmpSource.Length > 0)
                tmpSource = tmpSource.Substring(1);
            return tmpSource;

        }

        private string GetWaterDay()
        {
            string sql = "Select B.GroupName,m.CurrDay,sum (m.TPAEConsumption) as TPAEConsumption from udtLNMeterGroupCategory a inner join udtLNMeterGroup b on a.ID = b.MeterGroupCategoryID inner join udtLNMeterMap c on c.MeterGroupID = b.ID inner join udtLNElectricityMeter em on c.MeterID = em.ID inner join udtLNSerialPortServer ss on em.SerialPortServerID = ss.ID inner join udtLNElectricityMeterMaster m on m.ElectricityMeterID = em.ID Where a.GroupCategoryName = N'电' and b.GroupName = N'B17厂房总用电量' and m.CurrDay > DATEADD(day, -7, GETDATE()) group by B.GroupName,m.CurrDay Order by m.CurrDay";
            DataSet ds = new DataSet();
            DataSet dsSort = new DataSet();
            string tmpSource = string.Empty;
            DataTable dtSort = new DataTable();
            System.Text.StringBuilder sb = new System.Text.StringBuilder(" ");
            dtSort.Columns.Add("CurrDay", typeof(string));
            dtSort.Columns.Add("TPAEConsumption", typeof(string));
            ds = SQLHelper.ExecuteDataset(ConfigurationManager.ConnectionStrings["B17AMS"].ConnectionString, CommandType.Text,sql);

            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                tmpSource = tmpSource + ",\"" + ds.Tables[0].Rows[i]["CurrDay"].ToString() + "\"";
            }
            if (tmpSource.Length > 0)
                tmpSource = tmpSource.Substring(1);
            return tmpSource;
        }

        private string GetKongtiaoByDay()
        {
            string sql = "Select B.GroupName,wm.CurrDay,sum(CAST(ABS(wm.[AccumulatedheatConsumption])  AS REAL)) as Consumption from udtLNMeterGroupCategory a inner join udtLNMeterGroup b on a.ID = b.MeterGroupCategoryID inner join udtLNMeterMap c on c.MeterGroupID = b.ID inner join udtLNChilledWaterFlowMeter em on c.MeterID = em.ID inner join udtLNSerialPortServer ss on em.SerialPortServerID = ss.ID inner join udtLNChilledWaterFlowMeterMaster wm on wm.ChilledWaterFlowMeterID = em.ID Where a.GroupCategoryName=N'空调' and b.GroupName =N'B17厂房总用空调能量计' and  wm.CurrDay>DATEADD(day,-7,GETDATE()) group by B.GroupName,wm.CurrDay";
            DataSet ds = new DataSet();
            DataSet dsSort = new DataSet();
            string tmpSource = string.Empty;
            DataTable dtSort = new DataTable();
            System.Text.StringBuilder sb = new System.Text.StringBuilder(" ");
            dtSort.Columns.Add("CurrDay", typeof(string));
            dtSort.Columns.Add("Consumption", typeof(string));
            ds = SQLHelper.ExecuteDataset(ConfigurationManager.ConnectionStrings["B17AMS"].ConnectionString, CommandType.Text, sql);

            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                tmpSource = tmpSource + ",\"" + ds.Tables[0].Rows[i]["Consumption"].ToString() + "\"";
            }
            if (tmpSource.Length > 0)
                tmpSource = tmpSource.Substring(1);
            return tmpSource;

        }

        private string GetKongtiaoDay()
        {
            string sql = "Select B.GroupName,wm.CurrDay,sum( ABS(wm.[AccumulatedheatConsumption])) as Consumption from udtLNMeterGroupCategory a inner join udtLNMeterGroup b on a.ID = b.MeterGroupCategoryID inner join udtLNMeterMap c on c.MeterGroupID = b.ID inner join udtLNChilledWaterFlowMeter em on c.MeterID = em.ID inner join udtLNSerialPortServer ss on em.SerialPortServerID = ss.ID inner join udtLNChilledWaterFlowMeterMaster wm on wm.ChilledWaterFlowMeterID = em.ID Where a.GroupCategoryName=N'空调' and b.GroupName =N'B17厂房总用空调能量计' and  wm.CurrDay>DATEADD(day,-7,GETDATE()) group by B.GroupName,wm.CurrDay";
            DataSet ds = new DataSet();
            DataSet dsSort = new DataSet();
            string tmpSource = string.Empty;
            DataTable dtSort = new DataTable();
            System.Text.StringBuilder sb = new System.Text.StringBuilder(" ");
            dtSort.Columns.Add("CurrDay", typeof(string));
            dtSort.Columns.Add("Consumption", typeof(string));
            ds = SQLHelper.ExecuteDataset(ConfigurationManager.ConnectionStrings["B17AMS"].ConnectionString, CommandType.Text, sql);

            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                tmpSource = tmpSource + ",\"" + ds.Tables[0].Rows[i]["CurrDay"].ToString() + "\"";
            }
            if (tmpSource.Length > 0)
                tmpSource = tmpSource.Substring(1);
            return tmpSource;

        }

        private string GetAirByDay()
        {
            string sql = "Select B.GroupName,mm.[CurrDay],CAST(sum(mm.CompressedAirConsumption)  AS REAL) Consumption from udtLNMeterGroupCategory a inner join udtLNMeterGroup b on a.ID = b.MeterGroupCategoryID inner join udtLNMeterMap c on c.MeterGroupID = b.ID inner join udtLNCompressedAirFlowMeter em on c.MeterID = em.ID inner join udtLNSerialPortServer ss on em.SerialPortServerID = ss.ID inner join udtLNCompressedAirFlowMeterMaster mm on mm.CompressedAirFlowMeterID = em.ID Where a.GroupCategoryName=N'气' and b.GroupName =N'B17厂房总用压缩空气流量' and  mm.CurrDay>DATEADD(day,-7,GETDATE()) group by B.GroupName,mm.CurrDay";
            DataSet ds = new DataSet();
            DataSet dsSort = new DataSet();
            string tmpSource = string.Empty;
            DataTable dtSort = new DataTable();
            System.Text.StringBuilder sb = new System.Text.StringBuilder(" ");
            dtSort.Columns.Add("CurrDay", typeof(string));
            dtSort.Columns.Add("Consumption", typeof(string));
            ds = SQLHelper.ExecuteDataset(ConfigurationManager.ConnectionStrings["B17AMS"].ConnectionString, CommandType.Text, sql);

            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                tmpSource = tmpSource + ",\"" + ds.Tables[0].Rows[i]["Consumption"].ToString() + "\"";
            }
            if (tmpSource.Length > 0)
                tmpSource = tmpSource.Substring(1);
            return tmpSource;

        }

        private string GetAirDay()
        {
            string sql = "Select B.GroupName,mm.[CurrDay],CAST(sum(mm.CompressedAirConsumption)  AS REAL) Consumption from udtLNMeterGroupCategory a inner join udtLNMeterGroup b on a.ID = b.MeterGroupCategoryID inner join udtLNMeterMap c on c.MeterGroupID = b.ID inner join udtLNCompressedAirFlowMeter em on c.MeterID = em.ID inner join udtLNSerialPortServer ss on em.SerialPortServerID = ss.ID inner join udtLNCompressedAirFlowMeterMaster mm on mm.CompressedAirFlowMeterID = em.ID Where a.GroupCategoryName=N'气' and b.GroupName =N'B17厂房总用压缩空气流量' and  mm.CurrDay>DATEADD(day,-7,GETDATE()) group by B.GroupName,mm.CurrDay";
            DataSet ds = new DataSet();
            DataSet dsSort = new DataSet();
            string tmpSource = string.Empty;
            DataTable dtSort = new DataTable();
            System.Text.StringBuilder sb = new System.Text.StringBuilder(" ");
            dtSort.Columns.Add("CurrDay", typeof(string));
            dtSort.Columns.Add("Consumption", typeof(string));
            ds = SQLHelper.ExecuteDataset(ConfigurationManager.ConnectionStrings["B17AMS"].ConnectionString, CommandType.Text, sql);

            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                tmpSource = tmpSource + ",\"" + ds.Tables[0].Rows[i]["CurrDay"].ToString() + "\"";
            }
            if (tmpSource.Length > 0)
                tmpSource = tmpSource.Substring(1);
            return tmpSource;

        }

        private string GetKWHByDay()
        {
            string sql = "Select B.GroupName,m.CurrDay,sum (CAST(m.TPAEConsumption AS REAL)) as TPAEConsumption from udtLNMeterGroupCategory a inner join udtLNMeterGroup b on a.ID = b.MeterGroupCategoryID inner join udtLNMeterMap c on c.MeterGroupID = b.ID inner join udtLNElectricityMeter em on c.MeterID = em.ID inner join udtLNSerialPortServer ss on em.SerialPortServerID = ss.ID inner join udtLNElectricityMeterMaster m on m.ElectricityMeterID = em.ID Where a.GroupCategoryName = N'电' and b.GroupName = N'B17厂房总用电量' and m.CurrDay > DATEADD(day, -7, GETDATE()) group by B.GroupName,m.CurrDay Order by m.CurrDay";
            DataSet ds = new DataSet();
            DataSet dsSort = new DataSet();
            string tmpSource = string.Empty;
            DataTable dtSort = new DataTable();
            System.Text.StringBuilder sb = new System.Text.StringBuilder(" ");
            dtSort.Columns.Add("CurrDay", typeof(string));
            dtSort.Columns.Add("TPAEConsumption", typeof(string));
            ds = SQLHelper.ExecuteDataset(ConfigurationManager.ConnectionStrings["B17AMS"].ConnectionString, CommandType.Text, sql);

            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                tmpSource = tmpSource + ",\"" + ds.Tables[0].Rows[i]["TPAEConsumption"].ToString() + "\"";
            }
            if (tmpSource.Length > 0)
                tmpSource = tmpSource.Substring(1);
            return tmpSource;

        }

        private string GetKWHDay()
        {
            string sql = "Select B.GroupName,m.CurrDay,sum (CAST(m.TPAEConsumption AS REAL)) as TPAEConsumption from udtLNMeterGroupCategory a inner join udtLNMeterGroup b on a.ID = b.MeterGroupCategoryID inner join udtLNMeterMap c on c.MeterGroupID = b.ID inner join udtLNElectricityMeter em on c.MeterID = em.ID inner join udtLNSerialPortServer ss on em.SerialPortServerID = ss.ID inner join udtLNElectricityMeterMaster m on m.ElectricityMeterID = em.ID Where a.GroupCategoryName = N'电' and b.GroupName = N'B17厂房总用电量' and m.CurrDay > DATEADD(day, -7, GETDATE()) group by B.GroupName,m.CurrDay Order by m.CurrDay";
            DataSet ds = new DataSet();
            DataSet dsSort = new DataSet();
            string tmpSource = string.Empty;
            DataTable dtSort = new DataTable();
            System.Text.StringBuilder sb = new System.Text.StringBuilder(" ");
            dtSort.Columns.Add("CurrDay", typeof(string));
            dtSort.Columns.Add("TPAEConsumption", typeof(string));
            ds = SQLHelper.ExecuteDataset(ConfigurationManager.ConnectionStrings["B17AMS"].ConnectionString, CommandType.Text, sql);

            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                tmpSource = tmpSource + ",\"" + ds.Tables[0].Rows[i]["CurrDay"].ToString() + "\"";
            }
            if (tmpSource.Length > 0)
                tmpSource = tmpSource.Substring(1);
            return tmpSource;
        }
    }
    
}