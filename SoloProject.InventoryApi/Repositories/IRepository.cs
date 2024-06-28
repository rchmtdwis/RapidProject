namespace SoloProject.InventoryApi.Repositories
{
    public interface IRepository <T> where T : class
    {
        Task<T> Add(T entity);
        Task<IEnumerable<T>> GetAll();
        Task<T> Update(T entity);
        Task Delete(int id);
        Task<T> GetById(int id);

    }
}
