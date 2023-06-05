using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Text;

namespace LNElectricityMeterParser
{
    public class ElectricityDataPaser
    {
        public ElectricityDataPaser()
        {

        }

        public Hashtable ParserReceivedData(string ArrivedData)
        {
            try
            {
                Hashtable retHt = new Hashtable();
                string[] recKWH = ArrivedData.Split(new char[] { ' ' });
                string KWh = recKWH[3].ToString() + recKWH[4].ToString() + recKWH[5].ToString() + recKWH[6].ToString();
                int finalKWh = Convert.ToInt32(KWh, 16);
                retHt.Add("OrgData", ArrivedData);
                retHt.Add("KWh", finalKWh);
                return retHt;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public static int Hex2Ten(string hex)
        {
            int ten = 0;
            for (int i = 0, j = hex.Length - 1; i < hex.Length; i++)
            {
                ten += HexChar2Value(hex.Substring(i, 1)) * ((int)Math.Pow(16, j));
                j--;
            }
            return ten;
        }

        public static int HexChar2Value(string hexChar)
        {
            switch (hexChar)
            {
                case "0":
                case "1":
                case "2":
                case "3":
                case "4":
                case "5":
                case "6":
                case "7":
                case "8":
                case "9":
                    return Convert.ToInt32(hexChar);
                case "a":
                case "A":
                    return 10;
                case "b":
                case "B":
                    return 11;
                case "c":
                case "C":
                    return 12;
                case "d":
                case "D":
                    return 13;
                case "e":
                case "E":
                    return 14;
                case "f":
                case "F":
                    return 15;
                default:
                    return 0;
            }
        }
    }
}
