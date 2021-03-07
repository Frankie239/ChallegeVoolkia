using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Collections.Generic;
using Newtonsoft.Json.Linq;
using System.Linq;
using System.IO;
using System.Text;

namespace ChallengeVoolkia
{
    class Program
    {
        static void Main(string[] args)
        {

            Console.WriteLine("Bienvenido, por favor ingrese el seller_id que quiere consultar");
            string seller = Console.ReadLine();

            
            WriteFileWithSellerId(seller);

            Console.WriteLine("Escribiendo, por favor espere");

            

            Console.ReadKey();
        }

        private static async void WriteFileWithSellerId(string seller_id)
        {
            List<Result> ObjectsResult = new List<Result>();
            string rawData = await GetProductsAsync(seller_id);

            ObjectsResult = await ParseJsonToModel(rawData);

            FileWriter(ObjectsResult, seller_id);


        }


        /// <summary>
        /// Makes a request to the MELI Api to return all the products from a specificy seller. 
        /// </summary>
        /// <param name="seller_id">the seller id expresed as a string.</param>
        /// <returns></returns>
        private static async Task<string> GetProductsAsync(string seller_id)
        {
            string response = "";
            List<Result> itemsBySeller = new List<Result>();

            //create a Idisposable object so it can be throw away when this execution ends. 
            using (var client = new HttpClient())
            {
                //add a base addres. so this request can be done independent from the seller.
                client.BaseAddress = new Uri("https://api.mercadolibre.com/sites/MLA/search?seller_id=");
                client.DefaultRequestHeaders.Accept.Clear();
                //Accept the data type. 
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                //This returns the whole list requested from the seller. 
                response = await client.GetStringAsync(client.BaseAddress + seller_id);
            }


            return response.ToString();
        }

                
        /// <summary>
        /// Gets the raw json data and selects it with the Json.Net library.
        /// </summary>
        /// <param name="rawJson">the json string substracted from the api request</param>
        /// <returns></returns>
        private static async Task<List<Result>> ParseJsonToModel(string rawJson)
        {
            List<Result> Results = new List<Result>(); 

            JObject results = JObject.Parse(rawJson);

            for (int i = 0; i < results["results"].Count()-1; i++)
            {
                Result parsedObject = new Result()
                {
                    id = results["results"][i]["id"].ToString(),
                    title = results["results"][i]["title"].ToString(),
                    category_id = results["results"][i]["category_id"].ToString(),
                    


                };
                parsedObject.name = await GetCategoryName(parsedObject.category_id);

                Results.Add(parsedObject);
            }

            //List<Result> restults = (List<Result>)JsonSerializer.Deserialize(splitJson[1], typeof(Result));

            return Results;
            
        }

        /// <summary>
        /// Makes a request to the MELI API to get the category name of a specific
        /// category_id. then returns it to be used as a string. 
        /// </summary>
        /// <param name="category_id"></param>
        /// <returns></returns>
        private static async Task<string> GetCategoryName(string category_id)
        {
            string response = "";
            using (var client = new HttpClient())
            {
                //add a base addres. so this request can be done independent from the seller.
                client.BaseAddress = new Uri("https://api.mercadolibre.com/categories/");
                client.DefaultRequestHeaders.Accept.Clear();
                //Accept the data type. 
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                //This returns the whole list requested from the seller. 
                response = await client.GetStringAsync(client.BaseAddress + category_id);


            }
            JObject results = JObject.Parse(response);
                     
            return results["name"].ToString();
        }

        private static async void FileWriter(List<Result> results, string seller_id)
        {
            int index = 1; 
            StringBuilder sb = new StringBuilder();

            sb.AppendLine("Seller:        "+seller_id);

            foreach(Result res in results)
            {
                sb.AppendLine(index + "--------------------------------------------------");
                sb.AppendLine("ID: " + res.id);
                sb.AppendLine("Title: " + res.title);
                sb.AppendLine("Cat.ID: " + res.category_id);
                sb.AppendLine("Cat. Name: " + res.name);
                
                index++;
            }

            await File.WriteAllTextAsync("LOG.TXT", sb.ToString());
            Console.WriteLine("Finished Writing");

        }

        public class Result
        {
            public string id;

            public string title;

            public string category_id;

            public string name;
           
        }


    }
}
                
                
                
                

                
              
                


            




        


