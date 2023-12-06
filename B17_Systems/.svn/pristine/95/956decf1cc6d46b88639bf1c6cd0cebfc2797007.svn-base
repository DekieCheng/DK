using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace SDPSubSystem.Common.Security
{
    public class DESSecurity
    {

        //默认密钥向量
        private static string _iv = "*^K#!$@#";

        //密钥
        private static string _key = "*#FLEX%^";

        /// DES加密
        /// <param >待加密的字符串</param>
        /// <param >加密密钥,要求为8位</param>
        /// <returns>加密成功返回加密后的字符串，失败返回源串</returns>

        public static string Encrypt(string encryptString)
        {
            StringBuilder strRetValue = new StringBuilder();

            try
            {
                byte[] keyBytes = Encoding.UTF8.GetBytes(_key.Substring(0, 8));
                byte[] keyIV = keyBytes;
                byte[] inputByteArray = Encoding.UTF8.GetBytes(encryptString);
                DESCryptoServiceProvider provider = new DESCryptoServiceProvider();

                provider.Mode = CipherMode.ECB;//兼容其他语言的Des加密算法  
                provider.Padding = PaddingMode.Zeros;//自动补0

                MemoryStream mStream = new MemoryStream();
                CryptoStream cStream = new CryptoStream(mStream, provider.CreateEncryptor(keyBytes, keyIV), CryptoStreamMode.Write);
                cStream.Write(inputByteArray, 0, inputByteArray.Length);
                cStream.FlushFinalBlock();

                //不使用base64编码
                //return Convert.ToBase64String(mStream.ToArray()); 

                //组织成16进制字符串            
                foreach (byte b in mStream.ToArray())
                {
                    strRetValue.AppendFormat("{0:X2}", b);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
            return strRetValue.ToString();
        }

        /// DES解密
        /// <param >待解密的字符串</param>
        /// <param >解密密钥,要求为8位,和加密密钥相同</param>
        /// <returns>解密成功返回解密后的字符串，失败返源串</returns>

        public static string Decrypt(string decryptString)
        {
            string strRetValue = "";
            try
            {
                byte[] keyBytes = Encoding.UTF8.GetBytes(_key.Substring(0, 8));
                byte[] keyIV = keyBytes;

                //不使用base64解码
                //byte[] inputByteArray = Convert.FromBase64String(decryptString);

                //16进制转换为byte字节
                byte[] inputByteArray = new byte[decryptString.Length / 2];
                for (int x = 0; x < decryptString.Length / 2; x++)
                {
                    int i = (Convert.ToInt32(decryptString.Substring(x * 2, 2), 16));
                    inputByteArray[x] = (byte)i;
                }

                DESCryptoServiceProvider provider = new DESCryptoServiceProvider();

                provider.Mode = CipherMode.ECB;//兼容其他语言的Des加密算法  
                provider.Padding = PaddingMode.Zeros;//自动补0  

                MemoryStream mStream = new MemoryStream();
                CryptoStream cStream = new CryptoStream(mStream, provider.CreateDecryptor(keyBytes, keyIV), CryptoStreamMode.Write);
                cStream.Write(inputByteArray, 0, inputByteArray.Length);
                cStream.FlushFinalBlock();

                //需要去掉结尾的null字符
                //strRetValue = Encoding.UTF8.GetString(mStream.ToArray());
                strRetValue = Encoding.UTF8.GetString(mStream.ToArray()).TrimEnd('\0');
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
            return strRetValue;
        }
    }
}
