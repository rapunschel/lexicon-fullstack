using System;
using Garage.Models.Records;
namespace Garage.Views;


public static class VehicleDetailsFactory
{
    public static VehicleDetails Create(VehicleType type, VehicleDetails common) =>
    type switch
    {
        VehicleType.Car => new CarDetails(
            common.Registration,
            common.Brand,
            common.Color,
            common.WheelCount,
            InputReader.ReadPositiveInt("Enter number of engines:")),

        VehicleType.Bus => new BusDetails(
            common.Registration,
            common.Brand,
            common.Color,
            common.WheelCount,
            Length: InputReader.ReadPositiveDouble("Enter length of bus:")),

        VehicleType.Motorcycle => new MotorcycleDetails(
            common.Registration,
            common.Brand,
            common.Color,
            common.WheelCount,
            Seats: InputReader.ReadPositiveInt("How many seats:")),

        VehicleType.Boat => new BoatDetails(
            common.Registration,
            common.Brand,
            common.Color,
            common.WheelCount,
            Fuel: InputReader.ReadFuelTypeEnum("Fuel type (Gasoline / Diesel): ")),

        VehicleType.Airplane => new AirplaneDetails(
            common.Registration,
            common.Brand,
            common.Color,
            common.WheelCount,
            Speed: InputReader.ReadPositiveInt("Enter speed:")),

        _ => throw new ArgumentOutOfRangeException(nameof(type), type, null)
    };
}

