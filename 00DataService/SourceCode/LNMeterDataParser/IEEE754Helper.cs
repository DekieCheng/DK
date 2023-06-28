using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LNMeterDataParser
{
    /// <summary>
    /// IEEE754和浮点数的相互转化
    /// </summary>
    public class IEEE754Helper
    {
        /// <summary>
        /// 把一个浮点数转化成16进制字符串
        /// </summary>
        /// <param name="paraFloat">要转化的浮点数</param>
        /// <returns>返回的字符串</returns>

        public static string FloatToHex(float paraFloat)
        {
            StringBuilder sb = new StringBuilder();
            byte[] bytes = BitConverter.GetBytes(paraFloat);

            foreach (var item in bytes)
            {
                sb.Insert(0, item.ToString("X2"));
            }
            return sb.Insert(0, "0X").ToString();
        }

        /// <summary>
        /// 把4个字节的16进制字符串转化成一个浮点数
        /// </summary>
        /// <param name="hexStr">待转化字符串</param>
        /// <returns>返回浮点数（转化失败返回浮点数最小值）</returns>
        public static Single HexToFloat(string hexStr)
        {

            hexStr = hexStr.Trim().ToUpper();
            if (hexStr.StartsWith("0X") || hexStr.StartsWith("0x"))
            {
                hexStr = hexStr.Substring(2);
            }
            if (hexStr.Length != 8)
            {
                return float.MinValue;
            }
            byte[] bytes = new byte[4];
            for (int i = 0; i < 4; i++)
            {
                bytes[i] = Convert.ToByte(hexStr.Substring((3 - i) * 2, 2), 16);
            }
            return BitConverter.ToSingle(bytes, 0);
        }

        public static byte[] FloatToBtyes(float paraFloat)
        {
            return BitConverter.GetBytes(paraFloat);
        }

        public static float ByteToFloat(byte[] bytes)
        {
            if (bytes.Length != 4)
            {
                return float.MinValue;
            }
            return BitConverter.ToSingle(bytes, 0);
        }

        public static Single floatintStringToFloat(String data)
        {
            if (data.Length < 8 || data.Length > 8)
            {
                //throw new NotEnoughDataInBufferException(data.length(), 8);
                throw (new ApplicationException("缓存中的数据不完整。"));
            }
            else
            {
                byte[] intBuffer = new byte[4];
                // 将16进制串按字节逆序化（一个字节2个ASCII码）
                for (int i = 0; i < 4; i++)
                {
                    intBuffer[i] = Convert.ToByte(data.Substring((3 - i) * 2, 2), 16);
                }
                return BitConverter.ToSingle(intBuffer, 0);
            }
        }

        public decimal Hex2Decimail(byte b1, byte b2, byte b3, byte b4)
        {
            byte[] b = new byte[4];
            b[0] = Convert.ToByte(b1.ToString("X2"), 16);
            b[1] = Convert.ToByte(b2.ToString("X2"), 16);
            b[2] = Convert.ToByte(b3.ToString("X2"), 16);
            b[3] = Convert.ToByte(b4.ToString("X2"), 16);
            if (BitConverter.IsLittleEndian)
                Array.Reverse(b);
            decimal d = Convert.ToDecimal(BitConverter.ToSingle(b, 0));

            return d;
        }
        public static int Hex2Int(string hexStr)
        {

            hexStr = hexStr.Trim().ToUpper();
            if (hexStr.StartsWith("0X") || hexStr.StartsWith("0x"))
            {
                hexStr = hexStr.Substring(2);
            }
            if (hexStr.Length != 8)
            {
                return int.MinValue;
            }
            byte[] bytes = new byte[4];
            for (int i = 0; i < 4; i++)
            {
                bytes[i] = Convert.ToByte(hexStr.Substring((3 - i) * 2, 2), 16);
            }
            return BitConverter.ToInt32(bytes, 0);
        }


        public int Hex2Int(byte b1, byte b2)
        {
            byte[] b = new byte[2];
            b[0] = Convert.ToByte(b1.ToString("X2"), 16);
            b[1] = Convert.ToByte(b2.ToString("X2"), 16);
            if (BitConverter.IsLittleEndian)
                Array.Reverse(b);
            int f = BitConverter.ToInt16(b, 0);

            return f;
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
