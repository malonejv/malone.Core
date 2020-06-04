using malone.Core.EL.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ErgaOmnes.Core.EL.Model
{
    public class Ejemplo : IBaseEntity
    {
        public int Id { get; set; }
        public string Text { get; set; }
    }
}
