using ErgaOmnes.Core.CL.Exceptions;
using ErgaOmnes.Core.EL.Model;
using malone.Core.BL.Components.Implementations;
using malone.Core.BL.Components.Interfaces;
using malone.Core.CL.Exceptions;
using malone.Core.CL.Exceptions.Handler;
using malone.Core.DAL.Repositories;
using malone.Core.DAL.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ErgaOmnes.Core.BL
{
    public interface IEjemploBC : IBusinessComponent<Ejemplo, IEjemploBV, ErrorCodes> { }

    public class EjemploBC : BusinessComponent<Ejemplo, IEjemploBV, ErrorCodes>, IEjemploBC
    {
        public EjemploBC(IUnitOfWork unitOfWork, IEjemploBV businessValidator, ICoreRepository<Ejemplo,ErrorCodes> repository, IExceptionHandler<ErrorCodes> exceptionHandler)
            : base(unitOfWork, businessValidator, repository, exceptionHandler)
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
            catch (Exception)
            {

                throw;
            }
        }
    }
}
