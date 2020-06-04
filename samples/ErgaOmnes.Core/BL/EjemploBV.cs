using ErgaOmnes.Core.CL.Exceptions;
using ErgaOmnes.Core.EL.Model;
using malone.Core.BL.Components.Implementations;
using malone.Core.BL.Components.Interfaces;
using malone.Core.CL.Exceptions;
using malone.Core.CL.Exceptions.Handler;
using malone.Core.CL.Exceptions.Manager;
using malone.Core.DAL.Repositories;
using malone.Core.EF.EL.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ErgaOmnes.Core.BL
{

    public class EjemploBV : BusinessValidator<Ejemplo, ErrorCodes>, IEjemploBV
    {
        protected ICoreRepository<Ejemplo, ErrorCodes> Repository { get; }

        public EjemploBV(ICoreRepository<Ejemplo, ErrorCodes> repository, IMessageHandler<ErrorCodes> messageHandler, IExceptionHandler<ErrorCodes> exceptionHandler)
            : base(messageHandler, exceptionHandler)
        {
            Repository = repository;
        }

        /// <summary>
        /// Valida que el texto de la entidad Ejemplo no tenga caracteres especiales
        /// </summary>
        /// <param name="args">Recibe una instancia de la clase Ejemplo</param>
        /// <returns></returns>
        public ValidationResult ValidarCaracteresEspeciales(params object[] args)
        {
            try
            {
                if (args == null || args.Length == 0)
                    throw new ArgumentNullException("args");

                var ejemplo = Convert.ChangeType(args[0], typeof(Ejemplo));

                bool existe = false;

                if (existe)
                {
                    var message = string.Format(MessageHandler.GetMessage(ErrorCodes.E5000, typeof(Ejemplo)));
                    return new ValidationResult(ErrorCodes.E5000.ToString(), message);
                }
                return null;
            }
            catch (InvalidCastException ex)
            {
                ExceptionHandler.HandleException<TechnicalException<ErrorCodes>>(ex, ErrorCodes.E8000);

                throw new ArgumentNullException("args");
            }
        }

    }
}