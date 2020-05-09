using malone.Core.Sample.DI;
using malone.Core.CL.DI.ServiceLocator;
using Microsoft.Web.Infrastructure.DynamicModuleHelper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Unity.AspNet.Mvc;
using Unity.Injection;

//[assembly: WebActivatorEx.PreApplicationStartMethod(typeof(malone.Core.Sample.UI.EFSqlServer.App_Start.UnityMvcActivator), nameof(malone.Core.Sample.UI.EFSqlServer.App_Start.UnityMvcActivator.Start))]
//[assembly: WebActivatorEx.ApplicationShutdownMethod(typeof(malone.Core.Sample.UI.EFSqlServer.App_Start.UnityMvcActivator), nameof(malone.Core.Sample.UI.EFSqlServer.App_Start.UnityMvcActivator.Shutdown))]

//namespace malone.Core.Sample.UI.EFSqlServer.App_Start
//{
//    /// <summary>
//    /// Provides the bootstrapping for integrating Unity with ASP.NET MVC.
//    /// </summary>
//    public static class UnityMvcActivator
//    {
//        /// <summary>
//        /// Integrates Unity when the application starts.
//        /// </summary>
//        public static void Start()
//        {
//            FilterProviders.Providers.Remove(FilterProviders.Providers.OfType<FilterAttributeFilterProvider>().First());
//            FilterProviders.Providers.Add(new UnityFilterAttributeFilterProvider(UnityConfig.Container));
//        }

//        /// <summary>
//        /// Disposes the Unity container when the application is shut down.
//        /// </summary>
//        public static void Shutdown()
//        {
//            UnityConfig.Container.Dispose();
//        }
//    }
//}