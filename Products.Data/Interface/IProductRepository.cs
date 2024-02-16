using Products.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Products.Data.Interface
{
    public interface IProductRepository
    {
        List<ProductEntity> GetProducts();
        ProductEntity GetProductById(int prodId);
        Task<IEnumerable<ProductEntity>> GetProductsByName(string Name);
        int InsertProduct(ProductEntity product);
        ProductEntity UpdateProduct(ProductEntity product);
        bool DeleteProduct(ProductEntity product);
        bool ProductExists(string cBarCode);
    }
}
