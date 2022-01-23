using malone.Core.Commons.Initializers;
using Unity;

namespace malone.Core.Unity
{
    public class UnityActivator : IInjectorInitializer<IUnityContainer>
    {
        /// <summary>
        /// Integrates Unity when the application starts.
        /// </summary>
        public virtual IUnityContainer Initialize()
        {
            return UnityConfig.Container;
        }

        /// <summary>
        /// Disposes the Unity container when the application is shut down.
        /// </summary>
        public virtual void Terminate()
        {
            UnityConfig.Container.Dispose();
        }
    }
}
