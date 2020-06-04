using System.Web.Http;
using Unity;
using Unity.AspNet.WebApi;

namespace malone.Core.CL.DI.Unity.WebApi
{
    public class UnityWebApiActivator : UnityMvcActivator
    {
        public override IUnityContainer Initialize()
        {
            var container = base.Initialize();

            GlobalConfiguration.Configuration.DependencyResolver = new UnityDependencyResolver(container);

            return container;
        }
    }
}
