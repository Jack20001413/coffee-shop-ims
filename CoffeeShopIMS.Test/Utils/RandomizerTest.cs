using System;
using CoffeeShopIMS.Utils;
using FluentAssertions;

namespace CoffeeShopIMS.Test.Utils;

public class RandomizerTest
{
    [Fact]
    public void GenerateCode_GenerateRandomString_ShouldReturnNonEmptyString()
    {
        // Arrange

        // Act
        var randomString = Randomizer.GenerateOrderCode();

        // Assert
        randomString.Should().NotBeNullOrEmpty();
        randomString.Length.Should().Be(16);
    }
}
