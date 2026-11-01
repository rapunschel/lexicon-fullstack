using System;
using Garage.Models.Vehicles;
namespace Garage.Models.Factories;

using Garage.Models.Interfaces;
using Garage.Models.Records;

public sealed class BoatFactory : IVehicleFactory<Boat>
{
    public static BoatFactory Instance { get; } = new();

    private BoatFactory() { }

    public Boat Create(VehicleDetails details)
    {
        if (details is not BoatDetails boatDetails)
            throw new ArgumentException("BoatFactory requires BoatDetails.", nameof(details));

        return new Boat(
            brand: boatDetails.Brand,
            regNumber: boatDetails.Registration,
            color: boatDetails.Color,
            wheelCount: boatDetails.WheelCount,
            fuel: boatDetails.Fuel);
    }
}
