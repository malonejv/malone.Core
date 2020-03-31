﻿using malone.Core.CL.Configurations.Sections;
using malone.Core.EL;
using malone.Core.Sample.Middle.BL;
using malone.Core.Sample.Middle.CL.Features;
using malone.Core.Sample.Middle.EL;
using malone.Core.Sample.Middle.EL.Filters.AdoNetFilters.TodoList;
using malone.Core.Sample.Middle.EL.Filters.EFFilters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace malone.Core.Sample.UI.Controllers
{
    public class TodoListController : Controller
    {
        public ITodoListBC TodoListBC { get; set; }

        public TodoListController(ITodoListBC todoListBC)
        {
            TodoListBC = todoListBC;
        }
        // GET: TodoList
        public ActionResult Index()
        {

            if (FeatureSettings.IsEnabled(Features.EF))
            {
                var list = TodoListBC.GetAll(includeProperties: "Items");

                var list2 = TodoListBC.Get(new EFTodoListGetRequest()
                {
                    Expression = (x => x.Name.Contains("Test"))
                });
            }
            else if (FeatureSettings.IsEnabled(Features.AdoNet))
            {
                var list = TodoListBC.GetAll(includeProperties: "Items");

                var filter = new ANTodoListGetRequest();
                filter.Name = "Test";

                var list2 = TodoListBC.Get(filter);
            }
            return View();
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(TodoList todoList)
        {

            TodoListBC.Add(todoList);

            return View();
        }

    }

    public class Pepe<TEntity>
   where TEntity : class, IBaseEntity
    {
        public virtual IEnumerable<TEntity> Get<TFilter>(
        TFilter filter = default(TFilter),
        Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
        bool includeDeleted = false,
        string includeProperties = "")
        {
            return null;
        }
    }
}