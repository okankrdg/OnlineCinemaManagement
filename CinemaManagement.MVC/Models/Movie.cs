using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CinemaManagement.MVC.Models
{
    public class Movie
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Director { get; set; }
        public string Cast { get; set; }
        public string Description { get; set; }
        public int Duration { get; set; }
        public string PosterPhotoPath { get; set; }
        public DateTime? PublishedDate { get; set; }
        public bool IsActive { get; set; }
        public virtual ICollection<Hall> Halls { get; set; }
        public virtual ICollection<Show> Shows { get; set; }
    }
}