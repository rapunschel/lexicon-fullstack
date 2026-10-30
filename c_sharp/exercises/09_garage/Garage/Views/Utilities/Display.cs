using System;
namespace Garage.Views.Utilities;

public static class Display
{
    private static void WaitForKeyPress()
    {
        Console.ForegroundColor = ConsoleColor.Yellow;
        System.Console.WriteLine("Press any key to continue...");
        Console.ResetColor();
    }

    public static void CreateGarageInit(string vehicleType)
    {
        Console.ForegroundColor = ConsoleColor.Green;
        System.Console.WriteLine($"Initiating: {vehicleType} garage.");
        Console.ResetColor();
    }


    public static void ShowQuery(string query)
    {
        Console.ForegroundColor = ConsoleColor.Cyan;
        System.Console.WriteLine(query);
        Console.ResetColor();

    }



    public static void SuccessMessage(string message)
    {
        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine(message);
        Console.ResetColor();
    }

    public static void ErrorMessage(string error)
    {
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine($"Error: {error}");
        Console.ResetColor();
    }

    public static void WaitThenClear()
    {
        WaitForKeyPress();
        InputReader.WaitForUserKeyPress();
        Console.Clear();
    }

    public static void Clear()
    {
        Console.Clear();

    }


}
