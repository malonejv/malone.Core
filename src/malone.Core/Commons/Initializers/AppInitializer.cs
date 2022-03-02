//<author>Javier López Malone</author>
//<date>25/11/2020 02:48:01</date>

namespace malone.Core.Commons.Initializers
{
	using System;
	using System.Reflection;
	using malone.Core.Commons.DI;

	/// <summary>
	/// Defines the <see cref="AppInitializer{TInjectorInitializer, TContainer, TLayersInitializer}" />.
	/// </summary>
	/// <typeparam name="TInjectorInitializer">.</typeparam>
	/// <typeparam name="TContainer">.</typeparam>
	/// <typeparam name="TLayersInitializer">.</typeparam>
	public class AppInitializer<TInjectorInitializer, TContainer, TLayersInitializer>
		where TInjectorInitializer : IInjectorInitializer<TContainer>, new()
		where TLayersInitializer : class, ILayerInitializer<TContainer>, new()
	{
		/// <summary>
		/// The Initialize.
		/// </summary>
		public static void Initialize()
		{
			TContainer container = InitializeInjector();
			InitializeLayers(container);
		}

		/// <summary>
		/// The InitializeInjector.
		/// </summary>
		/// <returns>The <see cref="TContainer"/>.</returns>
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

		/// <summary>
		/// The FindInjectorInitializerInitializeMethod.
		/// </summary>
		/// <returns>The <see cref="MethodInfo"/>.</returns>
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

		/// <summary>
		/// The InitializeLayers.
		/// </summary>
		/// <param name="container">The container<see cref="TContainer"/>.</param>
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

		/// <summary>
		/// The FindLayersInitializerInitializeMethod.
		/// </summary>
		/// <returns>The <see cref="MethodInfo"/>.</returns>
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
