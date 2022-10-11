//<author>Javier López Malone</author>
//<date>25/11/2020 02:48:10</date>

using System.Security.Cryptography;

namespace malone.Core.Crypto
{
	public static class HashServiceFactory
	{
		public static HashService Create(HashAlgorithm algorithm = null)
		{
			if (algorithm == null)
			{
				return HashService.CreateSHA512();
			}
			else
			{
				return new HashService(algorithm);
			}
		}
	}
}
