using Products.Domain.Interface;
using Products.Domain.Models;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using Products.Data.Interface;
using Products.Data.Entities;
using System.Net;
using Products.Domain.Utils;

namespace Products.Domain.Service
{
    public class ProductService : IProductService
    {
        private IProductRepository _productRepository;
        private readonly IMapper _mapper;

        public ProductService(IProductRepository productRepository, IMapper mapper)
        {
            _productRepository = productRepository;
            _mapper = mapper;
        }

        public bool DeleteProduct(Product product)
        {
            throw new NotImplementedException();
        }

        public Product GetProductById(int prodId)
        {
            Product product= new Product();
            var productEntity = _productRepository.GetProductById(prodId);
            if (productEntity != null)
            {
                _mapper.Map(productEntity, product);
                return product;
            }
            else
                return null;


        }

        public List<Product> GetProducts()
        {
            List<Product> listProds = new List<Product>();
            var list = _productRepository.GetProducts();
            if (list.Count > 0)
                listProds = _mapper.Map(list, listProds);

            return listProds;
        }

        public List<Product> GetProductsByName(string Name)
        {
            List<Product> listProds = new List<Product>();
            var list = _productRepository.GetProductsByName(Name).Result.ToList();
            if (list.Count > 0)
                listProds = _mapper.Map(list, listProds);

            return listProds;
        }

        public ProductCreated InsertProduct(ProductCreate product)
        {
            if (!ProductExists(product.cBarCode) && Util.ValidaGTIN(product.cBarCode))
            {
                ProductEntity productEntity = new ProductEntity();
                ProductCreated productCreated = new ProductCreated();
                Product prod=new Product();
                prod.Id = 0;
                prod.cName = product.cName;
                prod.cCategory = product.cCategory;
                prod.cBarCode = product.cBarCode;
                prod.nValue = product.nValue;
                _productRepository.InsertProduct(_mapper.Map(prod, productEntity));

                if (productEntity.Id != 0)
                {
                    productCreated.Id = productEntity.Id;
                    productCreated.cBarCode=productEntity.cBarCode;
                    productCreated.Message = "Produto criado";
                    return productCreated;
                }
                else
                {
                    productCreated.Id = 0;
                    productCreated.cBarCode = product.cBarCode;
                    productCreated.Message = "Erro ao criar produto";
                    return productCreated;
                }
            }
            else
            {
                ProductCreated productCreated = new ProductCreated();
                productCreated.Id = 0;
                productCreated.cBarCode = product.cBarCode;
                productCreated.Message = "Código de barras já existe";
                return productCreated;
            }
        }

        public bool ProductExists(string cBarCode)
        {
            return _productRepository.ProductExists(cBarCode);
        }

        public Product UpdateProduct(Product product)
        {
            throw new NotImplementedException();
        }
    }
}
