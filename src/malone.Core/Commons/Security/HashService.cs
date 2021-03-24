//<author>Javier López Malone</author>
//<date>25/11/2020 02:48:10</date>

using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace malone.Core.Commons.Security
{
    /// <summary>
    /// Defines the <see cref="HashService" />.
    /// </summary>
    public class HashService : IDisposable
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
        /// Initializes a new instance of the <see cref="HashService"/> class.
        /// </summary>
        /// <param name="algorithm">The algorithm<see cref="HashAlgorithm"/>.</param>
        public HashService(HashAlgorithm algorithm)
        {
            HashAlgorithm = algorithm;
            Encoder = Encoding.UTF8;
        }

        /// <summary>
        /// Gets or sets the HashAlgorithm.
        /// </summary>
        protected HashAlgorithm HashAlgorithm { get; set; }

        /// <summary>
        /// Gets or sets the Encoder.
        /// </summary>
        public Encoding Encoder { get; set; }

        /// <summary>
        /// Gets the HashSize.
        /// </summary>
        public int HashSize
        {
            get
            {
                return HashAlgorithm.HashSize;
            }
        }

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
        /// The ComputeHash.
        /// </summary>
        /// <param name="input">The input<see cref="string"/>.</param>
        /// <param name="escapedOutput">The escapedOutput<see cref="bool"/>.</param>
        /// <returns>The <see cref="string"/>.</returns>
        public string ComputeHash(string input, bool escapedOutput = true)
        {
            string result = null;

            string value = input + KEY_128;
            byte[] bytes = Encoding.UTF8.GetBytes(value);
            byte[] hashBytes = ComputeHash(bytes);
            string base64HashedValue = Convert.ToBase64String(hashBytes);

            if (escapedOutput)
                result = Uri.EscapeDataString(base64HashedValue);
            else
                result = base64HashedValue;

            return result;
        }

        /// <summary>
        /// The ComputeHash.
        /// </summary>
        /// <param name="buffer">The buffer<see cref="byte[]"/>.</param>
        /// <returns>The <see cref="byte[]"/>.</returns>
        public byte[] ComputeHash(byte[] buffer)
        {
            return HashAlgorithm.ComputeHash(buffer);
        }

        /// <summary>
        /// The ComputeHash.
        /// </summary>
        /// <param name="buffer">The buffer<see cref="byte[]"/>.</param>
        /// <param name="offset">The offset<see cref="int"/>.</param>
        /// <param name="count">The count<see cref="int"/>.</param>
        /// <returns>The <see cref="byte[]"/>.</returns>
        public byte[] ComputeHash(byte[] buffer, int offset, int count)
        {
            return HashAlgorithm.ComputeHash(buffer, offset, count);
        }

        /// <summary>
        /// The ComputeHash.
        /// </summary>
        /// <param name="inputStream">The inputStream<see cref="Stream"/>.</param>
        /// <returns>The <see cref="byte[]"/>.</returns>
        public byte[] ComputeHash(Stream inputStream)
        {
            return HashAlgorithm.ComputeHash(inputStream);
        }

        /// <summary>
        /// The CreateMd5.
        /// </summary>
        /// <returns>The <see cref="HashService"/>.</returns>
        public static HashService CreateMd5()
        {
            return new HashService(new MD5CryptoServiceProvider());
        }

        /// <summary>
        /// The CreateRIPEMD160.
        /// </summary>
        /// <returns>The <see cref="HashService"/>.</returns>
        public static HashService CreateRIPEMD160()
        {
            return new HashService(new RIPEMD160Managed());
        }

        /// <summary>
        /// The CreateSHA256.
        /// </summary>
        /// <returns>The <see cref="HashService"/>.</returns>
        public static HashService CreateSHA256()
        {
            return new HashService(new SHA256Managed());
        }

        /// <summary>
        /// The CreateSHA384.
        /// </summary>
        /// <returns>The <see cref="HashService"/>.</returns>
        public static HashService CreateSHA384()
        {
            return new HashService(new SHA384Managed());
        }

        /// <summary>
        /// The CreateSHA512.
        /// </summary>
        /// <returns>The <see cref="HashService"/>.</returns>
        public static HashService CreateSHA512()
        {
            return new HashService(new SHA512Managed());
        }

        /// <summary>
        /// The CreateSHA1.
        /// </summary>
        /// <returns>The <see cref="HashService"/>.</returns>
        public static HashService CreateSHA1()
        {
            return new HashService(new SHA1Managed());
        }

        /// <summary>
        /// Defines the disposedValue.
        /// </summary>
        private bool disposedValue;// To detect redundant calls

        /// <summary>
        /// The Dispose.
        /// </summary>
        /// <param name="disposing">The disposing<see cref="bool"/>.</param>
        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                    HashAlgorithm.Dispose();
            }
            disposedValue = true;
        }

        /// <summary>
        /// The Dispose.
        /// </summary>
        public void Dispose()
        {
            // Do not change this code.  Put cleanup code in Dispose(disposing As Boolean) above.
            Dispose(true);
        }
    }
}
