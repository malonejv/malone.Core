using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using malone.Core.WebApi;
using ErgaOmnes.Core.EL.Model;

namespace ErgaOmnes.Core.EL.RequestParams
{
    public class EjemploGetRequestParams : IGetRequestParam<Ejemplo>
    {
        public string Text { get; set; }
    }
}
