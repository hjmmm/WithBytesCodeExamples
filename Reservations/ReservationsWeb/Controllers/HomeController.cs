using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Configuration;
using ReservationsCommons.Implementation;

namespace ReservationsWeb.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.config = new ConfigReader();
            return View();
        }
    }
}
