using System;
using System.Collections;

namespace LNMeterDataParser
{
    public class ElectricityDataParser : IDisposable
    {
        private bool disposedValue;

        public ElectricityDataParser()
        {

        }

        public Hashtable ParserReceivedData(Hashtable ArrivedData)
        {
            try
            {
                Hashtable retHt = new Hashtable();
                string hexTAP = string.Empty; //总有功功率 （Total active power）
                string hexTPAE = string.Empty; //正向有功总电能 (Total positive active energy)
                string hexAPV = string.Empty; //A相电压
                string hexBPV = string.Empty; //B相电压
                string hexCPV = string.Empty; //C相电压
                string hexAPC = string.Empty; //A相电流
                string hexBPC = string.Empty; //B相电流
                string hexCPC = string.Empty; //C相电流
                string hexCPPF = string.Empty; //合相功率因数

                bool isnewProtocal = IsNewProtocal(ArrivedData);
                string OrgRecDataStr = GetOrgRecData(ArrivedData, isnewProtocal ? "KEYOFORGDATA_NEW" : "KEYOFORGDATA_OLD");
                string[] OrgData = OrgRecDataStr.Split(new char[] { ' ' });
                if (!isnewProtocal)
                {
                    //Get TAP 总有功功率
                    hexTAP = OrgData[9].ToString() + OrgData[10].ToString();
                    double decTAP = System.Int32.Parse(hexTAP, System.Globalization.NumberStyles.HexNumber);
                    retHt.Add("TAP", decTAP.ToString());

                    //Read TPAE 正向有功总电能
                    hexTPAE = OrgData[49].ToString() + OrgData[50].ToString() + OrgData[51].ToString() + OrgData[52].ToString();
                    double decTAPE = System.Int32.Parse(hexTPAE, System.Globalization.NumberStyles.HexNumber);
                    retHt.Add("TPAE", decTAPE.ToString());

                    //A相电压
                    hexAPV = OrgData[27].ToString() + OrgData[28].ToString();
                    double decAPV = System.Int32.Parse(hexAPV, System.Globalization.NumberStyles.HexNumber) * 0.1;
                    retHt.Add("APV", decAPV.ToString());
                    //B相电压
                    hexBPV = OrgData[29].ToString() + OrgData[30].ToString();
                    double decBPV = System.Int32.Parse(hexBPV, System.Globalization.NumberStyles.HexNumber) * 0.1;
                    retHt.Add("BPV", decBPV.ToString());
                    //C相电压
                    hexCPV = OrgData[31].ToString() + OrgData[32].ToString();
                    double decCPV = System.Int32.Parse(hexCPV, System.Globalization.NumberStyles.HexNumber) * 0.1;
                    retHt.Add("CPV", decCPV.ToString());

                    //A相电流
                    hexAPC = OrgData[33].ToString() + OrgData[34].ToString();
                    double decAPC = System.Int32.Parse(hexAPC, System.Globalization.NumberStyles.HexNumber) * 0.1;
                    retHt.Add("APC", decAPC.ToString());
                    //B相电流
                    hexBPC = OrgData[35].ToString() + OrgData[36].ToString();
                    double decBPC = System.Int32.Parse(hexBPC, System.Globalization.NumberStyles.HexNumber) * 0.1;
                    retHt.Add("BPC", decBPC.ToString());
                    //C相电流
                    hexCPC = OrgData[37].ToString() + OrgData[38].ToString();
                    double decCPC = System.Int32.Parse(hexCPC, System.Globalization.NumberStyles.HexNumber) * 0.1;
                    retHt.Add("CPC", decCPC.ToString());

                    //合相功率因数
                    hexCPPF = OrgData[45] + OrgData[46];
                    double decCPPF = System.Int32.Parse(hexCPPF, System.Globalization.NumberStyles.HexNumber) * 0.001;
                    retHt.Add("CPPF", decCPPF.ToString());

                }
                else
                {
                    //Get TAP 总有功功率
                    hexTAP = OrgData[15].ToString() + OrgData[16].ToString() + OrgData[17].ToString() + OrgData[18].ToString();
                    double singleTAP = IEEE754Helper.HexToFloat(hexTAP) * 0.01;
                    retHt.Add("TAP", singleTAP.ToString());
                    //Read TPAE 正向有功总电能
                    hexTPAE = OrgData[111].ToString() + OrgData[112].ToString() + OrgData[113].ToString() + OrgData[114].ToString();
                    double singleTAPE = IEEE754Helper.HexToFloat(hexTPAE) * 0.01;
                    retHt.Add("TPAE", singleTAPE.ToString());

                    //A相电压
                    hexAPV = OrgData[51].ToString() + OrgData[52].ToString() + OrgData[53].ToString() + OrgData[54].ToString();
                    double decAPV = IEEE754Helper.HexToFloat(hexAPV) * 0.0001;
                    retHt.Add("APV", decAPV.ToString());

                    //B相电压
                    hexBPV = OrgData[55].ToString() + OrgData[56].ToString() + OrgData[57].ToString() + OrgData[58].ToString();
                    double decBPV = IEEE754Helper.HexToFloat(hexBPV) * 0.0001;
                    retHt.Add("BPV", decBPV.ToString());

                    //C相电压
                    hexCPV = OrgData[59].ToString() + OrgData[60].ToString() + OrgData[61].ToString() + OrgData[62].ToString();
                    double decCPV = IEEE754Helper.HexToFloat(hexCPV) * 0.0001;
                    retHt.Add("CPV", decCPV.ToString());

                    //A相电流
                    hexAPC = OrgData[63].ToString() + OrgData[64].ToString() + OrgData[65].ToString() + OrgData[66].ToString();
                    double decAPC = IEEE754Helper.HexToFloat(hexAPC) * 0.000001;
                    retHt.Add("APC", decAPC.ToString());

                    //B相电流
                    hexBPC = OrgData[67].ToString() + OrgData[68].ToString() + OrgData[69].ToString() + OrgData[70].ToString();
                    double decBPC = IEEE754Helper.HexToFloat(hexBPC) * 0.000001;
                    retHt.Add("BPC", decBPC.ToString());

                    //C相电流
                    hexCPC = OrgData[71].ToString() + OrgData[72].ToString() + OrgData[73].ToString() + OrgData[74].ToString();
                    double decCPC = IEEE754Helper.HexToFloat(hexCPC) * 0.000001;
                    retHt.Add("CPC", decCPC.ToString());
                }
                retHt.Add("OrgData", OrgRecDataStr);

                return retHt;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private bool IsNewProtocal(Hashtable ArrivedData)
        {
            bool newprotocal = false;
            DictionaryEntry newprotocalDE = GetHTDE(ArrivedData, "NEWPROTOCAL");
            newprotocal = bool.Parse(newprotocalDE.Value.ToString());
            return newprotocal;
        }
        private string GetOrgRecData(Hashtable ArrivedData, string keyName)
        {
            string RecData = string.Empty;
            DictionaryEntry newprotocalDE = GetHTDE(ArrivedData, keyName);
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

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    // TODO: dispose managed state (managed objects)
                }

                // TODO: free unmanaged resources (unmanaged objects) and override finalizer
                // TODO: set large fields to null
                disposedValue = true;
            }
        }

        // // TODO: override finalizer only if 'Dispose(bool disposing)' has code to free unmanaged resources
        // ~ElectricityDataPaser()
        // {
        //     // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
        //     Dispose(disposing: false);
        // }

        public void Dispose()
        {
            // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }
    }
}
