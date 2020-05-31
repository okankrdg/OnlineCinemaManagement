using CinemaManagement.MVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CinemaManagement.MVC.Areas.ViewModel
{
    public class HallEditViewModel
    {
        public List<Hall> Halls { get; set; }
        public List<Seat> Seats { get; set; }
    }
}