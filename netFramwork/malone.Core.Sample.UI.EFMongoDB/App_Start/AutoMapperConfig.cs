using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace malone.Core.Sample.UI.EFMongoDB
{
    public class AutoMapperConfig
    {
        public static MapperConfiguration RegisterProfiles()
        {
            var configuration = new MapperConfiguration(cfg => cfg.AddMaps(new[] { "malone.Core.Sample.Middle" }));

            return configuration;
        }
    }
}