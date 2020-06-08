using malone.Core.Entities.Model;

namespace malone.Core.Sample.Middle.EL.Model
{
    public class TaskItem : IBaseEntity, ISoftDelete
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public bool IsDeleted { get; set; }
    }
}
