using System;
using Microsoft.EntityFrameworkCore;
using CarReservationSystem.Models;

namespace CarReservationSystem.Data
{
    public class ReservationsContext : DbContext
    {
        public DbSet<Reservations> CarReservations { get; set; }
        public DbSet<Car> Cars { get; set; }
        public DbSet<Users> CarUsers { get; set; }

        public ReservationsContext(DbContextOptions<ReservationsContext> options) : base(options)
        {
        }
    }
}
