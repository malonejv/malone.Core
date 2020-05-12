using malone.Core.Patterns.Behavioral.Singleton;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace malone.Core.CL.DI
{
    public interface ICoreRegistrator<TContainer, TRegistrator>
        where TRegistrator : Singleton<TRegistrator>, ICoreRegistrator<TContainer, TRegistrator>
    {
    }

    public class CoreBootstrapper<TContainer, TRegistrator> : Singleton<TRegistrator>, ICoreRegistrator<TContainer, TRegistrator>
        where TRegistrator : Singleton<TRegistrator>, ICoreRegistrator<TContainer, TRegistrator>
    {
        public CoreBootstrapper() : base() { }

        public TRegistrator Registrator { get; set; }

        protected virtual IEnumerable<ILayerBootstrapper<TContainer>> CoreLayerBootstrappers { get => Enumerable.Empty<ILayerBootstrapper<TContainer>>(); }

        /// <summary>
        ///     Sets the up.
        /// </summary>
        public virtual void SetUp(TContainer container)
        {
            if (container == null)
            {
                throw new ArgumentNullException(nameof(container));
            }

            foreach (var bootstrapper in CoreLayerBootstrappers)
            {
                bootstrapper.RegisterTypes(container);
            }

            //ApplicationEnvironment.Container = container;
        }
    }
}
