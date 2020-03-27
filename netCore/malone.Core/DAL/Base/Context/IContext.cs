namespace malone.Core.DAL.Base.Context
{
    public interface IContext
    {
        int SaveChanges();
        void Dispose();
    }
}
