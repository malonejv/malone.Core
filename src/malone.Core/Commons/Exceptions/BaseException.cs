using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace malone.Core.Commons.Exceptions
{
    //TODO: MODIFICAR
    //Rethrow y Should log no son necesarios en el constructor. Vamos a setear por configuracion.
    //Por el momento dejo harcodeado
    public abstract class BaseException : Exception
    {
        public const string SUPPORT_ID = "SupportId";
        public const string ERROR_CODE = "ErrorCode";

        public BaseException() : base()
        {
        }

        public BaseException(string message) : base(message)
        {
        }

        public BaseException(string message, Exception innerException) : base(message, innerException)
        {
        }

        //    //private static string BaseExceptionMessage()
        //    //{
        //    //    //var contacto = "MAIL";

        //    //    //var mensaje = string.Format(ErrorCode.ToString(), contacto);

        //    //    //return mensaje;
        //    //}
        //}
    }
}
