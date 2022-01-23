//<author>Javier López Malone</author>
//<date>25/11/2020 02:47:50</date>

using System;

namespace malone.Core.Commons.DI
{
    /// <summary>
    /// Defines the <see cref="IServiceLocator" />.
    /// </summary>
    public interface IServiceLocator
    {
        /// <summary>
        /// The Get.
        /// </summary>
        /// <typeparam name="T">.</typeparam>
        /// <returns>The <see cref="T"/>.</returns>
        T Get<T>();
    }

    /// <summary>
    /// Defines the <see cref="ServiceLocator" />.
    /// </summary>
    public class ServiceLocator
    {
        /// <summary>
        /// Defines the _serviceLocator.
        /// </summary>
        private static ServiceLocator _serviceLocator = new ServiceLocator();

        /// <summary>
        /// Defines the _current.
        /// </summary>
        protected IServiceLocator _current;

        /// <summary>
        /// Initializes a new instance of the <see cref="ServiceLocator"/> class.
        /// </summary>
        public ServiceLocator()
        {
            InnerSetResolver(new DefaultServiceLocator());
        }

        /// <summary>
        /// Gets the Current.
        /// </summary>
        public static IServiceLocator Current
        {
            get
            {
                return _serviceLocator._current;
            }
        }

        /// <summary>
        /// The SetResolver.
        /// </summary>
        /// <param name="resolver">The resolver<see cref="IServiceLocator"/>.</param>
        public static void SetResolver(IServiceLocator resolver)
        {
            _serviceLocator.InnerSetResolver(resolver);
        }

        /// <summary>
        /// The SetResolver.
        /// </summary>
        /// <param name="commonServiceLocator">The commonServiceLocator<see cref="object"/>.</param>
        public static void SetResolver(object commonServiceLocator)
        {
            _serviceLocator.InnerSetResolver(commonServiceLocator);
        }

        /// <summary>
        /// The InnerSetResolver.
        /// </summary>
        /// <param name="resolver">The resolver<see cref="IServiceLocator"/>.</param>
        public void InnerSetResolver(IServiceLocator resolver)
        {
            if (resolver == null)
            {
                throw new ArgumentNullException("resolver");
            }

            _current = resolver;
        }

        /// <summary>
        /// The InnerSetResolver.
        /// </summary>
        /// <param name="commonServiceLocator">The commonServiceLocator<see cref="object"/>.</param>
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

    /// <summary>
    /// Defines the <see cref="DefaultServiceLocator" />.
    /// </summary>
    internal class DefaultServiceLocator : IServiceLocator
    {
        /// <summary>
        /// The Get.
        /// </summary>
        /// <typeparam name="T">.</typeparam>
        /// <returns>The <see cref="T"/>.</returns>
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
