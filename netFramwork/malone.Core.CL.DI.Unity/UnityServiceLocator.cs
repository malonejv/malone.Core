using System;
using Unity;

namespace malone.Core.CL.DI.Unity
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
