using malone.Core.BL.Components.Implementations;
using malone.Core.BL.Components.Interfaces;
using malone.Core.Sample.Middle.BL;
using malone.Core.Sample.Middle.BL.Implementations;
using malone.Core.Sample.Middle.EL;
using malone.Core.Sample.Middle.EL.Model;
using Unity;

namespace malone.core.Sample.DI
{
    public static class RegisterBusinessLayerTypes
    {
        public static IUnityContainer RegisterTypes(IUnityContainer container)
        {
            //BUSINESS VALIDATORS
            //container.RegisterType<IActividadBusinessValidator, ActividadBusinessValidator>();
            container.RegisterType<ITodoListBV, TodoListBV>();
            container.RegisterType<IBusinessValidator<decimal, TaskItem>, BusinessValidator<decimal, TaskItem>>();


            //BUSINESS COMPONENTS
            container.RegisterType<ITodoListBC, TodoListBC>();

            return container;
        }
    }
}
