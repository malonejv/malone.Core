//<author>Javier López Malone</author>
//<date>25/11/2020 02:48:10</date>

using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace malone.Core.Crypto
{
	public class HashService : IDisposable
	{
		private const string KEY_64 = "F)H@McQf";

		private const string KEY_128 = "nZq4t7w!z%C*F-Ja";

		private const string KEY_192 = "Zr4u7x!A%C*F-aNJ7C*7%u5o";

		private const string KEY_256 = "jWnZr4u7x!A%D*G-KaPdRgUkXp2s5v8y";

		public HashService(HashAlgorithm algorithm)
		{
			HashAlgorithm = algorithm;
			Encoder = Encoding.UTF8;
		}

		protected HashAlgorithm HashAlgorithm { get; set; }

		public Encoding Encoder { get; set; }

		public int HashSize
		{
			get
			{
				return HashAlgorithm.HashSize;
			}
		}

		public byte[] Key64
		{
			get
			{
				return Encoder.GetBytes(KEY_64);
			}
		}

		public byte[] Key128
		{
			get
			{
				return Encoder.GetBytes(KEY_128);
			}
		}

		public byte[] Key192
		{
			get
			{
				return Encoder.GetBytes(KEY_192);
			}
		}

		public byte[] Key256
		{
			get
			{
				return Encoder.GetBytes(KEY_256);
			}
		}

		public string ComputeHash(string input, bool escapedOutput = true)
		{
			string result = null;

			string value = input + KEY_128;
			byte[] bytes = Encoding.UTF8.GetBytes(value);
			byte[] hashBytes = ComputeHash(bytes);
			string base64HashedValue = Convert.ToBase64String(hashBytes);

			if (escapedOutput)
			{
				result = Uri.EscapeDataString(base64HashedValue);
			}
			else
			{
				result = base64HashedValue;
			}

			return result;
		}

		public byte[] ComputeHash(byte[] buffer)
		{
			return HashAlgorithm.ComputeHash(buffer);
		}

		public byte[] ComputeHash(byte[] buffer, int offset, int count)
		{
			return HashAlgorithm.ComputeHash(buffer, offset, count);
		}

		public byte[] ComputeHash(Stream inputStream)
		{
			return HashAlgorithm.ComputeHash(inputStream);
		}

		public static HashService CreateMd5()
		{
			return new HashService(new MD5CryptoServiceProvider());
		}

		public static HashService CreateRIPEMD160()
		{
			return new HashService(new RIPEMD160Managed());
		}

		public static HashService CreateSHA256()
		{
			return new HashService(new SHA256Managed());
		}

		public static HashService CreateSHA384()
		{
			return new HashService(new SHA384Managed());
		}

		public static HashService CreateSHA512()
		{
			return new HashService(new SHA512Managed());
		}

		public static HashService CreateSHA1()
		{
			return new HashService(new SHA1Managed());
		}

		private bool disposedValue;// To detect redundant calls

		protected virtual void Dispose(bool disposing)
		{
			if (!disposedValue)
			{
				if (disposing)
				{
					HashAlgorithm.Dispose();
				}
			}
			disposedValue = true;
		}

		public void Dispose()
		{
			// Do not change this code.  Put cleanup code in Dispose(disposing As Boolean) above.
			Dispose(true);
		}
	}
}
