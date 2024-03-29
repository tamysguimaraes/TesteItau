﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Products.Data.Entities
{
    public class ProductEntity
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [MaxLength(14)]
        [Required]
        public string cBarCode { get; set; }
        public string cName { get; set; }
        public string cCategory { get; set; }
        public decimal nValue { get; set; }
    }
}
