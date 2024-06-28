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
                await _db.Transactions.AddAsync(entity);
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
                var transaction = await _db.Transactions.FirstOrDefaultAsync(x => x.TransactionId == id);

                if(transaction == null)
                {
                    throw new Exception($"Transaksi dengan id {id} tidak ditemukan");
                }
                return transaction;
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }

        public Task<Transaction> Update(Transaction entity)
        {
            throw new NotImplementedException();
        }

        public async Task<Transaction> UpdateTransactionType(int id)
        {
            try
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
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
