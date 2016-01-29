using ClinicForAnimal.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using System.Web.UI.WebControls;

namespace ClinicForAnimal.Controllers
{
    public class MyAccountController : Controller
    {
        //
        // GET: /MyAccount/
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult LogIn(LogIn login, string ReturnUrl = "")
        {
            Users user = new Users();
            var list = user.Select();
            for (int i = 0; i < list.Count; i++)
            {
                if (list[i].UserName.Equals(login.UserName) && list[i].Password.Equals(login.Password))
                {
                    if (user != null)
                    {
                        FormsAuthentication.SetAuthCookie(list[i].UserName, login.RememberMe);
                        if (Url.IsLocalUrl(ReturnUrl))
                        {
                            return Redirect(ReturnUrl);
                        }
                        else
                        {
                            return RedirectToAction("MyProfile", "Home");
                        }
                    }
                    ModelState.Remove("Password");
                }
                else
                {
                    HttpNotFound();
                }
            }
            

            return View();
        }
        [Authorize]
        public ActionResult LogOut()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Index", "Home");
        }
        public ActionResult Some()
        {
            Users user = new Users();
            var a = user.Select();
            return View(a);
        }
        public ActionResult AddUser()
        {
            return View();
        }
        [HttpPost]
        public ActionResult AddUser(Users user)
        {
            user.AddUser(user.UserName, user.Password, user.FirstName, user.LastName, user.EmailId);
            return View(user);
        }
    }
}