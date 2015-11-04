using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlooringProgram.Models
{
    public class AddEntryRequest
    {

        public string CustomerName { get; set; }
        public string State { get; set; }
        public string ProductType { get; set; }
        public decimal Area { get; set; }
        public string OrderDate { get; set; }

    }
}
