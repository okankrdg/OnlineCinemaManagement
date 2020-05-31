using CinemaManagement.MVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CinemaManagement.MVC.Controllers
{
    public class BaseController : Controller
    {
        protected CinemaDbContext db = new CinemaDbContext();
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