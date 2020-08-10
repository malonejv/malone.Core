using malone.Core.Entities.Model;
using malone.Core.Identity.EntityFramework.Entities;

namespace ErgaOmnes.Core.EL.Model
{
    public class Ejemplo : IBaseEntity
    {
        public int Id { get; set; }
        public string Text { get; set; }
    }
}
