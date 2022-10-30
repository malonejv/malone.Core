using System.Collections.Generic;
using System.Web;
using System.Web.Mvc;
using AutoMapper;
using malone.Core.Commons.Helpers.Extensions;
using malone.Core.EF.Entities.Filters;
using malone.Core.Identity.EntityFramework.Entities;
using malone.Core.Sample.EF.SqlServer.Middle;
using malone.Core.Sample.EF.SqlServer.Middle.BL;
using malone.Core.Sample.EF.SqlServer.Middle.EL.Model;
using malone.Core.Sample.EF.SqlServer.mvc.Attributes;
using malone.Core.Sample.EF.SqlServer.mvc.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;

namespace malone.Core.Sample.EF.SqlServer.mvc.Controllers
{
    [Authorize]
    public class ListController : Controller
    {
        private ApplicationUserManager _userManager;
        private readonly ITodoListBC todoListBC;
        private readonly ITaskItemBC taskItemBC;

        public ApplicationUserManager UserManager
        {
            get
            {
                if (_userManager == null)
                    _userManager = HttpContext.GetOwinContext().Get<ApplicationUserManager>();
                return _userManager;
            }
        }

        public ListController(ITodoListBC todoListBC, ITaskItemBC taskItemBC)
        {
            this.todoListBC = todoListBC.ThrowIfNull();
            this.taskItemBC = taskItemBC.ThrowIfNull();
        }

        #region Lists

        public ActionResult Index()
        {
            //Ejemplo para filtrar la lista
            var lists = GetUserLists();

            return View(lists);
        }

        public ActionResult Detail(int id)
        {
            var todoList = todoListBC.GetById(id, includeProperties: "Items,User");

            return View(TodoListViewModel.CreateFromEntity(todoList));
        }

        public ActionResult Create()
        {
            TodoListViewModel model = new TodoListViewModel();
            return PartialView("_Create", model);
        }

        [HttpPost]
        [ValidateHeaderAntiForgeryToken]
        public ActionResult Create(TodoListViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = CurrentUser();
                var id = todoListBC.Add(new TodoList(model.Name, user));

                return Json(new { Url = Url.Action("Detail", new { Id= id }) });
            }
            else
                return PartialView("_Create", model);
        }

        public ActionResult Edit(int id)
        {
            var todoList = todoListBC.GetById(id);

            TodoListViewModel model = TodoListViewModel.CreateFromEntity(todoList);
            return PartialView("_Edit", model);
        }

        [HttpPost]
        [ValidateHeaderAntiForgeryToken]
        public ActionResult Edit(TodoListViewModel model)
        {
            if (ModelState.IsValid)
            {
                var todoList = todoListBC.GetById(model.Id, includeProperties: "User");
                todoList.UpdateName(model.Name);
                todoListBC.Update(todoList);

                return Json(new { Url = Url.Action("Index") });
            }
            return PartialView("_Edit", model);
        }

        public ActionResult Delete(int id)
        {
            var todoList = todoListBC.GetById(id, includeProperties: "Items");

            TodoListViewModel model = TodoListViewModel.CreateFromEntity(todoList);
            return PartialView("_Delete", model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(TodoListViewModel todoList)
        {
            todoListBC.Delete(todoList.Id);
            return RedirectToAction("Index");
        }

        #endregion

        #region Private Methods

        private CoreUser CurrentUser()
        {
            var userId = User.Identity.GetUserId<int>();
            return UserManager.FindById(userId);
        }

        private IEnumerable<TodoListViewModel> GetUserLists()
        {
            var userId = User.Identity.GetUserId<int>();

            var list = todoListBC.Get(
                new FilterExpression<TodoList>()
                {
                    Expression = (l => l.User.Id == userId)
                }, includeProperties: "Items,User");

            return TodoListViewModel.CreateFromList(list);
        }

        #endregion
    }
}