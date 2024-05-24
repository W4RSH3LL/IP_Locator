using System;
using System.Net.Http;
using Newtonsoft.Json;
using System.Threading.Tasks;

namespace ip_locator
{
    public class Data
    {
        public string city { get; set; }
        public string region { get; set; }
        public string country { get; set; }
        public string loc { get; set; }
        public string org { get; set; }
        public string postal { get; set; }
        public string timezone { get; set; }
    }

    class Program
    {
        static async Task Main(string[] args)
        {
            Console.Title = "IP Locator"; // Setting a Console Title
            Console.Write("Enter IP to locate: "); // Asking user for input
            string ip = Console.ReadLine(); // Reading input
            string url = $"https://ipinfo.io/{ip}/json"; // Crafting url with user input

            using (HttpClient client = new HttpClient()) // Creating our HTTP Client
            {
                try
                {
                    HttpResponseMessage response = await client.GetAsync(url); // Creating a response object from the client url
                    response.EnsureSuccessStatusCode();

                    Console.WriteLine("[+] Request Successfully Crafted ! [+]"); // Update user

                    string responseData = await response.Content.ReadAsStringAsync(); // Creating response 
                    Data ipInfo = JsonConvert.DeserializeObject<Data>(responseData); // Creating Data object and deserializing it

                    Console.Clear();
                    Console.WriteLine($"Country: {ipInfo.country}");
                    Console.WriteLine($"City: {ipInfo.city}");
                    Console.WriteLine($"Coordinates: {ipInfo.loc}");
                    Console.WriteLine($"Postal Code: {ipInfo.postal}");
                    Console.WriteLine($"Region: {ipInfo.region}");
                    Console.WriteLine($"ASN: {ipInfo.org}");
                    Console.WriteLine($"Time Zone: {ipInfo.timezone}");
                    string[] Coords = ipInfo.loc.Split(',');
                    Console.WriteLine($"Google Maps: https://www.google.com/maps/?q={Coords[0]},{Coords[1]}");
                }
                catch (HttpRequestException ex)
                {
                    Console.WriteLine($"Error : {ex.Message}");
                }
            }

            Console.WriteLine("Press any key to exit...");
            Console.ReadLine(); // Wait for user input before closing the console
        }
    }
}
