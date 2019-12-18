using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace olo_test.Models
{
    public class Order
    {
        //[JsonPropertyName("toppings")]
        private IEnumerable<String> Toppings { get; set; }
    }
}
