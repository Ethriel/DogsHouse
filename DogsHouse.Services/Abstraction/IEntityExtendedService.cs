using System.Linq.Expressions;

namespace DogsHouse.Services
{
    public interface IEntityExtendedService<T> : IEntityService<T> where T : class
    {
        T ReadById(object id);
        Task<T> ReadByIdAsync(object id);
        T ReadByCondition(Expression<Func<T, bool>> conditionExpression);
        Task<T> ReadByConditionAsync(Expression<Func<T, bool>> conditionExpression);
        IQueryable<T> ReadEntitiesByCondition(Expression<Func<T, bool>> conditionExpression);
        Task<IQueryable<T>> ReadEntitiesByConditionAsync(Expression<Func<T, bool>> conditionExpression);
        IQueryable<T> ReadPortion(int skip, int take);
        Task<IQueryable<T>> ReadPortionAsync(int skip, int take);
    }
}
