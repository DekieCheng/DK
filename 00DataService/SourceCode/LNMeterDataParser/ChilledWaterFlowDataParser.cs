using System;
using System.Collections;

namespace LNMeterDataParser
{
    public class ChilledWaterFlowDataParser : IDisposable
    {
        public ChilledWaterFlowDataParser()
        { }

        public Hashtable ParserReceivedData(Hashtable ArrivedData)
        {
            try
            {
                if (ArrivedData == null) return null;

                Hashtable retHt = new Hashtable();
                string hexInstantflow = string.Empty;               //瞬时流量(Nm3/h)
                string hexInstantaneousheat = string.Empty;         //瞬时热流量(GJ/h)
                string hexInstantflowrate = string.Empty;           //流体速度(m/s)
                string hexAccumulatedflow = string.Empty;           //正累积流量(Nm3)
                string hexAccumulatedheat = string.Empty;           //正累积热量(GJ)
                string hexInletWaterTemperature = string.Empty;     //进水温度 ℃ (供水温度)
                string hexOutletWaterTemperature = string.Empty;    //出水温度 ℃ (回水温度)


                string OrgRecDataStr = GetOrgRecData(ArrivedData);
                string[] OrgData = OrgRecDataStr.Split(new char[] { ' ' });

                //Get 瞬时流量(Nm3/h)
                hexInstantflow = OrgData[3].ToString() + OrgData[4].ToString() + OrgData[5].ToString() + OrgData[6].ToString();
                float decInstantflow = IEEE754Helper.HexToFloat(hexInstantflow);
                retHt.Add("IF", (decInstantflow).ToString());

                //Get 瞬时热流量(GJ/h)
                hexInstantaneousheat = OrgData[7].ToString() + OrgData[8].ToString() + OrgData[9].ToString() + OrgData[10].ToString();
                float decInstantaneousheat = IEEE754Helper.HexToFloat(hexInstantaneousheat);
                retHt.Add("IS", decInstantaneousheat.ToString());

                //Get 流体速度(m/s)
                hexInstantflowrate = OrgData[11].ToString() + OrgData[12].ToString() + OrgData[13].ToString() + OrgData[14].ToString();
                float decInstantflowrate = IEEE754Helper.HexToFloat(hexInstantflowrate);
                retHt.Add("IFR", decInstantflowrate.ToString());

                //Get 正累积流量(Nm3)
                hexAccumulatedflow = OrgData[19].ToString() + OrgData[20].ToString() + OrgData[21].ToString() + OrgData[22].ToString();
                long decAccumulatedflow = System.Int32.Parse(hexAccumulatedflow, System.Globalization.NumberStyles.HexNumber);
                retHt.Add("AF", decAccumulatedflow.ToString());

                //Get 正累积热量(GJ)
                hexAccumulatedheat = OrgData[35].ToString() + OrgData[36].ToString() + OrgData[37].ToString() + OrgData[38].ToString();
                long decAccumulatedheat = System.Int32.Parse(hexAccumulatedheat, System.Globalization.NumberStyles.HexNumber);
                retHt.Add("AH", decAccumulatedheat.ToString());

                //Get 进水温度 ℃ (供水温度)
                hexInletWaterTemperature = OrgData[67].ToString() + OrgData[68].ToString() + OrgData[69].ToString() + OrgData[70].ToString();
                float decInletWaterTemperature = IEEE754Helper.HexToFloat(hexInletWaterTemperature);
                retHt.Add("IWT", decInletWaterTemperature.ToString());

                //Get 出水温度 ℃ (回水温度)
                hexOutletWaterTemperature = OrgData[71].ToString() + OrgData[72].ToString() + OrgData[73].ToString() + OrgData[74].ToString();
                double decOutletWaterTemperature = IEEE754Helper.HexToFloat(hexOutletWaterTemperature);
                retHt.Add("OWT", decOutletWaterTemperature.ToString());

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
