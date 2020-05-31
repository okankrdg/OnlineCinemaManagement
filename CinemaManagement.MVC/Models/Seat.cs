using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CinemaManagement.MVC.Models
{
    public class Seat
    {
        public int Id { get; set; }
        public string Row { get; set; }
        public int Number { get; set; }
        public int HallId { get; set; }
        
        public bool IsAvaliable { get; set; }
        public virtual Hall Hall { get; set; }
       
        
    }
}