namespace malone.Core.WebApi.Attributes
{
	using System;
	using System.Net;
	using System.Net.Http;
	using System.Text;
	using System.Web.Http.Controllers;
	using System.Web.Http.Filters;

	/// <summary>
	/// Defines the <see cref="T: RequireHttpsAttribute" />.
	/// </summary>
	public class RequireHttpsAttribute : AuthorizationFilterAttribute
	{
		/// <summary>
		/// The OnAuthorization.
		/// </summary>
		/// <param name="actionContext">The actionContext<see cref="T: HttpActionContext"/>.</param>
		public override void OnAuthorization(HttpActionContext actionContext)
		{
			if (actionContext.Request.RequestUri.Scheme != Uri.UriSchemeHttps)
			{
				actionContext.Response = actionContext.Request.CreateResponse(HttpStatusCode.Found);
				actionContext.Response.Content = new StringContent("<p>Use HTTPS instead of HTTP</p>", Encoding.UTF8, "text/html");

				UriBuilder uriBuilder = new UriBuilder(actionContext.Request.RequestUri);
				uriBuilder.Scheme = Uri.UriSchemeHttps;
				//OPTION Agregar en configuracion el puerto https
				uriBuilder.Port = 443;

				actionContext.Response.Headers.Location = uriBuilder.Uri;
			}
			else
			{
				base.OnAuthorization(actionContext);
			}
		}
	}
}
