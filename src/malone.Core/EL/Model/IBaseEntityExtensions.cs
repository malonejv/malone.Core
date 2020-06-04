using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;

namespace malone.Core.EL.Model
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
