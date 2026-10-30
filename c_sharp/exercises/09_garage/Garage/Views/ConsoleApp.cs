using System;
using Garage.Models.Vehicles;
using Garage.ViewModels;
using Garage.Models.Factories;
using Garage.Models.Interfaces;
using Garage.Views.UI;
using Garage.Views.Utilities;

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
        UnparkVehicle = 5,
        ShowParked = 6,
        ShowNotParked = 7,
        InitTestData = 8
    }

    private const string _continueMessage = "Press any key to continue...";
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

        int capacity = InputReader.ReadPositiveInt(query: "How many parking spaces?");
        GarageViewModel<IVehicle> _gvm = new GarageViewModel<IVehicle>(factory: new VehicleFactory(), capacity: capacity);
        System.Console.WriteLine($"Created garage with {capacity} parking slots.\n");
        InputReader.WaitForUserKeyPress(_continueMessage);
        IUI<IVehicle> ui = new ConsoleUI<IVehicle>(gvm: _gvm);

        bool isRunning = true;
        while (isRunning)
        {


            System.Console.WriteLine("========================================");
            System.Console.WriteLine(menuCmdsMessage);
            System.Console.WriteLine("========================================");

            if (!int.TryParse(Console.ReadLine(), out int command))
            {
                Display.ErrorMessage(invalidCmdMessage);
                Display.WaitThenClear();
                continue;
            }


            switch ((Command)command)
            {
                case Command.Exit:
                    isRunning = false;
                    break;
                case Command.CreateGarage:
                    ui.CreateGarageView();
                    break;
                case Command.CreateVehicle:
                    ui.CreateVehicleView();
                    break;
                case Command.ParkVehicle:
                    ui.ParkVehicleView();
                    break;
                case Command.UnparkVehicle:
                    ui.UnparkVehicleView();
                    break;
                case Command.Search:
                    ui.SearchView();
                    break;
                case Command.ShowParked:
                    ui.ParkedVehiclesView();
                    break;
                case Command.ShowNotParked:
                    ui.UnparkedVehiclesView();

                    break;
                case Command.InitTestData:
                    try
                    {
                        ui.InitTestData();

                    }
                    catch (Exception ex)
                    {
                        Display.ErrorMessage(ex.Message);
                        Display.WaitThenClear();
                    }
                    break;
            }
        }

    }
}
