using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace CinemaManagement.MVC.Models
{
    public class Ticket
    {
        
        public int Id { get; set; }
        public int ShowId { get; set; }
       
        public int SeatId { get; set; }
        public int CustomerId { get; set; }
        public virtual Show Show { get; set; }
        public virtual Customer Customer { get; set; }
        public virtual Seat Seat { get; set; }
    }
}