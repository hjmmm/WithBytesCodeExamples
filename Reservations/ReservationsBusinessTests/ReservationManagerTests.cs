using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using Moq;
using ReservationsBusiness;
using ReservationsCommons;

namespace ReservationsBusinessTests {
    [TestFixture]
    public class ReservationManagerTests {

        ReservationManager subject;

        [SetUp]
        public void SetUp() {
            var configMock = new Mock<IConfigReader>();
            configMock.Setup(c => c.BookingTop).Returns((decimal)1.2);
            configMock.Setup(c => c.MaxCapacity).Returns(100);
            subject = new ReservationManager(configMock.Object);
        }

        [Test]
        public void AddReservationSuccess() {
            string name = "Jen";
            int guests = 5;
            DateTime date = DateTime.Now;

            IReservation reserve = GetReservationMock(name, date, guests);

            Assert.IsTrue(subject.AddReservation(reserve));
            Assert.AreEqual(reserve, subject.GetReservationById(reserve.id));
            Assert.IsNull(subject.GetReservationById(Guid.Empty));
        }

        [Test]
        public void AddReservationFailureNoSeatsAvailable() {
            int guests = 100;
            DateTime date = DateTime.Now.Date;

            IReservation r1 = GetReservationMock("Mr. Douglas", date.AddHours(2), 150);
            IReservation r2 = GetReservationMock("Jen", date.AddHours(3), guests);
            IReservation r3 = GetReservationMock("Mr. Douglas", date.AddHours(5), guests);
            IReservation r4 = GetReservationMock("Mr. Douglas", date.AddDays(1), guests);

            // Fails, not enough seats in the restaurant even with overbooking
            Assert.IsFalse(this.subject.AddReservation(r1));
            // Should pass since the capacity is enough
            Assert.IsTrue(this.subject.AddReservation(r2));
            // Should not pass since the previous reservation and the current one would overfill the restaurant
            Assert.IsFalse(this.subject.AddReservation(r3));
            // Should pass since the reservation is for an empty day
            Assert.IsTrue(this.subject.AddReservation(r4));
        }

        [Test]
        public void AddReservationOverbooking() {
            int guests = 120;
            DateTime date = DateTime.Now.Date;

            IReservation r1 = GetReservationMock("Jen", date.AddHours(3), guests);
            IReservation r2 = GetReservationMock("Mr. Douglas", date.AddHours(5), 1);
            IReservation r3 = GetReservationMock("Mr. Douglas", date.AddDays(1), guests);

            // Should pass since the capacity is enough
            Assert.IsTrue(this.subject.AddReservation(r1));
            // Should not pass since the previous reservation and the current one would overfill the restaurant
            Assert.IsFalse(this.subject.AddReservation(r2));
            // Should pass since the reservation is for an empty day
            Assert.IsTrue(this.subject.AddReservation(r3));

        }

        [Test]
        public void GetReservationsByDate() {
            int guests = 5;
            DateTime now = DateTime.Now;
            DateTime today = now.Date;
            DateTime tomorrow = today.AddDays(1);
            DateTime theDayAfter = tomorrow.AddDays(1);

            IReservation r1 = GetReservationMock("Moss", today, guests);
            IReservation r2 = GetReservationMock("Jen", today, guests);
            IReservation r3 = GetReservationMock("Roy", tomorrow, guests);
            IReservation r4 = GetReservationMock("Mr. Douglas", theDayAfter, guests);

            this.subject.AddReservation(r1);
            this.subject.AddReservation(r2);
            this.subject.AddReservation(r3);
            this.subject.AddReservation(r4);

            Assert.IsTrue(this.subject.GetReservationsByDate(today).Contains(r1));
            Assert.IsTrue(this.subject.GetReservationsByDate(now).Contains(r1));
            Assert.IsTrue(this.subject.GetReservationsByDate(today).Contains(r2));
            Assert.AreEqual(2, this.subject.GetReservationsByDate(today).Count);
            Assert.IsTrue(this.subject.GetReservationsByDate(tomorrow).Contains(r3));
            Assert.AreEqual(1, this.subject.GetReservationsByDate(tomorrow).Count);
            Assert.IsTrue(this.subject.GetReservationsByDate(theDayAfter).Contains(r4));
            Assert.AreEqual(1, this.subject.GetReservationsByDate(theDayAfter).Count);
            Assert.AreEqual(0, this.subject.GetReservationsByDate(theDayAfter.AddDays(1)).Count);
        }

        private IReservation GetReservationMock(string name, DateTime date, int guests) {
            var reservationMock = new Mock<IReservation>();
            reservationMock.SetupGet(r => r.date).Returns(date);
            reservationMock.SetupGet(r => r.numberOfGuests).Returns(guests);
            reservationMock.SetupGet(r => r.name).Returns(name);
            reservationMock.SetupGet(r => r.id).Returns(Guid.NewGuid());
            return reservationMock.Object;
        }

    }
}
