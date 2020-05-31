using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using CinemaManagement.MVC.Areas.ViewModel;
using CinemaManagement.MVC.ImageActions;
using CinemaManagement.MVC.Models;

namespace CinemaManagement.MVC.Areas.Admin.Controllers
{
    public class MoviesController : Controller
    {
        private CinemaDbContext db = new CinemaDbContext();

        // GET: Admin/Movies
        public ActionResult Index()
        {
            return View(db.Movies.ToList());
        }
        public ActionResult ShowingMovies()
        {
            var shows = db.Shows.Where(x => x.StartTime > DateTime.Now).OrderByDescending(x => x.StartTime).ToList();
            return View(shows);
        }
        // GET: Admin/Movies/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Movie movie = db.Movies.Find(id);
            if (movie == null)
            {
                return HttpNotFound();
            }
            return View(movie);
        }

        // GET: Admin/Movies/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/Movies/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(MovieCreateViewModel vm)
        {
            if (ModelState.IsValid)
            {
                var movie = new Movie
                {
                    Cast = vm.Cast,
                    Description = vm.Cast,
                    Director = vm.Director,
                    Duration = vm.Duration,
                    IsActive = vm.IsActive,
                    PublishedDate = vm.PublishedDate,
                    Title = vm.Title,
                    PosterPhotoPath = this.SaveImage(vm.Image)
                };
                db.Movies.Add(movie);
                db.SaveChanges();
                TempData["SuccessMessage"] = "Success Created";
                return RedirectToAction("Index");
            }

            return View(vm);
        }

        // GET: Admin/Movies/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Movie movie = db.Movies.Find(id);
            if (movie == null)
            {
                return HttpNotFound();
            }
            var vm = new MovieEditViewModel
            {
                Cast = movie.Cast,
                Description = movie.Description,
                Director = movie.Director,
                Duration = movie.Duration,
                PublishedDate = movie.PublishedDate,
                Title = movie.Title,
                PhotoPath = movie.PosterPhotoPath,
                IsActive=movie.IsActive
            };
            return View(vm);
        }

        // POST: Admin/Movies/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(MovieEditViewModel vm)
        {
            if (ModelState.IsValid)
            {
                var movie = db.Movies.Find(vm.Id);
                movie.Cast = vm.Cast;
                movie.Description = vm.Description;
                movie.Director = vm.Director;
                movie.Duration = vm.Duration;
                movie.IsActive = vm.IsActive;
                movie.PublishedDate = vm.PublishedDate;
                movie.Title = vm.Title;
                if (vm.Image != null)
                {
                    this.DeleteImage(movie.PosterPhotoPath);
                    movie.PosterPhotoPath = this.SaveImage(vm.Image);
                }
                
                db.SaveChanges();
                TempData["SuccessMessage"] = "Successfully Edited";
                return RedirectToAction("Index");
            }
            return View(vm);
        }

        // GET: Admin/Movies/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Movie movie = db.Movies.Find(id);
            if (movie == null)
            {
                return HttpNotFound();
            }
            return View(movie);
        }

        // POST: Admin/Movies/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Movie movie = db.Movies.Find(id);
            var isShowing = db.Shows.Where(x => x.StartTime > DateTime.Now);
            if (isShowing.Any(x => x.MovieId == movie.Id))
            {
                TempData["ErrorMessage"] = "The movie cannot be deleted because  showing now";
                return RedirectToAction("Index");
            }
            db.Movies.Remove(movie);
            db.SaveChanges();
            TempData["SuccessMessage"] = "Success";
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
