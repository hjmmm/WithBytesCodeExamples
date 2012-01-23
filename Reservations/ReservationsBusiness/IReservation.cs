using System;
namespace ReservationsBusiness {
    public interface IReservation : IComparable {
        Guid id { get; }
        DateTime date { get; }
        string name { get; }
        int numberOfGuests { get; }
    }
}
