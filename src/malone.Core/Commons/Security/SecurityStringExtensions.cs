using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;

namespace malone.Core.Commons.Security
{
    public static class SecurityExtensionMethods
    {


        /// <summary>
        ///     ''' Aplica un algoritmo de Hashing al texto de entrada. Si no se especifica un algoritmo se utiliza SHA512.
        ///     ''' </summary>
        ///     ''' <param name="value"></param>
        ///     ''' <returns></returns>
        public static string Hash(this string value, HashAlgorithm algorithm = null, bool escapedOutput = true)
        {
            string result = null;

            HashService hashService = HashServiceFactory.Create(algorithm);
            result = hashService.ComputeHash(value);

            return result;
        }



        /// <summary>
        ///     ''' Aplica un algoritmo de Hashing al texto de entrada. Si no se especifica un algoritmo se utiliza SHA512.
        ///     ''' </summary>
        ///     ''' <param name="value"></param>
        ///     ''' <returns></returns>
        public static string Encrypt(this string value, SymmetricAlgorithm algorithm = null, bool escapedOutput = true)
        {
            string result = null;

            CryptoService cryptoService = CryptoServiceFactory.Create(algorithm);
            result = cryptoService.Encrypt(value, escapedOutput);

            return result;
        }

        /// <summary>
        ///     ''' Aplica un algoritmo de Hashing al texto de entrada. Si no se especifica un algoritmo se utiliza SHA512.
        ///     ''' </summary>
        ///     ''' <param name="value"></param>
        ///     ''' <returns></returns>
        public static string Decrypt(this string value, SymmetricAlgorithm algorithm = null, bool escapedInput = true)
        {
            string result = null;

            CryptoService cryptoService = CryptoServiceFactory.Create(algorithm);
            result = cryptoService.Decrypt(value, escapedInput);

            return result;
        }
    }

}
