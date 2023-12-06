using System;
using System.IO;
using System.Runtime.CompilerServices;
using System.Text;
using Microsoft.VisualBasic;
using System.Security.Cryptography;
using Microsoft.VisualBasic.CompilerServices;
using LNMeterInfrastructure.DAL;
using System.Configuration;
using System.Linq;

namespace LNMeterInfrastructure.Common
{
    public static class LNGlobal
    {
        //  public static FFDALBase g_DB = new FFDALBase();
        public static LNDBWrapper g_DB = new LNDBWrapper();
        public static string g_AppServerName = Environment.MachineName;
        public static string g_ServiceFilter = ConfigurationManager.AppSettings.Get("ServiceFilter");
        public static bool g_SaveLogInDB = bool.Parse(ConfigurationManager.AppSettings.Get("SaveLogInDB"));
        public static bool g_SaveLogInLocalFile = bool.Parse(ConfigurationManager.AppSettings.Get("SaveLogInLocalFile"));
        public static bool g_SPLoggingEnabled = bool.Parse(ConfigurationManager.AppSettings.Get("SPLoggingEnabled"));
        public static int g_LoopIntervalTime = int.Parse(ConfigurationManager.AppSettings.Get("LoopIntervalTime"));
        public static int g_ReReadCounter = int.Parse(ConfigurationManager.AppSettings.Get("ReReadCounter"));

        public enum MeterDevCategory
        {
            Water = 1,
            Electricity = 2,
            ChilledWaterFlow = 3,
            CompressedAirFlow = 4,
            ReservedDevice001 = 5,
            ReservedDevice002 = 6,
            ReservedDevice003 = 7,
            ReservedDevice004 = 8,
            ReservedDevice005 = 9,
            ReservedDevice006 = 10,
            ReservedDevice007 = 11,
            ReservedDevice008 = 12,
            ReservedDevice009 = 13,
            ReservedDevice010 = 14
        }

        public static object IfIsDBNull(object value, object ifisNullValue)
        {
            if (Information.IsDBNull(RuntimeHelpers.GetObjectValue(value)))
                return ifisNullValue;
            if ((value == null))
                return ifisNullValue;
            return value;
        }

        public static bool IfIsDigitalStr(string str)
        {
            str = str.Replace("'", "");

            foreach (char a in str)
            {
                if (char.IsDigit(a))
                    return true;
            }
            return false;
        }

        public static string DecryptString(string inputString)
        {
            RijndaelManaged RM = new RijndaelManaged();
            CryptoStream crypt;
            MemoryStream MS;

            string retString = "";
            System.Text.ASCIIEncoding textEncoding = new System.Text.ASCIIEncoding();
            byte[] EncryptedBytes;
            byte[] DecryptedBytes;

            try
            {

                // Set the initialization vector and the public key
                byte[] Key = new byte[] { 186, 237, 232, 22, 49, 121, 195, 141, 62, 227, 111, 28, 197, 41, 49, 144, 53, 192, 110, 160, 15, 5, 196, 95, 131, 58, 66, 234, 139, 3, 29, 157 };
                byte[] IV = new byte[] { 217, 90, 231, 80, 136, 176, 7, 90, 35, 89, 156, 20, 17, 65, 91, 73 };

                // Set the initialization vector and the public key on the RijndaelManaged object
                RM.IV = IV;
                RM.Key = Key;

                // Convert the encrypted text sent in from base64 to a byte array
                EncryptedBytes = Convert.FromBase64String(inputString);

                // Set the byte array to hold the decrypted data
                DecryptedBytes = new byte[EncryptedBytes.Length + 1];

                // Set the encrypted bytes to the memory stream
                MS = new MemoryStream(EncryptedBytes);

                // Open a crypto stream for reading
                crypt = new CryptoStream(MS, RM.CreateDecryptor(), CryptoStreamMode.Read);

                // Read the decrypted bytes into a byte array
                crypt.Read(DecryptedBytes, 0, DecryptedBytes.Length);

                // Convert the decrypted bytes to a string
                foreach (char chr in textEncoding.GetChars(DecryptedBytes))
                {
                    if (Strings.Asc(chr) == 0)
                        break;
                    retString = retString + chr;
                }

                crypt.Close();
                MS.Close();
                return retString;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static string EncryptString(string inputString)
        {
            string str;
            RijndaelManaged managed = new RijndaelManaged();
            MemoryStream stream2 = new MemoryStream();
            UTF8Encoding encoding = new UTF8Encoding();
            try
            {
                byte[] buffer3 = new byte[] { 0xBA, 0xED, 0xE8, 0x16, 0x31, 0x79, 0xC3, 0x8D, 0x3E, 0xE3, 0x6F, 0x1, 0xC5, 0x29, 0x31, 0x90, 0x35, 0xC0, 110, 160, 15, 5, 0xC4, 0x5F, 0x83, 0x3A, 0x42, 0xEA, 0x8B, 3, 0x1D, 0x9D };
                managed.IV = new byte[] { 0xD9, 90, 0xE7, 80, 0x88, 0xB0, 7, 90, 0x23, 0x59, 0x9, 20, 0x11, 0x41, 0x5B, 0x49 };
                managed.Key = buffer3;
                CryptoStream stream = new CryptoStream(stream2, managed.CreateEncryptor(), CryptoStreamMode.Write);
                byte[] bytes = encoding.GetBytes(inputString);
                stream.Write(bytes, 0, bytes.Length);
                stream.FlushFinalBlock();
                stream.Close();
                string str2 = Convert.ToBase64String(stream2.ToArray());
                stream2.Close();
                str = str2;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (stream2 != null) { stream2.Close(); }
            }

            return str;
        }

        public static string ToConvertHexString(byte[] cmdByte)
        {
            string destStr = "";
            foreach (byte bt in cmdByte)
            {
                string rcr = Conversion.Hex(bt);
                if (rcr.Length == 1)
                    rcr = rcr.PadLeft(2, '0');
                destStr = destStr + rcr + Strings.Space(1);
            }
            char[] trimChars = new[] { ' ' };
            return destStr.TrimEnd(trimChars);
        }
        public static string ToConvertHexString(string[] cmdByte)
        {
            string destStr = "";
            foreach (string bt in cmdByte)
            {
                string rcr = bt;
                if (rcr.Length == 1) { rcr = rcr.PadLeft(2, '0'); }
                destStr = destStr + rcr + Strings.Space(1);
            }
            char[] trimChars = new[] { ' ' };
            return destStr.TrimEnd(trimChars);
        }

        public static byte[] HexStringToBytes(string hexStr)
        {
            if (string.IsNullOrEmpty(hexStr))
            {
                return new byte[0];
            }
            if (hexStr.StartsWith("0x"))
            {
                hexStr = hexStr.Remove(0, 2);
            }

            var count = hexStr.Length;
            if (count % 2 == 1)
            {
                throw new ArgumentException("Invalid length of bytes:" + count);
            }
            var byteCount = count / 2;
            var result = new byte[byteCount];
            for (int ii = 0; ii < byteCount; ++ii)
            {
                var tempBytes = Byte.Parse(hexStr.Substring(2 * ii, 2), System.Globalization.NumberStyles.HexNumber);
                result[ii] = tempBytes;
            }
            return result;
        }

        public static string BytesTohexString(byte[] bytes)
        {
            if (bytes == null || bytes.Count() < 1)
            {
                return string.Empty;
            }
            var count = bytes.Count();
            var cache = new StringBuilder();
            cache.Append("0x");
            for (int ii = 0; ii < count; ++ii)
            {
                var tempHex = Convert.ToString(bytes[ii], 16).ToUpper();
                cache.Append(tempHex.Length == 1 ? "0" + tempHex : tempHex);
            }
            return cache.ToString();
        }
        public static string BytesTohexString(byte[] bytes, int bytesRead)
        {
            if (bytes == null || bytes.Count() < 1)
            {
                return string.Empty;
            }
            var count = bytesRead;// bytes.Count();
            var cache = new StringBuilder();
            cache.Append("0x");
            for (int ii = 0; ii < count; ++ii)
            {
                var tempHex = Convert.ToString(bytes[ii], 16).ToUpper();
                cache.Append(tempHex.Length == 1 ? "0" + tempHex : tempHex);
            }
            return cache.ToString();
        }
        public static string Hex2Ten(string hex)
        {
            int ten = 0;
            for (int i = 0, j = hex.Length - 1; i < hex.Length; i++)
            {
                ten += HexChar2Value(hex.Substring(i, 1)) * ((int)Math.Pow(16, j));
                j--;
            }
            return ten.ToString();
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