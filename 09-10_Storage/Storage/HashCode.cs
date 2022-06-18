using System;
using System.Security.Cryptography;
using System.Text;

namespace Storage
{
    public static class HashCode
    {
        /// <summary>
        /// Расчет SHA256 хешкода пароля + соль.
        /// </summary>
        /// <param name="password"></param>
        /// <param name="salt"></param>
        /// <returns></returns>
        public static string GetHash(string password, string salt)
        {
            byte[] data = Encoding.Default.GetBytes(password + salt);
            var result = new SHA256Managed().ComputeHash(data);
            return BitConverter.ToString(result).Replace("-", "").ToLower();
        }
    }
}
