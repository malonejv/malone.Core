using malone.Core.Identity.EntityFramework;
using malone.Core.Identity.EntityFramework.Entities;
using malone.Core.Sample.EF.Firebird.Middle.BL;
using malone.Core.Sample.EF.Firebird.Middle.EL.Filters.EF.TodoListEntity;
using malone.Core.Sample.EF.Firebird.Middle.EL.Model;
using malone.Core.Sample.EF.Firebird.mvc.Attributes;
using malone.Core.Sample.EF.Firebird.mvc.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace malone.Core.Sample.EF.Firebird.mvc.Controllers
{
    [Authorize]
    public class ListController : Controller
    {
        private UserBusinessComponent _userManager;

        public ITodoListBC TodoListBC { get; set; }
        public ITaskItemBC TaskItemBC { get; set; }
        public UserBusinessComponent UserManager
        {
            get
            {
                if (_userManager == null)
                    _userManager = HttpContext.GetOwinContext().Get<UserBusinessComponent>();
                return _userManager;
            }
        }

        public ListController(ITodoListBC todoListBC, ITaskItemBC taskItemBC)
        {
            TodoListBC = todoListBC;
            TaskItemBC = taskItemBC;
        }

        #region Lists

        public ActionResult Index()
        {
            //Ejemplo para filtrar la lista
            var lists = GetUserLists();

            return View(new ListIndexViewModel
            {
                Listas = lists,
                NuevaLista = new TodoList()
            });
        }

        public ActionResult Details(int id)
        {
            var todoList = TodoListBC.GetById(id, includeProperties: "Items,User");

            return View(new ListDetailsViewModel
            {
                Lista = todoList,
                NuevaTarea = new TaskItem()
            });
        }

        [HttpPost]
        [ValidateAntiForgeryToken] //Annotation added
        public ActionResult Create(ListIndexViewModel model)
        {
            if (ModelState.IsValid)
            {
                model.NuevaLista.User = GetLoggedUser();
                TodoListBC.Add(model.NuevaLista);

                return RedirectToAction("Details", new { model.NuevaLista.Id });
            }
            model.Listas = GetUserLists();
            return View("Index", model);
        }

        public ActionResult Edit(int id)
        {
            var todoList = TodoListBC.GetById(id);
            var lists = GetUserLists();

            return View("Index", new ListIndexViewModel
            {
                Listas = lists,
                EditarLista = todoList
            });
        }

        [HttpPost]
        [ValidateAntiForgeryToken] //Annotation added
        public ActionResult Edit(ListIndexViewModel model)
        {
            if (ModelState.IsValid)
            {
                TodoListBC.Update(model.EditarLista);
                return RedirectToAction("Index");
            }
            model.Listas = GetUserLists();
            return View("Index", model);
        }

        public ActionResult Delete(int id)
        {
            var todoList = TodoListBC.GetById(id);
            var lists = GetUserLists();

            return View("Index", new ListIndexViewModel
            {
                Listas = lists,
                EliminarLista = todoList
            });
        }

        [HttpPost]
        [ValidateAntiForgeryToken] //Annotation added
        public ActionResult Delete(TodoList todoList)
        {
            TodoListBC.Delete(todoList.Id);
            return RedirectToAction("Index");
        }

        #endregion

        #region Tasks

        [HttpPost]
        //[ValidateAntiForgeryToken] //Annotation added
        public JsonResult CheckTask(int listId, int taskId)
        {
            var taskItem = TaskItemBC.GetById(taskId);
            taskItem.Done = !taskItem.Done;

            TaskItemBC.Update(taskItem);

            var redirectUrl = new UrlHelper(Request.RequestContext).Action("Details", new { id = listId });
            return Json(new { Url = redirectUrl });
        }

        [HttpPost]
        [ValidateAntiForgeryToken] //Annotation added
        public ActionResult AddTask(ListDetailsViewModel model)
        {
            model.Lista = TodoListBC.GetById(model.Lista.Id, includeProperties: "Items,User");

            if (ModelState.IsValid)
            {
                model.Lista.Items.Add(model.NuevaTarea);
                TodoListBC.Update(model.Lista);
                return RedirectToAction("Details", new { id = model.Lista.Id });
            }
            return View("Details", model);
        }

        public ActionResult EditTask(int listId, int taskId)
        {
            var list = TodoListBC.GetById(listId, includeProperties: "Items");

            return View("Details", new ListDetailsViewModel
            {
                Lista = list,
                EditarTarea = list.Items.Where(t => t.Id == taskId).FirstOrDefault()
            });
        }

        [HttpPost]
        [ValidateAntiForgeryToken] //Annotation added
        public ActionResult EditTask(ListDetailsViewModel model)
        {
            if (ModelState.IsValid)
            {
                TaskItemBC.Update(model.EditarTarea);
                return RedirectToAction("Details", new { model.Lista.Id });
            }
            model.Lista = TodoListBC.GetById(model.Lista.Id, includeProperties: "Items");
            return View("Details", model);
        }

        public ActionResult DeleteTask(int listId, int taskId)
        {
            TaskItemBC.Delete(taskId);
            return RedirectToAction("Details", new { id = listId });
        }

        #endregion

        private CoreUser GetLoggedUser()
        {
            var userId = User.Identity.GetUserId<int>();
            return UserManager.FindById(userId);
        }

        private IEnumerable<TodoList> GetUserLists()
        {
            var userId = User.Identity.GetUserId<int>();

            return TodoListBC.Get(new EFTodoListGetRequest()
            {
                Expression = (l => l.User.Id == userId)
            }, includeProperties: "Items,User");
        }

    }
}