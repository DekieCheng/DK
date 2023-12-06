using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace SDPSubSystem.Common.Customized
{
    public class FilterXml
    {
        public static TextReader EscapeXmlText(ref string obStr)
        {
            string tmp = "";
            string pattern = @"((?<=<\w+>).*)(<)(?=<\/\w+>)";
            TextReader reader = null;
            try
            {
                //Encoding en = Encoding.Default;
                byte[] array = Encoding.Default.GetBytes(obStr);

                Stream stream = new MemoryStream(array);
                using (StreamReader sr = new StreamReader(stream, Encoding.Default))
                {
                    tmp = sr.ReadToEnd().Replace("&", "&amp");

                    if (!string.IsNullOrEmpty(tmp))
                    {
                        tmp = Regex.Replace(tmp, pattern, "$1<");
                        tmp = tmp.Replace("\r\n", "");
                        tmp = Regex.Replace(tmp, @"\s{2,}", "");
                    }
                }
                StringReader streamStr = new StringReader(tmp);
                reader = streamStr;

            }
            catch (Exception ex)
            {
                reader.Dispose();
            }

            return reader;

        }

        /// <summary>
        /// Turns a string into a properly XML Encoded string.
        /// Uses simple string replacement.
        /// 
        /// Also see XmlUtils.XmlString() which uses XElement
        /// to handle additional extended characters.
        /// </summary>
        /// <param name="text">Plain text to convert to XML Encoded string</param>
        /// <param name="isAttribute">
        /// If true encodes single and double quotes, CRLF and tabs.
        /// When embedding element values quotes don't need to be encoded.
        /// When embedding attributes quotes need to be encoded.
        /// </param>
        /// <returns>XML encoded string</returns>
        /// <exception cref="InvalidOperationException">Invalid character in XML string</exception>
        public static string XmlString(string text, bool isAttribute = false)
        {
            var sb = new StringBuilder(text.Length);

            foreach (var chr in text)
            {
                if (chr == '<')
                    sb.Append("&lt;");
                else if (chr == '>')
                    sb.Append("&gt;");
                else if (chr == '&')
                    sb.Append("&amp;");

                // special handling for quotes
                else if (isAttribute && chr == '\"')
                    sb.Append("&quot;");
                else if (isAttribute && chr == '\'')
                    sb.Append("&apos;");

                // Legal sub-chr32 characters
                else if (chr == '\n')
                    sb.Append(isAttribute ? "&#xA;" : "\n");
                else if (chr == '\r')
                    sb.Append(isAttribute ? "&#xD;" : "\r");
                else if (chr == '\t')
                    sb.Append(isAttribute ? "&#x9;" : "\t");
                else
                {
                    
                    if (chr < 32)
                    {
                        //throw new InvalidOperationException("Invalid character in Xml String. Chr " +                Convert.ToInt16(chr) + " is illegal.");
                        char space = (char)32;
                        sb.Append(space);
                    }
                    else
                    {
                        sb.Append(chr);
                    }
                }
            }

            return sb.ToString();
        }

       
    }
}
