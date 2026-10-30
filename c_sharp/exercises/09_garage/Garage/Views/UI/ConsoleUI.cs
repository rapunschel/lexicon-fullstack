using System;
using Garage.Models.Interfaces;
using Garage.Models.Records;
using Garage.Models.Vehicles;
using Garage.ViewModels;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using Garage.Views.Utilities;
using Garage.Models.Utilities;

namespace Garage.Views.UI;

public class ConsoleUI<T> : IUI<T> where T : class, IVehicle
{

    private GarageViewModel<T> _gvm;
    private const string _continueMessage = "Press any key to continue...";

    public ConsoleUI(GarageViewModel<T> gvm)
    {
        _gvm = gvm;
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

        Display.WaitThenClear();
    }



    public void CreateVehicleView()
    {
        try
        {
            HashSet<string> typeSet = [.. AssemblyScanner.FindConcreteSubtypesOfT(typeof(T)).Select(type => type.Name)];
            string input = InputReader.PromptUntilValid<string>(
                tryRead: InputReader.ReadString,
                showQuery: () => Display.ShowQuery($"Do you want to create: {string.Join(", ", typeSet)}?"),
                showError: () => Display.ErrorMessage(error: "Must enter something"));

            bool isValid = typeSet.FirstOrDefault(type => string.Equals(type, input, StringComparison.OrdinalIgnoreCase)) != null;

            if (!isValid)
            {
                Display.ErrorMessage(error: "Not a valid vehicle type");
                return;
            }

            if (VehicleDetailsReaderUI.GetRecord(input, out VehicleDetails? details))
            {

                if (_gvm.CreateVehicle(details))
                {
                    Display.SuccessMessage("Successfully created vehicle");
                }
                else
                {
                    Display.ErrorMessage($"Failed to create vehicle. Unexpected problem");
                }
            }
        }
        catch (Exception ex)
        {
            Display.ErrorMessage(ex.Message);
        }
        finally
        {
            Display.WaitThenClear();
        }
    }


    public void InitTestData()
    {
        _gvm.InitTestData();
        System.Console.WriteLine("Test data added");
        InputReader.WaitForUserKeyPress(_continueMessage);
    }

    public void ParkedVehiclesView()
    {
        string output = _gvm.GetParkedCarsInfo();
        System.Console.WriteLine(output == "" ? "No garage is initated" : output);
        InputReader.WaitForUserKeyPress(_continueMessage);

    }

    public void ParkVehicleView()
    {
        if (_gvm.ParkVehicle(InputReader.ReadString(query: "Park vehicle with registration:")))
        {
            System.Console.WriteLine("Vehicle was parked.");
        }
        else System.Console.WriteLine("Failed to park. Garage full or vehicle doesnt exist or already parked");
        InputReader.WaitForUserKeyPress(_continueMessage);

    }

    public void SearchView()
    {

        VehicleDetails details = new();
        string input = InputReader.ReadString(query: "Search with following format (at least one): reg: <reg>, color: <color>, brand: <brand>, wheels: <number of wheels>");
        string[] parts = input.Split(',');

        foreach (string part in parts)
        {
            int colonIndex = part.IndexOf(":");

            if (colonIndex <= 0 || colonIndex == part.Length - 1)
            {
                Console.WriteLine(
                    "Invalid format. Example: reg: abc123, color: red");
                return;
            }
            string key = part[..colonIndex].Trim();
            string value = part[(colonIndex + 1)..].Trim();

            if (string.IsNullOrWhiteSpace(value))
            {
                Console.WriteLine(
                    "Missing value. Example: reg: abc123, color: red");
                return;
            }

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

        if (!_gvm.Search(details, out IEnumerable<T> items))
        {
            System.Console.WriteLine("Search failed.");
            return;

        }

        if (!items.Any())
        {
            System.Console.WriteLine("No matches");
            return;
        }

        StringBuilder sb = new StringBuilder();
        sb.Append("Search results:\n");
        System.Console.WriteLine("========================================");
        foreach (var item in items)
        {
            sb.Append($"{item}\n");
        }

        System.Console.WriteLine(sb.ToString());
        InputReader.WaitForUserKeyPress(_continueMessage);

    }

    public void UnparkedVehiclesView()
    {
        System.Console.WriteLine(_gvm.GetNotParkedVehicles());
        InputReader.WaitForUserKeyPress(_continueMessage);
    }

    public void UnparkVehicleView()
    {
        if (_gvm.RemoveParkedVehicle(InputReader.ReadString(query: "Remove vehicle from garage with registration:")))
        {
            System.Console.WriteLine("Vehicle was taken out of garage.");
        }
        else System.Console.WriteLine("Vehicle not found");
        InputReader.WaitForUserKeyPress(_continueMessage);

    }



}
