using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using malone.Core.BL.Components.Interfaces;
using malone.Core.Sample.Middle.EL;

namespace malone.Core.Sample.Middle.BL
{
    public interface ITaskItemBC : IBusinessComponent<TaskItem, IBusinessValidator<TaskItem>>
    {
    }
}
