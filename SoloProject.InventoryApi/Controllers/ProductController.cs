using Microsoft.AspNetCore.Mvc;
using SoloProject.InventoryApi.DTOs;
using SoloProject.InventoryApi.Models;
using SoloProject.InventoryApi.Repositories;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SoloProject.InventoryApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductRepository _productServices;

        public ProductController(IProductRepository productServices)
        {
            _productServices = productServices;
        }

        // GET: api/<ProductController>
        [HttpGet]
        public async Task<ActionResult<ProductDTO>> Get()
        {
            List<ProductDTO> productDTOs = new List<ProductDTO>();

            var products = await _productServices.GetAll();
            foreach (var product in products)
            {
                ProductDTO productDTO = new ProductDTO
                {
                    Id = product.Id,
                    Code = product.Code,
                    Name = product.Name,
                    Stock = product.Stock,
                    Price = product.Price,
                };
                productDTOs.Add(productDTO);

            }
            return Ok(productDTOs);
        }

        // GET api/<ProductController>/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ProductDTO>> Get(int id)
        {
            var product = await _productServices.GetById(id);

            ProductDTO productDto = new ProductDTO
            {
                Id = product.Id,
                Code = product.Code,
                Name = product.Name,
                Stock = product.Stock,
                Price = product.Price,
            };

            return Ok(productDto);
        }

        // POST api/<ProductController>
        [HttpPost]
        public async Task<ActionResult<ProductDTO>> Post(CreateProductDTO createProductDTO)
        {
            try
            {
                Product newProduct = new Product
                {
                    Code = createProductDTO.Code,
                    Name = createProductDTO.Name,
                    Stock = createProductDTO.Stock,
                    Price = createProductDTO.Price,
                };

                var product = await _productServices.Add(newProduct);

                ProductDTO productDTO = new ProductDTO
                {
                    Id = product.Id,
                    Code = product.Code,
                    Name = product.Name,
                    Stock = product.Stock,
                    Price = product.Price
                };

                return CreatedAtAction(nameof(Get),
                    new { id = newProduct.Id }, productDTO);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        // PUT api/<ProductController>/5
        [HttpPut("{id}")]
        public async Task<ActionResult> Put(int id, ProductDTO productDTO)
        {
            try
            {
                var updatedData = await _productServices.GetById(id);
                if (updatedData != null)
                {
                    updatedData.Code = productDTO.Code;
                    updatedData.Name = productDTO.Name;
                    updatedData.Stock = productDTO.Stock;
                    updatedData.Price = productDTO.Price;

                    var result = await _productServices.Update(updatedData);
                    return Ok(result);
                }
                return BadRequest($"Product dengan Id = {id} tidak ditemukan");
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        // DELETE api/<ProductController>/5
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            try
            {
                var getProduct = await _productServices.GetById(id);

                if (getProduct != null)
                {
                    var deletedProduct = new ProductDTO
                    {
                        Id = getProduct.Id,
                        Code = getProduct.Code,
                        Name = getProduct.Name,
                        Stock = getProduct.Stock,
                        Price = getProduct.Price
                    };

                    return Ok(new
                    {
                        Message = $"Produk dengan Id = {id} berhasil didelete",
                        DeletedProduct = deletedProduct
                    });
                }

                return BadRequest(new
                {
                    Message = $"Produk dengan Id = {id} tidak ditemukan"
                });
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
