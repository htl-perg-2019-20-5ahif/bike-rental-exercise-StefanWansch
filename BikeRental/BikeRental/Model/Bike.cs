using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace BikeRental_API.Model
{
    public class Bike
    {
        public int BikeID { get; set; }
        [Required]
        [MaxLength(25)]
        public string Brand { get; set; }
        [Required]
        public DateTime purchaseDate { get; set; }
        [MaxLength(1000)]
        public string Notes { get; set; }

        public DateTime? DateLastService { get; set; } //can be null
        [Required]
        [RegularExpression("\\d*\\.\\d{1,2}$")]
        [Range(0, double.PositiveInfinity)]
        public decimal RentalPriceFirstHour { get; set; }
        [Required]
        [RegularExpression("\\d*\\.\\d{1,2}$")]
        [Range(1, double.PositiveInfinity)]
        public decimal RentalPriceAdditionalHour { get; set; }

        [Required]
        public Categories BikeCategories { get; set; }

        public List<Rental> Rentals { get; set; }
    }
    public enum Categories
    {
        StandardBike,
        MountainBike,
        TreckingBike,
        RacingBike
    }
}
