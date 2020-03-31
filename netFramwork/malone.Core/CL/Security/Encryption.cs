using malone.Core.CL.Helpers.Extensions;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Security.Cryptography;
using System.Text;

namespace malone.Core.CL.Security
{
    public static class Encryption
    {

        private const string Salt128Value = "kljsdkkdlo4454GG";
        private const string Salt256Value = "kljsdkkdlo4454GG00155sajuklmbkdl";
        private enum Salts
        {
            [Description(Salt128Value)]
            Salt128,
            [Description(Salt256Value)]
            Salt256
        }

        private const string Plus = "+";
        private const string Minus = "-";
        private const string Slash = "/";
        private const string Underscore = "_";
        private const string EqualSign = "=";
        private const string Pipe = "|";
        private const string PuntoComa = ";";
        private const string Ampersand = "&";
        private static readonly IDictionary<string, string> _mapper;


        static Encryption()
        {
            _mapper = new Dictionary<string, string> { { Plus, Minus }, { Slash, Underscore }, { EqualSign, Pipe }, { PuntoComa, Ampersand } };
        }

        private static byte[] GetKey(Salts salt)
        {
            var keyValue = salt.GetDescription();
            var bytesKey = Encoding.UTF8.GetBytes(keyValue);

            return bytesKey;
        }

        public static string HashMd5(this string input)
        {
            using (MD5 md5 = MD5.Create())
            {
                byte[] inputBytes = System.Text.Encoding.ASCII.GetBytes(input);
                byte[] hashBytes = md5.ComputeHash(inputBytes);

                return hashBytes.ToHexString();
            }
        }

        public static string HashSha256(this string input)
        {
            using (HMAC hmac = HMAC.Create())
            {
                byte[] inputBytes = System.Text.Encoding.ASCII.GetBytes(input);
                byte[] hashBytes = hmac.ComputeHash(inputBytes);

                // Convert the byte array to hexadecimal string
                StringBuilder sb = new StringBuilder();
                for (int i = 0; i < hashBytes.Length; i++)
                {
                    sb.Append(hashBytes[i].ToString("X2"));
                }
                return sb.ToString();
            }
        }

        public static string Encrypt(this string value)
        {
            var key = GetKey(Salts.Salt128);

            using (TripleDESCryptoServiceProvider des = new TripleDESCryptoServiceProvider())
            {
                des.Key = key;
                des.Mode = CipherMode.ECB;
                des.Padding = PaddingMode.PKCS7;
                ICryptoTransform cTransform = des.CreateEncryptor();

                var bytesValue = Encoding.UTF8.GetBytes(value);
                var hashedValue = cTransform.TransformFinalBlock(bytesValue, 0, bytesValue.Length);
                var base64Hash = Convert.ToBase64String(hashedValue);
                //var encodedValue = EncodeBase64Url(base64Hash);

                return base64Hash;
            }
        }

        public static string Decrypt(this string value)
        {
            var key = GetKey(Salts.Salt128);

            using (TripleDESCryptoServiceProvider des = new TripleDESCryptoServiceProvider())
            {
                des.Key = key;
                des.Mode = CipherMode.ECB;
                des.Padding = PaddingMode.PKCS7;

                //var base64Hash = DecodeBase64Url(value);
                var bytesValue = Convert.FromBase64String(value);
                ICryptoTransform cTransform = des.CreateDecryptor();
                var bytesDecrypted = cTransform.TransformFinalBlock(bytesValue, 0, bytesValue.Length);
                var Description = Encoding.UTF8.GetString(bytesDecrypted);

                var encryptConfirm = Encrypt(Description);


                if (!value.Equals(encryptConfirm))
                {
                    return null;
                }

                return Description;
            }
        }

        public static string EncodeBase64Url(this string base64Str)
        {
            if (string.IsNullOrEmpty(base64Str))
                return base64Str;

            foreach (var pair in _mapper)
            {
                base64Str = base64Str.Replace(pair.Key, pair.Value);
            }

            return base64Str;
        }

        public static string DecodeBase64Url(this string safe64Url)
        {

            if (string.IsNullOrEmpty(safe64Url))
                return safe64Url;

            foreach (var pair in _mapper)
            {
                safe64Url = safe64Url.Replace(pair.Value, pair.Key);
            }

            return safe64Url;
        }

    }
}
