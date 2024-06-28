using SoloProject.InventoryApi.Models;

namespace SoloProject.InventoryApi.Repositories
{
    public interface ITransactionRepository : IRepository<Transaction>
    {
        Task<Transaction> UpdateTransactionType(int id);
    }
}
