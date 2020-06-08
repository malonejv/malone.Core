using System;

namespace malone.Core.Entities.Model
{

    public interface IBaseEntity<TKey>
        where TKey : IEquatable<TKey>
    {
        TKey Id { get; set; }
    }
    public interface IBaseEntity : IBaseEntity<int>
    {
        new int Id { get; set; }
    }
}
