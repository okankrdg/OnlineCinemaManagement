using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CinemaManagement.MVC.Models
{
    public class SeatReserved
    {
        public int Id { get; set; }
        public int ShowId { get; set; }
        public int SeatId { get; set; }
        public virtual Show Show { get; set; }
        public virtual Seat Seat { get; set; }
    }
}