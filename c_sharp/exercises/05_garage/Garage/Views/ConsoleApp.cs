using System;
using System.IO.Pipelines;
using System.Text;
using Garage.Models;
using Garage.Models.Vehicles;
using Garage.ViewModels;

namespace Garage.Views;

public class ConsoleApp
{

    private record VehicleDetails(
        string Registration,
        string Brand,
        string Color,
        int WheelCount
        );


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

    private VehicleDetails ReadVehicleDetails()
    {

        while (true)
        {
            string input = ReadString(query: "Enter the format: brand, registration, color, wheel count");
            string[] strings = input.Split(',');

            if (int.TryParse(strings[3].Trim(), out int wheelCount))
            {
                return new VehicleDetails(Brand: strings[0].Trim(), Registration: strings[1].Trim(), Color: strings[2].Trim(), WheelCount: wheelCount);
            }
            else
            {
                System.Console.WriteLine("Invalid input. Try again\n");
            }
        }
    }

    private void AddVehicle()
    {
        System.Console.WriteLine("Creating vehicle...");
        VehicleDetails details = ReadVehicleDetails();
        bool result = _gvm.AddVehicle(details.Registration, details.Brand, details.Color, details.WheelCount, out string error);
        if (result)
        {
            System.Console.WriteLine("Vehicle created successfully...");
        }
        else
        {
            System.Console.WriteLine($"Error: {error}");
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
            string? result = Console.ReadLine();

            if (result == "" || result == null)
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
