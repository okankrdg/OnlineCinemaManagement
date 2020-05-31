using CinemaManagement.MVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CinemaManagement.MVC.ViewModel
{
    public class HomeIndexViewModel
    {
        public List<Movie> Movies { get; set; }
        public List<Show> Shows { get; set; }
    }
}