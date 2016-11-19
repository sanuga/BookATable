using BookATableMVC.Services.EntityServices;
using DAL.Entites;
using DAL.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BookATableMVC.Helper.EntityServices
{
    public class ReservationService:BaseService<Reservation>
    {
        public ReservationService():base()
        {
                
        }

        public ReservationService(UnitOfWork unitOfWork):base(unitOfWork)
        {

        }
    }
}