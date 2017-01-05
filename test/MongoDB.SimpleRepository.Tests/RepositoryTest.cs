using MongoDB.Bson;
using System.Linq;
using Xunit;

namespace MongoDB.SimpleRepository.Tests
{
    public class RepositoryTest
    {
        private Repository<TestEntity> repo;
        private string connectionString = "mongodb://localhost/test";

        public RepositoryTest()
        {
            MongoConnection.ConnectionString = connectionString;
            repo = new Repository<TestEntity>();
        }

        [Fact]
        public void InsertTest()
        {
            var te = new TestEntity();
            repo.Insert(te);
            Assert.True(true);
            repo.Delete(te);
        }

        [Fact]
        public void DeleteTest()
        {
            var id = ObjectId.GenerateNewId().ToString();
            var te = new TestEntity();
            te.Id = id;
            repo.Insert(te);

            repo.Delete(te);
            var deletedTE = repo.FindById(id);
            Assert.Null(deletedTE);
        }

        [Fact]
        public void DeleteById()
        {
            var id = ObjectId.GenerateNewId().ToString();
            var te = new TestEntity();
            te.Id = id;
            repo.Insert(te);

            repo.Delete(id);
            var deletedTE = repo.FindById(id);
            Assert.Null(deletedTE);
        }

        [Fact]
        public void FindByIdTest()
        {
            var id = ObjectId.GenerateNewId().ToString();
            var newTE = new TestEntity();
            newTE.Id = id;
            repo.Insert(newTE);

            var foundTE = repo.FindById(id);
            Assert.NotNull(foundTE);
            repo.Delete(newTE);
        }

        [Fact]
        public void UpdateTest()
        {
            var id = ObjectId.GenerateNewId().ToString();
            var te = new TestEntity();
            te.Id = id;

            te.TestProperty = "VALUE";
            repo.Insert(te);
            string updateProp = "UPDATE VALUE";
            te.TestProperty = updateProp;
            repo.Update(te);
            var updatedTE = repo.FindById(id);
            Assert.Equal(updateProp, updatedTE.TestProperty);
            repo.Delete(te);
        }

        [Fact]
        public void UpsertTest()
        {
            var te = new TestEntity();
            te.TestProperty = "VALUE";

            repo.UpSert(te);
            var upsertedTE = repo.FindById(te.Id);
            Assert.NotNull(upsertedTE);

            string upsertProp = "UPSERT VALUE";
            upsertedTE.TestProperty = upsertProp;
            repo.UpSert(upsertedTE);
            upsertedTE = repo.FindById(te.Id);
            Assert.Equal(upsertProp, upsertedTE.TestProperty);
            repo.Delete(te);
        }

        [Fact]
        public void SearchTest()
        {
            var id = ObjectId.GenerateNewId().ToString();
            var te = new TestEntity();
            te.Id = id;
            te.TestProperty = "VALUE";
            repo.Insert(te);

            var searchResults = repo.Search(x => x.TestProperty.Contains("VAL")).ToList();
            Assert.Equal(id, searchResults[0].Id);
            repo.Delete(te);
        }

        [Fact]
        public void FindByNameTest()
        {
            NamedRepository<NamedTestEntity> repo = new NamedRepository<NamedTestEntity>();
            string name = "Bob";
            var nte = new NamedTestEntity();
            nte.Name = name;
            repo.Insert(nte);

            var foundNTE = repo.FindByName(name);
            Assert.NotNull(foundNTE);
            repo.Delete(nte);
        }

    }
}
