using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BikeRental_API;
using BikeRental_API.Model;

namespace BikeRental_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RentalsController : ControllerBase
    {
        private readonly BikeRentalContext _context;

        public RentalsController(BikeRentalContext context)
        {
            _context = context;
        }

        // GET: api/Rentals
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Rental>>> GetRentals()
        {
            return await _context.Rentals.ToListAsync();
        }

        // GET: api/Rentals/Unpaid
        [HttpGet("/Unpaid")]
        public async Task<List<UnpaidRental>> GetUnpaidRental()
        {
            List<UnpaidRental> unpaidRentals = new List<UnpaidRental>();
            var rentals = _context.Rentals.Where(r => r.Paid == false);
            foreach(var rental in rentals)
            {
                if (rental.TotalCosts > 0)
                {
                    UnpaidRental u = new UnpaidRental();
                    u.RentaldId = rental.RentalID;
                    u.Firstname = rental.Customer.FirstName;
                    u.Lastname = rental.Customer.LastName;
                    u.Start = rental.RentBegin;
                    u.End = rental.RentEnd.Value;
                    u.CustomerId = rental.CustomerID;
                    u.TotalPrice = rental.TotalCosts.Value;
                    unpaidRentals.Add(u);
                }

            }
            return unpaidRentals;
        }

        // PUT: api/Rentals/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPut("{id}")]
        public async Task<IActionResult> EndRental(int id, Rental rental)
        {
            if (id != rental.RentalID)
            {
                return BadRequest();
            }
            rental.RentEnd = System.DateTime.Now;
            rental.TotalCosts= CostCalculation(rental);

            _context.Entry(rental).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RentalExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }
        [HttpPut("{id}/paid")]
        public async Task<IActionResult> MarkasPaid(int id, Rental rental)
        {
            if (id != rental.RentalID)
            {
                return BadRequest();
            }
            rental.Paid = true;
            _context.Entry(rental).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RentalExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Rentals
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPost]
        public async Task<ActionResult<Rental>> StartRental(Rental rental)
        {
            rental.RentBegin = System.DateTime.Now;
            rental.RentEnd = DateTime.MinValue;
            rental.TotalCosts = -1;
            _context.Rentals.Add(rental);
            await _context.SaveChangesAsync();
            return CreatedAtAction("GetRental", new { id = rental.RentalID }, rental);
        }

       

        private bool RentalExists(int id)
        {
            return _context.Rentals.Any(e => e.RentalID == id);
        }

        public decimal CostCalculation(Rental r)
        {
            int priceOneHour = 3;
            int priceAdditionalHour = 5;
            int costs = 0;
            if (r.RentEnd.Value.Minute - r.RentBegin.Minute <= 15)
            {
                return 0;
            }
            else
            {
                costs += priceOneHour;
                int countMin = r.RentEnd.Value.Minute - r.RentBegin.Minute;
                int countHour = countMin%60;
                int countAdditional = r.RentEnd.Value.Hour - r.RentBegin.Hour;
                if(countHour != 0)
                {
                    countAdditional++;
                }
                if (countAdditional > 1) costs += priceAdditionalHour * countAdditional;
                return costs;
            }
        }

    }
}
