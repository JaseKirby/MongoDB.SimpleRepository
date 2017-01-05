using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace MongoDB.SimpleRepository
{
    public interface IRepository<TEntity> where TEntity : Entity
    {
        void Insert(TEntity entity);
        void Update(TEntity entity);
        void UpSert(TEntity entity);
        void Delete(TEntity entity);
        IEnumerable<TEntity> Search(Expression<Func<TEntity, bool>> predicate);
        IEnumerable<TEntity> GetAll();
        TEntity FindById(string id);
        IMongoCollection<TEntity> Collection();
    }
}
