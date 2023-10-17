using Microsoft.Owin;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.Cookies;
using Owin;

namespace WebApplicationMVC
{
    public partial class Startup
    {
        private const string ApplicayionName = "sampleApp";
        private const string CookieName = "sample.Cookies";
        public static void ConfigureAuth(IAppBuilder app)
        {

            app.SetDefaultSignInAsAuthenticationType(CookieAuthenticationDefaults.AuthenticationType);

            app.UseCookieAuthentication(new CookieAuthenticationOptions()
            {                
                CookieName = CookieName,
                CookiePath = "/",
                LoginPath = new PathString("/Account/Login"),
                LogoutPath = new PathString("/Account/Logout"), 
                ExpireTimeSpan = System.TimeSpan.FromMinutes(30),
            });
        }
    }
}