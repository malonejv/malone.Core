using System.Collections.Generic;
using System.Web;
using System.Web.Mvc;
using AutoMapper;
using malone.Core.Commons.Helpers.Extensions;
using malone.Core.EF.Entities.Filters;
using malone.Core.Identity.AdoNet.Entities;
using malone.Core.Sample.AN.SqlServer.Middle;
using malone.Core.Sample.AN.SqlServer.Middle.BL;
using malone.Core.Sample.AN.SqlServer.Middle.EL.Model;
using malone.Core.Sample.AN.SqlServer.mvc.Attributes;
using malone.Core.Sample.AN.SqlServer.mvc.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;


namespace malone.Core.Sample.AN.SqlServer.mvc.Controllers
{
    public class TaskController : Controller
    {
        private ApplicationUserManager _userManager;
        private readonly ITodoListBC todoListBC;
        private readonly ITaskItemBC taskItemBC;
        private readonly Mapper mapper;

        public ApplicationUserManager UserManager
        {
            get
            {
                if (_userManager == null)
                    _userManager = HttpContext.GetOwinContext().Get<ApplicationUserManager>();
                return _userManager;
            }
        }

        public TaskController(ITodoListBC todoListBC, ITaskItemBC taskItemBC, Mapper mapperInstance)
        {
            this.todoListBC = todoListBC.ThrowIfNull();
            this.taskItemBC = taskItemBC.ThrowIfNull();
            this.mapper = mapperInstance.ThrowIfNull();
        }

        #region Tasks

        [HttpPost]
        [ValidateHeaderAntiForgeryToken]
        public JsonResult ToggleDone(int listId, int taskId)
        {
            var taskItem = taskItemBC.GetById(taskId);
            taskItem.ToggleDone();

            taskItemBC.Update(taskItem);

            var redirectUrl = new UrlHelper(Request.RequestContext).Action("Detail", "List", new { id = listId });
            return Json(new { Url = redirectUrl });
        }

        public ActionResult Create(int listId) =>
            PartialView("_Create", TaskItemViewModel.CreateFromEntity(listId, new TaskItem()));

        [HttpPost]
        [ValidateHeaderAntiForgeryToken]
        public ActionResult Create(TaskItemViewModel model)
        {
            if (ModelState.IsValid)
            {
                var todoList = todoListBC.GetById(model.ListId, includeProperties: "Items,User");
                var taskItem = new TaskItem(model.Description);
                todoList.AddItem(taskItem);

                todoListBC.Update(todoList);
                return Json(new { Url = Url.Action("Detail", "List", new { Id = model.ListId }) });
            }
            else
                return PartialView("_Create", model);
        }

        [Route("List/{listId:int}/Task/Edit/{taskId:int}")]
        public ActionResult Edit(int listId, int taskId)
        {
            var taskItem = taskItemBC.GetById(taskId);

            return PartialView("_Edit", TaskItemViewModel.CreateFromEntity(listId, taskItem));
        }

        [HttpPost]
        [ValidateHeaderAntiForgeryToken]
        public ActionResult Edit(TaskItemViewModel model)
        {
            if (ModelState.IsValid)
            {
                var taskItem = taskItemBC.GetById(model.Id);
                taskItem.UpdateDescription(model.Description);
                taskItemBC.Update(taskItem);

                return Json(new { Url = Url.Action("Detail", "List", new { Id = model.ListId }) });
            }
            else
                return PartialView("_Edit", model);
        }

        [Route("List/{listId:int}/Task/Delete/{taskId:int}")]
        public ActionResult Delete(int listId, int taskId)
        {
            taskItemBC.Delete(taskId);
            return Json(new { Url = Url.Action("Detail", "List", new { Id = listId }) }, JsonRequestBehavior.AllowGet);
        }

        #endregion

    }
}