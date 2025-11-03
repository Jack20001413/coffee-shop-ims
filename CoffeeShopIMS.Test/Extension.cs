using System;
using Microsoft.EntityFrameworkCore;
using Moq;

namespace CoffeeShopIMS.Test;

public static class Extension
{
    public static Mock<DbSet<T>> GetDbSetMock<T>(this IQueryable<T> data) where T : class
    {
        var mockDbSet = new Mock<DbSet<T>>();

        mockDbSet.As<IQueryable<T>>().Setup(m => m.Provider).Returns(data.Provider);
        mockDbSet.As<IQueryable<T>>().Setup(m => m.ElementType).Returns(data.ElementType);
        mockDbSet.As<IQueryable<T>>().Setup(m => m.Expression).Returns(data.Expression);
        mockDbSet.As<IQueryable<T>>().Setup(m => m.GetEnumerator()).Returns(data.GetEnumerator);

        return mockDbSet;
    }
}
