using malone.Core.WebApi.Attributes;
using System.Web.Http;
using Unity;
using Unity.AspNet.WebApi;

namespace malone.Core.Unity.WebApi
{
    public class UnityWebApiActivator : UnityMvcActivator
    {
        public override IUnityContainer Initialize()
        {
            var container = base.Initialize();

            GlobalConfiguration.Configuration.DependencyResolver = new UnityDependencyResolver(container);
            GlobalConfiguration.Configuration.Filters.Add(new HandleExceptionFilterAttribute());

            return container;
        }
    }
}
