//<author>Javier López Malone</author>
//<date>25/11/2020 02:48:11</date>

using System.Security.Cryptography;

namespace malone.Core.Commons.Security
{
    /// <summary>
    /// Defines the <see cref="SecurityExtensionMethods" />.
    /// </summary>
    public static class SecurityExtensionMethods
    {
        /// <summary>
        /// ''' Aplica un algoritmo de Hashing al texto de entrada. Si no se especifica un algoritmo se utiliza SHA512.
        /// </summary>
        /// <param name="value">The value<see cref="string"/>.</param>
        /// <param name="algorithm">The algorithm<see cref="HashAlgorithm"/>.</param>
        /// <param name="escapedOutput">The escapedOutput<see cref="bool"/>.</param>
        /// <returns>The <see cref="string"/>.</returns>
        public static string Hash(this string value, HashAlgorithm algorithm = null, bool escapedOutput = true)
        {
            string result = null;

            HashService hashService = HashServiceFactory.Create(algorithm);
            result = hashService.ComputeHash(value);

            return result;
        }

        /// <summary>
        /// ''' Aplica un algoritmo de Hashing al texto de entrada. Si no se especifica un algoritmo se utiliza SHA512.
        /// </summary>
        /// <param name="value">The value<see cref="string"/>.</param>
        /// <param name="algorithm">The algorithm<see cref="SymmetricAlgorithm"/>.</param>
        /// <param name="escapedOutput">The escapedOutput<see cref="bool"/>.</param>
        /// <returns>The <see cref="string"/>.</returns>
        public static string Encrypt(this string value, SymmetricAlgorithm algorithm = null, bool escapedOutput = true)
        {
            string result = null;

            CryptoService cryptoService = CryptoServiceFactory.Create(algorithm);
            result = cryptoService.Encrypt(value, escapedOutput);

            return result;
        }

        /// <summary>
        /// ''' Aplica un algoritmo de Hashing al texto de entrada. Si no se especifica un algoritmo se utiliza SHA512.
        /// </summary>
        /// <param name="value">The value<see cref="string"/>.</param>
        /// <param name="algorithm">The algorithm<see cref="SymmetricAlgorithm"/>.</param>
        /// <param name="escapedInput">The escapedInput<see cref="bool"/>.</param>
        /// <returns>The <see cref="string"/>.</returns>
        public static string Decrypt(this string value, SymmetricAlgorithm algorithm = null, bool escapedInput = true)
        {
            string result = null;

            CryptoService cryptoService = CryptoServiceFactory.Create(algorithm);
            result = cryptoService.Decrypt(value, escapedInput);

            return result;
        }
    }
}
