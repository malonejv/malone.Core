//<author>Javier López Malone</author>
//<date>25/11/2020 02:48:14</date>

using malone.Core.DataAccess.Context;
using System;

namespace malone.Core.DataAccess.UnitOfWork
{
                public interface IUnitOfWork : IDisposable
    {
                                IContext Context { get; }

                                        int SaveChanges();

                                void Dispose();
    }
}
