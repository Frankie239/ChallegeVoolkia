using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Text.Json;

namespace ChallengeVoolkia
{
    class Program
    {
        static void Main(string[] args)
        {
            GetProductsAsync("179571326");
            Console.ReadKey();
        }


        /// <summary>
        /// Makes a request to the MELI Api to return all the products from a specificy seller. 
        /// </summary>
        /// <param name="seller_id">the seller id expresed as a string.</param>
        /// <returns></returns>
        private static async Task<string> GetProductsAsync(string seller_id)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://api.mercadolibre.com/sites/MLA/search?seller_id=");
                //client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                //This returns the whole list requested from the seller. 
                string response = await client.GetStringAsync(client.BaseAddress + seller_id);

                //r.ToString();
                //Console.WriteLine(response);
                
                return response;
            }
        }
        public class Result
        {
            public string Id { set; get; }
            public string Title { set; get; }
            public string Category_id { get; set; }

        }
    }
}
