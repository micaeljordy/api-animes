using System.Security.Cryptography;
using System.Text;
using Animes.Application.Interfaces;

namespace Animes.Application.Services
{
    public class EncryptionService : IEncryptionService
    {
        public string Encrypt(string texto)
        {
            using (MD5 md5 = MD5.Create())
            {
                byte[] inputBytes = Encoding.UTF8.GetBytes(texto);
                byte[] hashBytes = md5.ComputeHash(inputBytes);

                StringBuilder sb = new StringBuilder();
                for (int i = 0; i < hashBytes.Length; i++)
                {
                    sb.Append(hashBytes[i].ToString("x2"));
                }

                return sb.ToString();
            }
        }
    }
}