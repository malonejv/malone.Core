using AutoMapper;
using malone.Core.Commons.Initializers;
using Unity;

namespace GMS.Core.Initializers
{
    public class UILayerInitializer : IInitializer<IUnityContainer>
    {
        public void Initialize(IUnityContainer container)
        {
            var mapperConfiguration = AutoMapperConfig.RegisterProfiles();
            var mapper = new Mapper(mapperConfiguration);
            container.RegisterInstance(mapper);

        }
    }

    //Crear un proyecto de Configuracion de AutoMapper de ser necesario
    public static class AutoMapperConfig
    {
        public static MapperConfiguration RegisterProfiles()
        {
            var configuration = new MapperConfiguration(cfg => cfg.AddMaps(new[] { "GMS.Core" }));

            return configuration;
        }
    }
}
