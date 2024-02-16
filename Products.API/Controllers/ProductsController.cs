using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Products.Domain.Interface;
using Products.Domain.Models;
using System.Net;

namespace Products.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private IProductService _productService;

        public ProductsController(IProductService productService)
        {
            _productService = productService;
        }

        /// GET: api/Products
        [HttpGet]
        public ActionResult GetProducts()
        {
            try
            {
                var result = _productService.GetProducts();
                if (result.Count > 0)
                    return Ok(result);
                else
                    return BadRequest("Não há produtos cadastrados!");

            }
            catch (Exception ex)
            {
                return BadRequest($"Erro: {ex.Message}");
            }

        }

        // GET: api/Products/5
        [HttpGet("GetProductByID")]
        public ActionResult GetProductById(int id)
        {
            try
            {
                var result = _productService.GetProductById(id);
                if (result != null)
                    return Ok(result);
                else
                    return BadRequest("ID não encontrado!");

            }
            catch (Exception ex)
            {
                return BadRequest($"Erro: {ex.Message}");
            }
        }

        // api/Products/Name/{cName}
        [HttpGet("GetProductByName")]
        public ActionResult<IEnumerable<Product>> GetProductsByName(string cName)
        {
            try
            {
                var result = _productService.GetProductsByName(cName);
                if (result.Count>0)
                    return Ok(result);
                else
                    return BadRequest("Produto não encontrado!");

            }
            catch (Exception ex)
            {
                return BadRequest($"Erro: {ex.Message}");
            }
        }


        // POST: api/Products
        [HttpPost]
        public ActionResult PostProduct(ProductCreate product)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                var productCreated = _productService.InsertProduct(product);
                if (productCreated.Id == 0)
                    return BadRequest($"Erro: {productCreated.Message}");

                return Ok(productCreated);
            }
            catch (Exception ex)
            {
                return BadRequest($"Erro: {ex.Message}");
            }

        }
        
        //// PUT: api/Products/5
        //[HttpPut("{id}")]
        //public Task<IActionResult> PutProduct(long id, string cBarCode, Product product)
        //{
        //    try
        //    {
        //        //if (id != product.Id && cBarCode != product.cBarCode)
        //        //{
        //        //    return BadRequest();
        //        //}

        //        //_context.Entry(product).State = EntityState.Modified;

        //        //try
        //        //{
        //        //    await _context.SaveChangesAsync();
        //        //}
        //        //catch (DbUpdateConcurrencyException)
        //        //{
        //        //    if (!ProductExists(product.cBarCode))
        //        //    {
        //        //        return NotFound();
        //        //    }
        //        //    else
        //        //    {
        //        //        throw;
        //        //    }
        //        //}

        //        return NoContent();
        //    }
        //    catch (Exception ex)
        //    {
        //        return BadRequest($"Erro: {ex.Message}");
        //    }

        //}

        //// DELETE: api/Products/5
        //[HttpDelete("{id}")]
        //public Task<IActionResult> DeleteProduct(long id)
        //{
        //    try
        //    {
        //        //var product = await _context.Products.FindAsync(id);
        //        //if (product == null)
        //        //{
        //        //    return NotFound();
        //        //}

        //        //_context.Products.Remove(product);
        //        //await _context.SaveChangesAsync();

        //        return NoContent();
        //    }
        //    catch (Exception ex)
        //    {
        //        return BadRequest($"Erro: {ex.Message}");
        //    }

        //}

    }
}
