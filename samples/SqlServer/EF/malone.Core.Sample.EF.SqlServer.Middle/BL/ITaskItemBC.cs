using malone.Core.Services;
using malone.Core.Sample.EF.SqlServer.Middle.EL.Model;

namespace malone.Core.Sample.EF.SqlServer.Middle.BL
{
    public interface ITaskItemBC : IService<TaskItem, IServiceValidator<TaskItem>>
    {
    }
}
