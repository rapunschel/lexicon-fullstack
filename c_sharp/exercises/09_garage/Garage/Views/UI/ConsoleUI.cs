using System;
using Garage.Models.Interfaces;
using Garage.Models.Records;
using Garage.Models.Vehicles;
using Garage.ViewModels;
using System.Collections.Generic;
using System.Linq;
using Garage.Views.Utilities;
using Garage.Models.Utilities;
using Garage.Models.Enums;
using Garage.Utils;

namespace Garage.Views.UI;

public class ConsoleUI<T> : IUI where T : class, IVehicle
{

    private GarageViewModel<T> _gvm;
    public string Name
    {
        get;
        private set;
    }
    public ConsoleUI(GarageViewModel<T> gvm, string name)
    {
        _gvm = gvm;
        Name = name;
    }

    public void CreateGarageView()
    {
        Display.Clear();
        Display.CreateGarageInit(typeof(T).Name);

        int capacity = InputReader.PromptUntilValid<int>(
            tryRead: InputReader.ReadPositiveInt,
            showQuery: () => Display.ShowQuery("How many parking slots?"),
            showError: () => Display.ErrorMessage(error: "Not a positive integer"));

        if (_gvm.CreateNewGarageCollection(capacity))
            Display.SuccessMessage($"Created garage with {capacity} parking slots.");
        else Display.ErrorMessage("Unexpected error: creating garage.");

    }

    public string GetGarageStorageType()
    {
        HashSet<string> typeSet = [.. AssemblyScanner.FindConcreteSubtypesOfT(typeof(T)).Select(type => type.Name)];
        return typeSet.FirstOrDefault(type => type == typeof(T).Name) ?? "vehicle";
    }

    public void CreateVehicleView()
    {
        string input = "";
        try
        {
            HashSet<string> typeSet = [.. AssemblyScanner.FindConcreteSubtypesOfT(typeof(T)).Select(type => type.Name)];
            input = InputReader.PromptUntilValid<string>(
               tryRead: InputReader.ReadString,
               showQuery: () => Display.ShowQuery($"Choose what to create: {string.Join(", ", typeSet)}"),
               showError: () => Display.ErrorMessage(error: "Must enter something"));

            bool isValid = typeSet.FirstOrDefault(type => string.Equals(type, input, StringComparison.OrdinalIgnoreCase)) != null;

            if (!isValid)
            {
                Display.ErrorMessage(error: $"Not a valid vehicle type");
                return;
            }

            if (VehicleDetailsReaderUI.GetRecord(input, out VehicleDetails? details))
            {
                _gvm.CreateVehicle(details);
                Display.SuccessMessage($"Created {GetGarageStorageType()} successfully");
            }
        }
        catch (KeyNotFoundException ex)
        {
            Display.ErrorMessage($"Failed to get details for {input}. Error: {ex.Message}");
        }
        catch (Exception ex)
        {
            Display.ErrorMessage(ex.Message); // Vehicle throws argumentException, caught here to display error
        }
    }


    public void InitTestData()
    {
        Display.Clear();
        _gvm.InitTestData();
        Display.SuccessMessage("Test data added");
    }

    public void ParkedVehiclesView()
    {
        string output = _gvm.GetParkedCarsInfo();
        if (output == null) Display.ErrorMessage("No garage is initiated");
        else Display.ShowTable(output, $"PARKED", shouldClearBefore: true);
    }

    public void UnparkedVehiclesView()
    {
        string output = _gvm.GetNotParkedVehicles();
        if (output == null) Display.ErrorMessage("No garage is initiated");
        else Display.ShowTable(output, $"UNPARKED", shouldClearBefore: true);
    }

    public void ParkVehicleView()
    {
        try
        {
            string input = InputReader.PromptUntilValid<string>(
                tryRead: InputReader.ReadString,
                showQuery: () => Display.ShowQuery($"Park vehicle with registration:"),
                showError: () => Display.ErrorMessage(error: "Must enter something")
            );

            ParkResult result = _gvm.ParkVehicle(input);

            switch (result)
            {
                case ParkResult.Parked:
                    Display.SuccessMessage($"{GetGarageStorageType()} was parked.");
                    break;
                case ParkResult.AlreadyParked:
                    Display.ErrorMessage($"{GetGarageStorageType()} is already parked.");
                    break;
                case ParkResult.AtCapacity:
                    Display.ErrorMessage("Garage is full");
                    break;
                case ParkResult.VehicleNotCreated:
                    Display.ErrorMessage($"{GetGarageStorageType()} with registration: {input} not found.");
                    break;
                default:
                    throw new InvalidOperationException($"Unexpected result: {result}");
            }
        }
        catch (Exception ex)
        {
            Display.ErrorMessage(ex.Message);
        }
    }

    public void SearchView()
    {
        try
        {
            if (VehicleDetailsReaderUI.ReadVehicleSearchDetails(out VehicleDetails details))
                if (_gvm.Search(details, out IEnumerable<T> items))
                {
                    if (!items.Any())
                    {
                        Display.SuccessMessage("No matches");
                        return;
                    }

                    Display.ShowTable(TableFormatter<T>.FormatTable(items).ToString(), "Search results");
                }
                else
                    Display.ErrorMessage("Search failed.");
        }
        catch (Exception ex)
        {
            Display.ErrorMessage(ex.Message);
        }
    }

    public void UnparkVehicleView()
    {
        try
        {
            string input = InputReader.PromptUntilValid<string>(
                tryRead: InputReader.ReadString,
                showQuery: () => Display.ShowQuery($"Unpark vehicle with registration:"),
                showError: () => Display.ErrorMessage(error: "Must enter something")
            );
            ParkResult result = _gvm.RemoveParkedVehicle(input);

            switch (result)
            {
                case ParkResult.Unparked:
                    Display.SuccessMessage($"{GetGarageStorageType()} was unparked.");
                    break;
                case ParkResult.NotParked:
                    Display.ErrorMessage($"{GetGarageStorageType()} was not found in the garage.");
                    break;
                default:
                    throw new InvalidOperationException($"Unexpected result: {result}");
            }

        }
        catch (Exception ex)
        {
            Display.ErrorMessage(ex.Message);
        }

    }
}
