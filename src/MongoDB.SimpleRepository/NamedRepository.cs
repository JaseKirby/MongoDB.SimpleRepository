using MongoDB.Driver;

namespace MongoDB.SimpleRepository
{
    public class NamedRepository<TEntity> : Repository<TEntity> where TEntity : NamedEntity
    {
        public TEntity FindByName(string name)
        {
            var filter = Builders<TEntity>.Filter.Eq("Name", name);
            return collection.Find(filter).FirstOrDefault();
        }

    }
}
