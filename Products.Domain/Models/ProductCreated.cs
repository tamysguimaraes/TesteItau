using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Products.Domain.Models
{
    public class ProductCreated
    {
        public int Id { get; set; }
        public string cBarCode { get; set; }
        public string Message { get; set; }
    }
}
