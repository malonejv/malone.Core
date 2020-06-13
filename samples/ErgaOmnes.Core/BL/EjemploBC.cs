using ErgaOmnes.Core.CL.Exceptions;
using ErgaOmnes.Core.EL.Model;
using malone.Core.Business.Components;
using malone.Core.DataAccess.Repositories;
using malone.Core.DataAccess.UnitOfWork;
using System;
using System.Collections.Generic;

namespace ErgaOmnes.Core.BL
{
    public interface IEjemploBC : IBusinessComponent<Ejemplo, IEjemploBV> { }

    public class EjemploBC : BusinessComponent<Ejemplo, IEjemploBV>, IEjemploBC
    {
        public EjemploBC(IUnitOfWork unitOfWork, IEjemploBV businessValidator, IRepository<Ejemplo> repository)
            : base(unitOfWork, businessValidator, repository)
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
