using System;
namespace Garage.Views.Utilities;

public static class Display
{

    private static ConsoleColor _menuTextColor = ConsoleColor.Yellow;
    private static ConsoleColor _mainTextColor = ConsoleColor.Cyan;
    private static ConsoleColor _errorTextColor = ConsoleColor.DarkRed;
    private static ConsoleColor _successTextColor = ConsoleColor.DarkGreen;
    private const int _offset = -120;

    private const int _menuTextCentered = 60;
    public static void WaitForKeyPressMessage()
    {
        Console.ForegroundColor = ConsoleColor.DarkYellow;
        System.Console.WriteLine("Press any key to continue...");
        Console.ResetColor();
    }


    public static void ShowWelcomeMessage()
    {
        Console.ForegroundColor = _menuTextColor;
        const string menuMessage = "Welcome to the Garage";
        System.Console.WriteLine(menuMessage);
        Console.ResetColor();

    }
    public static void ShowGarageCommands()
    {
        const string menuCommands = "\n0: To exit program.\n" +
                         "1: Create Garage (this will override existing garage).\n" +
                         "2: Create vehicle.\n" +
                         "3: Park vehicle in garage\n" +
                         "4: Search vehicle in garage.\n" +
                         "5: Take out vehicle from garage.\n" +
                         "6: List all parked vehicles\n" +
                         "7: List all unparked vehicles\n" +
                         "8: Creates vehicles and populates garage with a few vehicles if it has available spaces";
        Console.ForegroundColor = _menuTextColor;
        System.Console.WriteLine(new string('=', Math.Abs(_offset)));
        System.Console.WriteLine(menuCommands);
        System.Console.WriteLine(new string('=', Math.Abs(_offset)));

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
        Console.ForegroundColor = _menuTextColor;
        System.Console.WriteLine($"{desc,_menuTextCentered}");

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
        Console.ForegroundColor = _mainTextColor;
        System.Console.WriteLine(query);
        Console.ResetColor();

    }

    public static void SuccessMessage(string message)
    {
        Console.ForegroundColor = _successTextColor;
        Console.WriteLine(message);
        Console.ResetColor();
    }

    public static void ErrorMessage(string error)
    {
        Console.ForegroundColor = _errorTextColor;
        Console.WriteLine(error);
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
