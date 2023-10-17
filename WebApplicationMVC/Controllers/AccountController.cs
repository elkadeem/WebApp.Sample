using Microsoft.Owin.Security;
using Microsoft.Owin.Security.Cookies;
using System.Collections.Generic;
using System.Security.Claims;
using System.Web;
using System.Web.Mvc;

namespace WebApplicationMVC.Controllers
{
    public class AccountController : Controller
    {        
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(LoginViewModel viewModel)
        {
            if (!ModelState.IsValid)
                return View(viewModel);

            var claims = new List<Claim>{
                    new Claim(ClaimTypes.Name, viewModel.UserName)
                    , new Claim(ClaimTypes.Email, viewModel.UserName)
                    , new Claim("NationId", "Id value from identity store")
                    
                };

            var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationType);
            var authProperties = new AuthenticationProperties
            {
                IsPersistent = true,
            };
                        
            Request.GetOwinContext()
                .Authentication
                .SignIn(authProperties
                , claimsIdentity);

            return RedirectToAction("Index", "Home");
        }

        // Logout action
        public ActionResult Logout()
        {
            Request.GetOwinContext()
                .Authentication
                .SignOut(CookieAuthenticationDefaults.AuthenticationType);

            return RedirectToAction("Index", "Home");
        }
    }

    public class LoginViewModel
    {
        public string UserName { get; set; }

        public string Password { get; set; }
    }
}