using BookATableMVC.ViewModels.Restaurants;
using DAL.Entites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DAL.Repositories;
using BookATableMVC.Helper;

namespace BookATableMVC.Controllers
{
    public abstract class BaseController <T, VM, LVM>: Controller
        where T : BaseEntity, new()
        where VM : RestaurantAddEditViewModel, new()
        where LVM: RestaurantListViewModel, new()
    {

        protected abstract BaseRepository<T> GetRepository();
        protected abstract ActionResult Redirect(T item = null);

        // GET: Base
        public  ActionResult Index()
        {
            if (AthenticationService.LoggedUser == null)
            {
                return RedirectToAction("Index", "Home");
            }
            LVM itemLVM = new LVM();
            BaseRepository<T> repo = GetRepository();
            TryUpdateModel(itemLVM);

            string controllerName = GetController();
            string actionName = GetAction();
            return View(itemLVM);

        }
        protected string GetAction()
        {
            return this.ControllerContext.RouteData.Values["action"].ToString();
        }
        protected string GetController()
        {
            return this.ControllerContext.RouteData.Values["controller"].ToString();
        }
    }
}