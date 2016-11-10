using BookATableMVC.ViewModels.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DAL.Entites;
using BookATableMVC.Helper;
using BookATableMVC.Services.EntityServices;

namespace BookATableMVC.Controllers
{
    public class UserController : Controller
    {
        // GET: User
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Edit()
        {
            UserAddEditViewModel model = new UserAddEditViewModel();
            User user =  AthenticationService.LoggedUser;
            if (AthenticationService.LoggedUser!=null)
            {
                model.Id = user.Id;
                model.Email = user.Email;
                model.Password = user.Password;
                model.Name = user.Name;
                model.Phone = user.Phone;
                
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
            
            return View(model);
        }
        [HttpPost]
        public ActionResult Edit(UserAddEditViewModel model)
        {
            TryUpdateModel(model);
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            User user = new User();
            user.Id = model.Id;
            user.Email = model.Email;
            user.Name = model.Name;
            user.Password = model.Password;
            user.Phone = model.Phone;
            UserService service = new UserService();
            service.Save(user);
            AthenticationService.Logout();
            return RedirectToAction("Login", "Accounts");
        }
    }
}