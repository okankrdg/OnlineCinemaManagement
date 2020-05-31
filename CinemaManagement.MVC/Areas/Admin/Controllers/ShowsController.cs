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
    public class ShowsController : AdminBaseController
    {

        // GET: Admin/Shows
        public ActionResult Index()
        {
            var shows = db.Shows.Include(s => s.Hall).Include(s => s.Movie).OrderByDescending(x=>x.StartTime).ToList();
            return View(shows);
        }
        public ActionResult NowShowing()
        {
            var shows = db.Shows.Where(x=>x.StartTime>DateTime.Now).OrderBy(x=>x.StartTime);
            return View(shows.ToList());
        }
        // GET: Admin/Shows/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Show show = db.Shows.Find(id);
            if (show == null)
            {
                return HttpNotFound();
            }
            return View(show);
        }

        // GET: Admin/Shows/Create
        public ActionResult Create()
        {
            ViewBag.HallId = new SelectList(db.Halls, "Id", "Name");
            ViewBag.MovieId = new SelectList(db.Movies, "Id", "Title");
            return View();
        }

        // POST: Admin/Shows/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ShowIndexViewModel vm)
        {
            if (ModelState.IsValid)
            {
                var movieDuration = db.Movies.FirstOrDefault(x => x.Id == vm.MovieId).Duration;
               
                for (DateTime date = vm.StartDate; date <= vm.EndDate; date=date.AddDays(1))
                {
                    int count = 0;
                    foreach (var time in vm.Times)
                    {
                        var newStDate = date + time;//Su anki
                        if (count>0)
                        {
                            var preStTime =vm.Times[count-1];
                            var preStDate = date + preStTime;
                            var newShow = new Show
                            {
                                HallId = vm.HallId,
                                MovieId = vm.MovieId,
                                StartTime = preStDate,
                                EndTime = newStDate - TimeSpan.FromMinutes(5),
                                Price = vm.Price
                            };
                            db.Shows.Add(newShow);
                        }
                        if (vm.Times.Length == count)
                        {
                            //sonuncusnu ekler
                            var preStTime = vm.Times[count - 1];
                            var difference = newStDate.TimeOfDay - preStTime;
                            var newShow = new Show
                            {
                                HallId = vm.HallId,
                                MovieId = vm.MovieId,
                                StartTime = newStDate,
                                EndTime = newStDate + difference,
                                Price = vm.Price
                            };
                        }
                        count++;
                       
                    }
                }
                db.SaveChanges();
                TempData["Success"] = "Successfully edited";
                return RedirectToAction("NowShowing");
            }

            ViewBag.HallId = new SelectList(db.Halls, "Id", "Name", vm.HallId);
            ViewBag.MovieId = new SelectList(db.Movies, "Id", "Title", vm.MovieId);
            return View(vm);
        }

        // GET: Admin/Shows/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Show show = db.Shows.Find(id);
            if (show == null)
            {
                return HttpNotFound();
            }
            ViewBag.HallId = new SelectList(db.Halls, "Id", "Name", show.HallId);
            ViewBag.MovieId = new SelectList(db.Movies, "Id", "Title", show.MovieId);
            
            return View(show);
        }

        // POST: Admin/Shows/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,HallId,MovieId,StartTime,EndTime,Price")] Show show)
        {
            if (ModelState.IsValid)
            {
                db.Entry(show).State = EntityState.Modified;
                db.SaveChanges();
                TempData["Success"] = "Successfully edited";
                return RedirectToAction("Index");
            }
            ViewBag.HallId = new SelectList(db.Halls, "Id", "Name", show.HallId);
            ViewBag.MovieId = new SelectList(db.Movies, "Id", "Title", show.MovieId);
            TempData["ErrorMessage"] = "Failed!";
            return View(show);
        }

        // GET: Admin/Shows/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Show show = db.Shows.Find(id);
            if (show == null)
            {
                return HttpNotFound();
            }
            var seatreserved = db.SeatReserveds.Any(x => x.ShowId == id);
            if (seatreserved)
            {
                TempData["ErrorMessage"] = "the show cannot be deleted because the seat for this show is reserved";
            }
            TempData["Success"] = "Successfully deleted";
            return View(show);
        }

        // POST: Admin/Shows/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Show show = db.Shows.Find(id);
            db.Shows.Remove(show);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

    }
}
