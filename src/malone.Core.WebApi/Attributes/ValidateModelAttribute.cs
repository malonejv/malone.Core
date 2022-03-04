namespace malone.Core.WebApi.Attributes
{
	using System.Net;
	using System.Net.Http;
	using System.Web.Http.Controllers;
	using System.Web.Http.Filters;

	//TODO: Agregar por coniguración
	/// <summary>
	/// Defines the <see cref="ValidateModelAttribute" />.
	/// </summary>
	public class ValidateModelAttribute : ActionFilterAttribute
	{
		/// <summary>
		/// The OnActionExecuting.
		/// </summary>
		/// <param name="actionContext">The actionContext<see cref="HttpActionContext"/>.</param>
		public override void OnActionExecuting(HttpActionContext actionContext)
		{
			if (actionContext.ModelState.IsValid == false)
			{
				actionContext.Response = actionContext.Request.CreateErrorResponse(
					HttpStatusCode.BadRequest, actionContext.ModelState);
			}
		}
	}
}
