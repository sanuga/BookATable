using BookATableMVC.ViewModels.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DAL.Entites;
using BookATableMVC.Services.EntityServices;
using BookATableMVC.Helper;

namespace BookATableMVC.Controllers
{
    public class AccountsController : Controller
    {
        // GET: Accounts
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Register()
        {
            UserAddEditViewModel model = new UserAddEditViewModel();
            return View(model);
        }
        [HttpPost]
        public ActionResult Register(UserAddEditViewModel model)
        {
            TryUpdateModel(model);
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            User user = new User();
            user.Email = model.Email;
            user.Password = model.Password;
            user.Name = model.Name;
            user.Phone = model.Phone;
            UserService service = new UserService();
            service.Save(user);

            return RedirectToAction("Index");
        }
        public ActionResult Login()
        {
            UserAuthenticationViewModel model = new UserAuthenticationViewModel();
            return View(model);
        }
        [HttpPost]
        public ActionResult Login(UserAuthenticationViewModel model)
        {
            TryUpdateModel(model);
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            AthenticationService.Authenticate(model.Email, model.Password);
            if (AthenticationService.LoggedUser != null)
            {
                return RedirectToAction("Index");
            }
            return View(model);
        }
        public ActionResult Logout()
        {
            AthenticationService.Logout();
            return RedirectToAction("Index", "Home");
        }
    }
}