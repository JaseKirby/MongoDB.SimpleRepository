using Xunit;

namespace MongoDB.SimpleRepository.Tests
{
    public class ConnectionTest
    {
        string connString = "mongodb://localhost/test";

        [Fact]
        public void RepositoryFirstConstTest() 
        {
            MongoConnection.ConnectionString = connString;
            Repository<TestEntity> repo = new Repository<TestEntity>();
            Assert.True(true);
        }

        [Fact]
        public void RepositorySecondConstTest()
        {
            Repository<TestEntity> repo = new Repository<TestEntity>(connString);
            Assert.True(true);
        }
    }
}
