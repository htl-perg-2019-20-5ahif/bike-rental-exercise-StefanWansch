using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace BikeRental_API.Model
{

    public enum GenderCustomer
    {
        Male,
        Female,
        Unknown
    }

    public class Customer
    {
        public int CustomerID { get; set; }

        public GenderCustomer Gender { get; set; }
        [Required]
        [MaxLength(50)]
        public string FirstName { get; set; }
        [Required]
        [MaxLength(75)]
        public string LastName { get; set; }
        [Required]
        public DateTime Birthday { get; set; }
        [Required]
        [MaxLength(75)]
        public string Street { get; set; }

        [MaxLength(10)]
        public string HouseNumber { get; set; }
        [Required]
        [MaxLength(10)]
        public string Zip { get; set; }
        [Required]
        [MaxLength(75)]
        public string Town { get; set; }

        public List<Rental> Rentals { get; set; }

    }


}

