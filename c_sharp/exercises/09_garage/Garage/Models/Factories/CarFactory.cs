using System;
using Garage.Models.Vehicles;
namespace Garage.Models.Factories;

using Garage.Models.Interfaces;
using Garage.Models.Records;

public sealed class CarFactory : IVehicleFactory<Car>
{
    public static CarFactory Instance { get; } = new();

    public Car Create(VehicleDetails details)
    {
        if (details is not CarDetails carDetails)
            throw new ArgumentException("CarFactory requires CarDetails.");
        return new Car(
            brand: carDetails.Brand,
            regNumber: carDetails.Registration,
            color: carDetails.Color,
            wheelCount: carDetails.WheelCount,
            engineCount: carDetails.EngineCount);
    }

}
