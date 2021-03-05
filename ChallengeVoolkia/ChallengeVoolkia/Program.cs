using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace ChallengeVoolkia
{
    class Program
    {
        static void Main(string[] args)
        {
            GetProductsAsync("179571326");
            Console.ReadKey();
        }

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

    }
}
