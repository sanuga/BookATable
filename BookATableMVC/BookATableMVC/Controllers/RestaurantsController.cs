using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DAL.Repositories;
using DAL.Entites;
using BookATableMVC.ViewModels.Restaurants;
using System.IO;
using BookATableMVC.Helper.EntityServices;
using BookATableMVC.ViewModels;
using BookATableMVC.Filters;

namespace BookATableMVC.Controllers
{
//    [AuthorizationFilter]
    public class RestaurantsController : Controller
    {
        [AuthenticationFilter]
        // GET: Restaurants
        public ActionResult Index(string searchString, string sortOrder,int? page)
        {
            
            RestaurantListViewModel model = new RestaurantListViewModel();
            model.Restaurants = new RestaurantService().GetAll().ToList();

            //model.Pager = new PagerViewModel(model.Restaurants.Count(), page);
            //model.Restaurants = model.Restaurants.Skip((model.Pager.CurrentPage - 1) * model.Pager.ItemsPerPage).Take(model.Pager.ItemsPerPage).ToList();

            /*
            ViewBag.searchString = searchString;
            RestaurantService service = new RestaurantService();
            if (!String.IsNullOrEmpty(searchString))
            {

                model.Restaurants =service.GetAll(r => r.Name.Contains(searchString)).ToList();

            }
            else
            {
                model.Restaurants = service.GetAll().ToList();
            }

            ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewBag.OpenHourSortParm = sortOrder == "Open" ? "open_desc" : "Open";
            ViewBag.CloseHourSortParm = sortOrder == "Close" ? "close_desc" : "Close";
            ViewBag.AddressSortParm = sortOrder == "Address" ? "address_desc" : "Address";
            ViewBag.CapacitySortParm = sortOrder == "Capacity" ? "capacity_desc" : "Capacity";
           
            switch (sortOrder)
            {
                case "name_desc":
                    model.Restaurants = model.Restaurants.OrderByDescending(s => s.Name).ToList();
                    break;
                case "Open":
                    model.Restaurants = model.Restaurants.OrderBy(s => s.OpenHour).ToList();
                    break;
                case "open_desc":
                    model.Restaurants = model.Restaurants.OrderByDescending(s => s.OpenHour).ToList();
                    break;
                case "Close":
                    model.Restaurants = model.Restaurants.OrderBy(s => s.CloseHour).ToList();
                    break;
                case "close_desc":
                    model.Restaurants = model.Restaurants.OrderByDescending(s => s.CloseHour).ToList();
                    break;
                case "Address":
                    model.Restaurants = model.Restaurants.OrderBy(s => s.Address).ToList();
                    break;
                case "address_desc":
                    model.Restaurants = model.Restaurants.OrderByDescending(s => s.Address).ToList();
                    break;
                case "Capacity":
                    model.Restaurants = model.Restaurants.OrderBy(s => s.Capacity).ToList();
                    break;
                case "capacity_desc":
                    model.Restaurants = model.Restaurants.OrderByDescending(s => s.Capacity).ToList();
                    break;
                default:
                    model.Restaurants = model.Restaurants.OrderBy(s => s.Name).ToList();
                    break;
            }
            */

            return View(model);
        
    }
        [AuthenticationFilter]
        //GET Action
        public ActionResult Edit(int? id)
        {
            Restaurant restaurant;
            if (!id.HasValue)
            {
                restaurant = new Restaurant();
            }
            else
            {
                RestaurantService service = new RestaurantService();
                restaurant = service.GetById(id.Value);
                if (restaurant == null)
                {
                    return RedirectToAction("Index");
                }
            }
            RestaurantAddEditViewModel model = new RestaurantAddEditViewModel();


            model.Id = restaurant.Id;
            model.Name = restaurant.Name;
            model.Address = restaurant.Address;
            model.Email = restaurant.Email;
            model.Phone = restaurant.Phone;
            model.CloseHour = restaurant.CloseHour;
            model.OpenHour = restaurant.OpenHour;
            model.ImagePath = restaurant.ImagePath;
            return View(model);
        }
        
        [HttpPost]
        public ActionResult Edit(RestaurantAddEditViewModel model)
        {
            
            RestaurantService service = new RestaurantService();
            TryUpdateModel(model);
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            Restaurant restaurant;
            if (model.Id == 0)
            {
                restaurant = new Restaurant();
            }
            else
            {
                restaurant = service.GetById(model.Id);
                if (restaurant == null)
                {
                    return RedirectToAction("List");
                }
            }

            if (model.ImageUpload != null && model.ImageUpload.ContentLength > 0)
            {
                var imageExtension = Path.GetExtension(model.ImageUpload.FileName);

                if (String.IsNullOrEmpty(imageExtension) || !imageExtension.Equals(".jpg", StringComparison.OrdinalIgnoreCase))
                {
                    ModelState.AddModelError(string.Empty, "Wrong image format.");
                }
                else
                {
                    string filePath = Server.MapPath("~/Uploads/");
                    model.ImagePath = model.ImageUpload.FileName;
                    model.ImageUpload.SaveAs(filePath + model.ImagePath);
                }
            }
            else
            {
                model.ImagePath = "default.jpg";
            }


            restaurant.Id = model.Id;
            restaurant.Name = model.Name;
            restaurant.Address = model.Address;
            restaurant.Email = model.Email;
            restaurant.Phone = model.Phone;
            restaurant.OpenHour = model.OpenHour;
            restaurant.CloseHour = model.CloseHour;
            restaurant.ImagePath = model.ImagePath;

            service.Save(restaurant);
            return RedirectToAction("Index");
        }
        
        public ActionResult Delete(int? id)
    {

        if (!id.HasValue)
        {
            return RedirectToAction("Index");
        }
            RestaurantService service = new RestaurantService();
        Restaurant model = new Restaurant();
        model = service.GetById(id.Value);
        if (model == null || model.IsDeleted)
        {
            return RedirectToAction("Index");
        }
        service.Delete(model);
        return RedirectToAction("Index");
    }
        
}
}