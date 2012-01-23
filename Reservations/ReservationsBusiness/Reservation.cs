using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ReservationsBusiness {
    public class Reservation : ReservationsBusiness.IReservation, IComparable {

        private string _name;
        private int _numberOfQuests;
        private DateTime _date;
        private Guid _id;

        public Reservation(string name, int guests, DateTime date) {
            this._name = name;
            this._numberOfQuests = guests;
            this._date = date.Date;
            this._id = Guid.NewGuid();
        }

        public string name {
            get {
                return this._name;
            }
        }

        public int numberOfGuests {
            get {
                return this._numberOfQuests;
            }
        }

        public DateTime date {
            get {
                return _date;
            }
        }

        public Guid id {
            get {
                return _id;
            }
        }

        #region IComparable Members

        /// <summary>
        /// Compares this instance to other instances of IReservation. 
        /// The comparision is ascending over the following fields, in that order:
        ///     # date
        ///     # name
        ///     # numberOfGuests
        ///     # id
        /// If obj is null or not an IReservation this method returns less than 0. 
        /// </summary>
        /// <param name="obj">Object to compare against</param>
        /// <returns>
        /// A value that indicates the relative order of the objects being compared as stablished in the 
        /// IComparable documentation.
        /// </returns>
        public int CompareTo(object obj) {
            IReservation right = obj as IReservation;
            int result;
            if (obj == null || !(obj is IReservation)) {
                return -1;
            }
            result = this.date.CompareTo(right.date);
            if (result == 0) {
                result = this.name.CompareTo(right.name);
            }
            if (result == 0) {
                result = this.numberOfGuests.CompareTo(right.numberOfGuests);
            }
            if (result == 0) {
                result = this.id.CompareTo(right.id);
            }
            return result;
        }

        #endregion
    }
}
