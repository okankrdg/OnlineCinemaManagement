using CinemaManagement.MVC.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CinemaManagement.MVC.Areas.ViewModel
{
    public class MovieCreateViewModel
    {
        public string Title { get; set; }
        public string Director { get; set; }
        public string Cast { get; set; }
        public string Description { get; set; }
        public bool IsActive { get; set; }
        public int Duration { get; set; }
        [Poster]
        public HttpPostedFileBase Image { get; set; }
        public DateTime? PublishedDate { get; set; }
    }
}