using System;
namespace Garage.Views.Utilities;

public static class Display
{
    public static void WaitForKeyPressMessage()
    {
        Console.ForegroundColor = ConsoleColor.Yellow;
        System.Console.WriteLine("Press any key to continue...");
        Console.ResetColor();
    }

    public static void ShowTable(string table, string desc, bool shouldClearBefore = true, bool shouldClearAfter = true)
    {
        if (shouldClearBefore) Clear();

        ConsoleColor[] rainbow = [
                    ConsoleColor.DarkYellow,
                    ConsoleColor.Green,
                    ConsoleColor.Red,
                    ConsoleColor.Cyan,
                    ConsoleColor.Blue,
                ];


        string[] lines = table.Split(Environment.NewLine);
        Console.ForegroundColor = ConsoleColor.Magenta;
        System.Console.WriteLine($"{desc,60}");

        for (int i = 0; i < lines.Length; i++)
        {

            if (i == lines.Length - 1) Console.ForegroundColor = ConsoleColor.DarkGreen;
            else if (i > 2)
                Console.ForegroundColor = rainbow[i % rainbow.Length];
            Console.WriteLine(lines[i]);
        }
        if (shouldClearAfter) WaitThenClear();

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
        Console.ForegroundColor = ConsoleColor.DarkGreen;
        Console.WriteLine(message);
        Console.ResetColor();
    }

    public static void ErrorMessage(string error)
    {
        Console.ForegroundColor = ConsoleColor.DarkRed;
        Console.WriteLine($"Error: {error}");
        Console.ResetColor();
    }

    public static void WaitThenClear()
    {
        WaitForKeyPressMessage();
        InputReader.WaitForUserKeyPress();
        Console.Clear();
    }

    public static void Clear()
    {
        Console.Clear();

    }


}
