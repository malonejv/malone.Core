using System;
using ErgaOmnes.Core.CL.Exceptions;
using ErgaOmnes.Core.EL.Model;
using malone.Core.Business.Components;
using malone.Core.Commons.Exceptions;
using malone.Core.Commons.Helpers.Extensions;
using malone.Core.Commons.Log;
using malone.Core.DataAccess.Repositories;

namespace ErgaOmnes.Core.BL
{

    public class EjemploBV : BusinessValidator<Ejemplo>, IEjemploBV
    {
        protected IRepository<Ejemplo> Repository { get; }
        protected IErrorLocalizationHandler ErrorLocalizationHandler { get; }
        protected ILogger Logger { get; }

        public EjemploBV(IRepository<Ejemplo> repository, IErrorLocalizationHandler errorLocalizationHandler, ILogger logger)
            : base()
        {
            Repository = repository;
            ErrorLocalizationHandler = errorLocalizationHandler;
            Logger = logger;
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

                var todoList = args[0] as Ejemplo;

                bool tieneCaracteresEspeciales = todoList.Text.HasSpecialCharacters();

                if (tieneCaracteresEspeciales)
                {
                    var message = ErrorLocalizationHandler.GetString(ErrorCode.BUSVAL5000);
                    //var formated = string.Format(message, typeof(Ejemplo));
                    return new ValidationResult(ErrorCode.BUSVAL5000.ToString(), message);
                }

                return null;
            }
            catch (Exception ex)
            {
                var techEx = ExceptionFactory<ErrorCode,IErrorLocalizationHandler>.CreateException<TechnicalException>(ex, ErrorCode.TECH1000);
                if (Logger != null) Logger.Error(techEx);

                throw techEx;
            }
        }

    }
}