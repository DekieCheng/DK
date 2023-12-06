using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Net.Mime;
using System.Text;
using System.Text.RegularExpressions;

namespace SDPSubSystem.Services.Common
{
    public class sdpMail
    {
        public static bool sdpSendMail(string SMTPServer, string port, bool ssl, string user, string password, string From, string DisplayName, string To, string CC, string BCC, MailPriority MailPriority, bool IsHtml, string Subject, string Body, string Attachments, ref string Msg)
        {
            bool blResult = false;
            string strPerson = "";
            Msg = "";
            MailMessage mail = new MailMessage();
            try
            {
                foreach (string sTo in To.Split(new char[] { ',', ';', '|' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    mail.To.Add(sTo.Replace(" ", ""));
                }
                foreach (string sCC in CC.Split(new char[] { ',', ';', '|' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    mail.CC.Add(sCC.Replace(" ", ""));
                }
                foreach (string sBCC in BCC.Split(new char[] { ',', ';', '|' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    mail.Bcc.Add(sBCC.Replace(" ", ""));
                }

                strPerson = "Send to " + (mail.To.Count > 0 ? mail.To.First().DisplayName + (mail.To.Count > 1 ? " etc." : "") : "");
                if (mail.CC.Count > 0) strPerson += (strPerson != "" ? "; " : "") + "CC to " + mail.CC.First().DisplayName + (mail.CC.Count > 1 ? " etc." : "");
                if (mail.Bcc.Count > 0) strPerson += (strPerson != "" ? "; " : "") + "BCC to " + mail.Bcc.First().DisplayName + (mail.Bcc.Count > 1 ? " etc." : "");

                mail.From = new MailAddress(From, DisplayName, Encoding.UTF8);    // Encoding.GetEncoding(936) for 中文 
                mail.Subject = Subject;
                mail.SubjectEncoding = Encoding.UTF8;
                mail.Body = Body;
                mail.BodyEncoding = Encoding.UTF8;
                mail.IsBodyHtml = IsHtml;
                mail.Priority = (MailPriority == MailPriority.High ? System.Net.Mail.MailPriority.High : (MailPriority == MailPriority.Low ? System.Net.Mail.MailPriority.Low : System.Net.Mail.MailPriority.Normal));
                if (Attachments.Trim() != "")
                {
                    foreach (string attachment in Attachments.Trim().Split(new char[] { ',', ';', '|' }, StringSplitOptions.RemoveEmptyEntries))
                    {
                        if (attachment.Trim() != "")
                        {
                            Attachment att = new Attachment(attachment);
                            mail.Attachments.Add(att);
                        }
                    }
                }

                SmtpClient client = new SmtpClient(SMTPServer);
                client.DeliveryMethod = SmtpDeliveryMethod.Network;
                client.EnableSsl = ssl;
                if (!Regex.IsMatch(port, @"^\d+$")) port = "0";
                int iPort = Convert.ToInt32(port);
                if (iPort > 0 && iPort != 25) client.Port = iPort;  //smtp default port is 25
                if (user != "")
                    client.Credentials = new NetworkCredential(user, password);
                else
                    client.UseDefaultCredentials = true;

                client.Send(mail);

                Msg = "Sent mail OK";
                blResult = true;
            }
            catch (SmtpException ex)
            {
                Msg = (strPerson != "" ? strPerson + " failed - " : "") + ex.Message + (ex.InnerException != null ? " " + (ex.InnerException.ToString().Length > 100 ? ex.InnerException.ToString().Substring(0, 100) + "..." : ex.InnerException.ToString()) : "");
                blResult = false;
            }
            catch (Exception ex)
            {
                Msg = (strPerson != "" ? strPerson + " failed - " : "") + ex.Message + (ex.InnerException != null ? " " + (ex.InnerException.ToString().Length > 100 ? ex.InnerException.ToString().Substring(0, 100) + "..." : ex.InnerException.ToString()) : "");
                blResult = false;
            }
            finally
            {
                foreach (Attachment item in mail.Attachments)
                {
                    item.Dispose();
                }
            }
            return blResult;
        }

        public static bool sdpSendMail(string SMTPServer, string port, bool ssl, string user, string password, string From, string DisplayName, string To, string CC, string BCC, MailPriority MailPriority, bool IsHtml, string Subject, AlternateView htmlBody, string Attachments, ref string Msg)
        {
            bool blResult = false;
            string strPerson = "";
            Msg = "";
            MailMessage mail = new MailMessage();
            try
            {
                foreach (string sTo in To.Split(new char[] { ',', ';', '|' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    mail.To.Add(sTo.Replace(" ", ""));
                }
                foreach (string sCC in CC.Split(new char[] { ',', ';', '|' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    mail.CC.Add(sCC.Replace(" ", ""));
                }
                foreach (string sBCC in BCC.Split(new char[] { ',', ';', '|' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    mail.Bcc.Add(sBCC.Replace(" ", ""));
                }

                strPerson = "Send to " + (mail.To.Count > 0 ? mail.To.First().DisplayName + (mail.To.Count > 1 ? " etc." : "") : "");
                if (mail.CC.Count > 0) strPerson += (strPerson != "" ? "; " : "") + "CC to " + mail.CC.First().DisplayName + (mail.CC.Count > 1 ? " etc." : "");
                if (mail.Bcc.Count > 0) strPerson += (strPerson != "" ? "; " : "") + "BCC to " + mail.Bcc.First().DisplayName + (mail.Bcc.Count > 1 ? " etc." : "");

                mail.From = new MailAddress(From, DisplayName, Encoding.UTF8);    // Encoding.GetEncoding(936) for 中文 
                mail.Subject = Subject;
                mail.SubjectEncoding = Encoding.UTF8;
                mail.AlternateViews.Add(htmlBody);
                mail.BodyEncoding = Encoding.UTF8;
                mail.IsBodyHtml = IsHtml;
                mail.Priority = (MailPriority == MailPriority.High ? System.Net.Mail.MailPriority.High : (MailPriority == MailPriority.Low ? System.Net.Mail.MailPriority.Low : System.Net.Mail.MailPriority.Normal));
                if (Attachments.Trim() != "")
                {
                    foreach (string attachment in Attachments.Trim().Split(new char[] { ',', ';', '|' }, StringSplitOptions.RemoveEmptyEntries))
                    {
                        if (attachment.Trim() != "")
                        {
                            Attachment att = new Attachment(attachment);
                            mail.Attachments.Add(att);
                        }
                    }
                }

                SmtpClient client = new SmtpClient(SMTPServer);
                client.DeliveryMethod = SmtpDeliveryMethod.Network;
                client.EnableSsl = ssl;
                if (!Regex.IsMatch(port, @"^\d+$")) port = "0";
                int iPort = Convert.ToInt32(port);
                if (iPort > 0 && iPort != 25) client.Port = iPort;  //smtp default port is 25
                if (user != "")
                    client.Credentials = new NetworkCredential(user, password);
                else
                    client.UseDefaultCredentials = true;

                /*
                ////要求回执的标志   
                mail.Headers.Add("Disposition-Notification-To", "ZYX-xDTS");
                ////自定义邮件头   
                mail.Headers.Add("X-Website", "http://www.zyx.org");
                ////针对 LOTUS DOMINO SERVER，插入回执头   
                mail.Headers.Add("ReturnReceipt", "1");

                mail.ReplyToList.Add(new MailAddress("test2@163.com", "我自己"));
                //如果发送失败，SMTP 服务器将发送 失败邮件告诉我   
                mail.DeliveryNotificationOptions = DeliveryNotificationOptions.OnFailure;
                //异步发送完成时的处理事件   
                client.SendCompleted += new SendCompletedEventHandler(smtp_SendCompleted);
                //开始异步发送   
                client.SendAsync(mm, null);
                */

                client.Send(mail);

                Msg = "Sent mail OK";
                blResult = true;
            }
            catch (SmtpException ex)
            {
                Msg = (strPerson != "" ? strPerson + " failed - " : "") + ex.Message + (ex.InnerException != null ? " " + (ex.InnerException.ToString().Length > 100 ? ex.InnerException.ToString().Substring(0, 100) + "..." : ex.InnerException.ToString()) : "");
                blResult = false;
            }
            catch (Exception ex)
            {
                Msg = (strPerson != "" ? strPerson + " failed - " : "") + ex.Message + (ex.InnerException != null ? " " + (ex.InnerException.ToString().Length > 100 ? ex.InnerException.ToString().Substring(0, 100) + "..." : ex.InnerException.ToString()) : "");
                blResult = false;
            }
            finally
            {
                foreach (Attachment item in mail.Attachments)
                {
                    item.Dispose();
                }
            }
            return blResult;
        }

        public static bool sdpSendMail(string SMTPServer, string port, bool ssl, string user, string password, string From, string DisplayName, string To, string CC, string BCC, MailPriority MailPriority, bool IsHtml, string Subject, string Body, string filename, MemoryStream ms, ref string Msg)
        {
            bool blResult = false;
            string strPerson = "";
            Msg = "";
            MailMessage mail = new MailMessage();
            try
            {
                foreach (string sTo in To.Split(new char[] { ',', ';', '|' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    mail.To.Add(sTo.Replace(" ", ""));
                }
                foreach (string sCC in CC.Split(new char[] { ',', ';', '|' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    mail.CC.Add(sCC.Replace(" ", ""));
                }
                foreach (string sBCC in BCC.Split(new char[] { ',', ';', '|' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    mail.Bcc.Add(sBCC.Replace(" ", ""));
                }

                strPerson = "Send to " + (mail.To.Count > 0 ? mail.To.First().DisplayName + (mail.To.Count > 1 ? " etc." : "") : "");
                if (mail.CC.Count > 0) strPerson += (strPerson != "" ? "; " : "") + "CC to " + mail.CC.First().DisplayName + (mail.CC.Count > 1 ? " etc." : "");
                if (mail.Bcc.Count > 0) strPerson += (strPerson != "" ? "; " : "") + "BCC to " + mail.Bcc.First().DisplayName + (mail.Bcc.Count > 1 ? " etc." : "");

                mail.From = new MailAddress(From, DisplayName, Encoding.UTF8);    // Encoding.GetEncoding(936) for 中文 
                mail.Subject = Subject;
                mail.SubjectEncoding = Encoding.UTF8;
                mail.Body = Body;
                mail.BodyEncoding = Encoding.UTF8;
                mail.IsBodyHtml = IsHtml;
                mail.Priority = (MailPriority == MailPriority.High ? System.Net.Mail.MailPriority.High : (MailPriority == MailPriority.Low ? System.Net.Mail.MailPriority.Low : System.Net.Mail.MailPriority.Normal));
                Attachment att = new Attachment(ms,filename);
                mail.Attachments.Add(att);

                //if (Attachments.Trim() != "")
                //{
                //    foreach (string attachment in Attachments.Trim().Split(new char[] { ',', ';', '|' }, StringSplitOptions.RemoveEmptyEntries))
                //    {
                //        if (attachment.Trim() != "")
                //        {
                //            Attachment att = new Attachment(attachment);
                //            mail.Attachments.Add(att);
                //        }
                //    }
                //}

                SmtpClient client = new SmtpClient(SMTPServer);
                client.DeliveryMethod = SmtpDeliveryMethod.Network;
                client.EnableSsl = ssl;
                if (!Regex.IsMatch(port, @"^\d+$")) port = "0";
                int iPort = Convert.ToInt32(port);
                if (iPort > 0 && iPort != 25) client.Port = iPort;  //smtp default port is 25
                if (user != "")
                    client.Credentials = new NetworkCredential(user, password);
                else
                    client.UseDefaultCredentials = true;

                client.Send(mail);

                Msg = "Sent mail OK";
                blResult = true;
            }
            catch (SmtpException ex)
            {
                Msg = (strPerson != "" ? strPerson + " failed - " : "") + ex.Message + (ex.InnerException != null ? " " + (ex.InnerException.ToString().Length > 100 ? ex.InnerException.ToString().Substring(0, 100) + "..." : ex.InnerException.ToString()) : "");
                blResult = false;
            }
            catch (Exception ex)
            {
                Msg = (strPerson != "" ? strPerson + " failed - " : "") + ex.Message + (ex.InnerException != null ? " " + (ex.InnerException.ToString().Length > 100 ? ex.InnerException.ToString().Substring(0, 100) + "..." : ex.InnerException.ToString()) : "");
                blResult = false;
            }
            finally
            {
                foreach (Attachment item in mail.Attachments)
                {
                    item.Dispose();
                }
            }
            return blResult;
        }

        public static bool sdpSendMail(string SMTPServer, string port, bool ssl, string user, string password, string From, string DisplayName, string To, string CC, string BCC, MailPriority MailPriority, bool IsHtml, string Subject, AlternateView htmlBody, string Attachments, ref string Msg,MemoryStream ms, string ContentType,string attchmentName)
        {
            bool blResult = false;
            string strPerson = "";
            Msg = "";
            MailMessage mail = new MailMessage();
            try
            {
                foreach (string sTo in To.Split(new char[] { ',', ';', '|' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    mail.To.Add(sTo.Replace(" ", ""));
                }
                foreach (string sCC in CC.Split(new char[] { ',', ';', '|' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    mail.CC.Add(sCC.Replace(" ", ""));
                }
                foreach (string sBCC in BCC.Split(new char[] { ',', ';', '|' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    mail.Bcc.Add(sBCC.Replace(" ", ""));
                }

                strPerson = "Send to " + (mail.To.Count > 0 ? mail.To.First().DisplayName + (mail.To.Count > 1 ? " etc." : "") : "");
                if (mail.CC.Count > 0) strPerson += (strPerson != "" ? "; " : "") + "CC to " + mail.CC.First().DisplayName + (mail.CC.Count > 1 ? " etc." : "");
                if (mail.Bcc.Count > 0) strPerson += (strPerson != "" ? "; " : "") + "BCC to " + mail.Bcc.First().DisplayName + (mail.Bcc.Count > 1 ? " etc." : "");

                mail.From = new MailAddress(From, DisplayName, Encoding.UTF8);    // Encoding.GetEncoding(936) for 中文 
                mail.Subject = Subject;
                mail.SubjectEncoding = Encoding.UTF8;
                mail.AlternateViews.Add(htmlBody);
                mail.BodyEncoding = Encoding.UTF8;
                mail.IsBodyHtml = IsHtml;
                mail.Priority = (MailPriority == MailPriority.High ? System.Net.Mail.MailPriority.High : (MailPriority == MailPriority.Low ? System.Net.Mail.MailPriority.Low : System.Net.Mail.MailPriority.Normal));
                if (Attachments.Trim() != "")
                {
                    foreach (string attachment in Attachments.Trim().Split(new char[] { ',', ';', '|' }, StringSplitOptions.RemoveEmptyEntries))
                    {
                        if (attachment.Trim() != "")
                        {
                            Attachment att = new Attachment(ms, attchmentName, ContentType);
                            mail.Attachments.Add(att);
                        }
                    }
                }

                SmtpClient client = new SmtpClient(SMTPServer);
                client.DeliveryMethod = SmtpDeliveryMethod.Network;
                client.EnableSsl = ssl;
                if (!Regex.IsMatch(port, @"^\d+$")) port = "0";
                int iPort = Convert.ToInt32(port);
                if (iPort > 0 && iPort != 25) client.Port = iPort;  //smtp default port is 25
                if (user != "")
                    client.Credentials = new NetworkCredential(user, password);
                else
                    client.UseDefaultCredentials = true;

                /*
                ////要求回执的标志   
                mail.Headers.Add("Disposition-Notification-To", "ZYX-xDTS");
                ////自定义邮件头   
                mail.Headers.Add("X-Website", "http://www.zyx.org");
                ////针对 LOTUS DOMINO SERVER，插入回执头   
                mail.Headers.Add("ReturnReceipt", "1");

                mail.ReplyToList.Add(new MailAddress("test2@163.com", "我自己"));
                //如果发送失败，SMTP 服务器将发送 失败邮件告诉我   
                mail.DeliveryNotificationOptions = DeliveryNotificationOptions.OnFailure;
                //异步发送完成时的处理事件   
                client.SendCompleted += new SendCompletedEventHandler(smtp_SendCompleted);
                //开始异步发送   
                client.SendAsync(mm, null);
                */

                client.Send(mail);

                Msg = "Sent mail OK";
                blResult = true;
            }
            catch (SmtpException ex)
            {
                Msg = (strPerson != "" ? strPerson + " failed - " : "") + ex.Message + (ex.InnerException != null ? " " + (ex.InnerException.ToString().Length > 100 ? ex.InnerException.ToString().Substring(0, 100) + "..." : ex.InnerException.ToString()) : "");
                blResult = false;
            }
            catch (Exception ex)
            {
                Msg = (strPerson != "" ? strPerson + " failed - " : "") + ex.Message + (ex.InnerException != null ? " " + (ex.InnerException.ToString().Length > 100 ? ex.InnerException.ToString().Substring(0, 100) + "..." : ex.InnerException.ToString()) : "");
                blResult = false;
            }
            finally
            {
                foreach (Attachment item in mail.Attachments)
                {
                    item.Dispose();
                }
            }
            return blResult;
        }


        public static bool sdpSendMail_practise(string SMTPServer, string port, bool ssl, string user, string password, string From, string DisplayName, string To, string CC, string BCC, MailPriority MailPriority, bool IsHtml, string Subject, AlternateView htmlBody, string Attachments, ref string Msg)
        {
            bool blResult = false;
            string strPerson = "";
            Msg = "";
            MailMessage mail = new MailMessage();
            try
            {
                foreach (string sTo in To.Split(new char[] { ',', ';', '|' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    mail.To.Add(sTo.Replace(" ", ""));
                }
                foreach (string sCC in CC.Split(new char[] { ',', ';', '|' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    mail.CC.Add(sCC.Replace(" ", ""));
                }
                foreach (string sBCC in BCC.Split(new char[] { ',', ';', '|' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    mail.Bcc.Add(sBCC.Replace(" ", ""));
                }

                strPerson = "Send to " + (mail.To.Count > 0 ? mail.To.First().DisplayName + (mail.To.Count > 1 ? " etc." : "") : "");
                if (mail.CC.Count > 0) strPerson += (strPerson != "" ? "; " : "") + "CC to " + mail.CC.First().DisplayName + (mail.CC.Count > 1 ? " etc." : "");
                if (mail.Bcc.Count > 0) strPerson += (strPerson != "" ? "; " : "") + "BCC to " + mail.Bcc.First().DisplayName + (mail.Bcc.Count > 1 ? " etc." : "");

                mail.From = new MailAddress(From, DisplayName, Encoding.UTF8);    // Encoding.GetEncoding(936) for 中文 
                mail.Subject = Subject;
                mail.SubjectEncoding = Encoding.UTF8;
                mail.AlternateViews.Add(htmlBody);
                mail.BodyEncoding = Encoding.UTF8;
                mail.IsBodyHtml = IsHtml;
                mail.Priority = (MailPriority == MailPriority.High ? System.Net.Mail.MailPriority.High : (MailPriority == MailPriority.Low ? System.Net.Mail.MailPriority.Low : System.Net.Mail.MailPriority.Normal));
                if (Attachments.Trim() != "")
                {
                    foreach (string attachment in Attachments.Trim().Split(new char[] { ',', ';', '|' }, StringSplitOptions.RemoveEmptyEntries))
                    {
                        if (attachment.Trim() != "")
                        {
                            Attachment att = new Attachment(attachment);
                            mail.Attachments.Add(att);
                        }
                    }
                }

                SmtpClient client = new SmtpClient(SMTPServer);
                client.DeliveryMethod = SmtpDeliveryMethod.Network;
                client.EnableSsl = ssl;
                if (!Regex.IsMatch(port, @"^\d+$")) port = "0";
                int iPort = Convert.ToInt32(port);
                if (iPort > 0 && iPort != 25) client.Port = iPort;  //smtp default port is 25
                if (user != "")
                    client.Credentials = new NetworkCredential(user, password);
                else
                    client.UseDefaultCredentials = true;

                /*
                ////要求回执的标志   
                mail.Headers.Add("Disposition-Notification-To", "ZYX-xDTS");
                ////自定义邮件头   
                mail.Headers.Add("X-Website", "http://www.zyx.org");
                ////针对 LOTUS DOMINO SERVER，插入回执头   
                mail.Headers.Add("ReturnReceipt", "1");

                mail.ReplyToList.Add(new MailAddress("test2@163.com", "我自己"));
                //如果发送失败，SMTP 服务器将发送 失败邮件告诉我   
                mail.DeliveryNotificationOptions = DeliveryNotificationOptions.OnFailure;
                //异步发送完成时的处理事件   
                client.SendCompleted += new SendCompletedEventHandler(smtp_SendCompleted);
                //开始异步发送   
                client.SendAsync(mm, null);
                */

                client.Send(mail);


                /*
                MailMessage m = new MailMessage();
                m.From = new MailAddress("ir@jb51.net", "Raja Item");
                m.To.Add(new MailAddress("su@jb51.net", "Sekaran Uma"));
                m.Subject = "html email with embedded image coming!";
                // Create the HTML message body
                // Reference embedded images using the content ID
                string htmlBody = "<html><body><h1>Picture</h1><br><img src=\"cid:Pic1\"></body></html>";
                AlternateView avHtml = AlternateView.CreateAlternateViewFromString
                  (htmlBody, null, MediaTypeNames.Text.Html);
                // Create a LinkedResource object for each embedded image
                LinkedResource pic1 = new LinkedResource("pic.jpg", MediaTypeNames.Image.Jpeg);
                pic1.ContentId = "Pic1";
                avHtml.LinkedResources.Add(pic1);
                // Create an alternate view for unsupported clients
                string textBody = "You must use an e-mail client that supports HTML messages";
                AlternateView avText = AlternateView.CreateAlternateViewFromString
                  (textBody, null, MediaTypeNames.Text.Plain);
                m.AlternateViews.Add(avHtml);
                m.AlternateViews.Add(avText);
                // Send the message
                SmtpClient client = new SmtpClient("smtp.jb51.net");
                client.Send(m);
                */


                Msg = "Sent mail OK";
                blResult = true;
            }
            catch (SmtpException ex)
            {
                Msg = (strPerson != "" ? strPerson + " failed - " : "") + ex.Message + (ex.InnerException != null ? " " + (ex.InnerException.ToString().Length > 100 ? ex.InnerException.ToString().Substring(0, 100) + "..." : ex.InnerException.ToString()) : "");
                blResult = false;
            }
            catch (Exception ex)
            {
                Msg = (strPerson != "" ? strPerson + " failed - " : "") + ex.Message + (ex.InnerException != null ? " " + (ex.InnerException.ToString().Length > 100 ? ex.InnerException.ToString().Substring(0, 100) + "..." : ex.InnerException.ToString()) : "");
                blResult = false;
            }
            finally
            {
                foreach (Attachment item in mail.Attachments)
                {
                    item.Dispose();
                }
            }
            return blResult;
        }
    }
}
