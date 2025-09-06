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
        var fakeSupplier = new Supplier { Id = 1, Name = "Amazon", Address = "US", CreatedAt = DateTime.Now, UpdatedAt = DateTime.Now };

        // Act
        var result = _supplierController.Create(fakeSupplier);

        // Assert
        result.Should().BeAssignableTo<IActionResult>();
        result.As<RedirectToActionResult>().ActionName.Should().Be("Index");
    }

    [Fact]
    public void HttpGetEdit_GoToEditPage_ShouldReturnSuccess()
    {
        // Arrange
        mockRepository.Setup(db => db.GetById(It.IsAny<int>())).Returns(new Supplier
        {
            Id = 1,
            Name = "Amazon",
            Address = "US",
            CreatedAt = DateTime.Now,
            UpdatedAt = DateTime.Now
        });

        // Act
        var result = _supplierController.Edit(1);

        // Assert
        result.Should().BeAssignableTo<IActionResult>();
        result.As<ViewResult>().Model.Should().BeOfType<Supplier>();
    }

    [Fact]
    public void HttpGetEdit_GoToEditPage_ShouldReturnNotFound()
    {
        // Arrange
        mockRepository.Setup(db => db.GetById(It.IsAny<int>())).Returns(null as Supplier);

        // Act
        var result = _supplierController.Edit(1);

        // Assert
        result.Should().BeAssignableTo<IActionResult>();
        result.Should().BeOfType<NotFoundResult>();
    }

    [Fact]
    public void HttpPostEdit_EditSingleSupplier_CanEditSupplier()
    {
        // Arrange
        var fakeSupplier = new Supplier
        {
            Id = 1,
            Name = "Amazon",
            Address = "US",
            CreatedAt = DateTime.Now,
            UpdatedAt = DateTime.Now
        };
        mockRepository.Setup(db => db.Update(It.IsAny<Supplier>()));

        // Act
        var result = _supplierController.Edit(fakeSupplier);

        // Assert
        result.Should().BeAssignableTo<IActionResult>();
        result.As<RedirectToActionResult>().ActionName.Should().Be("Index");
    }

    [Fact]
    public void HttpPostEdit_EditSingleSupplier_ShouldHaveInvalidModelState()
    {
        // Arrange
        var fakeSupplier = new Supplier
        {
            Id = 1,
            Name = "",
            Address = "US",
            CreatedAt = DateTime.Now,
            UpdatedAt = DateTime.Now
        };
        _supplierController.ModelState.AddModelError("Name", "Name is required");

        // Act
        var result = _supplierController.Edit(fakeSupplier);

        // Assert
        result.Should().BeAssignableTo<IActionResult>();
        result.As<ViewResult>().Model.Should().BeOfType<Supplier>();
        result.As<ViewResult>().ViewData.ModelState.IsValid.Should().BeFalse();
    }

    [Fact]
    public void Delete_DeleteSingleSupplier_ShouldDeleteSuccess()
    {
        // Arrange
        mockRepository.Setup(db => db.GetById(It.IsAny<int>())).Returns(new Supplier
        {
            Id = 1,
            Name = "Amazon",
            Address = "US",
            CreatedAt = DateTime.Now,
            UpdatedAt = DateTime.Now
        });
        mockRepository.Setup(db => db.Delete(It.IsAny<Supplier>()));

        // Act
        var result = _supplierController.Delete(1);

        // Assert
        result.Should().BeAssignableTo<IActionResult>();
        result.As<RedirectToActionResult>().ActionName.Should().Be("Index");
    }

    [Fact]
    public void Delete_DeleteSingleSupplier_ShouldNotFindSupplier()
    {
        // Arrange
        mockRepository.Setup(db => db.GetById(It.IsAny<int>())).Returns(null as Supplier);
        mockRepository.Setup(db => db.Delete(It.IsAny<Supplier>()));

        // Act
        var result = _supplierController.Delete(1);

        // Assert
        result.Should().BeOfType<NotFoundResult>();
    }
}
