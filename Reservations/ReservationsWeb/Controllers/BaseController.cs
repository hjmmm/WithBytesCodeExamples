using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ReservationsBusiness;
using ReservationsCommons;

namespace ReservationsWeb.Controllers
{
    public abstract class BaseController : Controller
    {
        public ReservationManager reservationManager {
            get {
                return HttpContext.Application[Constants.RESERVATION_MANAGER_APPLICATION_KEY] as ReservationManager;
            }
        }
    }
}
