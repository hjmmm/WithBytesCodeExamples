using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using ReservationsBusiness;

namespace ReservationsBusinessTests {
    [TestFixture]
    public class ReservationTest {

        [Test]
        public void ReservationCreationAndGetters() {
            string name = "Roy";
            int guests = 20;
            DateTime date = DateTime.Now;
            Reservation r = new Reservation(name, guests,date);

            Assert.AreEqual(name, r.name);
            Assert.AreEqual(guests, r.numberOfGuests);
            Assert.AreEqual(date.Date, r.date);
            Assert.AreNotEqual(Guid.Empty, r.id);
        }

        [Test]
        public void UniqueIdentifierPerReservation() {
            string name = "Roy";
            int guests = 20;
            DateTime date = new DateTime(2012, 1, 22);
            Reservation r1 = new Reservation(name, guests, date);
            Reservation r2 = new Reservation(name, guests, date);
            Reservation r3 = new Reservation("Moss", 5, DateTime.Now);

            Assert.AreNotEqual(r1.id, r2.id);
            Assert.AreNotEqual(r1.id, r3.id);
            Assert.AreNotEqual(r2.id, r3.id);
        }

        [Test]
        public void CompareToTest() {
            string name = "Richmond";
            DateTime today = DateTime.Now.Date;
            DateTime tomorrow = today.AddDays(1);

            Reservation r1 = new Reservation(name, 1, today);
            Reservation r2 = new Reservation(name, 2, today);
            Reservation r3 = new Reservation(name, 1, tomorrow);
            Reservation r4 = new Reservation("Roy", 1, today);

            Assert.AreEqual(0, r1.CompareTo(r1), "Same spot");
            Assert.Less(r1.CompareTo(r2), 0, "r1 goes first by number of guests");
            Assert.Less(r1.CompareTo(r3), 0, "r1 goes first by date");
            Assert.Less(r1.CompareTo(r4), 0, "r1 goes first by name");
            Assert.Greater(r2.CompareTo(r1), 0, "r2 goes second by number of guests");
            Assert.Greater(r3.CompareTo(r1), 0, "r3 goes second by date");
            Assert.Greater(r4.CompareTo(r1), 0, "r4 goes second by name");            
        }
    }
}
