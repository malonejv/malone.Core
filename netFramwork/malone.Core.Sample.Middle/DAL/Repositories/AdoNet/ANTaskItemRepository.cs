using AutoMapper;
using malone.Core.DAL.AdoNet.Repositories;
using malone.Core.DAL.AdoNet.Repositories.Implementations;
using malone.Core.DAL.Base.UnitOfWork;
using malone.Core.EL;
using malone.Core.EL.Filters.Extensions;
using malone.Core.Sample.Middle.EL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace malone.Core.Sample.Middle.DAL.Repositories.AdoNet
{
    public class ANTaskItemRepository : AdoNetRepository<decimal,TaskItem>
    {

        public ANTaskItemRepository(IUnitOfWork unitOfWork, Mapper mapper) : base(unitOfWork, mapper)
        {
        }

        protected override KeyValuePair<CommandType, string> ConfigureGetAllCommandText(bool includeDeleted, string includeProperties)
        {
            StringBuilder commandText = null;

            if (typeof(ISoftDelete).IsAssignableFrom(typeof(TaskItem)))
                commandText = new StringBuilder("SELECT Id, Description, TodoList_Id, IsDeleted FROM Sampleuser.TaskItems WHERE Description = @Description AND IsDeleted = @IsDeleted;");
            else
                commandText = new StringBuilder("SELECT Id, Description, TodoList_Id FROM Sampleuser.TaskItems WHERE Description = @Description;");

            return new KeyValuePair<CommandType, string>(CommandType.Text, commandText.ToString());
        }

        protected override KeyValuePair<CommandType, string> ConfigureGetByIdCommandText(bool includeDeleted, string includeProperties)
        {
            throw new NotImplementedException();
        }

        protected override KeyValuePair<CommandType, string> ConfigureGetCommandText(bool includeDeleted, string includeProperties)
        {
            throw new NotImplementedException();
        }

        protected override KeyValuePair<CommandType, string> ConfigureGetEntityCommandText(bool includeDeleted, string includeProperties)
        {
            throw new NotImplementedException();
        }

        protected override KeyValuePair<CommandType, string> ConfigureInsertCommandText()
        {
            throw new NotImplementedException();
        }

        protected override KeyValuePair<CommandType, string> ConfigureUpdateCommandText()
        {
            throw new NotImplementedException();
        }

        protected override KeyValuePair<CommandType, string> ConfigureDeleteCommandText()
        {
            throw new NotImplementedException();
        }

    }
}
