using System;
using System.Text;
using CoffeeShopIMS.Constants;

namespace CoffeeShopIMS.Utils;

public class Randomizer
{
    public static string GenerateOrderCode()
    {
        var random = new Random();
        var codeLength = 16;
        var codePattern = Pattern.ALPHA_NUMERICAL_CHARACTERS;
        var stringBuilder = new StringBuilder();

        for (var i = 0; i < codeLength; i++)
        {
            int randomValue = random.Next(codePattern.Length);
            stringBuilder.Append(codePattern[randomValue]);
        }
        
        return stringBuilder.ToString();
    }
}
