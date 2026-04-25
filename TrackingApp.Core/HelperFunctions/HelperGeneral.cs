using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace TrackingApp.Core.HelperFunctions
{
    public static class HelperGeneral
    {
        private static char[] specialChars = { '*', '.', ',', '?', '_', '(', ')' };
        private static string _version = string.Concat(DateTime.Now.Month, "_", DateTime.Now.Year,"_" ,DateTime.Now.Day);

        //AES-256 için 32 byte (256 bit) anahtar gereklidir
        private static readonly string Key = "a1b2c3d4e5f6g7h8i9j0k1l2m3n4o5p6";

        public static bool IsSpecialCharsContains(this string input)
        {
            if (string.IsNullOrEmpty(input)) return false;

            return input.Any(x => specialChars.Contains(x));
        }

        public static char[] GetSpecialChars() => specialChars;


        public static string Encrypt(this string plainText)
        {
            using (Aes aesAlg = Aes.Create())
            {
                aesAlg.Key = Encoding.UTF8.GetBytes(Key);
                aesAlg.GenerateIV(); 

                var iv = aesAlg.IV;
                var encryptor = aesAlg.CreateEncryptor(aesAlg.Key, iv);

                using (var msEncrypt = new MemoryStream())
                {
                  
                    msEncrypt.Write(iv, 0, iv.Length);

                    using (var csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
                    using (var swEncrypt = new StreamWriter(csEncrypt))
                    {
                        swEncrypt.Write(plainText);
                    }

                    return Convert.ToBase64String(msEncrypt.ToArray());
                }
            }
        }

        public static string Decrypt(this string cipherText)
        {
            var fullCipher = Convert.FromBase64String(cipherText);

            using (Aes aesAlg = Aes.Create())
            {
                aesAlg.Key = Encoding.UTF8.GetBytes(Key);

                var iv = new byte[aesAlg.BlockSize / 8];
                var cipher = new byte[fullCipher.Length - iv.Length];

                Buffer.BlockCopy(fullCipher, 0, iv, 0, iv.Length);
                Buffer.BlockCopy(fullCipher, iv.Length, cipher, 0, cipher.Length);

                aesAlg.IV = iv;
                var decryptor = aesAlg.CreateDecryptor(aesAlg.Key, aesAlg.IV);

                using (var msDecrypt = new MemoryStream(cipher))
                using (var csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read))
                using (var srDecrypt = new StreamReader(csDecrypt))
                {
                    return srDecrypt.ReadToEnd();
                }
            }
        }

        public static string GetAPIVersion() => _version;
        public static string GetCookieName() => string.Concat("TrackingApp_",_version);

    }
}
