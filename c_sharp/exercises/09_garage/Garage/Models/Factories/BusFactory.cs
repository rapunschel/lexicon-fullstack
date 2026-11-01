using System;
using Garage.Models.Vehicles;
namespace Garage.Models.Factories;

using Garage.Models.Interfaces;
using Garage.Models.Records;

public sealed class BusFactory : IVehicleFactory<Bus>
{
    public static BusFactory Instance { get; } = new();

    private BusFactory() { }

    public Bus Create(VehicleDetails details)
    {
        if (details is not BusDetails busDetails)
            throw new ArgumentException("BusFactory requires BusDetails.", nameof(details));

        return new Bus(
            brand: busDetails.Brand,
            regNumber: busDetails.Registration,
            color: busDetails.Color,
            wheelCount: busDetails.WheelCount,
            length: busDetails.Length);
    }
}
