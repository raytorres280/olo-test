using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace olo_test.Models
{
    public class Favorite
    {
        public int OrderCount { get; set; }
        public List<string> Toppings { get; set; }
    }
}
