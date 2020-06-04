using malone.Core.CL.Initializers;
using Unity;

namespace malone.Core.Sample.DI
{
    public class UILayerInitializer : ILayer<IUnityContainer>
    {
        public void Initialize(IUnityContainer container)
        {

            //var mapperConfiguration = AutoMapperConfig.RegisterProfiles();
            //var mapper = new Mapper(mapperConfiguration);
            //container.RegisterInstance(mapper);

        }
    }

    //Crear un proyecto de Configuracion de AutoMapper de ser necesario
    //public class AutoMapperConfig
    //{
    //    public static MapperConfiguration RegisterProfiles()
    //    {
    //        var configuration = new MapperConfiguration(cfg => cfg.AddMaps(new[] { "malone.Core.Sample.Middle" }));

    //        return configuration;
    //    }
    //}
}
