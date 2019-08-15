using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using CarReservationSystem.Models;
using CarReservationSystem.Data;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace CarReservationSystem.Controllers
{
    [Route("api/appointments")]
    [ApiController]
    public class ReservationsController : ControllerBase
    {
        private readonly ReservationsContext _context;

        public ReservationsController(ReservationsContext context)
        {
            _context = context;
        }

        // GET: api/appointments
        [HttpGet]
        public async Task<ActionResult<List<Reservations>>> GetAllCarReservations()
        {
            return await _context.CarReservations.ToListAsync();
        }

        // GET: api/appointments/byDate?start=2019-01-02&end=2019-01-08
        [HttpGet("byDate")]
        public async Task<ActionResult<List<Reservations>>> GetAllCarReservationsByDate([FromQuery]DateTime start, [FromQuery]DateTime end)
        {
            return await _context.CarReservations.Where(x => (x.reservationStartTime >= start) && (x.reservationEndTime <= end)).OrderBy(o => o.totalCost).ToListAsync();
        }

        // GET api/appointments/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Reservations>> GetCarReservation(int id)
        {
            var reservationsItem = await _context.CarReservations.FindAsync(id);

            if (reservationsItem == null)
                return NotFound();

            return reservationsItem;
        }

        //check if the car and user already exists
        //check if a car is not booked already for that date range
        // POST api/appointments
        [HttpPost]
        public async Task<ActionResult<Reservations>> PostCarReservation(Reservations item)
        {
            Users userItem = _context.CarUsers.Find(item.userID);
            Car carItem = _context.Cars.Find(item.carID);
            if (userItem == null || carItem == null)
                return BadRequest();

            // find if there is any reservation with this car during this range
            List<Reservations> found = reservationFound(carItem, item.reservationEndTime, item.reservationStartTime);
            
            if(found.Count != 0)
                return BadRequest();

            item.totalCost = carItem.rentalPricePerDay * (double)(item.reservationEndTime - item.reservationStartTime).TotalDays;
            _context.CarReservations.Add(item);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetAllCarReservations), new { id = item.reservationID }, item);
        }

        private List<Reservations> reservationFound(Car carItem, DateTime endTime, DateTime startTime)
        {
            List<Reservations> items = _context.CarReservations.Where(x => x.carID == carItem.carID
            && (x.reservationStartTime <= endTime && x.reservationEndTime >= startTime))
                .ToList();

            return (items);
            //throw new NotImplementedException();
        }

        //check if the car and user already exists
        //check if a car is not booked already for that date range for any other reservation except the current one
        // PUT: api/appointments/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCarReservation(int id, Reservations item)
        {
            if (id != item.reservationID)
                return BadRequest();

            _context.Entry(item).State = EntityState.Modified;

            Users userItem = _context.CarUsers.Find(item.userID);
            Car carItem = _context.Cars.Find(item.carID);

            if (userItem == null || carItem == null)
                return BadRequest();

            var previousReservationsItem = new List<Reservations>();
            previousReservationsItem.Add(_context.CarReservations.Find(id));

            List<Reservations> found = reservationFound(carItem, item.reservationEndTime, item.reservationStartTime).Except(previousReservationsItem).ToList();

            if (found.Count != 0)
                return BadRequest();

            item.totalCost = carItem.rentalPricePerDay * (double)(item.reservationEndTime - item.reservationStartTime).TotalDays;

            
            await _context.SaveChangesAsync();

            return NoContent();
        }

        // DELETE: api/appointments/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCarReservation(int id)
        {
            Reservations carItem = _context.CarReservations.Find(id);

            if (carItem == null)
                return NotFound();

            _context.CarReservations.Remove(carItem);
            await _context.SaveChangesAsync();

            return NoContent();
        }

    }
}
