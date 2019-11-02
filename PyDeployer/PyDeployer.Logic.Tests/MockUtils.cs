using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Moq;

namespace PyDeployer.Logic.Tests
{
    public static class MockUtils
    {
        public static Mock<DbSet<T>> CreateDbSetMock<T>(IEnumerable<T> elements) where T: class
        {
            var querableElements = elements.AsQueryable();

            var dbSetMock = new Mock<DbSet<T>>();

            dbSetMock.As<IQueryable<T>>().Setup(m => m.Provider).Returns(querableElements.Provider);
            dbSetMock.As<IQueryable<T>>().Setup(m => m.Expression).Returns(querableElements.Expression);
            dbSetMock.As<IQueryable<T>>().Setup(m => m.ElementType).Returns(querableElements.ElementType);
            dbSetMock.As<IQueryable<T>>().Setup(m => m.GetEnumerator()).Returns(querableElements.GetEnumerator());
                
            return dbSetMock;
        }
    }
}