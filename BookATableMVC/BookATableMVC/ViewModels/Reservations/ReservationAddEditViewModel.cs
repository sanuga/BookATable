﻿using DAL.Entites;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BookATableMVC.ViewModels.Reservations
{
    public class ReservationAddEditViewModel
    {
        public int Id { get; set; }
        [Required]
        public DateTime ReservationTime { get; set; }
        [Required]
        [Range(1, Int32.MaxValue)]
        public int PeopleCount { get; set; }
        public string Comment { get; set; }
        public int UserId { get; set; }
        public int RestaurantId { get; set; }
        
        
    }
}