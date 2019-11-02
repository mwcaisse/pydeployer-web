using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Moq;
using PyDeployer.Common.Encryption;
using PyDeployer.Common.Entities;
using PyDeployer.Common.ViewModels;
using PyDeployer.Data;
using PyDeployer.Logic.Services;
using Xunit;

namespace PyDeployer.Logic.Tests.Services
{
    public class DatabaseServiceTests
    {
        private const string testEncryptionKey = "amazingKey";
        
        private DatabaseService subject;

        private Mock<AesEncrypter> mockEncrypter;
        
        private Mock<PyDeployerDbContext> mockDatabaseContext;

        private Mock<DbSet<Database>> mockDbSet;

        private List<Database> testDatabases;

        public DatabaseServiceTests()
        {
            mockDatabaseContext = new Mock<PyDeployerDbContext>(MockBehavior.Default, new object[]
            {
                new DbContextOptions<PyDeployerDbContext>()
            });
            mockEncrypter = new Mock<AesEncrypter>();

         
            mockEncrypter.Setup(x => x.GenerateKey(256)).Returns(testEncryptionKey);
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

            mockDbSet = MockUtils.CreateDbSetMock(testDatabases);
            mockDatabaseContext.Setup(x => x.Databases).Returns(mockDbSet.Object);
            
            subject = new DatabaseService(mockDatabaseContext.Object, mockEncrypter.Object);
        }
        
        public class GetTests : DatabaseServiceTests
        {
            
            [Fact]
            public void TestReturnsCorrectDatabase()
            {
                var databaseOne = subject.Get(1);
                
                Assert.NotNull(databaseOne);
                Assert.Equal(1, databaseOne.DatabaseId);
                Assert.Equal("Database 1", databaseOne.Name);
            }

            [Fact]
            public void TestOnlyReturnsActiveDatabases()
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

        public class GetAllForEnvironmentTests : DatabaseServiceTests
        {
            [Fact]
            public void TestOnlyReturnsDatabasesForEnvironment()
            {
                testDatabases.Clear();
                
                testDatabases.AddRange(new List<Database>()
                {
                    new Database()
                    {
                        Name = "Dev Database 1",
                        DatabaseId = 1,
                        EnvironmentId = 1,
                        Active = true
                    },
                    new Database()
                    {
                        Name = "Prod Database 1",
                        DatabaseId = 2,
                        EnvironmentId = 2,
                        Active = true
                    },
                    new Database()
                    {
                        Name = "Prod Database 2",
                        DatabaseId = 3,
                        EnvironmentId = 2,
                        Active = true
                    },
                    new Database()
                    {
                        Name = "Dev Database 2",
                        DatabaseId = 4,
                        EnvironmentId = 1,
                        Active = true
                    }
                    ,
                    new Database()
                    {
                        Name = "Dev Database 3",
                        DatabaseId = 5,
                        EnvironmentId = 1,
                        Active = true
                    }
                });

                var devDatabases = subject.GetAllForEnvironment(1);
                var prodDatabases = subject.GetAllForEnvironment(2);
                
                Assert.NotNull(devDatabases);
                Assert.NotNull(prodDatabases);
                
                Assert.Equal(3, devDatabases.Count());
                Assert.Equal(2, prodDatabases.Count());
                
                Assert.All(devDatabases, item => Assert.Equal(1, item.EnvironmentId));
                Assert.All(prodDatabases, item => Assert.Equal(2, item.EnvironmentId));
            }

            [Fact]
            public void TestOnlyReturnsActiveDatabases()
            {
                testDatabases.Clear();
                testDatabases.AddRange(new List<Database>()
                {
                    new Database()
                    {
                        Name = "Dev Database 1",
                        DatabaseId = 1,
                        EnvironmentId = 1,
                        Active = true
                    },
                    new Database()
                    {
                        Name = "Dev Database 2",
                        DatabaseId = 4,
                        EnvironmentId = 1,
                        Active = true
                    }
                    ,
                    new Database()
                    {
                        Name = "Dev Database 3",
                        DatabaseId = 5,
                        EnvironmentId = 1,
                        Active = false
                    }
                });

                var devDatabases = subject.GetAllForEnvironment(1);
                
                Assert.NotNull(devDatabases);
                Assert.All(devDatabases, item => Assert.True(item.Active));
                Assert.Equal(2, devDatabases.Count());
            }

            [Fact]
            public void TestNonExistanceEnvironmentReturnsNone()
            {
                testDatabases.Clear();
                testDatabases.AddRange(new List<Database>()
                {
                    new Database()
                    {
                        Name = "Dev Database 1",
                        DatabaseId = 1,
                        EnvironmentId = 1,
                        Active = true
                    },
                    new Database()
                    {
                        Name = "Dev Database 2",
                        DatabaseId = 4,
                        EnvironmentId = 3,
                        Active = true
                    }
                    ,
                    new Database()
                    {
                        Name = "Dev Database 3",
                        DatabaseId = 5,
                        EnvironmentId = 5,
                        Active = false
                    }
                });
                
                Assert.Empty(subject.GetAllForEnvironment(2));
                Assert.Empty(subject.GetAllForEnvironment(4));
                Assert.Empty(subject.GetAllForEnvironment(6));
                Assert.Empty(subject.GetAllForEnvironment(100));
            }
        }


        public class CreateTests : DatabaseServiceTests
        {

            [Fact]
            public void CreateAddsEncryptionKey()
            {
                var created = subject.Create(new DatabaseViewModel());
                
                Assert.Equal(testEncryptionKey, created.EncryptionKey);
            }

            [Fact]
            public void TestCreateAddsDatabase()
            {
                var created = subject.Create(new DatabaseViewModel());
                
                Assert.True(created.Active);
                mockDatabaseContext.Verify(m => m.SaveChanges(), Times.Once());
                mockDbSet.Verify(m => m.Add(created), Times.Once());
                
                
            }
        }
    }
}