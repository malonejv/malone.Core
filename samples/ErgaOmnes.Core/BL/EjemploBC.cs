using ErgaOmnes.Core.CL.Exceptions;
using ErgaOmnes.Core.EL.Model;
using malone.Core.Business.Components;
using malone.Core.Commons.Exceptions;
using malone.Core.Commons.Log;
using malone.Core.DataAccess.Repositories;
using malone.Core.DataAccess.UnitOfWork;
using System;
using System.Collections.Generic;

namespace ErgaOmnes.Core.BL
{
    public interface IEjemploBC : IBusinessComponent<Ejemplo, IEjemploBV> { }

    public class EjemploBC : BusinessComponent<Ejemplo, IEjemploBV>, IEjemploBC
    {
        public EjemploBC(IEjemploBV businessValidator, IRepository<Ejemplo> repository, ILogger logger)
            : base(businessValidator, repository, logger)
        { }


        public override void Add(Ejemplo entity)
        {
            try
            {
                BusinessValidator.AddValidationRules
                    .Add(
                        new ValidationRule()
                        {
                            Method = BusinessValidator.ValidarCaracteresEspeciales,
                            Arguments = new List<object>() { entity }
                        });

                base.Add(entity);
            }
            catch (BusinessRulesValidationException) { throw; }
            catch (TechnicalException) { throw; }
            catch (Exception ex)
            {
                var techEx = ExceptionFactory<ErrorCode, IErrorLocalizationHandler>.CreateException<TechnicalException>(ex, ErrorCode.BUSINESS4000);
                if (Logger != null) Logger.Error(techEx);

                throw techEx;
            }
        }
    }
}
