//<author>Javier López Malone</author>
//<date>25/11/2020 02:48:12</date>

using System.Security.Cryptography;

namespace malone.Core.Commons.Security
{
    public class CryptoServiceFactory
    {
        public static CryptoService Create(SymmetricAlgorithm algorithm = null)
        {
            if (algorithm == null)
            {
                return CryptoService.CreateAES();
            }
            else
            {
                return new CryptoService(algorithm);
            }
        }
    }
}
