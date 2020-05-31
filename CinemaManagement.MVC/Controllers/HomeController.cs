using CinemaManagement.MVC.Models;
using CinemaManagement.MVC.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CinemaManagement.MVC.Controllers
{
    public class HomeController : BaseController
    {
        public ActionResult Index(DateTime? date)
        {
            var movies = db.Movies.Where(x => x.IsActive).ToList();
            var shows = db.Shows;
            var vm = new HomeIndexViewModel();

            if (date == null || date.Value.Day==DateTime.Now.Day)
            {
                vm.Shows = shows.Where(x => x.StartTime.Day == DateTime.Now.Day && x.StartTime.Hour > DateTime.Now.Hour).ToList();
                vm.Movies = movies.Where(x => x.Shows.Any(y => y.StartTime.Day == DateTime.Now.Day)).ToList();
              
            }
            else
            {
                vm.Shows = shows.Where(x => x.StartTime.Day == date.Value.Day).ToList();
                vm.Movies = movies.Where(x => x.Shows.Any(y => y.StartTime.Day == date.Value.Day)).ToList();
            }
          
            return View(vm);
        }
        public ActionResult ChooseSeat(int showId)
        {
            var show = db.Shows.FirstOrDefault(x => x.Id == showId);
           
            var vm = new ShowingSeatsViewModel
            {
                Seats = db.Seats.Where(x => x.HallId == show.HallId).ToList(),
                SeatReserveds = db.SeatReserveds.Where(x => x.ShowId == show.Id).ToList(),
                Show=show
            };
            return View(vm);
        }
        [HttpPost]
        public ActionResult TicketBuy(int showId, int seatId)
        {
            var seatReserve = new SeatReserved
            {
                SeatId = seatId,
                ShowId = showId
            };
            db.SeatReserveds.Add(seatReserve);
            db.SaveChanges();
            return Json(seatReserve);
        }
        
    }
}