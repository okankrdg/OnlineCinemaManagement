using CinemaManagement.MVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CinemaManagement.MVC.ViewModel
{
    public class ShowingSeatsViewModel
    {
        public ICollection<Seat> Seats { get; set; }
        public ICollection<SeatReserved> SeatReserveds { get; set; }
        public Show Show { get; set; }
    }
}