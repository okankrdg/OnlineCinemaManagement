using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CinemaManagement.MVC.Areas.Admin
{
    public class ShowIndexViewModel
    {
        public int MovieId { get; set; }
        public int HallId { get; set; }
        [DataType(DataType.DateTime)]
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public TimeSpan[] Times { get; set; }
        public decimal Price { get; set; }
    }
}