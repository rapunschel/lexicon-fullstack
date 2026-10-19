using System;
using System.Runtime.CompilerServices;
using System.Text;
using Garage.Models.Records;
using Garage.Models.Vehicles;
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
                         "2: Create vehicle.\n" +
                         "3: Park vehicle in garage\n" +
                         "4: Search vehicle in garage.\n" +
                         "5: Take out vehicle from garage.\n" +
                         "6: List all parked vehicles\n" +
                         "7: List all created vehicles\n";


        const string invalidCmdMessage = "Invalid command";
        System.Console.WriteLine(headMenuMessage);
        bool isRunning = true;
        while (isRunning)
        {
            System.Console.WriteLine("========================================");
            System.Console.WriteLine(menuCmdsMessage);
            System.Console.WriteLine("========================================");

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
                        CreateVehicleView();
                        WaitForUserKeyPress();
                        break;
                    case 3:
                        WaitForUserKeyPress();

                        break;
                    case 4:
                        WaitForUserKeyPress();

                        break;
                    case 5:
                        WaitForUserKeyPress();
                        break;

                    case 6:
                        ShowParkedVehicles();
                        WaitForUserKeyPress();

                        break;
                    case 7:
                        System.Console.WriteLine(_gvm.GetCreatedVehicles());
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

    private void CreateVehicleView()
    {
        void CreateCar(VehicleType type)
        {
            CarDetails details = CreateVehicle(common =>
            {
                int engineCount = ReadPositiveInt(query: "Enter number of engines:");
                return new CarDetails(common.Registration, common.Brand, common.Color, common.WheelCount, engineCount);
            });
            ValidateCreation(_gvm.CreateVehicle(details, type, out string? error), error);
        }

        void CreateBus(VehicleType type)
        {
            BusDetails details = CreateVehicle(common =>
            {
                return new BusDetails(common.Registration,
                                      common.Brand,
                                      common.Color,
                                      common.WheelCount,
                                      Length: ReadPositiveDouble(query: "Enter length of bus:"));
            });
            ValidateCreation(_gvm.CreateVehicle(details, type, out string? error), error);
        }

        void CreateMotorcycle(VehicleType type)
        {
            MotorcycleDetails details = CreateVehicle(common =>
            {
                return new MotorcycleDetails(common.Registration,
                                      common.Brand,
                                      common.Color,
                                      common.WheelCount,
                                      Seats: ReadPositiveInt(query: "How many seats:"));
            });
            ValidateCreation(_gvm.CreateVehicle(details, type, out string? error), error);
        }

        void CreateBoat(VehicleType type)
        {
            BoatDetails details = CreateVehicle(common =>
            {
                return new BoatDetails(common.Registration,
                                      common.Brand,
                                      common.Color,
                                      common.WheelCount,
                                      Fuel: ReadFuelTypeEnum(query: "Fuel type (Gasoline / Diesel): "));
            });
            ValidateCreation(_gvm.CreateVehicle(details, type, out string? error), error);
        }

        void CreateAirPlane(VehicleType type)
        {
            AirplaneDetails details = CreateVehicle(common =>
            {
                return new AirplaneDetails(common.Registration,
                                      common.Brand,
                                      common.Color,
                                      common.WheelCount,
                                      Speed: ReadPositiveInt(query: "Enter speed:"));
            });
            ValidateCreation(_gvm.CreateVehicle(details, type, out string? error), error);
        }

        T CreateVehicle<T>(Func<VehicleDetails, T> createDetails) where T : VehicleDetails
        {
            T details = createDetails(ReadVehicleDetails());

            return details;

        }
        void ValidateCreation(bool result, string? error)
        {
            if (result)
            {
                System.Console.WriteLine("Successfully created vehicle");
            }
            else
            {
                System.Console.WriteLine($"Error: {error}");
            }
        }


        VehicleType? type = GetVehicleType(
                            ReadString("Do you want to create: airplane, car, bus, motorcycle or boat?")
                            );

        if (type == null)
        {
            System.Console.WriteLine("Not a valid vehicle type");
        }

        switch (type)
        {
            case VehicleType.Car:
                CreateCar((VehicleType)type);
                return;
            case VehicleType.Bus:
                CreateBus((VehicleType)type);
                return;
            case VehicleType.Motorcycle:
                CreateMotorcycle((VehicleType)type);

                return;
            case VehicleType.Boat:
                CreateBoat((VehicleType)type);
                return;
            case VehicleType.Airplane:
                CreateAirPlane((VehicleType)type);
                return;
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
        System.Console.WriteLine(_gvm.GetParkedCarsInfo());
    }


    private FuelType ReadFuelTypeEnum(string query = "", string errorMessage = "Not a valid fuel type.")
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

    private double ReadPositiveDouble(string query, string errorMessage = "Not a valid number.")
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
