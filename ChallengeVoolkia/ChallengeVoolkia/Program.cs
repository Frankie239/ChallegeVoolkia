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

            Console.WriteLine("Bienvenido, por favor ingrese el seller_id que quiere consultar, si desea agregar mas de uno, separelos por comas.");
            //Capture the seller_id(long string) 
            string seller = Console.ReadLine();
            //Splitting them by the commas. 
            string[] sellers = seller.Split(',');

            
            for (int i = 0; i < sellers.Count(); i++)
            {
                //For each one of them, execute this function: 
                WriteFileWithSellerId(sellers[i]);
            }
            

            Console.WriteLine("Escribiendo, por favor no presione enter y espere...");


            Console.ReadLine();
            
        }

        /// <summary>
        /// Method to have everything encapsulated. 
        /// </summary>
        /// <param name="seller_id"></param>
        private static async void WriteFileWithSellerId(string seller_id)
        {
            //Creating a list to store all the results. 
            List<Result> ObjectsResult = new List<Result>();
            //String to store the json without being parsed
            string rawData = await ApiController.GetProductsAsync(seller_id);

            //Geting the data into the list of objects to later be printed into the txt file.

            ObjectsResult = await ParseJsonToModel(rawData);

            //printing the file. 
            FileWriter(ObjectsResult, seller_id);


        }


       

                
        /// <summary>
        /// Gets the raw json data and selects it with the Json.Net library.
        /// </summary>
        /// <param name="rawJson">the json string substracted from the api request</param>
        /// <returns></returns>
        private static async Task<List<Result>> ParseJsonToModel(string rawJson)
        {
            List<Result> Results = new List<Result>(); 

            JObject jsonParsed = JObject.Parse(rawJson);

            for (int i = 0; i < jsonParsed["results"].Count()-1; i++)
            {
                //Create a new object to later store it into the list. 
                Result parsedObject = new Result()
                {
                    id = jsonParsed["results"][i]["id"].ToString(),
                    title = jsonParsed["results"][i]["title"].ToString(),
                    category_id = jsonParsed["results"][i]["category_id"].ToString(),
                    


                };
                parsedObject.name = await ApiController.GetCategoryName(parsedObject.category_id);

                Results.Add(parsedObject);
            }

            //List<Result> restults = (List<Result>)JsonSerializer.Deserialize(splitJson[1], typeof(Result));

            return Results;
            
        }

      



        /// <summary>
        /// Method to give format and to print the txt file
        /// </summary>
        /// <param name="results">a list of the objects to print</param>
        /// <param name="seller_id"> the seller id, just to give a title and a header</param>
        private static async void FileWriter(List<Result> results, string seller_id)
        {
            int index = 1; 
            StringBuilder sb = new StringBuilder();

            sb.AppendLine("Seller:        "+seller_id);

            foreach(Result res in results)
            {
                sb.Append(res.Serialize(index));

                index++;
            }

            await File.WriteAllTextAsync("LOG-"+seller_id+"-.txt", sb.ToString());

            Console.WriteLine("Finnished " +seller_id);

        }



    }
}
                
                
                
                

                
              
                


            




        


