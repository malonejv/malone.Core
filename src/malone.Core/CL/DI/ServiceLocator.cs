using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace malone.Core.CL.DI
{
    public interface IServiceLocator
    {
        T Get<T>();
    }

    public class ServiceLocator
    {
        private static ServiceLocator _serviceLocator = new ServiceLocator();

        protected IServiceLocator _current;


        public ServiceLocator()
        {
            InnerSetResolver(new DefaultServiceLocator());
        }

        public static IServiceLocator Current
        {
            get
            {
                return _serviceLocator._current;
            }
        }


        public static void SetResolver(IServiceLocator resolver)
        {
            _serviceLocator.InnerSetResolver(resolver);
        }

        public static void SetResolver(object commonServiceLocator)
        {
            _serviceLocator.InnerSetResolver(commonServiceLocator);
        }

        public void InnerSetResolver(IServiceLocator resolver)
        {
            if (resolver == null)
            {
                throw new ArgumentNullException("resolver");
            }

            _current = resolver;
        }

        public void InnerSetResolver(object commonServiceLocator)
        {
            if (commonServiceLocator == null)
            {
                throw new ArgumentNullException("commonServiceLocator");
            }

            Type locatorType = commonServiceLocator.GetType();
            if (typeof(IServiceLocator).IsAssignableFrom(locatorType))
            {
                throw new ArgumentException(String.Format("{0} no implementa {1}", nameof(commonServiceLocator), nameof(IServiceLocator)));
            }

            InnerSetResolver(commonServiceLocator as IServiceLocator);
        }
    }

    internal class DefaultServiceLocator : IServiceLocator
    {
        public T Get<T>()
        {
            Type type = typeof(T);
            // Since attempting to create an instance of an interface or an abstract type results in an exception, immediately return null
            // to improve performance and the debugging experience with first-chance exceptions enabled.
            if (type.IsInterface || type.IsAbstract)
            {
                return default(T);
            }

            try
            {
                return Activator.CreateInstance<T>();
            }
            catch
            {
                return default(T);
            }
        }
    }

}
