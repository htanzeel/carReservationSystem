using System;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace CarReservationSystem.Models
{
    public class Reservations
    {
        [Key] public int reservationID { get; set; }
        public int userID { get; set; }
        public int carID { get; set; }
        public DateTime reservationStartTime { get; set; }
        public DateTime reservationEndTime { get; set; }
        [DisplayFormat(DataFormatString = "{0:0.###}")]
        public double totalCost { get; set; }
    }
}
