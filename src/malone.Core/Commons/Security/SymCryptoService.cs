//<author>Javier López Malone</author>
//<date>25/11/2020 02:48:11</date>

using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace malone.Core.Commons.Security
{
    /// <summary>
    /// Defines the <see cref="CryptoService" />.
    /// </summary>
    public class CryptoService : IDisposable
    {
        /// <summary>
        /// Defines the KEY_64.
        /// </summary>
        private const string KEY_64 = "F)H@McQf";

        /// <summary>
        /// Defines the KEY_128.
        /// </summary>
        private const string KEY_128 = "nZq4t7w!z%C*F-Ja";

        /// <summary>
        /// Defines the KEY_192.
        /// </summary>
        private const string KEY_192 = "Zr4u7x!A%C*F-aNJ7C*7%u5o";

        /// <summary>
        /// Defines the KEY_256.
        /// </summary>
        private const string KEY_256 = "jWnZr4u7x!A%D*G-KaPdRgUkXp2s5v8y";

        /// <summary>
        /// Defines the Plus.
        /// </summary>
        private const char Plus = '+';

        /// <summary>
        /// Defines the Minus.
        /// </summary>
        private const char Minus = '-';

        /// <summary>
        /// Defines the Slash.
        /// </summary>
        private const char Slash = '/';

        /// <summary>
        /// Defines the Underscore.
        /// </summary>
        private const char Underscore = '_';

        /// <summary>
        /// Defines the EqualSign.
        /// </summary>
        private const char EqualSign = '=';

        /// <summary>
        /// Defines the Pipe.
        /// </summary>
        private const char Pipe = '|';

        /// <summary>
        /// Defines the _mapper.
        /// </summary>
        private readonly Dictionary<char, char> _mapper = new Dictionary<char, char>()
    {
        {
            Plus,
            Minus
        },
        {
            Slash,
            Underscore
        },
        {
            EqualSign,
            Pipe
        }
    };

        /// <summary>
        /// Initializes a new instance of the <see cref="CryptoService"/> class.
        /// </summary>
        /// <param name="algorithm">The algorithm<see cref="SymmetricAlgorithm"/>.</param>
        public CryptoService(SymmetricAlgorithm algorithm)
        {
            CryptoAlgorithm = algorithm;
            Encoder = Encoding.UTF8;

            if (algorithm.GetType() == typeof(AesCryptoServiceProvider))
            {
                CryptoAlgorithm.Key = Key256;
                CryptoAlgorithm.IV = Key128;
            }
            else if (algorithm.GetType() == typeof(TripleDESCryptoServiceProvider))
            {
                CryptoAlgorithm.Key = Key192;
                CryptoAlgorithm.IV = Key64;
            }
            else
            {
                CryptoAlgorithm.Key = Key128;
                CryptoAlgorithm.IV = Key64;
            }
        }

        /// <summary>
        /// Gets or sets the CryptoAlgorithm.
        /// </summary>
        protected SymmetricAlgorithm CryptoAlgorithm { get; set; }

        /// <summary>
        /// Gets or sets the Encoder.
        /// </summary>
        public Encoding Encoder { get; set; }

        /// <summary>
        /// Gets the Key64.
        /// </summary>
        public byte[] Key64
        {
            get
            {
                return Encoder.GetBytes(KEY_64);
            }
        }

        /// <summary>
        /// Gets the Key128.
        /// </summary>
        public byte[] Key128
        {
            get
            {
                return Encoder.GetBytes(KEY_128);
            }
        }

        /// <summary>
        /// Gets the Key192.
        /// </summary>
        public byte[] Key192
        {
            get
            {
                return Encoder.GetBytes(KEY_192);
            }
        }

        /// <summary>
        /// Gets the Key256.
        /// </summary>
        public byte[] Key256
        {
            get
            {
                return Encoder.GetBytes(KEY_256);
            }
        }

        /// <summary>
        /// The Encrypt.
        /// </summary>
        /// <param name="plainText">The plainText<see cref="string"/>.</param>
        /// <param name="escapedOutput">The escapedOutput<see cref="bool"/>.</param>
        /// <returns>The <see cref="string"/>.</returns>
        public string Encrypt(string plainText, bool escapedOutput = true)
        {
            string result = null;
            byte[] encrypted;
            ICryptoTransform encryptor = CryptoAlgorithm.CreateEncryptor(CryptoAlgorithm.Key, CryptoAlgorithm.IV);

            using (MemoryStream ms = new MemoryStream())
            {
                using (CryptoStream cs = new CryptoStream(ms, encryptor, CryptoStreamMode.Write))
                {
                    using (StreamWriter sw = new StreamWriter(cs))
                    {
                        sw.Write(plainText);
                    }

                    encrypted = ms.ToArray();
                }
            }

            string base64EncryptedValue = Convert.ToBase64String(encrypted);

            foreach (KeyValuePair<char, char> pair in _mapper)
                base64EncryptedValue = base64EncryptedValue.Replace(pair.Key, pair.Value);

            if (escapedOutput)
                result = Uri.EscapeDataString(base64EncryptedValue);
            else
                result = base64EncryptedValue;

            return result;
        }

        /// <summary>
        /// The Decrypt.
        /// </summary>
        /// <param name="base64CipherText">The base64CipherText<see cref="string"/>.</param>
        /// <param name="escapedInput">The escapedInput<see cref="bool"/>.</param>
        /// <returns>The <see cref="string"/>.</returns>
        public string Decrypt(string base64CipherText, bool escapedInput = true)
        {
            string plaintext = null;
            string base64EncryptedValue = null;


            foreach (KeyValuePair<char, char> pair in _mapper)
                base64CipherText = base64CipherText.Replace(pair.Value, pair.Key);

            if (escapedInput)
                base64EncryptedValue = Uri.UnescapeDataString(base64CipherText);
            else
                base64EncryptedValue = base64CipherText;

            byte[] cipherBytes = Convert.FromBase64String(base64EncryptedValue);
            ICryptoTransform decryptor = CryptoAlgorithm.CreateDecryptor(CryptoAlgorithm.Key, CryptoAlgorithm.IV);

            using (MemoryStream ms = new MemoryStream(cipherBytes))
            {
                using (CryptoStream cs = new CryptoStream(ms, decryptor, CryptoStreamMode.Read))
                {
                    using (StreamReader reader = new StreamReader(cs))
                    {
                        plaintext = reader.ReadToEnd();
                    }
                }
            }

            return plaintext;
        }

        /// <summary>
        /// The EncryptFile.
        /// </summary>
        /// <param name="inName">The inName<see cref="string"/>.</param>
        /// <param name="outName">The outName<see cref="string"/>.</param>
        public void EncryptFile(string inName, string outName)
        {
            byte[] salt = Key256;
            FileStream fsCrypt = new FileStream(inName + ".aes", FileMode.Create);
            byte[] passwordBytes = Key128;
            RijndaelManaged AES = new RijndaelManaged();
            AES.KeySize = 256;
            AES.BlockSize = 128;
            AES.Padding = PaddingMode.PKCS7;
            Rfc2898DeriveBytes key = new Rfc2898DeriveBytes(passwordBytes, salt, 50000);
            AES.Key = key.GetBytes(System.Convert.ToInt32(AES.KeySize / (double)8));
            AES.IV = key.GetBytes(System.Convert.ToInt32(AES.BlockSize / (double)8));
            AES.Mode = CipherMode.CFB;
            fsCrypt.Write(salt, 0, salt.Length);
            CryptoStream cs = new CryptoStream(fsCrypt, AES.CreateEncryptor(), CryptoStreamMode.Write);
            FileStream fsIn = new FileStream(inName, FileMode.Open);
            byte[] buffer = new byte[1048576];
            int read;

            try
            {
                while (System.Convert.ToInt32((read = fsIn.Read(buffer, 0, buffer.Length))) > 0)
                    cs.Write(buffer, 0, read);

                fsIn.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }
            finally
            {
                cs.Close();
                fsCrypt.Close();
            }
        }

        /// <summary>
        /// The DecryptFile.
        /// </summary>
        /// <param name="inName">The inName<see cref="string"/>.</param>
        /// <param name="outName">The outName<see cref="string"/>.</param>
        public void DecryptFile(string inName, string outName)
        {
            byte[] passwordBytes = Key128;
            byte[] salt = new byte[32];
            FileStream fsCrypt = new FileStream(inName, FileMode.Open);
            fsCrypt.Read(salt, 0, salt.Length);
            RijndaelManaged AES = new RijndaelManaged();
            AES.KeySize = 256;
            AES.BlockSize = 128;
            Rfc2898DeriveBytes key = new Rfc2898DeriveBytes(passwordBytes, salt, 50000);
            AES.Key = key.GetBytes(System.Convert.ToInt32(AES.KeySize / (double)8));
            AES.IV = key.GetBytes(System.Convert.ToInt32(AES.BlockSize / (double)8));
            AES.Padding = PaddingMode.PKCS7;
            AES.Mode = CipherMode.CFB;
            CryptoStream cs = new CryptoStream(fsCrypt, AES.CreateDecryptor(), CryptoStreamMode.Read);
            FileStream fsOut = new FileStream(outName, FileMode.Create);
            int read;
            byte[] buffer = new byte[1048576];

            try
            {
                while (System.Convert.ToInt32((read = cs.Read(buffer, 0, buffer.Length))) > 0)
                    fsOut.Write(buffer, 0, read);
            }
            catch (CryptographicException ex_CryptographicException)
            {
                Console.WriteLine("CryptographicException error: " + ex_CryptographicException.Message);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }

            try
            {
                cs.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error by closing CryptoStream: " + ex.Message);
            }
            finally
            {
                fsOut.Close();
                fsCrypt.Close();
            }
        }

        /// <summary>
        /// The Create3DES.
        /// </summary>
        /// <returns>The <see cref="CryptoService"/>.</returns>
        public static CryptoService Create3DES()
        {
            return new CryptoService(new TripleDESCryptoServiceProvider());
        }

        /// <summary>
        /// The CreateAES.
        /// </summary>
        /// <returns>The <see cref="CryptoService"/>.</returns>
        public static CryptoService CreateAES()
        {
            return new CryptoService(new AesCryptoServiceProvider());
        }

        /// <summary>
        /// Defines the disposedValue.
        /// </summary>
        private bool disposedValue;

        /// <summary>
        /// The Dispose.
        /// </summary>
        /// <param name="disposing">The disposing<see cref="bool"/>.</param>
        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                    CryptoAlgorithm.Dispose();
            }

            disposedValue = true;
        }

        /// <summary>
        /// The Dispose.
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
        }
    }
}
