using CoffeeShopIMS.Controllers;
using CoffeeShopIMS.Data;
using CoffeeShopIMS.Models;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Moq;

namespace CoffeeShopIMS.Test.Controllers;

public class SupplierControllerTest
{
    private SupplierController _controller;
    private Mock<ApplicationDbContext> _mockContext;

    public SupplierControllerTest()
    {
        // Dependencies
        _mockContext = new Mock<ApplicationDbContext>();

        // SUT - System Under Test
        _controller = new SupplierController(_mockContext.Object);
    }

    [Fact]
    public void Index_GoToIndexPage_ShouldReturnSuccess()
    {
        // Arrange
        var suppliers = new List<Supplier>
        {
            new()
            {
                Id = 1,
                Name = "Test supplier",
                Address = "Test address",
                CreatedAt = DateTime.MinValue,
                UpdatedAt = DateTime.MinValue
            }
        };
        var mockDbSetSupplier = suppliers.AsQueryable().GetDbSetMock();

        _mockContext.Setup(m => m.Suppliers).Returns(mockDbSetSupplier.Object);

        // Act
        var result = _controller.Index();

        // Assert
        result.Should().BeAssignableTo<IActionResult>();
        result.As<ViewResult>().Model.Should().BeOfType<List<Supplier>>();
        result.As<ViewResult>().Model.Should().BeEquivalentTo(suppliers);
    }

    [Fact]
    public void HttpPostCreate_CreateSingleSupplier_CanCreateSupplier()
    {
        // Arrange
        var testSupplier = new Supplier
        {
            Id = 1,
            Name = "Test supplier",
            Address = "Test address",
            CreatedAt = DateTime.MinValue,
            UpdatedAt = DateTime.MinValue
        };

        var mockDbSetSupplier = new Mock<DbSet<Supplier>>();
        _mockContext.Setup(m => m.Suppliers).Returns(mockDbSetSupplier.Object);

        // Act
        var result = _controller.Create(testSupplier);

        // Assert
        result.Should().BeAssignableTo<IActionResult>();
        result.As<RedirectToActionResult>().ActionName.Should().Be("Index");
        mockDbSetSupplier.Verify(m => m.Add(It.IsAny<Supplier>()), Times.Once());
        _mockContext.Verify(m => m.SaveChanges(), Times.Once());
    }

    [Fact]
    public void HttpGetEdit_GoToEditPage_ShouldReturnSuccess()
    {
        // Arrange
        var testSupplier = new Supplier
        {
            Id = 1,
            Name = "Test supplier",
            Address = "Test address",
            CreatedAt = DateTime.MinValue,
            UpdatedAt = DateTime.MinValue

        };
        var mockDbSetSupplier = new List<Supplier>
        {
            testSupplier
        }.AsQueryable().GetDbSetMock();

        _mockContext.Setup(m => m.Suppliers).Returns(mockDbSetSupplier.Object);

        // Act
        var result = _controller.Edit(1);

        // Assert
        result.Should().BeAssignableTo<IActionResult>();
        result.As<ViewResult>().Model.Should().BeOfType<Supplier>();
        result.As<ViewResult>().Model.Should().BeEquivalentTo(testSupplier);
    }

    [Fact]
    public void HttpGetEdit_GoToEditPage_ShouldReturnNotFound()
    {
        // Arrange
        var testSupplierId = 0;
        var mockDbSetSupplier = new List<Supplier>
        {
            new() {
                Id = 1,
                Name = "Test supplier",
                Address = "Test address",
                CreatedAt = DateTime.MinValue,
                UpdatedAt = DateTime.MinValue
            }
        }.AsQueryable().GetDbSetMock();

        _mockContext.Setup(m => m.Suppliers).Returns(mockDbSetSupplier.Object);

        // Act
        var result = _controller.Edit(testSupplierId);

        // Assert
        result.Should().BeAssignableTo<IActionResult>();
        result.Should().BeOfType<NotFoundObjectResult>();
        result.As<NotFoundObjectResult>().StatusCode.Should().Be(404);
        result.As<NotFoundObjectResult>().Value.Should().Be($"Supplier with ID {testSupplierId} not found");
    }

    [Fact]
    public void HttpPostEdit_EditSingleSupplier_CanEditSupplier()
    {
        // Arrange
        var testSupplier = new Supplier
        {
            Id = 1,
            Name = "Test supplier",
            Address = "Test address",
            CreatedAt = DateTime.MinValue,
            UpdatedAt = DateTime.MinValue
        };
        var mockDbSetSupplier = new Mock<DbSet<Supplier>>();
        _mockContext.Setup(m => m.Suppliers).Returns(mockDbSetSupplier.Object);

        // Act
        var result = _controller.Edit(testSupplier);

        // Assert
        result.Should().BeAssignableTo<IActionResult>();
        result.As<RedirectToActionResult>().ActionName.Should().Be("Index");

        mockDbSetSupplier.Verify(m => m.Update(It.IsAny<Supplier>()), Times.Once());
        _mockContext.Verify(m => m.SaveChanges(), Times.Once());
    }

    [Fact]
    public void HttpPostEdit_EditSingleSupplier_ShouldHaveInvalidModelState()
    {
        // Arrange
        var testSupplier = new Supplier
        {
            Id = 1,
            Name = "",
            Address = "US",
            CreatedAt = DateTime.Now,
            UpdatedAt = DateTime.Now
        };
        _controller.ModelState.AddModelError("Name", "Name is required");

        // Act
        var result = _controller.Edit(testSupplier);

        // Assert
        result.Should().BeAssignableTo<IActionResult>();
        result.As<ViewResult>().Model.Should().BeOfType<Supplier>();
        result.As<ViewResult>().Model.Should().BeEquivalentTo(testSupplier);

        var modelState = _controller.ModelState;
        modelState.IsValid.Should().BeFalse();
    }

    [Fact]
    public void Delete_DeleteSingleSupplier_ShouldDeleteSuccess()
    {
        // Arrange
        var mockDbSetSupplier = new List<Supplier>
        {
            new()
            {
                Id = 1,
                Name = "Test supplier",
                Address = "Test address",
                CreatedAt = DateTime.MinValue,
                UpdatedAt = DateTime.MinValue
            }
        }.AsQueryable().GetDbSetMock();

        _mockContext.Setup(m => m.Suppliers).Returns(mockDbSetSupplier.Object);

        // Act
        var result = _controller.Delete(1);

        // Assert
        result.Should().BeAssignableTo<IActionResult>();
        result.As<RedirectToActionResult>().ActionName.Should().Be("Index");

        mockDbSetSupplier.Verify(m => m.Remove(It.IsAny<Supplier>()), Times.Once);
        _mockContext.Verify(m => m.SaveChanges(), Times.Once);
    }

    [Fact]
    public void Delete_DeleteSingleSupplier_ShouldNotFindSupplier()
    {
        // Arrange
        var testSupplierId = 1;
        var mockDbSetSupplier = new List<Supplier>().AsQueryable().GetDbSetMock();

        _mockContext.Setup(m => m.Suppliers).Returns(mockDbSetSupplier.Object);

        // Act
        var result = _controller.Delete(testSupplierId);

        // Assert
        result.Should().BeOfType<NotFoundObjectResult>();
        result.As<NotFoundObjectResult>().StatusCode.Should().Be(404);
        result.As<NotFoundObjectResult>().Value.Should().Be($"Supplier with ID {testSupplierId} not found");

    }
}
