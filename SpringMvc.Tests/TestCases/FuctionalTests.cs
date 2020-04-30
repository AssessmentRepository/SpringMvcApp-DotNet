using Microsoft.Extensions.Options;
using MongoDB.Driver;
using Moq;
using SpringMvc.BusinessLayer.Repository;
using SpringMvc.Datalayer;
using SpringMvc.Entities;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace SpringMvc.Tests.TestCases
{
  public  class FuctionalTests
    {
        private Mock<IMongoCollection<User>> _mockCollection;
        private Mock<IMongoCollection<Admin>> _adminmockCollection;
        private Mock<IMongoUserDBContext> _mockContext;
        private User _user;
        private Admin _admin;
        private readonly IList<User> _list;
        
        private Mock<IOptions<Mongosettings>> _mockOptions; 
       
        public FuctionalTests()
        {
            _user = new User
            {
                UserName = "bb",
                Password = "123456789",
                ConfirmPassword = "123456789",
                Email = "aa@gmail.com",
                Photo = "Pho"
            };
            _admin = new Admin
            {
                Name="admin"
            };
            _mockCollection = new Mock<IMongoCollection<User>>();
            _mockCollection.Object.InsertOne(_user);
            _adminmockCollection = new Mock<IMongoCollection<Admin>>();
            _mockCollection.Object.InsertOne(_user);
            _mockContext = new Mock<IMongoUserDBContext>();
           
            _mockOptions = new Mock<IOptions<Mongosettings>>();
            _list = new List<User>();
            _list.Add(_user);
        }
        Mongosettings settings = new Mongosettings()
        {
            Connection = "mongodb://localhost:27017",
            DatabaseName = "guestbook"
        };



        [Fact]
        public async Task GetAllUsers()
        {
            //Arrange
            Mock<IAsyncCursor<User>> _userCursor = new Mock<IAsyncCursor<User>>();
            _userCursor.Setup(_ => _.Current).Returns(_list);
            _userCursor
                .SetupSequence(_ => _.MoveNext(It.IsAny<CancellationToken>()))
                .Returns(true)
                .Returns(false);
            
            _mockCollection.Setup(op => op.FindSync(It.IsAny<FilterDefinition<User>>(),
            It.IsAny<FindOptions<User, User>>(),
             It.IsAny<CancellationToken>())).Returns(_userCursor.Object);

            
            _mockContext.Setup(c => c.GetCollection<User>(typeof(User).Name)).Returns(_mockCollection.Object);

            _mockOptions.Setup(s => s.Value).Returns(settings);

            var context = new MongoUserDBContext(_mockOptions.Object); 

            var userRepo = new UserRepository(context);

            //Act
            var result = await userRepo.Get();

            //Assert 
            //loop only first item and assert
            foreach (User user in result)
            {
                Assert.NotNull(user);
                Assert.Equal(user.UserName, _user.UserName);
                Assert.Equal(user.Email, _user.Email);
                break;
            }
        }


        [Fact]
        public async void TestFor_CreateNewUser()
        {
            //mocking
            _mockCollection.Setup(op => op.InsertOneAsync(_user, null,
            default(CancellationToken))).Returns(Task.CompletedTask);
            _mockContext.Setup(c => c.GetCollection<User>(typeof(User).Name)).Returns(_mockCollection.Object);
     
            _mockOptions.Setup(s => s.Value).Returns(settings);
            var context = new MongoUserDBContext(_mockOptions.Object); 
            var userRepo = new UserRepository(context);

            //Act
            await userRepo.Create(_user);

            var result = await userRepo.Get(_user.Id);

            //Assert
            Assert.Equal(_user.UserName, result.UserName);
           
        }

        [Fact]
        public async Task TestFor_UpDateUser()
        {
            //Arrange

            //mocking
            //Arrange
            _mockCollection.Setup(s => s.UpdateOneAsync(It.IsAny<FilterDefinition<User>>(), It.IsAny<UpdateDefinition<User>>(), It.IsAny<UpdateOptions>(), It.IsAny<CancellationToken>()));
            //mocking
            _mockContext.Setup(c => c.GetCollection<User>(typeof(User).Name)).Returns(_mockCollection.Object);


            //Craetion of new Db
            _mockOptions.Setup(s => s.Value).Returns(settings);
            var context = new MongoUserDBContext(_mockOptions.Object);
            var userRepo = new UserRepository(context);

            //Act
            await userRepo.Create(_user);
            await userRepo.Update(_user);
            var result = await userRepo.Get(_user.Id);

            //Assert
            Assert.Equal(_user.UserName, result.UserName);

        }

        [Fact]
        public async Task TestFor_DeleteUserForUsers()
        {
            //Arrange

            //mocking
            _mockCollection.Setup(op => op.InsertOneAsync(_user, null,
            default(CancellationToken))).Returns(Task.CompletedTask);
            _mockContext.Setup(c => c.GetCollection<User>(typeof(User).Name));

            
            _mockOptions.Setup(s => s.Value).Returns(settings);
            var context = new MongoUserDBContext(_mockOptions.Object);
            var userRepo = new UserRepository(context);

            //Act
            await userRepo.Create(_user);
            await userRepo.Delete(_user.Id);
            var result = await userRepo.Get(_user.Id);

            //Assert
            Assert.Null(result);

        }
        [Fact]
        public async Task TestFor_GetUserById()
        {
            //Arrange

            //mocking
            _mockCollection.Setup(op => op.FindSync(It.IsAny<FilterDefinition<User>>(),
           It.IsAny<FindOptions<User, User>>(),
            It.IsAny<CancellationToken>()));

            _mockContext.Setup(c => c.GetCollection<User>(typeof(User).Name));
            _mockOptions.Setup(s => s.Value).Returns(settings);
            var context = new MongoUserDBContext(_mockOptions.Object);
            var userRepo = new UserRepository(context);

            //Act
            await userRepo.Create(_user);
            var result = await userRepo.Get(_user.Id);

            //Assert
            Assert.NotNull(result);

        }
        [Fact]
        public async Task TestFor_GetUserByIdForAdmin()
        {
            //Arrange
            //mocking
            _mockCollection.Setup(op => op.InsertOneAsync(_user, null,
            default(CancellationToken))).Returns(Task.CompletedTask);
            _mockContext.Setup(c => c.GetCollection<User>(typeof(User).Name));

          
            _mockOptions.Setup(s => s.Value).Returns(settings);
            var context = new MongoUserDBContext(_mockOptions.Object);
            var userRepo = new AdminRepository(context);

            //Act
            var result = await userRepo.Get(_user.Id);

            //Assert
            Assert.NotNull(result);

        }

        [Fact]
        public async Task TestFor_DeleteUserByAdmin()
        {
            //Arrange

            //mocking
            _adminmockCollection.Setup(op => op.DeleteOne(_user.Id, null, default(CancellationToken)));
            _mockContext.Setup(c => c.GetCollection<User>(typeof(User).Name));

            //Craetion of new Db
            _mockOptions.Setup(s => s.Value).Returns(settings);

            _mockOptions.Setup(s => s.Value).Returns(settings);
            var context = new MongoUserDBContext(_mockOptions.Object);
            var userRepo = new UserRepository(context);
            var adminRepo = new AdminRepository(context);

            //Act
           await userRepo.Create(_user);
            await adminRepo.Delete(_user.Id);
            var result = await adminRepo.Get(_user.Id);

            //Assert
            Assert.NotNull(result);
        }
    }
}
