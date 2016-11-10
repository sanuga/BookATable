using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DAL.Repositories;
using DAL.Entites;
using BookATableMVC.ViewModels.Restaurants;

namespace BookATableMVC.Controllers
{
    public class RestaurantsController : Controller
    {
        // GET: Restaurants
        public ActionResult Index()
        {
            RestaurantsRepositories repository = new RestaurantsRepositories();
            RestaurantListViewModel model = new RestaurantListViewModel();
            model.Restaurants = repository.GetAll().ToList();
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
            model.OpenHour = restaurant.OpenHour;
            model.CloseHour = restaurant.CloseHour;
            return View(model);
        }
        [HttpPost]
        public ActionResult Edit()
        {
            Restaurant model = new Restaurant();
            TryUpdateModel(model);
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            RestaurantsRepositories repository = new RestaurantsRepositories();
            repository.Save(model);
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
            model =  repo.GetById(id.Value);
            if (model == null || model.IsDeleted)
	        {
                return RedirectToAction("Index");
	        }
            repo.Delete(model);
            return RedirectToAction("Index");
        }
    }
}