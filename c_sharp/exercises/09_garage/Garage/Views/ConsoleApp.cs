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

    public void Run()
    {


        int capacity = InputReader.PromptUntilValid<int>(
            tryRead: InputReader.ReadPositiveInt,
            showQuery: () => Display.ShowQuery($"How many parking spaces:"),
            showError: () => Display.ErrorMessage(error: "Must enter something")
        );


        GarageViewModel<IVehicle> _gvm = new GarageViewModel<IVehicle>(factory: new VehicleFactory(), capacity: capacity);
        System.Console.WriteLine($"Created garage with {capacity} parking slots.\n");
        Display.WaitForKeyPressMessage();
        IUI<IVehicle> ui = new ConsoleUI<IVehicle>(gvm: _gvm);


        bool isRunning = true;
        while (isRunning)
        {
            Display.Clear();
            Display.ShowWelcomeMessage();
            Display.ShowGarageCommands();

            if (!int.TryParse(Console.ReadLine(), out int command))
            {
                Display.ErrorMessage("Not a valid command");
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
