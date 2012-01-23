using System;
namespace ReservationsCommons {
    public interface IConfigReader {

        /// <summary>
        /// Number of seats available at the restaurant.
        /// </summary>
        int MaxCapacity { get; }

        /// <summary>
        /// Percentage of the seats that are available for booking. A value of 1 means that all seats are
        /// available for booking, 0.8 would mean that only 80% of the seats are available, a value over 1
        /// means we are accepting overbooking. So, a value of 1.2 means we are accepting bookings for 
        /// 20% more seats than we actually have.
        /// </summary>
        decimal BookingTop { get; }

    }
}
