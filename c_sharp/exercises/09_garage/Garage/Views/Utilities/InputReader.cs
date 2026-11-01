using System;
using Garage.Models.Vehicles;


namespace Garage.Views;

public static class InputReader
{


    public static void WaitForUserKeyPress(string message)
    {
        System.Console.WriteLine($"\n{message}.");
        Console.ReadKey();
        System.Console.WriteLine();
    }


    public static bool ReadPositiveInt(out int value)
    {
        return int.TryParse(Console.ReadLine(), out value) && value >= 0;

    }

    public static bool ReadPositiveDouble(out double value)
    {
        return double.TryParse(Console.ReadLine(), out value) && value >= 0;

    }

    public static void WaitForUserKeyPress()
    {
        Console.ReadKey();
    }

    public static bool ReadString(out string text)
    {
        text = Console.ReadLine() ?? "";
        return text != "";
    }

    public static bool ReadFuelTypeEnum(out FuelType fuel)
    {

        string? input = Console.ReadLine();
        if (Enum.TryParse(input, ignoreCase: true, out FuelType result) &&
            Enum.IsDefined(result))
        {
            fuel = result;
            return true;
        }
        fuel = default;
        return false;

    }
    public delegate bool TryReadValue<TValue>(out TValue value);
    public static TValue PromptUntilValid<TValue>(TryReadValue<TValue> tryRead, Action showQuery, Action showError)
    {
        while (true)
        {
            showQuery.Invoke();
            if (tryRead(out TValue value)) return value;

            showError();
        }
    }
}
