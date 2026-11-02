using System;
using Garage.Models.Vehicles;
using Garage.Models.Interfaces;
using Garage.Views.Utilities;
using System.Collections.Generic;
using System.Text;
using Garage.Models.Utilities;
using System.Linq;
using Garage.ViewModels;
using Garage.Models.Factories;
namespace Garage.Views.UI;

public class GarageHandlerUI
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


    private List<IUI> _garageList = [];
    private IUI? _selectedGarage;

    public void AddGarageUI(IUI ui)
    {
        _garageList.Add(ui);
    }
    public void Run()
    {
        if (_selectedGarage == null) throw new ArgumentNullException("Have not selected a garage");


        StringBuilder commands = new StringBuilder();
        commands.AppendLine($"{(int)Command.Exit}: Back.");
        commands.AppendLine($"{(int)Command.CreateGarage}: Create Garage (this will override existing garage).");
        commands.AppendLine($"{(int)Command.CreateVehicle}: Create {_selectedGarage.GetGarageStorageType()}.");
        commands.AppendLine($"{(int)Command.ParkVehicle}: Park {_selectedGarage.GetGarageStorageType()} in garage.");
        commands.AppendLine($"{(int)Command.Search}: Search {_selectedGarage.GetGarageStorageType()} in garage.");
        commands.AppendLine($"{(int)Command.UnparkVehicle}: Take out {_selectedGarage.GetGarageStorageType()} from garage.");
        commands.AppendLine($"{(int)Command.ShowParked}: List all parked {_selectedGarage.GetGarageStorageType()}.");
        commands.AppendLine($"{(int)Command.ShowNotParked}: List all created {_selectedGarage.GetGarageStorageType()}.");
        commands.Append($"{(int)Command.InitTestData}: Create {_selectedGarage.GetGarageStorageType()} and populate the garage if spaces are available.");

        while (true)
        {

            Display.Clear();
            Display.ShowWelcomeMessage($"Welcome to my garage: {_selectedGarage.Name}");
            Display.ShowCommands(commands.ToString());

            if (!int.TryParse(Console.ReadLine(), out int command))
            {
                Display.ErrorMessage("Not a valid command");
                Display.WaitThenClear();
                continue;
            }

            switch ((Command)command)
            {
                case Command.Exit:
                    return;
                case Command.CreateGarage:
                    _selectedGarage.CreateGarageView();
                    break;
                case Command.CreateVehicle:
                    _selectedGarage.CreateVehicleView();
                    break;
                case Command.ParkVehicle:
                    _selectedGarage.ParkVehicleView();
                    break;
                case Command.UnparkVehicle:
                    _selectedGarage.UnparkVehicleView();
                    break;
                case Command.Search:
                    _selectedGarage.SearchView();
                    break;
                case Command.ShowParked:
                    _selectedGarage.ParkedVehiclesView();
                    break;
                case Command.ShowNotParked:
                    _selectedGarage.UnparkedVehiclesView();

                    break;
                case Command.InitTestData:
                    try
                    {
                        _selectedGarage.InitTestData();

                    }
                    catch (Exception ex)
                    {
                        Display.ErrorMessage(ex.Message);
                    }
                    break;
            }
            Display.WaitThenClear();

        }
    }

    public void CreateGarageView()
    {

        Dictionary<string, Type> types = AssemblyScanner.FindConcreteSubtypesOfT(typeof(IVehicle)).ToDictionary(t => t.Name.ToLower(), t => t);
        types["vehicle"] = typeof(IVehicle);

        Dictionary<Type, Func<int, string, IUI>> _creators = new()
        {
            [typeof(Car)] = (capacity, name) => new ConsoleUI<Car>(new GarageViewModel<Car>(factory: CarFactory.Instance, capacity: capacity), name),
            [typeof(Boat)] = (capacity, name) => new ConsoleUI<Boat>(new GarageViewModel<Boat>(BoatFactory.Instance, capacity), name),
            [typeof(Motorcycle)] = (capacity, name) => new ConsoleUI<Motorcycle>(new GarageViewModel<Motorcycle>(MotorcycleFactory.Instance, capacity), name),
            [typeof(Airplane)] = (capacity, name) => new ConsoleUI<Airplane>(new GarageViewModel<Airplane>(AirplaneFactory.Instance, capacity), name),
            [typeof(Bus)] = (capacity, name) => new ConsoleUI<Bus>(new GarageViewModel<Bus>(BusFactory.Instance, capacity), name),
            [typeof(IVehicle)] = (capacity, name) => new ConsoleUI<IVehicle>(new GarageViewModel<IVehicle>(VehicleFactory.Instance, capacity), name)

        };
        dynamic type = InputReader.PromptUntilValid<Type>(
            tryRead: (out value) =>
            {
                value = default!;
                string input = Console.ReadLine() ?? "";
                string? result = types.Keys.ToList().FirstOrDefault(k => string.Equals(k, input, StringComparison.OrdinalIgnoreCase));

                if (result == null)
                    return false;
                value = types[result];
                return true;
            },
            showQuery: () => Display.ShowQuery($"Create garage for: {string.Join(", ", types.Keys)}"),
            showError: () => Display.ErrorMessage(error: "Unsupported vehicle")
        );

        int capacity = InputReader.PromptUntilValid<int>(
            tryRead: InputReader.ReadPositiveInt,
            showQuery: () => Display.ShowQuery($"How many parking spaces:"),
            showError: () => Display.ErrorMessage(error: "Must enter something")
        );

        string name = InputReader.PromptUntilValid<string>(
            tryRead: InputReader.ReadString,
            showQuery: () => Display.ShowQuery($"Enter garage name:"),
            showError: () => Display.ErrorMessage(error: "Must enter something")
        );

        if (_creators.TryGetValue(type, out Func<int, string, IUI> createGarage))
        {
            _garageList.Add(createGarage(capacity, name));
            Display.SuccessMessage($"Created garage with name {name} and capacity: {capacity}");
        }
    }

    public void ViewGarageMenu()
    {
        if (_garageList.Count == 0)
        {
            Display.ErrorMessage("Must first create a garage");
            return;
        }

        StringBuilder sb = new StringBuilder();

        sb.AppendLine("0: Back");

        for (int i = 0; i < _garageList.Count; i++)
        {
            if (i < _garageList.Count)
                sb.AppendLine($"{i + 1}: {_garageList[i].Name}");
            else
                sb.Append($"{i + 1}: {_garageList[i].Name}");
        }


        while (true)
        {
            Display.ShowCommands(sb.ToString());
            if (!int.TryParse(Console.ReadLine(), out int value))
            {
                Display.ErrorMessage("Not a valid command");
                Display.WaitThenClear();
                continue;
            }
            if (value == 0) return;
            else if (0 < value && value <= _garageList.Count)
            {
                _selectedGarage = _garageList[value - 1];
                Run();
                return;
            }

            Display.ErrorMessage($"Input must be in range: {0} to {_garageList.Count}");
        }
    }
}
