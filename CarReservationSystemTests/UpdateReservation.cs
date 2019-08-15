using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CarReservationSystemTests
{
    public class UpdateReservation
    {
        public static async Task UpdateCarID()
        {
            try
            {
                // Create a new Reservation
                Reservations orgReservation = new Reservations
                {
                    reservationID = 40,
                    userID = 1,
                    carID = 2,
                    reservationStartTime = DateTime.Parse("2020-08-10"),
                    reservationEndTime = DateTime.Parse("2020-08-15"),
                    totalCost = 500.00
                };

                // Create a Reservation for Update for carID 3
                Reservations orgupdatedReservation = new Reservations
                {
                    reservationID = 40,
                    userID = 1,
                    carID = 3,
                    reservationStartTime = DateTime.Parse("2020-08-10"),
                    reservationEndTime = DateTime.Parse("2020-08-15"),
                    totalCost = 2500.00
                };

                Reservations newReservation = new Reservations();
                var url = await Program.CreateReservationAsync(orgReservation);

                // Get the Reservation
                var query = url.Query.Substring(url.Query.IndexOf('=')+1);
                newReservation = await Program.GetReservationAsync(url.AbsolutePath + $"/{query}");
                Assert.IsNotNull(newReservation);
                Assert.AreEqual(Program.ShowReservation(orgReservation), Program.ShowReservation(newReservation));
                
                // Update the Reservation
                newReservation.carID = 3;
                await Program.UpdateReservationAsync(newReservation);

                // Get the updated Reservation
                newReservation = await Program.GetReservationAsync(url.AbsolutePath + $"/{query}");
                Assert.IsNotNull(newReservation);
                Assert.AreEqual(Program.ShowReservation(orgupdatedReservation), Program.ShowReservation(newReservation));
                 
                // Delete the Reservation
                var statusCode = await Program.DeleteReservationAsync(newReservation.reservationID.ToString());
                Assert.IsNotNull(statusCode);
                Assert.AreEqual(204, (int)statusCode);

                // Get deleted Reservation
                newReservation = await Program.GetReservationAsync(url.AbsolutePath + $"/{query}");
                Assert.IsNull(newReservation);

                Console.WriteLine("UpdateCarID: " + "Pass");
                
            }
            catch (Exception e) 
            {
                Console.WriteLine(e.Message);
            }
        }
    }
}