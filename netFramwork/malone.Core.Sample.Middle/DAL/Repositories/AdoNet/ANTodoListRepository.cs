using AutoMapper;
using malone.Core.AdoNet.DAL.Repositories;
using malone.Core.DAL.UnitOfWork;
using malone.Core.EL.Model;
using malone.Core.Sample.Middle.EL.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace malone.Core.Sample.Middle.DAL.Repositories.AdoNet
{
    public class ANTodoListRepository : AdoNetRepository<decimal, TodoList>
    {

        public ANTodoListRepository(IUnitOfWork unitOfWork, Mapper mapper) : base(unitOfWork, mapper)
        {
        }

        protected override KeyValuePair<CommandType, string> ConfigureGetAllCommandText(bool includeDeleted, string includeProperties)
        {
            StringBuilder commandText = null;

            if (typeof(ISoftDelete).IsAssignableFrom(typeof(TodoList)))
                commandText = new StringBuilder("SELECT Id, Name, IsDeleted FROM Sampleuser.TodoLists WHERE IsDeleted = @IsDeleted;");
            else
                commandText = new StringBuilder("SELECT Id, Name FROM Sampleuser.TodoLists;");

            return new KeyValuePair<CommandType, string>(CommandType.Text, commandText.ToString());
        }

        protected override KeyValuePair<CommandType, string> ConfigureGetByIdCommandText(bool includeDeleted, string includeProperties)
        {
            throw new NotImplementedException();
        }

        protected override KeyValuePair<CommandType, string> ConfigureGetCommandText(bool includeDeleted, string includeProperties)
        {
            StringBuilder commandText = null;

            if (typeof(ISoftDelete).IsAssignableFrom(typeof(TodoList)))
                commandText = new StringBuilder("SELECT Id, Name, IsDeleted FROM Sampleuser.TodoLists WHERE lower(Name) like '%'+lower(@Name)+'%' AND IsDeleted = @IsDeleted;");
            else
                commandText = new StringBuilder("SELECT Id, Name FROM Sampleuser.TodoLists WHERE lower(Name) like '%'+lower(@Name)+'%';");

            return new KeyValuePair<CommandType, string>(CommandType.Text, commandText.ToString());
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
