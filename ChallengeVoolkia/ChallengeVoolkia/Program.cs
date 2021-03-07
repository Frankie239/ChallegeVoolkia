﻿using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Collections.Generic;
using Newtonsoft.Json.Linq;
using System.Linq;

namespace ChallengeVoolkia
{
    class Program
    {
        static void Main(string[] args)
        {



            
            WriteFileWithSellerId("179571326");
            
            
            Console.ReadKey();
        }

        private static async void WriteFileWithSellerId(string seller_id)
        {
            string rawData = await GetProductsAsync(seller_id);

            ParseJsonToModel(rawData);

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

            Console.WriteLine(results["results"][0]["id"]);
            Console.WriteLine(results["results"][0]["title"]);
            Console.WriteLine(results["results"][0]["category_id"]);
            Console.WriteLine(results["results"].Count());



            //List<Result> restults = (List<Result>)JsonSerializer.Deserialize(splitJson[1], typeof(Result));

            return Results;
            
        }

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
            Console.WriteLine(results["name"].ToString());
           
            return results["name"].ToString();
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
