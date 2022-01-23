//<author>Javier López Malone</author>
//<date>25/11/2020 02:48:10</date>

using System.Security.Cryptography;

namespace malone.Core.Commons.Security
{
    /// <summary>
    /// Defines the <see cref="HashServiceFactory" />.
    /// </summary>
    public static class HashServiceFactory
    {
        /// <summary>
        /// ''' Crear una instancia de HashService. Si no se especifica un algoritmo de Hashing implementa Sha512.
        /// </summary>
        /// <param name="algorithm">The algorithm<see cref="HashAlgorithm"/>.</param>
        /// <returns>The <see cref="HashService"/>.</returns>
        public static HashService Create(HashAlgorithm algorithm = null)
        {
            if (algorithm == null)
                return HashService.CreateSHA512();
            else
                return new HashService(algorithm);
        }
    }
}
