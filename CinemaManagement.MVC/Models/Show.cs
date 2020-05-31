using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CinemaManagement.MVC.Models
{
    public class Show
    {
        public int Id { get; set; }
        public int HallId { get; set; }
        public int MovieId { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public decimal Price { get; set; }
        public virtual Hall Hall { get; set; }
        public virtual Movie Movie { get; set; }
    }
}