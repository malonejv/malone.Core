//<author>Javier López Malone</author>
//<date>25/11/2020 02:48:12</date>

using System.Security.Cryptography;

namespace malone.Core.Commons.Security
{
    /// <summary>
    /// Defines the <see cref="CryptoServiceFactory" />.
    /// </summary>
    public class CryptoServiceFactory
    {
        /// <summary>
        /// The Create.
        /// </summary>
        /// <param name="algorithm">The algorithm<see cref="SymmetricAlgorithm"/>.</param>
        /// <returns>The <see cref="CryptoService"/>.</returns>
        public static CryptoService Create(SymmetricAlgorithm algorithm = null)
        {
            if (algorithm == null)
                return CryptoService.CreateAES();
            else
                return new CryptoService(algorithm);
        }
    }
}
