using System;
using CoffeeShopIMS.Controllers;
using CoffeeShopIMS.Data;
using CoffeeShopIMS.Models;
using CoffeeShopIMS.ViewModels;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Moq;

namespace CoffeeShopIMS.Test.Controllers;

public class OrderControllerTest
{
    private Mock<ApplicationDbContext> _mockContext;
    private Mock<DbSet<PurchaseOrder>> _mockDbSet;

    public OrderControllerTest()
    {
        // Dependencies
        _mockDbSet = new Mock<DbSet<PurchaseOrder>>();

        _mockContext = new Mock<ApplicationDbContext>();
    }
    
    [Fact]
    public void Index_GoToIndexPage_ShoudReturnSuccess()
    {
        // Arrange
        var orders = new List<PurchaseOrder>()
        {
            new() { Id = 1, OrderNumber = "orderNo1", CreationDate = DateOnly.MinValue, OrderPerson = "Person A", CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow},
        }.AsQueryable();

        _mockDbSet.As<IQueryable<PurchaseOrder>>().Setup(m => m.Provider).Returns(orders.Provider);
        _mockDbSet.As<IQueryable<PurchaseOrder>>().Setup(m => m.Expression).Returns(orders.Expression);
        _mockDbSet.As<IQueryable<PurchaseOrder>>().Setup(m => m.ElementType).Returns(orders.ElementType);
        _mockDbSet.As<IQueryable<PurchaseOrder>>().Setup(m => m.GetEnumerator()).Returns(orders.GetEnumerator);

        _mockContext.Setup(c => c.PurchaseOrders).Returns(_mockDbSet.Object);

        var orderController = new OrderController(_mockContext.Object);

        // Act
        var result = orderController.Index();

        // Assert
        result.Should().BeAssignableTo<IActionResult>();
        result.As<ViewResult>().Model.Should().BeAssignableTo<IEnumerable<PurchaseOrder>>();
        result.As<ViewResult>().Model.Should().BeEquivalentTo(orders.ToList());
    }

    [Fact]
    public void HttpGetCreate_GoToCreatePage_ShouldReturnSuccess()
    {
        // Arrange
        var ingredients = new List<Ingredient>()
        {
            new() { Id = 1, Name = "Coffee", Quantity = 100, Unit = "kg", Sku = "COF100", CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow}
        }.AsQueryable();

        var mockDbSetIngredient = new Mock<DbSet<Ingredient>>();
        mockDbSetIngredient.As<IQueryable<Ingredient>>().Setup(m => m.Provider).Returns(ingredients.Provider);
        mockDbSetIngredient.As<IQueryable<Ingredient>>().Setup(m => m.Expression).Returns(ingredients.Expression);
        mockDbSetIngredient.As<IQueryable<Ingredient>>().Setup(m => m.ElementType).Returns(ingredients.ElementType);
        mockDbSetIngredient.As<IQueryable<Ingredient>>().Setup(m => m.GetEnumerator()).Returns(ingredients.GetEnumerator);
        
        var warehouses = new List<Warehouse>()
        {
            new() { Id = 1, Address = "123 Warehouse St", PersonInCharge = "Employee A", CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow}
        }.AsQueryable();

        var mockDbSetWarehouse = new Mock<DbSet<Warehouse>>();
        mockDbSetWarehouse.As<IQueryable<Warehouse>>().Setup(m => m.Provider).Returns(warehouses.Provider);
        mockDbSetWarehouse.As<IQueryable<Warehouse>>().Setup(m => m.Expression).Returns(warehouses.Expression);
        mockDbSetWarehouse.As<IQueryable<Warehouse>>().Setup(m => m.ElementType).Returns(warehouses.ElementType);
        mockDbSetWarehouse.As<IQueryable<Warehouse>>().Setup(m => m.GetEnumerator()).Returns(warehouses.GetEnumerator);
        
        var vendors = new List<Supplier>()
        {
            new() { Id = 1, Name = "Supplier A", Address = "123 Supplier St", CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow}
        }.AsQueryable();

        var mockDbSetVendor = new Mock<DbSet<Supplier>>();
        mockDbSetVendor.As<IQueryable<Supplier>>().Setup(m => m.Provider).Returns(vendors.Provider);
        mockDbSetVendor.As<IQueryable<Supplier>>().Setup(m => m.Expression).Returns(vendors.Expression);
        mockDbSetVendor.As<IQueryable<Supplier>>().Setup(m => m.ElementType).Returns(vendors.ElementType);
        mockDbSetVendor.As<IQueryable<Supplier>>().Setup(m => m.GetEnumerator()).Returns(vendors.GetEnumerator);

        _mockContext.Setup(c => c.Ingredients).Returns(mockDbSetIngredient.Object);
        _mockContext.Setup(c => c.Warehouses).Returns(mockDbSetWarehouse.Object);
        _mockContext.Setup(c => c.Suppliers).Returns(mockDbSetVendor.Object);

        var orderController = new OrderController(_mockContext.Object);

        // Act
        var result = orderController.Create();

        // Assert
        result.Should().BeAssignableTo<IActionResult>();
        result.As<ViewResult>().Model.Should().BeAssignableTo<PurchaseRequestViewModel>();
        result.As<ViewResult>().Model.As<PurchaseRequestLoadViewModel>().Should().NotBeNull();
    }
}
