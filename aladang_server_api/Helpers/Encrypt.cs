using System;
using System.Text;
using System.IO;
using System.Security.Cryptography;

namespace aladang_server_api.Helpers
{
	public class Encrypt
	{
        public static string EncriptSha256PassWord(string Password)
        {
            try
            {
                #pragma warning disable SYSLIB0021 // Type or member is obsolete
                var crypt = new SHA256Managed();
                #pragma warning restore SYSLIB0021 // Type or member is obsolete
                var hash = new System.Text.StringBuilder();
                byte[] crypto = crypt.ComputeHash(Encoding.UTF8.GetBytes(Password));
                foreach (byte theByte in crypto)
                {
                    hash.Append(theByte.ToString("x2"));
                }

                return hash.ToString();
                //SHA256Managed hasher = new SHA256Managed();

                //byte[] pwdBytes = new UTF8Encoding().GetBytes(Password);
                //byte[] keyBytes = hasher.ComputeHash(pwdBytes);

                //hasher.Dispose();
                //return Convert.ToBase64String(keyBytes);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
        }
    }
}

