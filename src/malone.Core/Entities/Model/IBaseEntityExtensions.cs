//<author>Javier López Malone</author>
//<date>25/11/2020 02:48:17</date>

using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace malone.Core.Entities.Model
{
                public static class IBaseEntityExtensions
    {
                                                                public static TEntity Clone<TEntity, TKey>(this TEntity entityToClone)
            where TKey : IEquatable<TKey>
            where TEntity : class, IBaseEntity<TKey>
        {

            if (entityToClone != null)
            {
                BinaryFormatter formatter = new BinaryFormatter();
                MemoryStream stream = new MemoryStream();

                formatter.Serialize(stream, entityToClone);
                stream.Seek(0, SeekOrigin.Begin);

                TEntity result = (TEntity)formatter.Deserialize(stream);

                stream.Close();

                return result;
            }
            else
                return default(TEntity);
        }
    }
}
