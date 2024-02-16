﻿using Products.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Products.Domain.Interface
{
    public interface IProductService
    {
        List<Product> GetProducts();
        Product GetProductById(int prodId);
        List<Product> GetProductsByName(string Name);
        ProductCreated InsertProduct(ProductCreate product);
        ProductCreated UpdateProduct(Product product);
        bool DeleteProduct(int prodId);
        bool ProductExists(string cBarCode);
    }
}
