using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace ChallengeVoolkia
{
    static class ApiController
    {
        private static async Task<string> HttpGetToMeliApi(string requestString)
        {
            string response = "";

            using (var client = new HttpClient())
            {
                //add a base addres. so this request can be done independent from the seller.
                client.BaseAddress = new Uri("https://api.mercadolibre.com");
                client.DefaultRequestHeaders.Accept.Clear();
                //Accept the data type. 
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                //This returns the whole list requested from the seller. 
                response = await client.GetStringAsync(client.BaseAddress + requestString);
            }

            return response;

        }

        /// <summary>
        /// Makes a request to the MELI API to get the category name of a specific
        /// category_id. then returns it to be used as a string. 
        /// </summary>
        /// <param name="category_id"></param>
        /// <returns></returns>
        public static async Task<string> GetCategoryName(string category_id)
        {
            string response = await HttpGetToMeliApi("/categories/" + category_id);

            JObject results = JObject.Parse(response);

            return results["name"].ToString();
        }

        /// <summary>
        /// Makes a request to the MELI Api to return all the products from a specificy seller. 
        /// </summary>
        /// <param name="seller_id">the seller id expresed as a string.</param>
        /// <returns></returns>
        public static async Task<string> GetProductsAsync(string seller_id)
        {
            string response;
            List<Result> itemsBySeller = new List<Result>();
            response = await HttpGetToMeliApi("/sites/MLA/search?seller_id=" + seller_id);

            return response.ToString();
        }
    }
}
