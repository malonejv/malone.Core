using malone.Core.Commons.Initializers;
using Unity;

namespace malone.Core.Unity
{
	public class UnityActivator : IInjectorInitializer<IUnityContainer>
	{
		public virtual IUnityContainer Initialize()
		{
			return UnityConfig.Container;
		}

		public virtual void Terminate()
		{
			UnityConfig.Container.Dispose();
		}
	}
}
