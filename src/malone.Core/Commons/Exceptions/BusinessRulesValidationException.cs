using malone.Core.Business.Components;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace malone.Core.Commons.Exceptions
{
    /// <summary>
    /// Encapsulates funcional validation exceptions in Business Layer
    /// </summary>
    public class BusinessRulesValidationException : Exception
    {
        public ValidationResultList Results { get; private set; }

        public bool HideErrorCodes { get; protected set; }

        public new string Message
        {
            get
            {
                StringBuilder msg = new StringBuilder();
                if (Results != null)
                {
                    foreach (var e in Results)
                    {
                        msg.AppendLine(string.Format("[{0}] - {1}", e.ErrorCode.ToUpper(), e.Message));
                    }
                }
                return msg.ToString();
            }
        }

        public BusinessRulesValidationException(ValidationResultList results)
            :base()
        {
            Results = results;
            //TODO: Configurar desde web.config
            HideErrorCodes = false;
        }
    }

}
