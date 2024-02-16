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
        public ActionResult<List<Product>> GetProductsByName(string cName)
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

        // PUT: api/Products/5
        [HttpPut]
        public ActionResult PutProduct(Product product)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);
                var productCreated = _productService.UpdateProduct(product);
                if (productCreated.Id == 0)
                    return BadRequest($"Erro: {productCreated.Message}");

                return Ok(productCreated);
            }
            catch (Exception ex)
            {
                return BadRequest($"Erro: {ex.Message}");
            }

        }

        // DELETE: api/Products/5
        [HttpDelete("{id}")]
        public ActionResult DeleteProduct(int prodId)
        {
            try
            {
                if (_productService.DeleteProduct(prodId))
                    return Ok("Produto deletado");
                else
                    return BadRequest("ID não encontrado!");

            }
            catch (Exception ex)
            {
                return BadRequest($"Erro: {ex.Message}");
            }
        }

    }
}
