using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace DogsHouse.Services
{
    public class EntityExtendedService<T> : EntityService<T>, IEntityExtendedService<T> where T : class
    {
        public EntityExtendedService(DbContext context) : base(context)
        {
        }
        public T ReadByCondition(Expression<Func<T, bool>> conditionExpression)
        {
            return set.FirstOrDefault(conditionExpression);
        }

        public async Task<T> ReadByConditionAsync(Expression<Func<T, bool>> conditionExpression)
        {
            return await Task.FromResult(ReadByCondition(conditionExpression));
        }

        public T ReadById(object id)
        {
            return set.Find(id);
        }

        public async Task<T> ReadByIdAsync(object id)
        {
            return await Task.FromResult(ReadById(id));
        }

        public IQueryable<T> ReadEntitiesByCondition(Expression<Func<T, bool>> conditionExpression)
        {
            return set.Where(conditionExpression);
        }

        public async Task<IQueryable<T>> ReadEntitiesByConditionAsync(Expression<Func<T, bool>> conditionExpression)
        {
            return await Task.FromResult(ReadEntitiesByCondition(conditionExpression));
        }

        public IQueryable<T> ReadPortion(int skip, int take)
        {
            return set.Skip(skip).Take(take);
        }

        public async Task<IQueryable<T>> ReadPortionAsync(int skip, int take)
        {
            return await Task.FromResult(ReadPortion(skip, take));
        }
    }
}
