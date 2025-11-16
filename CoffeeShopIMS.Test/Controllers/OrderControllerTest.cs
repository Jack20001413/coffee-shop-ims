using CoffeeShopIMS.Controllers;
using CoffeeShopIMS.Data;
using CoffeeShopIMS.Models;
using CoffeeShopIMS.ViewModels;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Moq;

namespace CoffeeShopIMS.Test.Controllers;

public class OrderControllerTest
{
    private Mock<ApplicationDbContext> _mockContext;

    public OrderControllerTest()
    {
        // Dependencies
        _mockContext = new Mock<ApplicationDbContext>();
    }

    [Fact]
    public void Index_GoToIndexPage_ShoudReturnSuccess()
    {
        // Arrange
        var orders = new List<PurchaseOrder>()
        {
            new()
            {
                Id = 1,
                OrderNumber = "orderNo1",
                CreationDate = DateOnly.MinValue,
                OrderPerson = "Test employee",
                CreatedAt = DateTime.MinValue,
                UpdatedAt = DateTime.MinValue
            },
        }.AsQueryable();
        var mockDbSetOrder = orders.GetDbSetMock();

        _mockContext.Setup(c => c.PurchaseOrders).Returns(mockDbSetOrder.Object);

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
        var mockIngredient = new List<Ingredient>()
        {
            new()
            {
                Id = 1,
                Name = "Test ingredient",
                Quantity = 100,
                Unit = "kg",
                Sku = "TEST",
                CreatedAt = DateTime.MinValue,
                UpdatedAt = DateTime.MinValue
}
        }.AsQueryable();
        var mockDbSetIngredient = mockIngredient.GetDbSetMock();

        var mockWarehouse = new List<Warehouse>()
        {
            new()
            {
                Id = 1,
                Address = "Test address",
                PersonInCharge = "Test employee",
                CreatedAt = DateTime.MinValue,
                UpdatedAt = DateTime.MinValue
            }
        }.AsQueryable();
        var mockDbSetWarehouse = mockWarehouse.GetDbSetMock();

        var mockSupplier = new List<Supplier>()
        {
            new()
            {
                Id = 1,
                Name = "Test supplier",
                Address = "Test address",
                CreatedAt = DateTime.MinValue,
                UpdatedAt = DateTime.MinValue
            }
        }.AsQueryable();
        var mockDbSetVendor = mockSupplier.GetDbSetMock();

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

    [Fact]
    public void HttpPostCreate_CreateNewOrder_ShouldCreateOrderSuccessful()
    {
        // Arrange
        var testOrderDetails = new List<PurchaseOrderDetail>
        {
            new()
            {
                IngredientId = 1,
                Quantity = 1,
                OrderId = 1,
                CreatedAt = DateTime.MinValue,
                UpdatedAt = DateTime.MinValue
            }
        };

        var testReceiveData = new PurchaseRequestViewModel
        {
            ReceiveViewModel = new PurchaseRequestReceiveViewModel
            {
                OrderPerson = "Test employee",
                SupplierId = 1,
                OrderedIngredients = testOrderDetails,
                CreationDate = DateOnly.MinValue,
                WarehouseId = 1,
            }
        };

        var mockSupplier = new List<Supplier> {
            new()
            {
                Id = 1,
                Name = "Test supplier",
                Address = "Test address",
                CreatedAt = DateTime.MinValue,
                UpdatedAt = DateTime.MinValue
            }
        }.AsQueryable();
        var mockDbSetSupplier = mockSupplier.GetDbSetMock();

        var mockIngredient = new List<Ingredient>
        {
            new()
            {
                Id = 1,
                Name = "Test ingredient",
                Quantity = 1,
                Unit = "kg",
                Sku = "TEST",
                CreatedAt = DateTime.MinValue,
                UpdatedAt = DateTime.MinValue
            }
        }.AsQueryable();
        var mockDbSetIngredient = mockIngredient.GetDbSetMock();

        mockDbSetIngredient.Setup(m => m.Find(It.IsAny<int>())).Returns(mockIngredient.First());

        var mockDbSetOrder = new Mock<DbSet<PurchaseOrder>>();

        _mockContext.Setup(c => c.Suppliers).Returns(mockDbSetSupplier.Object);
        _mockContext.Setup(c => c.Ingredients).Returns(mockDbSetIngredient.Object);
        _mockContext.Setup(c => c.PurchaseOrders).Returns(mockDbSetOrder.Object);

        var controller = new OrderController(_mockContext.Object);

        // Act
        var result = controller.Create(testReceiveData);

        // Assert
        result.As<RedirectToActionResult>().ActionName.Should().Be("Index");
        mockDbSetOrder.Verify(m => m.Add(It.IsAny<PurchaseOrder>()), Times.Once);
        mockDbSetIngredient.Verify(m => m.Find(It.IsAny<int>()), Times.AtLeastOnce);
        mockDbSetIngredient.Verify(m => m.Update(It.IsAny<Ingredient>()), Times.AtLeastOnce);
        _mockContext.Verify(m => m.SaveChanges(), Times.Once);
    }

    [Fact]
    public void HttpPostCreate_CreateNewOrder_ShouldNotFindSupplier()
    {
        // Arrange
        var testOrderDetails = new List<PurchaseOrderDetail>
        {
            new()
            {
                IngredientId = 1,
                Quantity = 1,
                OrderId = 1,
                CreatedAt = DateTime.MinValue,
                UpdatedAt = DateTime.MinValue
            }
        };

        var testReceiveData = new PurchaseRequestViewModel
        {
            ReceiveViewModel = new PurchaseRequestReceiveViewModel
            {
                OrderPerson = "Test employee",
                SupplierId = 1,
                OrderedIngredients = testOrderDetails,
                CreationDate = DateOnly.MinValue,
                WarehouseId = 1,
            }
        };

        var mockDbSetSupplier = new List<Supplier>().AsQueryable().GetDbSetMock();

        _mockContext.Setup(m => m.Suppliers).Returns(mockDbSetSupplier.Object);

        var controller = new OrderController(_mockContext.Object);

        // Act
        var result = controller.Create(testReceiveData);

        // Assert
        result.Should().BeOfType<NotFoundObjectResult>();
        result.As<NotFoundObjectResult>().StatusCode.Should().Be(404);
        result.As<NotFoundObjectResult>().Value.Should().Be("Supplier not found.");
    }

    [Fact]
    public void HttpPostCreate_CreateNewOrder_ShouldNotReceiveNullDataFromClient()
    {
        // Arrange
        PurchaseRequestViewModel testReceiveData = null!;

        var controller = new OrderController(_mockContext.Object);
    
        // Act
        var result = controller.Create(testReceiveData);
    
        // Assert
        result.As<BadRequestObjectResult>().StatusCode.Should().Be(400);
        result.As<BadRequestObjectResult>().Value.Should().Be("No data received from client.");
    }

    [Fact]
    public void HttpPostCreate_CreateNewOrder_ShouldNotHaveEmptyOrderedIngredients()
    {
        // Arrange
        var testReceiveData = new PurchaseRequestViewModel
        {
          ReceiveViewModel = new PurchaseRequestReceiveViewModel
          {
              OrderedIngredients = []
          }  
        };

        var mockDbSetIngredient = new List<Ingredient>().AsQueryable().GetDbSetMock();
        var mockDbSetSupplier = new List<Supplier>().AsQueryable().GetDbSetMock();
        var mockDbSetWarehouse = new List<Warehouse>().AsQueryable().GetDbSetMock();

        _mockContext.Setup(m => m.Ingredients).Returns(mockDbSetIngredient.Object);
        _mockContext.Setup(m => m.Suppliers).Returns(mockDbSetSupplier.Object);
        _mockContext.Setup(m => m.Warehouses).Returns(mockDbSetWarehouse.Object);

        var controller = new OrderController(_mockContext.Object);
    
        // Act
        var result = controller.Create(testReceiveData);
    
        // Assert
        var modelState = controller.ModelState;
        modelState.IsValid.Should().Be(false);
        modelState["ReceiveViewModel.OrderedIngredients"]!.Errors[0].ErrorMessage.Should().Be("At least one ingredient must be ordered");
    }
}
