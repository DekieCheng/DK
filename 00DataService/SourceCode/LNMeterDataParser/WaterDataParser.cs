using System;
using System.Collections;

namespace LNMeterDataParser
{
    public class WaterDataParser : IDisposable
    {
        public WaterDataParser()
        { }

        public Hashtable ParserReceivedData(Hashtable ArrivedData)
        {
            try
            {
                if (ArrivedData == null) return null;

                Hashtable retHt = new Hashtable();
                string hexMediumtemperature = string.Empty; //介质温度
                string hexCumulativeTrafficUnder100 = string.Empty; //累计流量的百位以下
                string hexCumulativeTrafficOver100 = string.Empty; // 累计流量的百位以上
                string hexInstantaneousDelivery = string.Empty; //瞬时流量
                string hexTransientflowrate = string.Empty; //瞬时流速
                string hexSensorVoltageValue = string.Empty; //传感器电压值

                string OrgRecDataStr = GetOrgRecData(ArrivedData);
                string[] OrgData = OrgRecDataStr.Split(new char[] { ' ' });

                //Get 介质温度
                hexMediumtemperature = OrgData[5].ToString() + OrgData[6].ToString() + OrgData[4].ToString() + OrgData[3].ToString();
                double decMediumtemperature = System.Int32.Parse(hexMediumtemperature, System.Globalization.NumberStyles.HexNumber);
                retHt.Add("MDTEMP", hexMediumtemperature.ToString());

                //Get 瞬时流量
                hexInstantaneousDelivery = OrgData[9].ToString() + OrgData[10].ToString() + OrgData[7].ToString() + OrgData[8].ToString();
                double decIND = System.Int32.Parse(hexInstantaneousDelivery, System.Globalization.NumberStyles.HexNumber);
                retHt.Add("IND", decIND.ToString());

                //Get 瞬时流速
                hexTransientflowrate = OrgData[13].ToString() + OrgData[14].ToString() + OrgData[11].ToString() + OrgData[12].ToString();
                double decTFR = System.Int32.Parse(hexTransientflowrate, System.Globalization.NumberStyles.HexNumber);
                retHt.Add("TFR", decTFR.ToString());

                //Get 传感器电压值
                hexSensorVoltageValue = OrgData[13].ToString() + OrgData[14].ToString() + OrgData[11].ToString() + OrgData[12].ToString();
                double decSVV = System.Int32.Parse(hexSensorVoltageValue, System.Globalization.NumberStyles.HexNumber);
                retHt.Add("SVV", decSVV.ToString());

                //Get 累计流量计算公式：累计流量 = 累计流量的百位以上 x 100 + 累计流量的百位以下
                hexCumulativeTrafficOver100 = OrgData[21].ToString() + OrgData[22].ToString() + OrgData[19].ToString() + OrgData[20].ToString();
                hexCumulativeTrafficUnder100 = OrgData[25].ToString() + OrgData[26].ToString() + OrgData[23].ToString() + OrgData[24].ToString();
                double decCTO = System.Int32.Parse(hexCumulativeTrafficOver100, System.Globalization.NumberStyles.HexNumber) * 100;
                double decCTU = System.Int32.Parse(hexCumulativeTrafficUnder100, System.Globalization.NumberStyles.HexNumber);
                retHt.Add("CT", (decCTO + decCTU).ToString());

                retHt.Add("OrgData", OrgRecDataStr);

                return retHt;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private string GetOrgRecData(Hashtable ArrivedData)
        {
            string RecData = string.Empty;
            DictionaryEntry newprotocalDE = GetHTDE(ArrivedData, "KEYOFORGDATA");
            RecData = newprotocalDE.Value.ToString();
            return RecData;
        }

        private DictionaryEntry GetHTDE(Hashtable ArrivedData, string keyName)
        {
            DictionaryEntry retDE;
            foreach (DictionaryEntry de in ArrivedData)
            {
                if (de.Key.ToString().ToUpper().Equals(keyName.ToUpper()))
                {
                    retDE = de;
                    break;
                }
            }
            return retDE;
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }
    }
}
