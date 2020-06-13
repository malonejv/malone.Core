using AutoMapper;
using ErgaOmnes.Core.EL.Model;
using ErgaOmnes.Core.EL.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ErgaOmnes.Core.EL.Profiles
{
    public class EjemploViewModelProfile : Profile
    {
        public EjemploViewModelProfile()
        {
            IMappingExpression<Ejemplo, EjemploViewModel> mappingExpression = CreateMap<Ejemplo, EjemploViewModel>();
        }
    }
}
