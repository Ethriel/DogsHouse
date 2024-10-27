namespace DogsHouse.Services
{
    public interface IEntityService<T> where T : class
    {
        bool Create(T entity);
        Task<bool> CreateAsync(T entity);
        IQueryable<T> Read();
        Task<IQueryable<T>> ReadAsync();
        T Update(T oldEntity, T newEntity);
        Task<T> UpdateAsync(T oldEntity, T newEntity);
        bool Delete(object id);
        Task<bool> DeleteAsync(object id);
    }
}
