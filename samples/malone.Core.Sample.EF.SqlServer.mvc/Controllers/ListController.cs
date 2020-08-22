using malone.Core.Sample.EF.SqlServer.Middle.BL;
using malone.Core.Sample.EF.SqlServer.Middle.EL.Filters.EF.TodoListEntity;
using malone.Core.Sample.EF.SqlServer.Middle.EL.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace malone.Core.Sample.EF.SqlServer.mvc.Controllers
{
    public class ListController : Controller
    {
        public ITodoListBC TodoListBC { get; set; }

        public ListController(ITodoListBC todoListBC)
        {
            TodoListBC = todoListBC;
        }

        public ActionResult Index()
        {
            var list = TodoListBC.GetAll(includeProperties: "Items");

            //Ejemplo para filtrar la lista
            //var list2 = TodoListBC.Get(new EFTodoListGetRequest()
            //{
            //    Expression = (x => x.Name.Contains("List"))
            //});

            return View(list);
        }

        public ActionResult Details(int id)
        {
            var todoList = TodoListBC.GetById(id);

            return View(todoList);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken] //Annotation added
        public ActionResult Create(TodoList todoList)
        {
            if (ModelState.IsValid)
            {
                TodoListBC.Add(todoList);
                return RedirectToAction("Index");
            }
            return View(todoList);
        }

        public ActionResult Edit(int id)
        {
            var todoList = TodoListBC.GetById(id);

            return View(todoList);
        }

        [HttpPost]
        [ValidateAntiForgeryToken] //Annotation added
        public ActionResult Edit(TodoList todoList)
        {
            if (ModelState.IsValid)
            {
                TodoListBC.Update(todoList.Id, todoList);
                return RedirectToAction("Index");
            }
            return View(todoList);
        }

        public ActionResult Delete(int id)
        {
            var todoList = TodoListBC.GetById(id);

            return View(todoList);
        }

        [HttpPost]
        [ValidateAntiForgeryToken] //Annotation added
        public ActionResult Delete(TodoList todoList)
        {
            TodoListBC.Delete(todoList.Id);
            return RedirectToAction("Index");
        }
    }
}