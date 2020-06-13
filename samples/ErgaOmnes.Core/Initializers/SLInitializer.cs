using AutoMapper;
using malone.Core.Commons.Initializers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unity;

namespace ErgaOmnes.Core.Initializers
{
    public class SLInitializer : IInitializer<IUnityContainer>
    {
        public void Initialize(IUnityContainer container)
        {
            var mapperConfiguration = AutoMapperConfig.RegisterProfiles();
            var mapper = new Mapper(mapperConfiguration);
            container.RegisterInstance(mapper);
        }
    }

    //Crear un proyecto de Configuracion de AutoMapper de ser necesario
    internal static class AutoMapperConfig
    {
        public static MapperConfiguration RegisterProfiles()
        {
            var configuration = new MapperConfiguration(cfg => cfg.AddMaps(new[] { "ErgaOmnes.Core" }));

            return configuration;
        }
    }
}
