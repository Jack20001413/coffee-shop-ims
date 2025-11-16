using System;
using CoffeeShopIMS.Utils;
using FluentAssertions;

namespace CoffeeShopIMS.Test.Utils;

public class RandomizerTest
{
    [Fact]
    public void GenerateCode_GenerateRandomString_ShouldReturnNonEmptyString()
    {
        // Act
        var randomString = Randomizer.GenerateOrderCode();

        // Assert
        randomString.Should().NotBeNullOrEmpty();
    }

    [Fact]
    public void GenerateCode_GenerateRandomString_ShouldReturnStringWithFixedLength()
    {
        // Act
        var randomString = Randomizer.GenerateOrderCode();

        // Assert
        randomString.Length.Should().Be(16);
    }
}
