using CinemaManagement.MVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CinemaManagement.MVC.Areas.Admin.Controllers
{
    public class AdminBaseController : Controller
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