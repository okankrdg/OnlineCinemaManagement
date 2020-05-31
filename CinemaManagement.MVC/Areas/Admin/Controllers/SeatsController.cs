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
    public class SeatsController : Controller
    {
        private CinemaDbContext db = new CinemaDbContext();

        // GET: Admin/Seats
        public ActionResult Index()
        {
            var seats = db.Seats.Include(s => s.Hall);
            return View(seats.ToList());
        }

        // GET: Admin/Seats/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Seat seat = db.Seats.Find(id);
            if (seat == null)
            {
                return HttpNotFound();
            }
            return View(seat);
        }

        // GET: Admin/Seats/Create
        public ActionResult Create()
        {
            ViewBag.HallId = new SelectList(db.Halls, "Id", "Name");
            ViewBag.ShowId = new SelectList(db.Shows, "Id", "Id");
            return View();
        }

        // POST: Admin/Seats/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Row,Number,HallId,ShowId,IsAvaliable")] Seat seat)
        {
            if (ModelState.IsValid)
            {
                db.Seats.Add(seat);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.HallId = new SelectList(db.Halls, "Id", "Name", seat.HallId);
           
            return View(seat);
        }

        // GET: Admin/Seats/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Seat seat = db.Seats.Find(id);
            if (seat == null)
            {
                return HttpNotFound();
            }
            ViewBag.HallId = new SelectList(db.Halls, "Id", "Name", seat.HallId);
          
            return View(seat);
        }

        // POST: Admin/Seats/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Row,Number,HallId,ShowId,IsAvaliable")] Seat seat)
        {
            if (ModelState.IsValid)
            {
                db.Entry(seat).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.HallId = new SelectList(db.Halls, "Id", "Name", seat.HallId);
       
            return View(seat);
        }

        // GET: Admin/Seats/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Seat seat = db.Seats.Find(id);
            if (seat == null)
            {
                return HttpNotFound();
            }
            return View(seat);
        }

        // POST: Admin/Seats/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Seat seat = db.Seats.Find(id);
            db.Seats.Remove(seat);
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
