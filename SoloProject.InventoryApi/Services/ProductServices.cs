using Microsoft.EntityFrameworkCore;
using SoloProject.InventoryApi.Models;
using SoloProject.InventoryApi.Repositories;

namespace SoloProject.InventoryApi.Services
{
    public class ProductServices : IProductRepository
    {
        private readonly ProductDbContext _db;

        public ProductServices(ProductDbContext db)
        {
            _db = db;
        }

        public async Task<Product> Add(Product entity)
        {
            try
            {
                await _db.Products.AddAsync(entity);
                await _db.SaveChangesAsync();
                return entity;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task Delete(int id)
        {
            try
            {
                var product = await GetById(id);
                _db.Remove(product);
                await _db.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<IEnumerable<Product>> GetAll()
        {
            var products = await _db.Products.ToListAsync();

            return products;
        }

        public async Task<Product> GetById(int id)
        {
            try
            {
                var product = await _db.Products.FirstOrDefaultAsync(x => x.Id == id);
                if (product == null)
                {
                    throw new Exception("Product  tidak ditemukan");
                }

                return product;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<Product> Update(Product entity)
        {
            try
            {
                var product = await GetById(entity.Id);
                product.Name = entity.Name;
                product.Price = entity.Price;
                product.Stock = entity.Stock;

                await _db.SaveChangesAsync();

                return entity;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
