using System;
using System.IO;
using System.Text;
using System.Security.Cryptography;

namespace malone.Core.Commons.Security
{
    public static class HashServiceFactory
    {

        /// <summary>
        ///     ''' Crear una instancia de HashService. Si no se especifica un algoritmo de Hashing implementa Sha512.
        ///     ''' </summary>
        ///     ''' <param name="algorithm"></param>
        ///     ''' <returns></returns>
        public static HashService Create(HashAlgorithm algorithm = null)
        {
            if (algorithm == null)
                return HashService.CreateSHA512();
            else
                return new HashService(algorithm);
        }
    }


}
