using AutoMapper;
using malone.Core.CL.DI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unity;

namespace malone.Core.Sample.DI
{
    public class UILayerBootstrapper : ILayerBootstrapper<IUnityContainer>
    {
        public void RegisterTypes(IUnityContainer container)
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
