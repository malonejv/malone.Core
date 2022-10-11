//<author>Javier López Malone</author>
//<date>25/11/2020 02:48:11</date>

using System.Security.Cryptography;

namespace malone.Core.Crypto
{
	public static class SecurityExtensionMethods
	{
		public static string Hash(this string value, HashAlgorithm algorithm = null, bool escapedOutput = true)
		{
			string result = null;

			HashService hashService = HashServiceFactory.Create(algorithm);
			result = hashService.ComputeHash(value);

			return result;
		}

		public static string Encrypt(this string value, SymmetricAlgorithm algorithm = null, bool escapedOutput = true)
		{
			string result = null;

			CryptoService cryptoService = CryptoServiceFactory.Create(algorithm);
			result = cryptoService.Encrypt(value, escapedOutput);

			return result;
		}

		public static string Decrypt(this string value, SymmetricAlgorithm algorithm = null, bool escapedInput = true)
		{
			string result = null;

			CryptoService cryptoService = CryptoServiceFactory.Create(algorithm);
			result = cryptoService.Decrypt(value, escapedInput);

			return result;
		}
	}
}
