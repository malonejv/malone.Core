using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using malone.Core.Sample.Middle.BL;
using malone.Core.Sample.Middle.EL;

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
            var list = TodoListBC.GetAll(includeProperties: "Items");
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
}