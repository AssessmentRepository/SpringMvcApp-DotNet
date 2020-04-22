using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Driver;
using Moq;
using SpringMvc.BusinessLayer.Repository;
using SpringMvc.Datalayer;
using SpringMvc.Entities;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace SpringMvc.Tests.TestCases
{
  public  class FuctionalTests
    {
        private Mock<IMongoCollection<User>> _mockCollection;
        private Mock<IMongoUserDBContext> _mockContext;
        private User _user;
        private readonly IList<User> _list;
        // MongoSettings declaration
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
            _mockCollection = new Mock<IMongoCollection<User>>();
            _mockCollection.Object.InsertOne(_user);
            _mockContext = new Mock<IMongoUserDBContext>();
            //MongoSettings initialization
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
            //Mock MoveNextAsync
            Mock<IAsyncCursor<User>> _userCursor = new Mock<IAsyncCursor<User>>();
            _userCursor.Setup(_ => _.Current).Returns(_list);
            _userCursor
                .SetupSequence(_ => _.MoveNext(It.IsAny<CancellationToken>()))
                .Returns(true)
                .Returns(false);

            //Mock FindSync
            _mockCollection.Setup(op => op.FindSync(It.IsAny<FilterDefinition<User>>(),
            It.IsAny<FindOptions<User, User>>(),
             It.IsAny<CancellationToken>())).Returns(_userCursor.Object);

            //Mock GetCollection
            _mockContext.Setup(c => c.GetCollection<User>(typeof(User).Name)).Returns(_mockCollection.Object);

            //Jayanth Creating one more instance of DB
         
            _mockOptions.Setup(s => s.Value).Returns(settings);

            // Creating one more instance of DB
            // Passing _mockOptions instaed of _mockContext
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
     
            //Craetion of new Db
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
            _mockCollection.Setup(op => op.InsertOneAsync(_user, null,
            default(CancellationToken))).Returns(Task.CompletedTask);
            _mockContext.Setup(c => c.GetCollection<User>(typeof(User).Name)).Returns(_mockCollection.Object);

            //Craetion of new Db
            _mockOptions.Setup(s => s.Value).Returns(settings);
            var context = new MongoUserDBContext(_mockOptions.Object);
            var userRepo = new UserRepository(context);

            //Act
            await userRepo.Create(_user);
            userRepo.Update(_user);
            var result = await userRepo.Get(_user.Id);

            //Assert
            Assert.Equal(_user.UserName, result.UserName);

        }

        [Fact]
        public async Task TestFor_DeleteUser()
        {
            //Arrange

            //mocking
            _mockCollection.Setup(op => op.InsertOneAsync(_user, null,
            default(CancellationToken))).Returns(Task.CompletedTask);
            _mockContext.Setup(c => c.GetCollection<User>(typeof(User).Name));

            //Craetion of new Db
            _mockOptions.Setup(s => s.Value).Returns(settings);
            var context = new MongoUserDBContext(_mockOptions.Object);
            var userRepo = new UserRepository(context);

            //Act
            await userRepo.Create(_user);
            userRepo.Delete(_user.Id);
            var result = await userRepo.Get(_user.Id);

            //Assert
            Assert.Null(result);

        }
        [Fact]
        public async Task TestFor_GetUserById()
        {
            //Arrange

            //mocking
            _mockCollection.Setup(op => op.InsertOneAsync(_user, null,
            default(CancellationToken))).Returns(Task.CompletedTask);
            _mockContext.Setup(c => c.GetCollection<User>(typeof(User).Name));

            //Craetion of new Db
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

            //Craetion of new Db
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
        public async Task TestFor_DeleteUserByAdmin()
        {
            //Arrange

            //mocking
            _mockCollection.Setup(op => op.InsertOneAsync(_user, null,
            default(CancellationToken))).Returns(Task.CompletedTask);
            _mockContext.Setup(c => c.GetCollection<User>(typeof(User).Name));

            //Craetion of new Db
            _mockOptions.Setup(s => s.Value).Returns(settings);
            var context = new MongoUserDBContext(_mockOptions.Object);
            var userRepo = new UserRepository(context);
            var adminRepo = new AdminRepository(context);

            //Act
           await userRepo.Create(_user);
            adminRepo.Delete(_user.Id);
            var result = await adminRepo.Get(_user.Id);

            //Assert
            Assert.NotNull(result);

        }


    }
}
