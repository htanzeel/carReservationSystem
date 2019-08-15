using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using CarReservationSystem.Models;
using CarReservationSystem.Data;
using Microsoft.EntityFrameworkCore;

namespace CarReservationSystem.Controllers
{
    [Route("api/cars")]
    [ApiController]
    public class CarsController : ControllerBase
    {
        private readonly ReservationsContext _context;

        public CarsController(ReservationsContext context)
        {
            _context = context;
        }

        // GET: api/cars
        [HttpGet]
        public ActionResult<List<Car>> GetAllCars()
        {
            return _context.Cars.ToList();
        }

        // GET api/cars/5
        [HttpGet("{id}")]
        public ActionResult<Car> GetCarByID(int id)
        {
            var CarsItem = _context.Cars.Find(id);

            if (CarsItem == null)
            {
                return NotFound();
            }

            return CarsItem;
        }

        // POST api/cars
        [HttpPost]
        public ActionResult<Car> PostCar(Car item)
        {
            _context.Cars.Add(item);
            _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetAllCars), new { id = item.carID }, item);
        }

        // PUT: api/cars/5
        [HttpPut("{id}")]
        public IActionResult PutCars(int id, Car item)
        {
            if (id != item.carID)
            {
                return BadRequest();
            }

            _context.Entry(item).State = EntityState.Modified;
            _context.SaveChangesAsync();

            return NoContent();
        }

        // DELETE: api/cars/5
        [HttpDelete("{id}")]
        public IActionResult DeleteCars(int id)
        {
            Car carItem = _context.Cars.Find(id);

            if (carItem == null)
            {
                return NotFound();
            }

            _context.Cars.Remove(carItem);
            _context.SaveChangesAsync();

            return NoContent();
        }

    }
}
