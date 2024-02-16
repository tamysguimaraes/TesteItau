using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Products.Domain.Models
{
    public class Product
    {
        public int Id { get; set; }
        public string cBarCode { get; set; }
        public string cName { get; set; }
        public string cCategory { get; set; }
        public decimal nValue { get; set; }
    }
}
