using CinemaManagement.MVC.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CinemaManagement.MVC.Areas.ViewModel
{
    public class MovieEditViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Director { get; set; }
        public string Cast { get; set; }
        public string Description { get; set; }
        public bool IsActive { get; set; }
        public int Duration { get; set; }
        [Poster]
        public HttpPostedFileBase Image { get; set; }
        public string PhotoPath { get; set; }
        public DateTime? PublishedDate { get; set; }
    }
}