using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CinemaManagement.MVC.Models
{
    public class Hall
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public virtual ICollection<Seat> Seats { get; set; }
    }
}