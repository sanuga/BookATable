using BookATableMVC.Helper.EntityServices;
using BookATableMVC.ViewModels.Reservations;
using DAL.Entites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BookATableMVC.Helper.EntityServices;
using BookATableMVC.Helper;

namespace BookATableMVC.Controllers
{
    public class ReservationsController : Controller
    {
        // GET: Reservations
        public ActionResult Index()
        {
            ReservationListViewModel model = new ReservationListViewModel();

            
            model.Reservations = new ReservationService().GetAll(u => u.UserId == AthenticationService.LoggedUser.Id).ToList();
            if (model.Reservations == null)
            {
                return RedirectToAction("Index", "Home");
            }
            return View(model);
        }

        public ActionResult Edit(int? id)
        {
            Reservation reservation;
            
            if (!id.HasValue)
            {
                reservation = new Reservation();
            }else
            {
                ReservationService service = new ReservationService();
                reservation = service.GetById(id.Value);
                if (reservation == null)
                {
                    RedirectToAction("Index");

                }
            }
           

            
            ReservationAddEditViewModel model = new ReservationAddEditViewModel();
            model.Comment = reservation.Comment;
            model.PeopleCount = reservation.PeopleCount;
            model.RestaurantId = reservation.RestaurantId;
            model.UserId = reservation.UserId;
            model.ReservationTime = reservation.ReservationTime;
            model.Id = reservation.Id;
           

            return View(model);
        }

        [HttpPost]
        public ActionResult Edit(ReservationAddEditViewModel model)
        {
            
            ReservationService service = new ReservationService();
            TryUpdateModel(model);
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            Reservation reservation;
            if (model.Id == 0)
            {
                reservation = new Reservation();
            }
            else
            {
                reservation = service.GetById(model.Id);
                if (reservation == null)
                {
                    return RedirectToAction("List");
                }
            }

            reservation.Id = model.Id;
            reservation.Comment = model.Comment;
            reservation.PeopleCount = model.PeopleCount;
            reservation.RestaurantId = model.RestaurantId;
            reservation.ReservationTime = model.ReservationTime;
            reservation.UserId = model.UserId;



            service.Save(reservation);
            return RedirectToAction("Index");
        }

        public ActionResult Delete(int? id)
        {
            Reservation model;
            ReservationService service = new ReservationService();
            model = service.GetById(id.Value);
            if (model==null || model.IsDeleted)
            {
                return RedirectToAction("Index");
            }
            service.Delete(model);
            return RedirectToAction("Index");

        }
    }
}