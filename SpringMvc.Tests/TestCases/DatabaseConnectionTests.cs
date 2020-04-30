using Microsoft.Extensions.Options;
using MongoDB.Driver;
using Moq;
using SpringMvc.Datalayer;
using SpringMvc.Entities;
using Xunit;

namespace SpringMvc.Tests.TestCases
{
  public  class DatabaseConnectionTests
    {
        private Mock<IOptions<Mongosettings>> _mockOptions;

        private Mock<IMongoDatabase> _mockDB;

        private Mock<IMongoClient> _mockClient;

        public DatabaseConnectionTests()
        {
            _mockOptions = new Mock<IOptions<Mongosettings>>();
            _mockDB = new Mock<IMongoDatabase>();
            _mockClient = new Mock<IMongoClient>();
        }

        [Fact]
        public void MongoBookDBContext_Constructor_Success()
        {
            var settings = new Mongosettings()
            {
                Connection = "mongodb://test123 ",
                DatabaseName = "TestDB"
            };
            _mockOptions.Setup(s => s.Value).Returns(settings);
            _mockClient.Setup(c => c
            .GetDatabase(_mockOptions.Object.Value.DatabaseName, null))
                .Returns(_mockDB.Object);

            //Act 
            var context = new MongoUserDBContext(_mockOptions.Object);

            //Assert 
            Assert.NotNull(context);
        }


        [Fact]
        public void MongoBookDBContext_GetCollection_ValidName_Success()
        {
            //Arrange
            var settings = new Mongosettings()
            {
                Connection = "mongodb://tes123 ",
                DatabaseName = "TestDB"
            };

            _mockOptions.Setup(s => s.Value).Returns(settings);

            _mockClient.Setup(c => c.GetDatabase(_mockOptions.Object.Value.DatabaseName, null)).Returns(_mockDB.Object);

            //Act 
            var context = new MongoUserDBContext(_mockOptions.Object);
            var myCollection = context.GetCollection<User>("User");

            //Assert 
            Assert.NotNull(myCollection);
        }
    }
}
