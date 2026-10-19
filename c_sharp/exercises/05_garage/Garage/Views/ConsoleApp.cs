using System;
using System.IO.Pipelines;
using System.Text;
using Garage.Models;
using Garage.ViewModels;

namespace Garage.Views;

public class ConsoleApp
{

    private GarageViewModel _gvm = new();

    public void Run()
    {
        const string headMenuMessage = "Welcome to the Garage";
        const string menuCmdsMessage = "\n0: To exit program.\n" +
                         "1: Create Garage.\n" +
                         "2: Add vehicle to garage.\n" +
                         "3: Search vehicle in garage.\n" +
                         "4: Remove vehicle in garage.\n" +
                         "5: List all parked vehicles\n";


        //GarageCollection collection;
        const string invalidCmdMessage = "Invalid command";
        System.Console.WriteLine(headMenuMessage);
        bool isRunning = true;
        while (isRunning)
        {

            System.Console.WriteLine(menuCmdsMessage);
            if (int.TryParse(Console.ReadLine(), out int command))
            {
                switch (command)
                {
                    case 0:
                        isRunning = false;
                        break;
                    case 1:
                        CreateGarageCollection();
                        WaitForUserKeyPress();
                        break;
                    case 2:
                        AddVehicle();
                        WaitForUserKeyPress();

                        break;
                    case 3:
                        WaitForUserKeyPress();

                        break;
                    case 4:
                        WaitForUserKeyPress();

                        break;
                    case 5:
                        ShowParkedVehicles();
                        WaitForUserKeyPress();

                        break;
                    default:
                        System.Console.WriteLine(invalidCmdMessage);
                        break;
                }

            }
            else System.Console.WriteLine(invalidCmdMessage);
        }
    }

    private void CreateGarageCollection()
    {
        while (true)
        {
            int capacity = ReadPositiveInt(query: "How many parking spaces?");
            if (_gvm.CreateGarageCollection(capacity))
            {
                System.Console.WriteLine($"Created garage with {capacity} parking slots.\n");
                return;
            }
            System.Console.WriteLine("Error creating garage. Try again.");
        }
    }


    private void AddVehicle()
    {
        System.Console.WriteLine("Creating vehicle...");
        while (true)
        {
            string input = ReadString(query: "Enter the details as following: registration, brand, color, wheel count");


            string[] strings = input.Split(',');
            if (int.TryParse(strings[3].Trim(), out int wheelCount))
            {
                if (_gvm.AddVehicle(regNumber: strings[0], color: strings[2], brand: strings[1], wheelCount, out string error))
                {
                    System.Console.WriteLine("Vehicle added to garage");
                    return;
                }
                System.Console.WriteLine($"Error: {error}");
                return;
            }

            System.Console.WriteLine("Invalid input. Try again\n");
        }
    }


    private void ShowParkedVehicles()
    {
        System.Console.WriteLine(_gvm.GetGarageInfo());
    }

    private string ReadString(string query = "", string errorMessage = "Empty string.")
    {
        while (true)
        {
            System.Console.WriteLine(query);
            string result = Console.ReadLine();

            if (result == "")
            {
                System.Console.WriteLine(errorMessage);
                continue;
            }
            return result;
        }
    }

    private int ReadPositiveInt(string query, string errorMessage = "Not a valid number.")
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

    private void WaitForUserKeyPress()
    {
        System.Console.WriteLine("\nPress any key to continue...");
        Console.ReadKey();
        System.Console.WriteLine();
    }
}
