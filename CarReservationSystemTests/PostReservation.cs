using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CarReservationSystemTests
{
    public class PostReservation
    {
        public static async Task PostWithUniqueDate()
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

                Reservations newReservation = new Reservations();
                var url = await Program.CreateReservationAsync(orgReservation);

                // Get the Reservation
                var query = url.Query.Substring(url.Query.IndexOf('=')+1);
                newReservation = await Program.GetReservationAsync(url.AbsolutePath + $"/{query}");
                Assert.IsNotNull(newReservation);
                Assert.AreEqual(Program.ShowReservation(orgReservation), Program.ShowReservation(newReservation));

                // Delete the Reservation
                var statusCode = await Program.DeleteReservationAsync(newReservation.reservationID.ToString());
                Assert.IsNotNull(statusCode);
                Assert.AreEqual(204, (int)statusCode);

                // Get deleted Reservation
                newReservation = await Program.GetReservationAsync(url.AbsolutePath + $"/{query}");
                Assert.IsNull(newReservation);

                Console.WriteLine("PostWithUniqueDate: " + "Pass");
                
            }
            catch (Exception e) 
            {
                Console.WriteLine(e.Message);
            }
        }

        public static async Task PostWithExistingDateRange()
        {
            try
            {
                // Create a new Reservation
                Reservations orgReservation = new Reservations
                {
                    reservationID = 40,
                    userID = 1,
                    carID = 1,
                    reservationStartTime = DateTime.Parse("2020-08-01"),
                    reservationEndTime = DateTime.Parse("2020-08-15"),
                    totalCost = 2800
                };

                Reservations orgReservationSameDates = new Reservations
                {
                    reservationID = 41,
                    userID = 1,
                    carID = 1,
                    reservationStartTime = DateTime.Parse("2020-08-01"),
                    reservationEndTime = DateTime.Parse("2020-08-15"),
                    totalCost = 2800
                };

                Reservations newReservation = new Reservations();
                var url = await Program.CreateReservationAsync(orgReservation);

                // Get the Reservation
                var query = url.Query.Substring(url.Query.IndexOf('=')+1);
                newReservation = await Program.GetReservationAsync(url.AbsolutePath + $"/{query}");
                Assert.IsNotNull(newReservation);
                Assert.AreEqual(Program.ShowReservation(orgReservation), Program.ShowReservation(newReservation));

                // Post bad request
                var newReservation2 = new Reservations();
                var statusCode = await Program.CreateReservationAsyncStatusCode(orgReservationSameDates);
                Assert.AreEqual(400, (int)statusCode);

                // Get posted Reservation
                newReservation2 = await Program.GetReservationAsync(Program.reservationUri + "/" + orgReservationSameDates.reservationID.ToString());
                Assert.IsNull(newReservation2);

                // Delete the Reservation
                var statusCodeDelete = await Program.DeleteReservationAsync(orgReservation.reservationID.ToString());
                Assert.IsNotNull(statusCodeDelete);
                Assert.AreEqual(204, (int)statusCodeDelete);

                // Get deleted Reservation
                newReservation = await Program.GetReservationAsync(url.AbsolutePath + $"/{query}");
                Assert.IsNull(newReservation);

                Console.WriteLine("PostWithExistingDateRange: " + "Pass");
                
            }
            catch (Exception e) 
            {
                Console.WriteLine(e.Message);
            }
        }
    }
}