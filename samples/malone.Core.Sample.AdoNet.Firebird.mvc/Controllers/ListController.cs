using System.Web.Mvc;
using malone.Core.Sample.AdoNet.Firebird.Middle.BL;
using malone.Core.Sample.AdoNet.Firebird.Middle.EL.Filters;
using malone.Core.Sample.AdoNet.Firebird.Middle.EL.Model;

namespace malone.Core.Sample.AdoNet.Firebird.mvc.Controllers
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

            ////Ejemplo para filtrar la lista
            //var list = TodoListBC.Get(new TodoListGetRequest()
            //{
            //    Name = "UE"
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
                TodoListBC.Update(todoList);
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