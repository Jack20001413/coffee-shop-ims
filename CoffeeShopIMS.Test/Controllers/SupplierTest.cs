using CoffeeShopIMS.Controllers;
using CoffeeShopIMS.Models;
using CoffeeShopIMS.Repositories;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace CoffeeShopIMS.Test.Controllers;

public class SupplierTest
{
    private SupplierController _supplierController;
    private Mock<ISupplierRepository> mockRepository;

    public SupplierTest()
    {
        // Dependencies
        mockRepository = new Mock<ISupplierRepository>();

        // SUT - System Under Test
        _supplierController = new SupplierController(mockRepository.Object);
    }

    [Fact]
    public void Index_GoToIndexPage_ShouldReturnSuccess()
    {
        // Arrange
        mockRepository.Setup(db => db.GetAll()).Returns(new List<Supplier> {
            new Supplier { Id = 1, Name = "Amazon", Address = "US", CreatedAt = DateTime.Now, UpdatedAt = DateTime.Now}
        });

        // Act
        var result = _supplierController.Index();

        // Assert
        result.Should().BeAssignableTo<IActionResult>();
    }

    [Fact]
    public void HttpPostCreate_CreateSingleSupplier_CanCreateSupplier()
    {
        // Arrange
        mockRepository.Setup(db => db.Create(It.IsAny<Supplier>()));

        // Act
        var result = _supplierController.Create(new Supplier { Id = 1, Name = "Amazon", Address = "US", CreatedAt = DateTime.Now, UpdatedAt = DateTime.Now });

        // Assert
        result.Should().BeAssignableTo<IActionResult>();
        result.As<RedirectToActionResult>().ActionName.Should().Be("Index");
    }
}
