using Microsoft.EntityFrameworkCore;
using SoloProject.InventoryApi.Models;
using SoloProject.InventoryApi.Repositories;

namespace SoloProject.InventoryApi.Services
{
    public class TransactionServices : ITransactionRepository
    {
        private readonly ProductDbContext _db;

        public TransactionServices(ProductDbContext db)
        {
            _db = db;
        }
        public async Task<Transaction> Add(Transaction entity)
        {
            try
            {
                var product = await _db.Products.FirstOrDefaultAsync(x => x.Id == entity.ProductId);

                if (product == null)
                {
                    throw new Exception("Product not found");
                }

                if (entity.TransactionType == false && product.Stock == 0)
                {
                    throw new Exception("Cannot create a sale transaction for a product with zero stock level");
                }

                await _db.Transactions.AddAsync(entity);

                await _db.SaveChangesAsync();

                return entity;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.InnerException.Message);
            }
        }

        public async Task Delete(int id)
        {
            try
            {
                var transaction = await GetById(id);
                _db.Transactions.Remove(transaction);
                await _db.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<IEnumerable<Transaction>> GetAll()
        {
            try
            {
                var transactions = await _db.Transactions.Include(x => x.Product).ToListAsync();

                return transactions;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<Transaction> GetById(int id)
        {
            try
            {
                var transaction = await _db.Transactions.Include(x => x.Product).FirstOrDefaultAsync(x => x.TransactionId == id);

                if (transaction == null)
                {
                    throw new Exception($"Transaksi dengan id {id} tidak ditemukan");
                }
                return transaction;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }

        public async Task<Transaction> Update(Transaction entity)
        {
            throw new NotImplementedException();
        }

        public async Task<Transaction> UpdateTransactionType(int id)
        {
            var transaction = await _db.Transactions.Include(x => x.Product).FirstOrDefaultAsync(x => x.TransactionId == id);
            if (transaction == null)
            {
                throw new ArgumentException($"Transaction with ID {id} not found.");
            }

            transaction.TransactionType = !transaction.TransactionType; // Toggle the transaction type

            await _db.SaveChangesAsync();

            return transaction;
        }


    }
}
