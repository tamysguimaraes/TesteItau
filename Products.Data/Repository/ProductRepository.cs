using Microsoft.EntityFrameworkCore;
using Products.Data.Context;
using Products.Data.Entities;
using Products.Data.Interface;

namespace Products.Data.Repository
{
    public class ProductRepository : IProductRepository
    {
        private readonly APIContext _context;

        public ProductRepository(APIContext context)
        {
            _context = context;
        }

        public List<ProductEntity> GetProducts()
        {
            return  _context.ProductEntity.ToList();
        }

        public ProductEntity GetProductById(int prodId)
        {
            var result =  _context.ProductEntity.Find(prodId);
            if (result == null)
                return null;
            else
                return result;
        }

        public async Task<IEnumerable<ProductEntity>> GetProductsByName(string Name)
        {
            return await _context.ProductEntity.Where(x => x.cName.ToUpper().Contains(Name.ToUpper())).ToListAsync();
        }

        public int InsertProduct(ProductEntity product)
        {
            _context.ProductEntity.Add(product);
            _context.SaveChanges();
            return product.Id;
        }

        public ProductEntity UpdateProduct(ProductEntity product)
        {
            try
            {
                _context.Entry(product).State = EntityState.Modified;
                _context.SaveChangesAsync();
                return product;
            }
            catch (DbUpdateConcurrencyException)
            {

                throw;
            }
            
        }

        public bool DeleteProduct(ProductEntity product)
        {
            try
            {
                _context.ProductEntity.Remove(product);
                _context.SaveChanges();
                return true;
            }
            catch (DbUpdateConcurrencyException)
            {
                return false;
            }
        }

        public bool ProductExists(string cBarCode)
        {
            return _context.ProductEntity.Any(e => e.cBarCode == cBarCode);
        }
    }
}
