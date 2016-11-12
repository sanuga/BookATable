using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DAL.Repositories;
using DAL.Entites;
using BookATableMVC.ViewModels.Restaurants;
using System.IO;

namespace BookATableMVC.Controllers
{
    public class RestaurantsController : Controller
    {
        // GET: Restaurants
        public ActionResult Index(string searchString)
        {
            RestaurantsRepositories repository = new RestaurantsRepositories();
            RestaurantListViewModel model = new RestaurantListViewModel();

            List<Restaurant> restaurants = null;


            ViewBag.searchString = searchString;

            if (!String.IsNullOrEmpty(searchString))
            {

                model.Restaurants = repository.GetAll(r => r.Name.Contains(searchString)).ToList();

            }
            else
            {
                model.Restaurants = repository.GetAll().ToList();
            }






            return View(model);
        
    }
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
                RestaurantsRepositories repository = new RestaurantsRepositories();
                restaurant = repository.GetById(id.Value);
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
        public ActionResult Edit()
        {
            RestaurantAddEditViewModel model = new RestaurantAddEditViewModel();
            RestaurantsRepositories repository = new RestaurantsRepositories();
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
                restaurant = repository.GetById(model.Id);
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

            repository.Save(restaurant);
            return RedirectToAction("Index");
        }
    
    public ActionResult Delete(int? id)
    {

        if (!id.HasValue)
        {
            return RedirectToAction("Index");
        }
        RestaurantsRepositories repo = new RestaurantsRepositories();
        Restaurant model = new Restaurant();
        model = repo.GetById(id.Value);
        if (model == null || model.IsDeleted)
        {
            return RedirectToAction("Index");
        }
        repo.Delete(model);
        return RedirectToAction("Index");
    }
}
}