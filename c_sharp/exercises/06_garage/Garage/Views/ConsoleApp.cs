using System;
using System.Collections.Generic;
using System.Text;
using Garage.Models.Records;
using Garage.Models.Vehicles;
using Garage.ViewModels;
namespace Garage.Views;

public class ConsoleApp
{
    private enum Command
    {
        Exit = 0,
        CreateGarage = 1,
        CreateVehicle = 2,
        ParkVehicle = 3,
        Search = 4,
        RemoveFromGarage = 5,
        ShowParked = 6,
        ShowNotParked = 7,
        InitTestData = 8
    }

    private GarageViewModel _gvm = default!;

    public void Run()
    {
        const string headMenuMessage = "Welcome to the Garage";
        const string menuCmdsMessage = "\n0: To exit program.\n" +
                         "1: Create Garage (this will override existing garage).\n" +
                         "2: Create vehicle.\n" +
                         "3: Park vehicle in garage\n" +
                         "4: Search vehicle in garage.\n" +
                         "5: Take out vehicle from garage.\n" +
                         "6: List all parked vehicles\n" +
                         "7: List all unparked vehicles\n" +
                         "8: Creates vehicles and populates garage with a few vehicles if it has available spaces";


        const string invalidCmdMessage = "Invalid command";
        System.Console.WriteLine(headMenuMessage);
        bool isRunning = true;
        while (isRunning)
        {

            if (_gvm != null)
            {
                System.Console.WriteLine("========================================");
                System.Console.WriteLine(menuCmdsMessage);
                System.Console.WriteLine("========================================");

                if (int.TryParse(Console.ReadLine(), out int command))
                {

                    switch ((Command)command)
                    {
                        case Command.Exit:
                            isRunning = false;
                            break;
                        case Command.CreateGarage:
                            CreateGarageCollection();
                            WaitForUserKeyPress();
                            break;
                        case Command.CreateVehicle:
                            CreateVehicleView();
                            WaitForUserKeyPress();
                            break;
                        case Command.ParkVehicle:
                            ParkVehicleView();
                            WaitForUserKeyPress();
                            break;
                        case Command.RemoveFromGarage:
                            UnparkVehicleView();
                            WaitForUserKeyPress();
                            break;
                        case Command.Search:
                            SearchView();
                            WaitForUserKeyPress();
                            break;
                        case Command.ShowParked:
                            ShowParkedVehicles();
                            WaitForUserKeyPress();
                            break;
                        case Command.ShowNotParked:
                            System.Console.WriteLine(_gvm.GetNotParkedVehicles());
                            WaitForUserKeyPress();
                            break;
                        case Command.InitTestData:
                            try
                            {
                                _gvm.InitTestData();
                                System.Console.WriteLine("Test data added");
                                WaitForUserKeyPress();

                            }
                            catch (Exception ex)
                            {
                                System.Console.WriteLine(ex.Message);
                                WaitForUserKeyPress();
                            }
                            break;
                        default:
                            System.Console.WriteLine(invalidCmdMessage);
                            break;
                    }

                }
                else System.Console.WriteLine(invalidCmdMessage);
            }
            else
            {
                System.Console.WriteLine("\n0: To exit program.\n" +
                         "1: Create Garage.\n");

                if (int.TryParse(Console.ReadLine(), out int command))
                {

                    switch ((Command)command)
                    {
                        case Command.Exit:
                            isRunning = false;
                            break;
                        case Command.CreateGarage:
                            int capacity = Utilities.ReadPositiveInt(query: "How many parking spaces?");
                            _gvm = new GarageViewModel(capacity);
                            System.Console.WriteLine($"Created garage with {capacity} parking slots.\n");
                            WaitForUserKeyPress();
                            break;
                    }
                }
                else System.Console.WriteLine(invalidCmdMessage);

            }
        }
    }



    private void SearchView()
    {

        VehicleDetails details = new();
        string input = Utilities.ReadString(query: "Search with following format (at least one): reg: <reg>, color: <color>, brand: <brand>, wheels: <number of wheels>");
        string[] parts = input.Split(',');

        foreach (string part in parts)
        {
            int colonIndex = part.IndexOf(":");
            string key = part[..colonIndex].Trim();
            string value = part[(colonIndex + 1)..].Trim();

            switch (key.ToLower())
            {
                case "reg":
                    details.Registration = value;
                    break;
                case "color":
                    details.Color = value;
                    break;
                case "brand":
                    details.Brand = value;
                    break;
                case "wheels":
                    if (int.TryParse(value, out int result) && result >= 0)
                    {
                        details.WheelCount = result;
                    }
                    else
                    {
                        System.Console.WriteLine("Invalid wheels parameter or non-positive value");
                        return;
                    }
                    break;
                default:
                    System.Console.WriteLine("Failed to parse search query.");
                    return;
            }
        }

        if (_gvm.Search(details, out List<Vehicle> items))
        {
            if (items.Count == 0)
            {
                System.Console.WriteLine("No matches");
            }
            else
            {
                StringBuilder sb = new StringBuilder();
                sb.Append("Search results:\n");
                System.Console.WriteLine("========================================");
                foreach (var item in items)
                {
                    sb.Append($"{item}\n");
                }

                System.Console.WriteLine(sb.ToString());
            }
        }
        else
        {
            System.Console.WriteLine("Search failed.");
        }
    }

    private void ParkVehicleView()
    {
        if (_gvm.ParkVehicle(Utilities.ReadString(query: "Park vehicle with registration:")))
        {
            System.Console.WriteLine("Vehicle was parked.");
        }
        else System.Console.WriteLine("Failed to park. Garage full or vehicle doesnt exist or already parked");
    }

    private void UnparkVehicleView()
    {
        if (_gvm.RemoveParkedVehicle(Utilities.ReadString(query: "Remove vehicle from garage with registration:")))
        {
            System.Console.WriteLine("Vehicle was taken out of garage.");
        }
        else System.Console.WriteLine("Vehicle not found");

    }
    private void CreateGarageCollection()
    {
        while (true)
        {
            int capacity = Utilities.ReadPositiveInt(query: "How many parking spaces?");
            if (_gvm.CreateNewGarageCollection(capacity))
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
            try
            {
                string input = Utilities.ReadString(query: "Enter the format: brand, registration, color, wheel count");
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
            catch (Exception ex)
            {
                System.Console.WriteLine("Incorrect format. Example: Ford, abc123, red, 5");
            }
        }
    }

    private void CreateVehicleView()
    {
        if (GetVehicleType(Utilities.ReadString(
                "Do you want to create: airplane, car, bus, motorcycle or boat?"))
            is not VehicleType type)
        {
            Console.WriteLine("Not a valid vehicle type");
            return;
        }

        if (_gvm.CreateVehicle(VehicleDetailsFactory.Create(type, ReadVehicleDetails()), type, out string? error))
        {
            System.Console.WriteLine("Successfully created vehicle");
        }
        else
        {
            System.Console.WriteLine($"Error: {error}");
        }
    }

    private VehicleType? GetVehicleType(string input)
    {
        foreach (VehicleType type in Enum.GetValues(typeof(VehicleType)))
        {
            if (string.Equals(type.ToString(), input,
                            StringComparison.OrdinalIgnoreCase))
            {
                return type;
            }
        }
        return null;
    }

    private void ShowParkedVehicles()
    {
        string output = _gvm.GetParkedCarsInfo();
        System.Console.WriteLine(output == "" ? "No garage is initated" : output);
    }

    public void WaitForUserKeyPress()
    {
        System.Console.WriteLine("\nPress any key to continue...");
        Console.ReadKey();
        System.Console.WriteLine();
    }

    private static class VehicleDetailsFactory
    {
        public static VehicleDetails Create(VehicleType type, VehicleDetails common) =>
        type switch
        {
            VehicleType.Car => new CarDetails(
                common.Registration,
                common.Brand,
                common.Color,
                common.WheelCount,
                Utilities.ReadPositiveInt("Enter number of engines:")),

            VehicleType.Bus => new BusDetails(
                common.Registration,
                common.Brand,
                common.Color,
                common.WheelCount,
                Length: Utilities.ReadPositiveDouble("Enter length of bus:")),

            VehicleType.Motorcycle => new MotorcycleDetails(
                common.Registration,
                common.Brand,
                common.Color,
                common.WheelCount,
                Seats: Utilities.ReadPositiveInt("How many seats:")),

            VehicleType.Boat => new BoatDetails(
                common.Registration,
                common.Brand,
                common.Color,
                common.WheelCount,
                Fuel: Utilities.ReadFuelTypeEnum("Fuel type (Gasoline / Diesel): ")),

            VehicleType.Airplane => new AirplaneDetails(
                common.Registration,
                common.Brand,
                common.Color,
                common.WheelCount,
                Speed: Utilities.ReadPositiveInt("Enter speed:")),

            _ => throw new ArgumentOutOfRangeException(nameof(type), type, null)
        };
    }
}
