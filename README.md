# MongoDB.SimpleRepository

CRUD repository pattern implementation for .net core MongoDB driver.

### Set Universal Connection String
```csharp
    MongoConnection.ConnectionString = "mongodb://localhost/test";
```

### Specify Entity Classes
```csharp
    public class YourClass : Entity
    {
        public string TestProperty { get; set; }
    }
```
### Create Generic Repository
```csharp
    Repository<YourClass> repo = new Repository<YourClass>();
    YourClass yourClass = new YourClass();
    yourClass.TestProperty = "Value";
    repo.Insert(yourClass);
```

### Creating Your Own Repositories
```csharp
    public abstract class NamedEntity : Entity
    {
        public string Name { get; set; }
    }
```
```csharp
    public class NamedRepository<TEntity> : Repository<TEntity> where TEntity : NamedEntity
    {
        public TEntity FindByName(string name)
        {
            var filter = Builders<TEntity>.Filter.Eq("Name", name);
            return collection.Find(filter).FirstOrDefault();
        }
    }
```