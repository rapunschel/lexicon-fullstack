using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using Garage.Models.Records;
using Garage.Models.Vehicles;
using Garage.ViewModels;
using Garage.Views.Utilities;

namespace Garage.Views.UI;

public static class VehicleDetailsReaderUI
{
    private static readonly Dictionary<string, Func<VehicleDetails, VehicleDetails>> dict = new()
    {
        ["airplane"] = GetAirplaneDetails,
        ["boat"] = GetBoatDetails,
        ["car"] = GetCarDetails,
        ["bus"] = GetBusDetails,
        ["motorcycle"] = GetMotorcycleDetails,
    };


    public static bool GetRecord(string type, [NotNullWhen(true)] out VehicleDetails? details)
    {
        details = null;
        if (!dict.Keys.Contains(type.ToLower())) throw new KeyNotFoundException($"{type} was not available in the method dictionary");

        details = dict[type](GetCommonDetails());

        return true;

    }
    private static CarDetails GetCarDetails(VehicleDetails common)
    {
        int engineCount = InputReader.PromptUntilValid<int>(
            tryRead: InputReader.ReadPositiveInt,
            showQuery: () => Display.ShowQuery("How many engines: "),
            showError: () => Display.ErrorMessage(error: "Not a positive integer"));

        return new CarDetails(
            common.Registration,
            common.Brand,
            common.Color,
            common.WheelCount,
            EngineCount: engineCount);
    }
    private static MotorcycleDetails GetMotorcycleDetails(VehicleDetails common)
    {
        int seats = InputReader.PromptUntilValid<int>(
            tryRead: InputReader.ReadPositiveInt,
            showQuery: () => Display.ShowQuery("How many seats: "),
            showError: () => Display.ErrorMessage(error: "Not a positive integer"));

        return new MotorcycleDetails(
            common.Registration,
            common.Brand,
            common.Color,
            common.WheelCount,
            Seats: seats);
    }

    private static BoatDetails GetBoatDetails(VehicleDetails common)
    {
        FuelType fuel = InputReader.PromptUntilValid<FuelType>(
            tryRead: InputReader.ReadFuelTypeEnum,
            showQuery: () => Display.ShowQuery("Fuel type (Gasoline / Diesel): "),
            showError: () => Display.ErrorMessage(error: "Invalid fuel type"));

        return new BoatDetails(
            common.Registration,
            common.Brand,
            common.Color,
            common.WheelCount,
            Fuel: fuel);
    }

    private static AirplaneDetails GetAirplaneDetails(VehicleDetails common)
    {
        int speed = InputReader.PromptUntilValid<int>(
            tryRead: InputReader.ReadPositiveInt,
            showQuery: () => Display.ShowQuery("Enter speed: "),
            showError: () => Display.ErrorMessage(error: "Not a positive integer"));

        return new AirplaneDetails(
            common.Registration,
            common.Brand,
            common.Color,
            common.WheelCount,
            Speed: speed);
    }
    private static BusDetails GetBusDetails(VehicleDetails common)
    {
        double length = InputReader.PromptUntilValid<double>(
            tryRead: InputReader.ReadPositiveDouble,
            showQuery: () => Display.ShowQuery("Enter length of bus: "),
            showError: () => Display.ErrorMessage(error: "Not a positive value"));


        return new BusDetails(
            common.Registration,
            common.Brand,
            common.Color,
            common.WheelCount,
            Length: length);
    }


    private static VehicleDetails GetCommonDetails()
    {
        while (true)
        {
            try
            {

                string input = InputReader.PromptUntilValid<string>(
                        tryRead: InputReader.ReadString,
                        showQuery: () => Display.ShowQuery("Enter the format (space separated): brand registration color wheel count"),
                        showError: () => Display.ErrorMessage(error: "Must enter something"));
                string[] strings = input.Split(' ');

                if (int.TryParse(strings[3].Trim(), out int wheelCount))
                {
                    return new VehicleDetails(Brand: strings[0].Trim(), Registration: strings[1].Trim(), Color: strings[2].Trim(), WheelCount: wheelCount);
                }
                else
                {
                    Display.ErrorMessage("Invalid input. Try again");
                }
            }
            catch (Exception)
            {
                Display.ErrorMessage("Incorrect format. Example: Ford abc123 red 5");
            }
        }
    }
}
