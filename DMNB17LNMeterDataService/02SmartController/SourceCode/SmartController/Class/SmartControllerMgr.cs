using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.IO;
using System.Text;
using System.Xml;
using LNInterfaces;
using LNMeterInfrastructure;
using LNMeterInfrastructure.Common;

namespace LNSmartController
{
    public static class SmartControllerMgr
    {
        private static string GetFFUDPName()
        {
            return ConfigurationManager.AppSettings.Get("FFUDPName");
        }
        private static string GetEPPSUDPName()
        {
            return ConfigurationManager.AppSettings.Get("EPPSUDPName");
        }
        private static string GetLineName()
        {
            return ConfigurationManager.AppSettings.Get("LineName");
        }
        private static string GetStationNameBefore()
        {
            return ConfigurationManager.AppSettings.Get("StationNameBefore");
        }
        private static string GetStationNameAfter()
        {
            return ConfigurationManager.AppSettings.Get("StationNameAfter");
        }
        private static string GetSKIPDB()
        {
            return ConfigurationManager.AppSettings.Get("SKIP");
        }
        public static bool FFExecuteCoolDown(string EPPSPOInfo, ref string CoolDownMsg)
        {
            bool flag = false;

            try
            {
                if (bool.Parse(GetSKIPDB())) { return false; }
                int retVal = 0;
                LNParameterCollection lNParameterCollection = new LNParameterCollection();
                lNParameterCollection.Add("@xmlExtraData", new LNParameter("@xmlExtraData", GenXMLBuilder(EPPSPOInfo)));
                LNDBWrapper g_FFDB = new LNDBWrapper(ConfigurationManager.AppSettings.Get("FFConnectionString"));
                DataSet ds = g_FFDB.ExecuteSPWithResults(GetFFUDPName(), lNParameterCollection, ref retVal);
                if (ds != null)
                {
                    if (ds.Tables.Count > 0)
                    {
                        string TriggerCondition = ds.Tables[0].Rows[0][0].ToString();
                        CoolDownMsg = ds.Tables[0].Rows[0][1].ToString();
                        if (TriggerCondition == "1") { return true; }
                    }
                }
                return false;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return flag;
        }

        public static string GetPOInfofromEPPS()
        {
            try
            {
                if (bool.Parse(GetSKIPDB())) { return string.Empty; }

                int retVal = 0;
                LNParameterCollection inputParameterCollection = new LNParameterCollection();
                inputParameterCollection.Add("@LineName", new LNParameter("@LineName", GetLineName()));
                inputParameterCollection.Add("@msg", new LNParameter("@msg", ""));

                LNParameterCollection outputParameters = new LNParameterCollection();
                LNDBWrapper g_FFDB = new LNDBWrapper(ConfigurationManager.AppSettings.Get("EPPSConnectionString"));
                DataSet ds = g_FFDB.ExecuteSPWithOutPutParamsAndResults(GetEPPSUDPName(), inputParameterCollection, ref outputParameters, ref retVal);
                if (ds != null)
                {
                    if (ds.Tables.Count > 0)
                    {
                        return GetPOInfoXML(POInfoCollection(ds.Tables[0]));
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return string.Empty;
        }

        public static LNPOInfoCollection POInfoCollection(DataTable sourceDT)
        {
            LNPOInfoCollection _lNCollection = new LNPOInfoCollection();
            foreach (DataRow dr in sourceDT.Rows)
            {
                EPPSPOInfo ePPSPOInfo = new EPPSPOInfo()
                {
                    LineName = dr[0].ToString(),
                    PlanDate = dr[1].ToString(),
                    PlanQty = int.Parse(dr[2].ToString()),
                    WO = dr[3].ToString(),
                    PN = dr[4].ToString(),
                    WOQty = int.Parse(dr[5].ToString()),
                    StartTime = dr[6].ToString(),
                    EndTime = dr[7].ToString(),
                };
                _lNCollection.Add(ePPSPOInfo);
            }
            return _lNCollection;
        }


        private static string GenXMLBuilder(string EPPSPOInfo)
        {
            LNXMLDetailBuilder lNXMLDetailBuilder = new LNXMLDetailBuilder();
            lNXMLDetailBuilder.Add("LineName", GetLineName());
            lNXMLDetailBuilder.Add("StationNameBefore", GetStationNameBefore());
            lNXMLDetailBuilder.Add("StationNameAfter", GetStationNameAfter());
            lNXMLDetailBuilder.Add("EPPSPOInfo", EPPSPOInfo);
            return lNXMLDetailBuilder.ToXMLString();
        }

        public static string GetPOInfoXML(LNPOInfoCollection _lNCollection)
        {
            StringWriter sw = new StringWriter();
            XmlTextWriter xmlw = new XmlTextWriter(sw);

            if ((_lNCollection != null))
            {
                xmlw.WriteProcessingInstruction("xml", "version=\"1.0\" encoding=\"ISO-8859-1\"");
                xmlw.WriteStartElement("POInfo");

                foreach (EPPSPOInfo ePPSPOInfo in _lNCollection)
                {
                    if ((ePPSPOInfo != null))
                    {
                        xmlw.WriteStartElement("PO");
                        xmlw.WriteAttributeString("LineName", "", ePPSPOInfo.LineName);
                        xmlw.WriteAttributeString("PlanDate", "", ePPSPOInfo.PlanDate);
                        xmlw.WriteAttributeString("PlanQty", "", ePPSPOInfo.PlanQty.ToString());
                        xmlw.WriteAttributeString("WO", "", ePPSPOInfo.WO);
                        xmlw.WriteAttributeString("PN", "", ePPSPOInfo.PN);
                        xmlw.WriteAttributeString("WOQty", "", ePPSPOInfo.WOQty.ToString());
                        xmlw.WriteAttributeString("StartTime", "", ePPSPOInfo.StartTime);
                        xmlw.WriteAttributeString("EndTime", "", ePPSPOInfo.EndTime);
                        xmlw.WriteEndElement();
                    }
                }
                xmlw.WriteEndElement();
            }


            return sw.ToString();
        }
    }
}
