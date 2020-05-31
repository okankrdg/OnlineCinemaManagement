using System;
using System.Collections.Generic;
using System.Data.Entity;

using System.Linq;
using System.Web;

namespace CinemaManagement.MVC.Models
{
    public class CinemaDbContext:DbContext
    {
        public CinemaDbContext():base("name=constring")
        {
            
        }
        public DbSet<Movie> Movies { get; set; }
        public DbSet<Hall> Halls { get; set; }
        public DbSet<Show> Shows { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Admin> Admins { get; set; }
        public DbSet<Seat> Seats { get; set; }
        public DbSet<SeatReserved> SeatReserveds { get; set; }

        public System.Data.Entity.DbSet<CinemaManagement.MVC.Models.Ticket> Tickets { get; set; }
    }
}