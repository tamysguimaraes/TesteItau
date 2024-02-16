using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API.Test.Requests
{
    public class ProductRequest
    {
        public string cBarCode { get; set; }
        public string cName { get; set; }
        public string cCategory { get; set; }
        public decimal nValue { get; set; }
    }
}
