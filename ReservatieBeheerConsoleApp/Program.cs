using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace ReservatieBeheerConsoleApp
{
    class Program
    {
        static readonly HttpClient client = new HttpClient();
        static async Task Main(string[] args)
        {
            try
            {
                DateTime now = DateTime.Now;
                DateTime dateForReservatie = new DateTime(now.Year, now.Month, now.Day, now.Hour, 30, 0).AddDays(1);

                var postResponse = await MaakReservatie(new ReservatieDto
                {
                    AantalPlaatsen = 2,
                    Datum = dateForReservatie
                }, 2, 2);

                Console.WriteLine("POST Response: " + postResponse);
            }
            catch (HttpRequestException e)
            {
                Console.WriteLine("\nException Caught!");
                Console.WriteLine("Message :{0} ", e.Message);
            }
            try
            {
                // GET request: Haal een specifieke reservering op
                var getResponse = await HaalReservatiesOp(1); // Voorbeeld: reserverings-ID 1
                Console.WriteLine("GET Response: " + getResponse);
            }
            catch (HttpRequestException e)
            {
                Console.WriteLine("\nException Caught!");
                Console.WriteLine("Message :{0} ", e.Message);
            }
        }

        static async Task<string> MaakReservatie(ReservatieDto reservatie, int klantId, int tafelNummer)
        {
            string json = JsonConvert.SerializeObject(reservatie);
            StringContent content = new StringContent(json, Encoding.UTF8, "application/json");

            // Construct the URL with the provided klantId and tafelNummer
            string url = $"https://localhost:7175/api/Reservatie/maakReservatie/{klantId}/{tafelNummer}";

            HttpResponseMessage response = await client.PostAsync(url, content);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadAsStringAsync();
        }
        static async Task<string> HaalReservatiesOp(int klantId)
        {
            // Construct the URL with the query parameter klantId
            string url = $"https://localhost:7175/api/Reservatie/zoekReservaties?klantId={klantId}";

            HttpResponseMessage response = await client.GetAsync(url);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadAsStringAsync();
        }
    }

    public class ReservatieDto
    {
        public int AantalPlaatsen { get; set; }
        public DateTime Datum { get; set; }
    }
}
