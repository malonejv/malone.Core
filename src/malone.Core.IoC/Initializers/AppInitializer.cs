//<author>Javier López Malone</author>
//<date>25/11/2020 02:48:01</date>

using System;
using System.Reflection;
using malone.Core.Commons.DI;

namespace malone.Core.IoC.Initializers
{
    public class AppInitializer<TInjectorInitializer, TContainer, TLayersInitializer>
        where TInjectorInitializer : IInjectorInitializer<TContainer>, new()
        where TLayersInitializer : class, ILayerInitializer<TContainer>, new()
    {
        public static void Initialize()
        {
            TContainer container = InitializeInjector();
            InitializeLayers(container);
        }

        private static TContainer InitializeInjector()
        {
            TInjectorInitializer instance = new TInjectorInitializer();

            var methodInitialize = FindInjectorInitializerInitializeMethod();
            if (methodInitialize != null)
            {
                object container = methodInitialize.Invoke(instance, Type.EmptyTypes);

                return (TContainer)container;
            }
            return default(TContainer);
        }

        private static MethodInfo FindInjectorInitializerInitializeMethod()
        {

            MethodInfo methodInfo = null;
            var methodName = "Initialize";
            var type = typeof(TInjectorInitializer);
            if (type.IsPublic)
            {
                // Verify that type is public to avoid allowing internal code execution. This implementation will not match
                // nested public types.
                methodInfo = type.GetMethod(methodName, BindingFlags.Public | BindingFlags.Instance | BindingFlags.IgnoreCase,
                                binder: null,
                                types: Type.EmptyTypes,
                                modifiers: null);
            }
            return methodInfo;
        }

        private static void InitializeLayers(TContainer container)
        {
            if (container.Equals(default(TContainer)))
            {
                throw new ArgumentNullException(nameof(container));
            }

            TLayersInitializer instance = new TLayersInitializer();

            var methodInitialize = FindLayersInitializerInitializeMethod();
            methodInitialize.Invoke(instance, new object[] { container });
        }

        private static MethodInfo FindLayersInitializerInitializeMethod()
        {

            MethodInfo methodInfo = null;
            var methodName = "Initialize";
            var type = typeof(TLayersInitializer);
            if (type.IsPublic)
            {
                // Verify that type is public to avoid allowing internal code execution. This implementation will not match
                // nested public types.
                methodInfo = type.GetMethod(methodName, BindingFlags.Public | BindingFlags.Instance | BindingFlags.IgnoreCase,
                                binder: null,
                                types: new Type[] { typeof(TContainer) },
                                modifiers: null);
            }
            return methodInfo;
        }
    }
}
