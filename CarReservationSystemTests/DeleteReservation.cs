using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CarReservationSystemTests
{
    public class DeleteReservation
    {
        public static async Task DeleteExistingID()
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

                var url = await Program.CreateReservationAsync(orgReservation);
                var query = url.Query.Substring(url.Query.IndexOf('=')+1);
                
                // Delete the Reservation
                var statusCode = await Program.DeleteReservationAsync(orgReservation.reservationID.ToString());
                Assert.IsNotNull(statusCode);
                Assert.AreEqual(204, (int)statusCode);

                // Get deleted Reservation
                Reservations newReservation = await Program.GetReservationAsync(url.AbsolutePath + $"/{query}");
                Assert.IsNull(newReservation);

                Console.WriteLine("DeleteExistingID: " + "Pass");
                
            }
            catch (Exception e) 
            {
                Console.WriteLine(e.Message);
            }
        }

        public static async Task DeleteNonExistingID()
        {
            try
            {
                var newReservation = await Program.GetReservationAsync(Program.reservationUri  + $"/1000");
                Assert.IsNull(newReservation);

                // Delete the Reservation
                var statusCode = await Program.DeleteReservationAsync("1000");
                Assert.IsNotNull(statusCode);
                Assert.AreEqual(404, (int)statusCode);

                // Get deleted Reservation
                Reservations delReservation = await Program.GetReservationAsync(Program.reservationUri + "/" + "1000");
                Assert.IsNull(delReservation);

                Console.WriteLine("DeleteNonExistingID: " + "Pass");
                
            }
            catch (Exception e) 
            {
                Console.WriteLine(e.Message);
            }
        }
    }
}