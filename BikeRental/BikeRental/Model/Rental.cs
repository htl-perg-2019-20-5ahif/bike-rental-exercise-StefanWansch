using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace BikeRental_API.Model
{
    public class Rental
    {
        public int RentalID { get; set; }

        public int CustomerID { get; set; }
        [Required]
        public Customer Customer { get; set; }

        public int BikeID { get; set; }
        [Required]
        public Bike Bike { get; set; }
        [Required]
        public DateTime RentBegin { get; set; }

        public DateTime? RentEnd { get; set; } // can be null
        [RegularExpression("\\d*\\.\\d{1,2}$")]
        [Range(0, double.PositiveInfinity)]
        public decimal? TotalCosts { get; set; } //can be null
        [Required]
        public bool Paid { get; set; }


    }
}
