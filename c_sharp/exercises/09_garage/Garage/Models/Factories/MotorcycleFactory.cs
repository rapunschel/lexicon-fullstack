using System;
using Garage.Models.Vehicles;
using Garage.Models.Interfaces;
using Garage.Models.Records;
namespace Garage.Models.Factories;

public sealed class MotorcycleFactory : IVehicleFactory<Motorcycle>
{
    public static MotorcycleFactory Instance { get; } = new();

    private MotorcycleFactory() { }

    public Motorcycle Create(VehicleDetails details)
    {
        if (details is not MotorcycleDetails motorcycleDetails)
            throw new ArgumentException("MotorcycleFactory requires MotorcycleDetails.", nameof(details));

        return new Motorcycle(
            brand: motorcycleDetails.Brand,
            regNumber: motorcycleDetails.Registration,
            color: motorcycleDetails.Color,
            wheelCount: motorcycleDetails.WheelCount,
            seats: motorcycleDetails.Seats);
    }
}