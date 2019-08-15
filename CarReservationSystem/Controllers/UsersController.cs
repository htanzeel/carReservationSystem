using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using CarReservationSystem.Models;
using CarReservationSystem.Data;
using Microsoft.EntityFrameworkCore;

namespace CarReservationSystem.Controllers
{
    [Route("api/users")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly ReservationsContext _context;

        public UsersController(ReservationsContext context)
        {
            _context = context;
        }

        // GET: api/users
        [HttpGet]
        public ActionResult<List<Users>> GetAllUsers()
        {
            return _context.CarUsers.ToList();
        }

        // GET api/users/5
        [HttpGet("{id}")]
        public ActionResult<Users> GetUserByID(int id)
        {
            var userItem = _context.CarUsers.Find(id);

            if (userItem == null)
            {
                return NotFound();
            }

            return userItem;
        }

        // POST api/users
        [HttpPost]
        public ActionResult<Users> PostUser(Users item)
        {
            _context.CarUsers.Add(item);
            _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetAllUsers), new { id = item.userID }, item);
        }

        // PUT: api/users/5
        [HttpPut("{id}")]
        public IActionResult PutUsers(int id, Users item)
        {
            if (id != item.userID)
            {
                return BadRequest();
            }

            _context.Entry(item).State = EntityState.Modified;
            _context.SaveChangesAsync();

            return NoContent();
        }

        // DELETE: api/users/5
        [HttpDelete("{id}")]
        public IActionResult DeleteUsers(int id)
        {
            Users userItem = _context.CarUsers.Find(id);

            if (userItem == null)
            {
                return NotFound();
            }

            _context.CarUsers.Remove(userItem);
            _context.SaveChangesAsync();

            return NoContent();
        }

    }
}
