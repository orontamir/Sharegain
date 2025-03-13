using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using System;
using MongoDB.Driver.Linq;
using MongoDB.Driver;

namespace LogoManager.DAL.Mongo
{
    public interface IMongoDb<T> where T : class, new()
    {
        IMongoQueryable<T> Query { get; }

        T GetOne(Expression<Func<T, bool>> expression);

        T FindOneAndUpdate(Expression<Func<T, bool>> expression, UpdateDefinition<T> update, FindOneAndUpdateOptions<T> option);

        void UpdateOne(Expression<Func<T, bool>> expression, UpdateDefinition<T> update);

        Task ReplaceOneAsync(Expression<Func<T, bool>> expression, T entity);
        Task<UpdateResult> ReplaceMultipleAsync(Expression<Func<T, bool>> expression, UpdateDefinition<T> updateDefinition);
        void DeleteOne(Expression<Func<T, bool>> expression);

        void InsertMany(IEnumerable<T> items);

        void InsertOne(T item);
        Task<T> GetOneAsync(Expression<Func<T, bool>> expression);
        Task<List<T>> GetAsync(Expression<Func<T, bool>> expression);
        Task DeleteOneAsync(Expression<Func<T, bool>> expression);
        Task DeleteManyAsync(Expression<Func<T, bool>> expression);
        long CollectionLength();
    }
}
