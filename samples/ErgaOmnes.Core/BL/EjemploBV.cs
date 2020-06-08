using ErgaOmnes.Core.CL.Exceptions;
using ErgaOmnes.Core.EL.Model;
using malone.Core.Business.Components;
using malone.Core.Commons.Exceptions;
using malone.Core.Commons.Exceptions.Handler;
using malone.Core.Commons.Exceptions.Manager;
using malone.Core.DataAccess.Repositories;
using System;

namespace ErgaOmnes.Core.BL
{

    public class EjemploBV : BusinessValidator<Ejemplo>, IEjemploBV
    {
        protected IRepository<Ejemplo> Repository { get; }
        protected IMessageHandler<ErrorCodes> MessageHandler { get; }
        protected IExceptionHandler<ErrorCodes> ExceptionHandler { get; }

        public EjemploBV(IRepository<Ejemplo> repository, IMessageHandler<ErrorCodes> messageHandler, IExceptionHandler<ErrorCodes> exceptionHandler)
            : base()
        {
            Repository = repository;
            MessageHandler = messageHandler;
            ExceptionHandler = exceptionHandler;
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
                ExceptionHandler.HandleException<TechnicalException<ErrorCodes>>(ex,ErrorCodes.E8000);

                throw new ArgumentNullException("args");
            }
        }

    }
}