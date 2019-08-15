using System;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace CarReservationSystem.Models
{
    public class Users
    {
        [Key] public int userID { get; set; }
        public string firstName { get; set; }
        public string LastName { get; set; }
        public string email { get; set; }
        public string licenseNumber { get; set; }
    }
}
