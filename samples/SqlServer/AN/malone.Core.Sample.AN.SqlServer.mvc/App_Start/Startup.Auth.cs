using System;
using malone.Core.Identity.AdoNet.Entities;
using malone.Core.Sample.AN.SqlServer.Middle;
using malone.Core.Sample.AN.SqlServer.Middle.DAL.Context;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.Cookies;
using Owin;

namespace malone.Core.Sample.AN.SqlServer.mvc
{
    public partial class Startup
    {
        //private static string clientId = ConfigurationManager.AppSettings["ida:ClientId"];
        //private static string aadInstance = EnsureTrailingSlash(ConfigurationManager.AppSettings["ida:AADInstance"]);
        //private static string tenantId = ConfigurationManager.AppSettings["ida:TenantId"];
        //private static string postLogoutRedirectUri = ConfigurationManager.AppSettings["ida:PostLogoutRedirectUri"];
        //private static string authority = aadInstance + tenantId + "/v2.0";

        // For more information on configuring authentication, please visit https://go.microsoft.com/fwlink/?LinkId=301864
        public void ConfigureAuth(IAppBuilder app)
        {
            app.SetDefaultSignInAsAuthenticationType(CookieAuthenticationDefaults.AuthenticationType);

            // Configure the db context, user manager and signin manager to use a single instance per request
            app.CreatePerOwinContext(SampleContext.Create);
            app.CreatePerOwinContext<ApplicationUserManager>(ApplicationUserManager.Create);
            app.CreatePerOwinContext<ApplicationSignInManager>(ApplicationSignInManager.Create);

            // Enable the application to use a cookie to store information for the signed in user
            // and to use a cookie to temporarily store information about a user logging in with a third party login provider
            // Configure the sign in cookie
            app.UseCookieAuthentication(new CookieAuthenticationOptions
            {
                AuthenticationType = DefaultAuthenticationTypes.ApplicationCookie,
                LoginPath = new PathString("/Account/Login"),
                Provider = new CookieAuthenticationProvider
                {
                    // Enables the application to validate the security stamp when the user logs in.
                    // This is a security feature which is used when you change a password or add an external login to your account.  
                    OnValidateIdentity = SecurityStampValidator.OnValidateIdentity<ApplicationUserManager, CoreUser, int>(
                        validateInterval: TimeSpan.FromMinutes(30),
                        regenerateIdentityCallback: (manager, user) => manager.CreateIdentityAsync(user, DefaultAuthenticationTypes.ApplicationCookie),
                        getUserIdCallback: claims => claims.GetUserId<int>()
                    )
                }
            });

            //app.UseOpenIdConnectAuthentication(
            //    new OpenIdConnectAuthenticationOptions
            //    {
            //        ClientId = clientId,
            //        Authority = authority,
            //        PostLogoutRedirectUri = postLogoutRedirectUri,

            //        Notifications = new OpenIdConnectAuthenticationNotifications()
            //        {
            //            SecurityTokenValidated = (context) =>
            //            {
            //                string name = context.AuthenticationTicket.Identity.FindFirst("preferred_username").Value;
            //                context.AuthenticationTicket.Identity.AddClaim(new Claim(ClaimTypes.Name, name, string.Empty));
            //                return System.Threading.Tasks.Task.FromResult(0);
            //            }
            //        }
            //    });
            
        }

        private static string EnsureTrailingSlash(string value)
        {
            if (value == null)
            {
                value = string.Empty;
            }

            if (!value.EndsWith("/", StringComparison.Ordinal))
            {
                return value + "/";
            }

            return value;
        }
    }
}