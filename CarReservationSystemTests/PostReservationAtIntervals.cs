using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CarReservationSystemTests
{
    public class PostReservationsAtIntervals
    {
        public static async Task PostReservations()
        {
            try
            {
                // Create a new Reservation
               

                // Create a new Car
                Car orgCar = new Car
                {
                    make = "RandomMake",
                    license = "RandomLicense",
                    rentalPricePerDay = 10.0
                };

                Car newCar = new Car();
                var url = await Program.CreateCarAsyn(orgCar);
                var query = url.Query.Substring(url.Query.IndexOf('=')+1);

                 Reservations orgReservation = new Reservations
                {
                    userID = 1,
                    carID = Convert.ToInt32(query),
                    reservationStartTime = DateTime.Parse("2020-08-10"),
                    reservationEndTime = DateTime.Parse("2020-08-11")
                };
                Reservations newReservation = new Reservations();
                await Program.CreateReservationAsync(orgReservation);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }
}