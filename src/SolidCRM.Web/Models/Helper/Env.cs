using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using System; 
using System.IO; 
using System.Security.Cryptography;
using System.Text; 
using System.Security.Claims;
using System.Collections.Generic;
using System.Linq;

namespace SolidCRM.Web
{
    public static class Env
    {

        public static string GetUserInfo(string value, IEnumerable<Claim> Claim)
        { 
            string ReturnVal = string.Empty;
            
            foreach (var item in Claim)
            {
                if (value == "Id" && item.Type == ClaimTypes.NameIdentifier)
                    ReturnVal = item.Value;
                if (value == "UserName" && item.Type == ClaimTypes.Name)
                    ReturnVal = item.Value;
                if (value == "Role" && item.Type == ClaimTypes.Role)
                    ReturnVal = item.Value;
                if (value== "RoleId" && item.Type == ClaimTypes.Rsa)
                    ReturnVal = item.Value;

                //if (value == "DateOfBirth" && item.ValueType == ClaimValueTypes.DateTime)
                //    ReturnVal = item.Value;

            }


            return ReturnVal;

        }

        public static HtmlString AppInfo(string value, IEnumerable<Claim> Claim)
        {
            string ReturnVal = string.Empty;

            foreach (var item in Claim)
            {
                if (item.Type == ClaimTypes.Webpage)
                {
                    try
                    {
                        string[] claimValue = item.Value.Split('#').Where(i => i.Split('=')[0] == value).ToArray();
                        ReturnVal = claimValue[0].Split('=')[1];
                    }
                    catch (Exception)
                    {
                        ReturnVal = "";
                    } 
                }
            }

            return new HtmlString(ReturnVal);
        }

        /// <summary>
        /// Decrypt Method used for Decrypt to Encrypted String. you may use this for password encryption and decryption or other string.
        /// </summary>
        /// <param name="cryptedString"></param>
        /// <returns></returns>
        public static string Decrypt(string cryptedString)
        { 
            byte[] bytes = ASCIIEncoding.ASCII.GetBytes("ZeroCool");
            if (String.IsNullOrEmpty(cryptedString))
            {
                throw new ArgumentNullException("The string which needs to be decrypted can not be null.");
            }

            DESCryptoServiceProvider cryptoProvider = new DESCryptoServiceProvider();
            MemoryStream memoryStream = new MemoryStream(Convert.FromBase64String(cryptedString));
            CryptoStream cryptoStream = new CryptoStream(memoryStream, cryptoProvider.CreateDecryptor(bytes, bytes), CryptoStreamMode.Read);
            StreamReader reader = new StreamReader(cryptoStream);

            return reader.ReadToEnd();
        }

        /// <summary>
        /// Encrypt Method used for Encrypt to any String. you may use this for password encryption and decryption or other string.
        /// </summary>
        /// <param name="originalString"></param>
        /// <returns></returns>
        public static string Encrypt(string originalString)
        { 
            byte[] bytes = ASCIIEncoding.ASCII.GetBytes("ZeroCool");
            if (String.IsNullOrEmpty(originalString))
            {
                throw new ArgumentNullException("The string which needs to be encrypted can not be null.");
            }

            DESCryptoServiceProvider cryptoProvider = new DESCryptoServiceProvider();
            MemoryStream memoryStream = new MemoryStream();
            CryptoStream cryptoStream = new CryptoStream(memoryStream, cryptoProvider.CreateEncryptor(bytes, bytes), CryptoStreamMode.Write);

            StreamWriter writer = new StreamWriter(cryptoStream);
            writer.Write(originalString);
            writer.Flush();
            cryptoStream.FlushFinalBlock();
            writer.Flush();
            string output = Convert.ToBase64String(memoryStream.GetBuffer(), 0, (int)memoryStream.Length);
            //if (output.Contains('+'))
            //{
            //    output = output.Replace("+", "%2B");
            //}
            return output;
        }

        public static void SendEmail(string ToUser, string subject, string body)
        {
            var toAddress = new System.Net.Mail.MailAddress(ToUser, ToUser);

            try
            {
                System.Net.Mail.MailMessage emessage = new System.Net.Mail.MailMessage();

                string Username = "yougmail@gmail.com";
                string Password = "yourpassword";
                int port = 587;
                string SmptServer = "smtp.gmail.com";

                emessage.To.Add(toAddress);
                emessage.Subject = subject;
                emessage.From = new System.Net.Mail.MailAddress(Username);
                emessage.Body = body;
                emessage.IsBodyHtml = true;
                System.Net.Mail.SmtpClient sc = new System.Net.Mail.SmtpClient();
                var netCredential = new System.Net.NetworkCredential(Username, Password);
                sc.Host = SmptServer;
                sc.DeliveryMethod = System.Net.Mail.SmtpDeliveryMethod.Network;
                sc.UseDefaultCredentials = false;
                sc.Credentials = netCredential;
                sc.Port = Convert.ToInt32(port);
                sc.Send(emessage);

            }
            catch (Exception ex)
            {

            }
        }
  
    }
}

