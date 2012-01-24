using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ReservationsBusiness;
using ReservationsWeb.Models;

namespace ReservationsWeb.Controllers
{
    public class ReservationController : BaseController
    {
        //
        // GET: /Reservation/

        public ActionResult Index()
        {
            IEnumerable<IReservation> reservations = this.reservationManager.GetAllReservations();
            ViewBag.list = reservations.Select<IReservation, ReservationModel>(r => ReservationToModel(r));
            ViewBag.listCount = reservations.Count();
            return View();
        }

        //
        // GET: /Reservation/Create

        public ActionResult Create() {
            return View(new ReservationModel { date=DateTime.Now.Date, numberOfGuests=4 });
        }

        //
        // POST: /Reservation/Create

        [HttpPost]
        public ActionResult Create(ReservationModel model)
        {
            try
            {
                string message = null;

                if (ModelState.IsValid) {
                    if (this.reservationManager.AddReservation(ModelToReservation(model))) {
                        message = "Reservation added";
                    } else {
                        message = "Not enough seats available for the selected date";
                    }
                }
                ViewBag.message = message;
                ModelState.Clear();
                return View(new ReservationModel { date = DateTime.Now.Date, numberOfGuests = 4 });
            }
            catch
            {
                return View(model);
            }
        }

        // GET: /Reservation/Daily

        public ActionResult Daily() {
            return View();
        }

        // GET: /Reservation/ByDate/{year}/{month}/{day}

        public ActionResult ByDate(int year, int month, int day) {
            DateTime requested;
            object response;
            IEnumerable<IReservation> reservations;
            try {
                requested = new DateTime(year, month, day);
                reservations = this.reservationManager.GetReservationsByDate(requested);
                response = new { success = true, list = reservations.Select<IReservation, ReservationModel>(r => ReservationToModel(r)) };
            } catch{
                response = new { success = false, error = "Invalid date" };
            }
            
            return Json(response, JsonRequestBehavior.AllowGet);
        }

        private ReservationModel ReservationToModel(IReservation r) {
            return new ReservationModel { date = r.date, name = r.name, numberOfGuests = r.numberOfGuests, id = r.id };
        }

        private Reservation ModelToReservation(ReservationModel m) {
            return new Reservation(m.name, m.numberOfGuests, m.date);
        }
        
    }
}
