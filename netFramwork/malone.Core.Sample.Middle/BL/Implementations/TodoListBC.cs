using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using malone.Core.BL.Components.Implementations;
using malone.Core.BL.Components.Interfaces;
using malone.Core.CL.Exceptions;
using malone.Core.CL.Exceptions.Handler.Interfaces;
using malone.Core.CL.Exceptions.Manager.Interfaces;
using malone.Core.DAL.Repositories;
using malone.Core.DAL.UnitOfWork;
using malone.Core.Sample.Middle.EL;
using malone.Core.Sample.Middle.EL.Model;

namespace malone.Core.Sample.Middle.BL.Implementations
{
    public class TodoListBC : BusinessComponent<decimal, TodoList, ITodoListBV>, ITodoListBC
    {
        public TodoListBC(IUnitOfWork unitOfWork, ITodoListBV businessValidator, IRepository<decimal, TodoList> repository, IExceptionMessageManager exManager, IExceptionHandler exHandler)
            : base(unitOfWork, businessValidator, repository, exManager, exHandler)
        { }

        public override void Add(TodoList entity)
        {
            BusinessValidator.AddValidationRules
                .Add(
                    new ValidationRule()
                    {
                        Method = BusinessValidator.ValidarNombreRepetido,
                        Arguments = new List<object>() { entity }
                    });

            base.Add(entity);
        }
    }
}
