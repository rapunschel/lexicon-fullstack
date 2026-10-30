using System;
using Garage.Models.Vehicles;


namespace Garage.Views;

public static class InputReader
{
    public static int ReadPositiveInt(string query, string errorMessage = "Not a valid number.")
    {
        while (true)
        {
            System.Console.WriteLine(query);
            if (int.TryParse(Console.ReadLine(), out int number) && number >= 0)
            {
                return number;
            }
            else System.Console.WriteLine(errorMessage);
        }
    }
    public static double ReadPositiveDouble(string query, string errorMessage = "Not a valid number.")
    {
        while (true)
        {
            System.Console.WriteLine(query);
            if (double.TryParse(Console.ReadLine(), out double number) && number >= 0)
            {
                return number;
            }
            else System.Console.WriteLine(errorMessage);
        }
    }

    public static void WaitForUserKeyPress(string message)
    {
        System.Console.WriteLine($"\n{message}.");
        Console.ReadKey();
        System.Console.WriteLine();
    }

    public static string ReadString(string query = "", string errorMessage = "Empty string.")
    {
        while (true)
        {
            System.Console.WriteLine(query);
            string? result = Console.ReadLine();

            if (result == "" || result == null)
            {
                System.Console.WriteLine(errorMessage);
                continue;
            }
            return result;
        }
    }


    public static FuelType ReadFuelTypeEnum(string query = "", string errorMessage = "Not a valid fuel type.")
    {
        while (true)
        {
            System.Console.WriteLine(query);
            string input = Console.ReadLine() ?? "";
            foreach (FuelType type in Enum.GetValues(typeof(FuelType)))
            {
                if (string.Equals(type.ToString(), input,
                    StringComparison.OrdinalIgnoreCase))
                {
                    return type;
                }
            }
            System.Console.WriteLine(errorMessage);
        }
    }

    public static bool ReadPositiveInt(out int value)
    {
        return int.TryParse(Console.ReadLine(), out value) && value >= 0;

    }

    public static bool ReadPositiveDouble(out int value)
    {
        return int.TryParse(Console.ReadLine(), out value) && value >= 0;

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

    public static bool ReadFuelTypeEnum(out FuelType? fuel)
    {

        string? input = Console.ReadLine();
        foreach (FuelType type in Enum.GetValues(typeof(FuelType)))
        {
            if (string.Equals(type.ToString(), input,
                StringComparison.OrdinalIgnoreCase))
            {
                fuel = type;
                return true;
            }
        }
        fuel = null;
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
