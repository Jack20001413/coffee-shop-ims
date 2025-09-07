using System;
using CoffeeShopIMS.Controllers;
using CoffeeShopIMS.Models;
using CoffeeShopIMS.Repositories;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace CoffeeShopIMS.Test.Controllers;

public class IngredientTest
{
    private IngredientController _ingredientController;
    private Mock<IIngredientRepository> mockRepository;

    public IngredientTest()
    {
        // Dependencies
        mockRepository = new Mock<IIngredientRepository>();

        // SUT - System Under Test
        _ingredientController = new IngredientController(mockRepository.Object);
    }

    [Fact]
    public void Index_GoToIndexPage_ShouldReturnSuccess()
    {
        // Arrange
        var fakeIngrediens = new List<Ingredient>
        {
            new Ingredient { Id = 1, Name = "Coffee", Quantity = 100, CreatedAt = DateTime.Now, UpdatedAt = DateTime.Now }
        };
        mockRepository.Setup(db => db.GetAll()).Returns(fakeIngrediens);

        // Act
        var result = _ingredientController.Index();

        // Assert
        result.Should().BeAssignableTo<IActionResult>();
        result.As<ViewResult>().Model.Should().BeOfType<List<Ingredient>>();
        result.As<ViewResult>().Model.Should().BeEquivalentTo(fakeIngrediens);
    }
}
