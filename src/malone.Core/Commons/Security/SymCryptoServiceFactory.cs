using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Security.Cryptography;

namespace malone.Core.Commons.Security
{
    public class CryptoServiceFactory
    {
        public static CryptoService Create(SymmetricAlgorithm algorithm = null)
        {
            if (algorithm == null)
                return CryptoService.CreateAES();
            else
                return new CryptoService(algorithm);
        }
    }

}
