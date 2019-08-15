using System;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace CarReservationSystem.Models
{
    public class Car
    {
        [Key] public int carID { get; set; }
        public string make { get; set; }
        public string license { get; set; }
        [DisplayFormat(DataFormatString = "{0:0.###}")]
        public double rentalPricePerDay { get; set; }
    }
}
