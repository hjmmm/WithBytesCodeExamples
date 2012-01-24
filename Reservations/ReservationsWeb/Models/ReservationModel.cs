using System;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Web.Mvc;

namespace ReservationsWeb.Models {
    public class ReservationModel {

        [Display(Name="ID")]
        public Guid id { get; set; }

        [Required]
        [Display(Name = "Customer name")]
        public string name { get; set; }

        [Required]
        [Display(Name = "Date")]
        [DataType(DataType.Date)]
        public DateTime date { get; set; }

        [Required]
        [Display(Name = "Number of guests")]
        [Range(0, int.MaxValue, ErrorMessage="The reservation needs to include at least one client")]
        public int numberOfGuests { get; set; }

    }
}