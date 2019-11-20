
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BikeRental_API
{
    public class UnpaidRental
    {
        public int CustomerId { get; set; }
        public string Lastname { get; set; }
        public string Firstname { get; set; }
        public int RentaldId { get; set; }
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
        public decimal TotalPrice { get; set; }
    }

}
