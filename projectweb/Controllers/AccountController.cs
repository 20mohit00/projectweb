using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using projectweb.Models;
using System.Web.Security;

namespace projectweb.Controllers
{
    [AllowAnonymous]
    public class AccountController : Controller
    {
        // GET: Account
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(Models.Membership model)
        {
            using (var context = new CrudEntities())
            {
                bool isValid = context.login.Any(x => x.Username == model.Username && x.Password == model.Password);
                if (isValid)
                {
                    FormsAuthentication.SetAuthCookie(model.Username,false);
                    return RedirectToAction("Index","employees");
                }
                ModelState.AddModelError("", "Invalid Username and Password!!");
                return View();
            }
            
        }

        public ActionResult SignUp()
        {
            return View();
        }

        [HttpPost]
        public ActionResult SignUp(login model)
        {
            try
            {
                using (var context = new CrudEntities())
                {
                    context.login.Add(model);
                    context.SaveChanges();
                }
            } catch (Exception e ) 
            {
                ViewBag.message = "plzz fill all the fields!";
                return View();
            }
            return RedirectToAction("Login");
        }
        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Login");
        }
    }
}