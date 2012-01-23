using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ReservationsCommons;

namespace ReservationsBusiness {
    public class ReservationManager {

        private IConfigReader _config;
        private IDictionary<Guid, IReservation> _reservationsById;
        private IDictionary<DateTime, ICollection<IReservation> > _reservationsByDate;

        public ReservationManager(IConfigReader config) {
            this._config = config;
            this._reservationsById = new Dictionary<Guid, IReservation>();
            this._reservationsByDate = new Dictionary<DateTime, ICollection<IReservation> >();
        }

        /// <summary>
        /// Adds a reservation to the current schedule
        /// </summary>
        /// <param name="reserve">Reservation to be added</param>
        /// <returns>true if successful, false otherwise</returns>
        public bool AddReservation(IReservation reserve) {
            if (reserve.numberOfGuests <= GetRemainingCapacityForDate(reserve.date)) {
                AddReserveToStorage(reserve);
                return true;
            }
            return false;
        }

        // We can extend this method later to actually store the reserves to a database or file
        private void AddReserveToStorage(IReservation reserve) {
            this._reservationsById.Add(reserve.id, reserve);
            if (!this._reservationsByDate.ContainsKey(reserve.date.Date)) {
                this._reservationsByDate.Add(reserve.date.Date, new List<IReservation>());
            }
            this._reservationsByDate[reserve.date.Date].Add(reserve);
        }

        private int GetRemainingCapacityForDate(DateTime date) {
            decimal taken = 0;
            // TODO : We could keep a count of taken seats per date if we have performance issues with this
            if (this._reservationsByDate.ContainsKey(date.Date)) {
                taken = this._reservationsByDate[date.Date].Sum(reserve => reserve.numberOfGuests);
            }
            return (int)(GetFullCapacityWithOverbooking() - taken);
        }

        private decimal GetFullCapacityWithOverbooking() {
            return Math.Floor(this._config.MaxCapacity * this._config.BookingTop);
        }

        /// <summary>
        /// Gets a reservation by id
        /// </summary>
        /// <param name="guid">ID to look for</param>
        /// <returns>The reservation object if found or null if the reservation is not registered</returns>
        public IReservation GetReservationById(Guid guid) {
            if (this._reservationsById.ContainsKey(guid)) {
                return this._reservationsById[guid];
            }
            return null;
        }

        /// <summary>
        /// Gets a list of the reservations register for a particular date. If no reservations are available for the
        /// date an empty list is returned
        /// </summary>
        /// <param name="date">Date to search for reservations</param>
        /// <returns>List of available reservations</returns>
        public ICollection<IReservation> GetReservationsByDate(DateTime date) {
            List<IReservation> result = new List<IReservation>();
            if (this._reservationsByDate.ContainsKey(date.Date)) {
                result.AddRange(this._reservationsByDate[date.Date]);
            }
            return result;
        }

        /// <summary>
        /// Returns a list of all the registered reservations
        /// </summary>
        /// <returns>List of reservations</returns>
        public ICollection<IReservation> GetAllReservations() {
            return this._reservationsById.Values;
        }
    }
}
