namespace malone.Core.WebApi.Attributes
{
	using System;
	using System.Net;
	using System.Net.Http;
	using System.Security.Principal;
	using System.Text;
	using System.Threading;
	using System.Web.Http.Controllers;
	using System.Web.Http.Filters;

	/// <summary>
	/// Defines the <see cref="BasicAuthenticationAttribute" />.
	/// </summary>
	public class BasicAuthenticationAttribute : AuthorizationFilterAttribute
	{
		/// <summary>
		/// The OnAuthorization.
		/// </summary>
		/// <param name="actionContext">The actionContext<see cref="HttpActionContext"/>.</param>
		public override void OnAuthorization(HttpActionContext actionContext)
		{
			if (actionContext.Request.Headers.Authorization == null)
			{
				actionContext.Response = actionContext.Request.CreateResponse(HttpStatusCode.Unauthorized);
			}
			else
			{
				string authenticationToken = actionContext.Request.Headers.Authorization.Parameter;
				string decodedAuthenticationToken = Encoding.UTF8.GetString(Convert.FromBase64String(authenticationToken));
				string[] authenticationValues = decodedAuthenticationToken.Split(':');
				string username = authenticationValues[0];
				string password = authenticationValues[1];


				if (true) //TODO: Reemplazar con LoginComponent.Login(username,password)
				{
					Thread.CurrentPrincipal = new GenericPrincipal(new GenericIdentity(username), null);
				}
				else
				{
					actionContext.Response = actionContext.Request.CreateResponse(HttpStatusCode.Unauthorized);
				}

			}

			base.OnAuthorization(actionContext);
		}
	}
}
