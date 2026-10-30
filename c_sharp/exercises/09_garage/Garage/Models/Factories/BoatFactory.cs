using System;
using Garage.Models.Vehicles;
namespace Garage.Models.Factories;

using Garage.Models.Interfaces;
using Garage.Models.Records;

public sealed class BoatFactory : IVehicleFactory<Boat>
{
    public Boat Create(VehicleDetails details)
    {
        throw new NotImplementedException();
    }
}
