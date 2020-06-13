using malone.Core.Sample.Middle.BL;
using malone.Core.Sample.Middle.EL.Filters.EF.TodoListEntity;
using malone.Core.Sample.Middle.EL.Model;
using System.Linq;
using System.Web.Mvc;

namespace malone.Core.Sample.UI.EFSqlServer.Controllers
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

            var list2 = TodoListBC.Get(new EFTodoListGetRequest()
            {
                Expression = (x => x.Name.Contains("List"))
            });


            var item = list.First();
            item.Name = "asdf";
            TodoListBC.Update(item.Id, item);


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