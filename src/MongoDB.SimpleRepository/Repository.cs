using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using MongoDB.Bson;

namespace MongoDB.SimpleRepository
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : Entity
    {
        protected IMongoDatabase db;
        protected IMongoCollection<TEntity> collection;

        public Repository()
        {
            MongoUrl mongoUrl = new MongoUrl(MongoConnection.ConnectionString);
            var client = new MongoClient(mongoUrl);
            db = client.GetDatabase(mongoUrl.DatabaseName);
            SetCollection();
        }

        public Repository(string connectionString)
        {
            MongoUrl mongoUrl = new MongoUrl(connectionString);
            var client = new MongoClient(mongoUrl);
            db = client.GetDatabase(mongoUrl.DatabaseName);
            SetCollection();
        }

        private void SetCollection()
        {
            collection = db.GetCollection<TEntity>(typeof(TEntity).Name);
        }

        public void SetCollectionByName(string collectionName)
        {
            collection = db.GetCollection<TEntity>(collectionName);
        }

        public IMongoCollection<TEntity> Collection()
        {
            return collection;
        }

        public void Insert(TEntity entity)
        {
            if (entity.Id == null)
                entity.Id = ObjectId.GenerateNewId().ToString();
            collection.InsertOne(entity);
        }

        public void Update(TEntity entity)
        {
            var filter = Builders<TEntity>.Filter.Eq("_id", ObjectId.Parse(entity.Id));
            collection.ReplaceOne(filter, entity);
        }

        public void UpSert(TEntity entity)
        {
            if (entity.Id == null)
                Insert(entity);
            else
                Update(entity);
        }

        public void Delete(TEntity entity)
        {
            var filter = Builders<TEntity>.Filter.Eq("_id", ObjectId.Parse(entity.Id));
            collection.DeleteOne(filter);
        }

        public void Delete(string id)
        {
            var filter = Builders<TEntity>.Filter.Eq("_id", ObjectId.Parse(id));
            collection.DeleteOne(filter);
        }

        public IEnumerable<TEntity> Search(Expression<Func<TEntity, bool>> predicate)
        {
            return collection.AsQueryable<TEntity>().Where(predicate.Compile());
        }

        public IEnumerable<TEntity> GetAll()
        {
            return collection.AsQueryable();
        }

        public TEntity FindById(string id)
        {
            var filter = Builders<TEntity>.Filter.Eq("_id", ObjectId.Parse(id));
            return collection.Find(filter).FirstOrDefault();
        }

    }
}
