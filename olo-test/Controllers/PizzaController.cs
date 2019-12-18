using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using olo_test.Models;

namespace olo_test.Controllers
{
    [Route("api/pizza")]
    [ApiController]
    public class PizzaController : ControllerBase
    {
        [HttpGet("favorites")]
        public List<Favorite> GetTop20Toppings()
        {

            var toppingHashMap = new Dictionary<string, Favorite>();
            var client = new System.Net.WebClient();

            client.DownloadFile("http://files.olo.com/pizzas.json", @"temp\fav.json");

            var myJsonString = System.IO.File.ReadAllText(@"temp\fav.json"); ;
            var orders = JsonConvert.DeserializeObject<List<Order>>(myJsonString);
            JArray arr = JArray.Parse(myJsonString);


            foreach (var order in arr)
            {
                var toppingsField = order.First();
                var toppings = toppingsField.Values();
                var toppingComboKey = "";
                var toppingList = new List<string>();

                foreach (var topping in toppings)
                {
                    var str = topping.Value<string>();
                    toppingComboKey += str;
                    toppingList.Add(str);
                }

                //after combining all toppings into a string, sort the string.
                //this allows [cheese, sausage] & [sausage, cheese] to have the same key and be counted the same
                toppingComboKey = String.Concat(toppingComboKey.OrderBy(c => c));

                if (toppingHashMap.ContainsKey(toppingComboKey))
                {
                    var toppingHash = toppingHashMap[toppingComboKey].OrderCount++;
                }
                else
                {
                    //add the list of toppings for viewing.
                    toppingHashMap.Add(toppingComboKey, new Favorite() { OrderCount = 1, Toppings = toppingList });
                }

            }
            var favList = toppingHashMap.ToList();

            favList.Sort((a, b) => b.Value.OrderCount.CompareTo(a.Value.OrderCount));

            //get 0-19, ignore the rest.
            favList.RemoveRange(20, favList.Count - 20);

            //dont need keys anymore.
            var favs = new List<Favorite>();
            foreach (var fav in favList)
            {
                favs.Add(fav.Value);
            }

            return favs;
        }
    }
}