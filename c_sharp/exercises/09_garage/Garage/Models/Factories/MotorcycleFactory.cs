using System;
using Garage.Models.Vehicles;
namespace Garage.Models.Factories;

using Garage.Models.Interfaces;
using Garage.Models.Records;

public sealed class MotorcycleFactory : IVehicleFactory<Motorcycle>
{
    public Motorcycle Create(VehicleDetails details)
    {
        throw new NotImplementedException();
    }
}