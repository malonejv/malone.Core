using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace malone.Core.DataAccess.Repositories
{
    /// <summary>
    /// Defines the <see cref="ICustomRepository{T}" />.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface ICustomRepository<T>
        where T : class
    {
    }
}
