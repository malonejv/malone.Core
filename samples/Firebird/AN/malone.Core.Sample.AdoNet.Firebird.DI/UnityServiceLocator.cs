using malone.Core.CL.DI.ServiceLocator;
using System;
using System.Reflection;
using Unity;

namespace malone.core.Sample.DI
{
    public class UnityServiceLocator : IServiceLocator
    {
        private readonly IUnityContainer _container;  // Ninject kernel

        public UnityServiceLocator(IUnityContainer container)
        {

            if (container == null)
            {
                throw new ArgumentNullException("container");
            }

            _container = container;
        }

        public T Get<T>()
        {
            return _container.Resolve<T>();
        }

    }
}
