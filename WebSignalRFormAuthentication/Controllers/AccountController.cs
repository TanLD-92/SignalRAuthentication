using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using WebSignalRFormAuthentication.Models;

namespace WebSignalRFormAuthentication.Controllers
{
    [AllowAnonymous]
    public class AccountController : Controller
    {
        // GET: Account
        public ActionResult Login()
        {
            return View();
        }
        public ActionResult Register()
        {
            return View();
        }
        [HttpPost]
        [ActionName("Register")]
        public ActionResult Signup(Register register)
        {
            //if (ModelState.IsValid && (register.Password == register.ConfirmPassword))
            //{
                using (var dataContext = new DataContext())
                {
                if (!CheckExistUser(register) && (register.Password == register.ConfirmPassword))
                {
                    dataContext.Registers.Add(register);
                    dataContext.SaveChanges();
                    FormsAuthentication.SetAuthCookie(register.UserName, true);
                    return RedirectToAction("Index", "Home");
                }
                else return View(register);

                }
            //}
            //return false;
        }
        [HttpPost]
        [ActionName("Login")]
        public ActionResult Signin(LoginModel loginModel)
        {
            using (var dataContext = new DataContext())
            {
                var user = (from user1 in dataContext.Registers where user1.UserName.Equals(loginModel.UserName) && user1.Password.Equals(loginModel.Password) select user1).FirstOrDefault();
                if (user != null)
                {
                    FormsAuthentication.SetAuthCookie(loginModel.UserName, true);
                    return RedirectToAction("Index", "Home");
                }
                else return View(loginModel);
            }
        }
        [HttpPost]
        [ActionName("SignOut")]
        public ActionResult PostSignOut()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("index", "home");
        }
        public bool CheckExistUser(Register register)
        {
            using (var dataContext = new DataContext())
            {
                var user = (from user1 in dataContext.Registers where user1.UserName.Equals(register.UserName) && user1.Password.Equals(register.Password) && (register.Password == register.ConfirmPassword) select user1).FirstOrDefault();
                if (user != null)
                {
                    return true;
                }
                else return false;
            }
        }
    }
}