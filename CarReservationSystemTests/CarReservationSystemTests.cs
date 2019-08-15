using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.ComponentModel;
using System.Collections.Generic;

namespace CarReservationSystemTests
{
    public class Reservations
    {
        public int reservationID { get; set; }
        public int userID { get; set; }
        public int carID { get; set; }
        public DateTime reservationStartTime { get; set; }
        public DateTime reservationEndTime { get; set; }
        public double totalCost { get; set; }

    }

    public class Car
    {
        public int carID { get; set; }
        public string make { get; set; }
        public string license { get; set; }
        public double rentalPricePerDay { get; set; }
    }

    class Program
    {
        public const string reservationUri = "http://localhost:80/appointments";
        public static HttpClient client = new HttpClient();

        public static string ShowReservation(Reservations reservation)
        {
            return ($"reservationID: {reservation.reservationID}\tuserID: " +
                $"{reservation.userID}\tcarID: {reservation.carID}\treservationStartTime: " +
                $"{reservation.reservationStartTime}\treservationEndTime: {reservation.reservationEndTime}\t" +
                $"totalCost: {reservation.totalCost}");
        }

        public static async Task<Uri> CreateReservationAsync(Reservations reservation)
        {
            HttpResponseMessage response = await client.PostAsJsonAsync(
                "api/appointments", reservation);
            response.EnsureSuccessStatusCode();

            // return URI of the created resource.
            return response.Headers.Location;
        }

        public static async Task<HttpStatusCode> CreateReservationAsyncStatusCode(Reservations reservation)
        {
            HttpResponseMessage response = await client.PostAsJsonAsync(
                "api/appointments", reservation);
            //response.EnsureSuccessStatusCode();

            // return URI of the created resource.
            return response.StatusCode;
        }

        public static async Task<Uri> CreateCarAsyn(Car newCar)
        {
            HttpResponseMessage response = await client.PostAsJsonAsync(
                "api/cars", newCar);
            response.EnsureSuccessStatusCode();

            // return URI of the created resource.
            return response.Headers.Location;
        }

        public static async Task<Reservations> GetReservationAsync(string path)
        {
            Reservations reservation = null;
            HttpResponseMessage response = await client.GetAsync(path);
            if (response.IsSuccessStatusCode)
            {
                string data = await response.Content.ReadAsStringAsync();
                reservation = JsonConvert.DeserializeObject<Reservations>(data);
            }
            return reservation;
        }

        public static async Task<Reservations> UpdateReservationAsync(Reservations reservation)
        {
            HttpResponseMessage response = await client.PutAsJsonAsync(
                $"api/appointments/{reservation.reservationID}", reservation);
            response.EnsureSuccessStatusCode();

            // Deserialize the updated Reservation from the response body.
            string data = await response.Content.ReadAsStringAsync();
            reservation = JsonConvert.DeserializeObject<Reservations>(data);
            return reservation;
        }

        public static async Task<HttpStatusCode> DeleteReservationAsync(string id)
        {
            HttpResponseMessage response = await client.DeleteAsync(
                $"api/appointments/{id}");
            return response.StatusCode;
        }

        static void Main()
        {
            Program.client.BaseAddress = new Uri("http://localhost:80/");
            Program.client.DefaultRequestHeaders.Accept.Clear();
            Program.client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));

            GetReservation.GetReservationByID().GetAwaiter().GetResult();
            GetReservation.GetNonExistingID().GetAwaiter().GetResult();
            PostReservation.PostWithUniqueDate().GetAwaiter().GetResult();
            PostReservation.PostWithExistingDateRange().GetAwaiter().GetResult();
            UpdateReservation.UpdateCarID().GetAwaiter().GetResult();
            DeleteReservation.DeleteExistingID().GetAwaiter().GetResult();
            DeleteReservation.DeleteNonExistingID().GetAwaiter().GetResult();

            
            // run in a loop for schedular

            /* 
            Random random = new Random();
            var waitTime = random.Next(1,5);
            DateTime quitTime = DateTime.Now.AddMinutes(waitTime);
            TimeToQuitAsync(quitTime).GetAwaiter().GetResult();
            var task = Task.Factory.StartNew(() => TimeToQuitAsync(quitTime));
            var temp = task.GetAwaiter().GetResult();
            */

        }

        public static async Task TimeToQuitAsync(DateTime timeToAct)
        {
            Boolean timeHasCome = false;
            while (!timeHasCome)
            {
                System.Threading.Thread.Sleep(60000);

                timeHasCome |= DateTime.Now >= timeToAct;
            }

            if (timeHasCome)
            {
                await PostReservationsAtIntervals.PostReservations();
            }
        }

        
    }
}