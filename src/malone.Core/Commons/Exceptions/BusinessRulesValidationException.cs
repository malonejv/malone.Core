//<author>Javier López Malone</author>
//<date>25/11/2020 02:47:51</date>

using malone.Core.Business.Components;
using System;
using System.Text;

namespace malone.Core.Commons.Exceptions
{
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
            : base()
        {
            Results = results;
            //TODO: Configurar desde web.config
            HideErrorCodes = false;
        }
    }
}
