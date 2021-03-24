//<author>Javier López Malone</author>
//<date>25/11/2020 02:47:51</date>

using malone.Core.Business.Components;
using System;
using System.Text;

namespace malone.Core.Commons.Exceptions
{
    /// <summary>
    /// Encapsulates funcional validation exceptions in Business Layer.
    /// </summary>
    public class BusinessRulesValidationException : Exception
    {
        /// <summary>
        /// Gets the Results.
        /// </summary>
        public ValidationResultList Results { get; private set; }

        /// <summary>
        /// Gets or sets a value indicating whether HideErrorCodes.
        /// </summary>
        public bool HideErrorCodes { get; protected set; }

        /// <summary>
        /// Gets the Message.
        /// </summary>
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

        /// <summary>
        /// Initializes a new instance of the <see cref="BusinessRulesValidationException"/> class.
        /// </summary>
        /// <param name="results">The results<see cref="ValidationResultList"/>.</param>
        public BusinessRulesValidationException(ValidationResultList results)
            : base()
        {
            Results = results;
            //TODO: Configurar desde web.config
            HideErrorCodes = false;
        }
    }
}
