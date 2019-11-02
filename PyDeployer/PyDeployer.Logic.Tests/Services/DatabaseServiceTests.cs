using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Moq;
using PyDeployer.Common.Encryption;
using PyDeployer.Common.Entities;
using PyDeployer.Data;
using PyDeployer.Logic.Services;
using Xunit;

namespace PyDeployer.Logic.Tests.Services
{
    public class DatabaseServiceTests
    {
        private DatabaseService subject;

        private Mock<AesEncrypter> mockEncrypter;
        
        private Mock<PyDeployerDbContext> mockDatabaseContext;

        private List<Database> testDatabases;

        public DatabaseServiceTests()
        {
            mockDatabaseContext = new Mock<PyDeployerDbContext>(MockBehavior.Default, new object[]
            {
                new DbContextOptions<PyDeployerDbContext>()
            });
            mockEncrypter = new Mock<AesEncrypter>();

         
            mockEncrypter.Setup(x => x.GenerateKey(256)).Returns("amazingKey");
            mockDatabaseContext.Setup(x => x.SaveChanges()).Verifiable(); // Make SaveChanges do nothing

            testDatabases = new List<Database>()
            {
                new Database()
                {
                    DatabaseId = 1,
                    Name = "Database 1",
                    Active = true
                },
                new Database()
                {
                    DatabaseId = 2,
                    Name = "Database 2",
                    Active = true
                }
            };

            mockDatabaseContext.Setup(x => x.Databases).Returns(MockUtils.CreateDbSetMock(testDatabases).Object);
            
            subject = new DatabaseService(mockDatabaseContext.Object, mockEncrypter.Object);
        }
        
        public class GetTests : DatabaseServiceTests
        {
            
            [Fact]
            public void TestGetReturnsCorrectDatabase()
            {
                var databaseOne = subject.Get(1);
                
                Assert.NotNull(databaseOne);
                Assert.Equal(1, databaseOne.DatabaseId);
                Assert.Equal("Database 1", databaseOne.Name);
            }

            [Fact]
            public void TestGetOnlyReturnsActiveDatabases()
            {
                testDatabases.Add(new Database()
                {
                    DatabaseId = 3,
                    Active = false
                });

                var inactiveDatabase = subject.Get(3);
                
                Assert.Null(inactiveDatabase);
            }

            [Fact]
            public void TestNonExistantIdReturnsNull()
            {
                testDatabases.Clear();

                Assert.Null(subject.Get(1));
            }
            
            
            
        }

        public class GetAllForEnvironmentTests
        {
            
        }
    }
}