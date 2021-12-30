using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace PlaceToPay.Integrations.WebForms.Tests
{
    public static class util
    {
        public static string HashSha256(string texto)
        {
            var sha256 = new System.Security.Cryptography.SHA256Managed();
            var plaintextBytes = Encoding.UTF8.GetBytes(texto);
            var hashBytes = sha256.ComputeHash(plaintextBytes);

            var sb = new StringBuilder();
            foreach (var hashByte in hashBytes)
            {
                sb.AppendFormat("{0:x2}", hashByte);
            }

            var hashString = sb.ToString();
            return hashString;
        }

        public static string Base64Encode(string plainText)
        {
            var plainTextBytes = System.Text.Encoding.UTF8.GetBytes(plainText);
            return System.Convert.ToBase64String(plainTextBytes);
        }

    }


}