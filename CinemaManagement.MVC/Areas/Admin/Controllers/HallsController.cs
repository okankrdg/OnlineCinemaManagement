using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using CinemaManagement.MVC.Models;

namespace CinemaManagement.MVC.Areas.Admin.Controllers
{
    public class HallsController : Controller
    {
        private CinemaDbContext db = new CinemaDbContext();

        // GET: Admin/Halls
        public ActionResult Index()
        {
            return View(db.Halls.ToList());
        }

        // GET: Admin/Halls/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Hall hall = db.Halls.Find(id);
            if (hall == null)
            {
                return HttpNotFound();
            }
            return View(hall);
        }

        // GET: Admin/Halls/Create
        public ActionResult Create()
        {
          
            return View();
        }

        // POST: Admin/Halls/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name")] Hall hall)
        {
            if (ModelState.IsValid)
            {
                db.Halls.Add(hall);
                db.SaveChanges();
                TempData["SuccessMessage"] = "Hall created success redirect to Seat add and edit ";
                return RedirectToAction("Edit",new { id=hall.Id});
            }
            TempData["ErrorMessage"] = "Fail";
            return View(hall);
        }
        [HttpPost]
        public ActionResult SeatsAdd(string row,int RowCount,int hallId)
        {
            List<Seat> seats = new List<Seat>();
            if (db.Seats.Any(x=>x.Row==row && x.HallId==hallId))
            {
                TempData["ErrorMessage"] = "There is Row already ";
                return Json("Fail");
            }
            for (int i = 1; i <= RowCount; i++)
            {
                seats.Add(new Seat
                {
                    Row = row,
                    Number = i,
                    IsAvaliable = true,
                    HallId = hallId
                });
            };
            db.Seats.AddRange(seats);
            db.SaveChanges();
            TempData["SuccessMessage"] = "Success ";
            return Json(seats);
        }
        [HttpPost]
        public JsonResult SeatCreate(int id)
        {
            var seat = db.Seats.FirstOrDefault(x => x.Id == id);
            var newSeat = new Seat
            {
                HallId = seat.HallId,
                IsAvaliable = true,
                Number = seat.Number + 1,
                Row = seat.Row
            };
            db.Seats.Add(newSeat);
            db.SaveChanges();
            return Json(newSeat);
        }
        [HttpPost]
        public JsonResult SeatRemove(int id)
        {
            var seat = db.Seats.FirstOrDefault(x => x.Id == id);
            var isReserve = db.SeatReserveds.Any(x => x.SeatId == id);
            if (isReserve)
            {
                return Json("Fail");
            }
            db.Seats.Remove(seat);
            db.SaveChanges();
            return Json(seat);
        }
        // GET: Admin/Halls/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Hall hall = db.Halls.Find(id);
            if (hall == null)
            {
                return HttpNotFound();
            }
            string[] rowList = new string[] { "A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M", "N", "O", "P", "S", "T", "U", "V", "Y", "Z" };
            SelectList list = new SelectList(rowList);
            ViewBag.Row = list;
            return View(hall);
        }

        // POST: Admin/Halls/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name")] Hall hall)
        {
            if (ModelState.IsValid)
            {
                db.Entry(hall).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(hall);
        }

        // GET: Admin/Halls/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Hall hall = db.Halls.Find(id);
            if (hall == null)
            {
                return HttpNotFound();
            }
            return View(hall);
        }

        // POST: Admin/Halls/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Hall hall = db.Halls.Find(id);
            db.Halls.Remove(hall);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
