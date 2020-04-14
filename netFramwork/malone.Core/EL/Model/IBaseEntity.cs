namespace malone.Core.EL.Model
{

    public interface IBaseEntity<TKey>
    {
        TKey Id { get; set; }
    }
    public interface IBaseEntity : IBaseEntity<int>
    {
        new int Id { get; set; }
    }
}
