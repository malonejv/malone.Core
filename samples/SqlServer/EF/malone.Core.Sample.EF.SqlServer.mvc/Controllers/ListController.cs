using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AutoMapper;
using malone.Core.Commons.Helpers.Extensions;
using malone.Core.EF.Entities.Filters;
using malone.Core.Identity.EntityFramework;
using malone.Core.Identity.EntityFramework.Entities;
using malone.Core.Sample.EF.SqlServer.Middle.BL;
using malone.Core.Sample.EF.SqlServer.Middle.BL.Requests;
using malone.Core.Sample.EF.SqlServer.Middle.EL.Model;
using malone.Core.Sample.EF.SqlServer.mvc.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;

namespace malone.Core.Sample.EF.SqlServer.mvc.Controllers
{
    [Authorize]
    public class ListController : Controller
    {
        private UserService _userManager;
        private readonly ITodoListBC todoListBC;
        private readonly ITaskItemBC taskItemBC;
        private readonly Mapper mapper;

        public UserService UserManager
        {
            get
            {
                if (_userManager == null)
                    _userManager = HttpContext.GetOwinContext().Get<UserService>();
                return _userManager;
            }
        }

        public ListController(ITodoListBC todoListBC, ITaskItemBC taskItemBC, Mapper mapperInstance)
        {
            this.todoListBC = todoListBC.ThrowIfNull();
            this.taskItemBC = taskItemBC.ThrowIfNull();
            this.mapper = mapperInstance.ThrowIfNull();
        }

        #region Lists

        public ActionResult Index()
        {
            //Ejemplo para filtrar la lista
            var lists = GetUserLists();

            return View(new ListIndexViewModel
            {
                Listas = lists,
                NuevaLista = new TodoListViewModel()
            });
        }

        public ActionResult Details(int id)
        {
            var todoList = todoListBC.GetById(id, includeProperties: "Items,User");

            return View(new ListDetailsViewModel
            {
                Lista = AsViewModel(todoList),
                NuevaTarea = new TaskItemViewModel()
            });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(TodoListViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = CurrentUser();
                todoListBC.Add(new TodoList(model.Name, user));

                return RedirectToAction("Details", new { model.Id });
            }
            return View("Index", new ListIndexViewModel()
            {
                Listas = GetUserLists()
            });
        }

        public ActionResult Edit(int id)
        {
            var lists = GetUserLists();
            var todoList = todoListBC.GetById(id);

            return View("Index", new ListIndexViewModel
            {
                Listas = lists,
                EditarLista = AsViewModel(todoList)
            });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(TodoListViewModel model)
        {
            if (ModelState.IsValid)
            {
                var todoList = todoListBC.GetById(model.Id);
                todoList.UpdateName(model.Name);
                todoListBC.Update(todoList);

                return RedirectToAction("Index");
            }

            return View("Index", new ListIndexViewModel()
            {
                Listas = GetUserLists()
            });
        }

        public ActionResult Delete(int id)
        {
            var todoList = todoListBC.GetById(id);
            var lists = GetUserLists();

            return View("Index", new ListIndexViewModel
            {
                Listas = lists,
                EliminarLista = AsViewModel(todoList)
            });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(TodoList todoList)
        {
            todoListBC.Delete(todoList.Id);
            return RedirectToAction("Index");
        }

        #endregion

        #region Tasks

        [HttpPost]
        //[ValidateAntiForgeryToken] 
        public JsonResult CheckTask(int listId, int taskId)
        {
            var taskItem = taskItemBC.GetById(taskId);
            taskItem.ToggleDone();

            taskItemBC.Update(taskItem);

            var redirectUrl = new UrlHelper(Request.RequestContext).Action("Details", new { id = listId });
            return Json(new { Url = redirectUrl });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddTask(ListDetailsViewModel model)
        {
            var todoList = todoListBC.GetById(model.Lista.Id, includeProperties: "Items,User");
            model.Lista = AsViewModel(todoList);

            if (ModelState.IsValid)
            {
                //model.Lista.Items.Add(model.NuevaTarea);
                //taskItemBC.add(new TaskItem);
                return RedirectToAction("Details", new { id = model.Lista.Id });
            }
            return View("Details", model);
        }

        public ActionResult EditTask(int listId, int taskId)
        {
            var list = todoListBC.GetById(listId, includeProperties: "Items");

            return View("Details", new ListDetailsViewModel
            {
                Lista = AsViewModel( list),
                //EditarTarea = list.Items.Where(t => t.Id == taskId).FirstOrDefault()
            });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditTask(ListDetailsViewModel model)
        {
            //if (ModelState.IsValid)
            //{
            //    taskItemBC.Update(new UpdateListRequest()
            //    {
            //        model.EditarTarea
            //    });
            //    return RedirectToAction("Details", new { model.Lista.Id });
            //}
            //model.Lista = todoListBC.GetById(model.Lista.Id, includeProperties: "Items");
            return View("Details", model);
        }

        public ActionResult DeleteTask(int listId, int taskId)
        {
            taskItemBC.Delete(taskId);
            return RedirectToAction("Details", new { id = listId });
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

            return AsViewModelList(list);
        }

        protected IEnumerable<TodoListViewModel> AsViewModelList(IEnumerable<TodoList> list) => mapper.Map<IEnumerable<TodoList>, List<TodoListViewModel>>(list);

        protected TodoListViewModel AsViewModel(TodoList entity) => mapper.Map<TodoList, TodoListViewModel>(entity);

        #endregion
    }
}