using AutoMapper;
using malone.Core.Commons.Initializers;
using Unity;

namespace malone.Core.Sample.EF.Firebird.Middle.Initializers
{
    public class ServiceLayerInitializer : IInitializer<IUnityContainer>
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
            var configuration = new MapperConfiguration(cfg => cfg.AddMaps(new[] { "malone.Core.Sample.EF.SqlServer.Middle" }));

            return configuration;
        }
    }
}
