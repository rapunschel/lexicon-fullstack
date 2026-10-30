using System;
using Garage.Models.Vehicles;


namespace Garage.Views;

public static class Utilities
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
            string? input = Console.ReadLine();
            foreach (FuelType type in Enum.GetValues(typeof(FuelType)))
            {
                if (string.Equals(type.ToString(), input,
                    StringComparison.OrdinalIgnoreCase))
                {
                    return type;
                }
                System.Console.WriteLine(errorMessage);
            }
        }
    }


}
