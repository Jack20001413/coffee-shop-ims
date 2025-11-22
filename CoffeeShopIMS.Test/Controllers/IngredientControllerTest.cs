using CoffeeShopIMS.Controllers;
using CoffeeShopIMS.Data;
using CoffeeShopIMS.Models;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace CoffeeShopIMS.Test.Controllers;

public class IngredientControllerTest
{
    private IngredientController _controller;
    private Mock<ApplicationDbContext> _mockContext;

    public IngredientControllerTest()
    {
        // Dependencies
        _mockContext = new Mock<ApplicationDbContext>();

        // SUT - System Under Test
        _controller = new IngredientController(_mockContext.Object);
    }

    [Fact]
    public void Index_GoToIndexPage_ShouldReturnSuccess()
    {
        // Arrange
        var ingredients = new List<Ingredient>
        {
            new() 
            {
                Id = 1,
                Name = "Test ingredient",
                Quantity = 1,
                CreatedAt = DateTime.MinValue,
                UpdatedAt = DateTime.MinValue
            }
        };
        var mockDbSetIngredient = ingredients.AsQueryable().GetDbSetMock();

        _mockContext.Setup(m => m.Ingredients).Returns(mockDbSetIngredient.Object);

        // Act
        var result = _controller.Index();

        // Assert
        result.Should().BeAssignableTo<IActionResult>();
        result.As<ViewResult>().Model.Should().BeOfType<List<Ingredient>>();
        result.As<ViewResult>().Model.Should().BeEquivalentTo(ingredients);
    }
}
