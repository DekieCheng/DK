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
                string hexStere = string.Empty; //当前水表读数

                string OrgRecDataStr = GetOrgRecData(ArrivedData);
                string[] OrgData = OrgRecDataStr.Split(new char[] { ' ' });

                //当前水表读数
                hexStere = OrgData[5].ToString() + OrgData[6].ToString() + OrgData[3].ToString() + OrgData[4].ToString();
                double decStere = System.Int32.Parse(hexStere, System.Globalization.NumberStyles.HexNumber);
                retHt.Add("Stere", decStere.ToString());
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
