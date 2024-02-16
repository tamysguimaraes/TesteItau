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
        [Required(ErrorMessage = "ID é campo obrigatório")]
        public int Id { get; set; }
        [Required(ErrorMessage = "Código de Barras é campo obrigatório")]
        [StringLength(14, ErrorMessage = "Código de Barras deve ter no máximo {1} caracteres.")]
        public string cBarCode { get; set; }
        public string cName { get; set; }
        public string cCategory { get; set; }
        public decimal nValue { get; set; }
    }
}
