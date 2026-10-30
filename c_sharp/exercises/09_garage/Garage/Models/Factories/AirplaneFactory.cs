using System;
using Garage.Models.Vehicles;
namespace Garage.Models.Factories;

using Garage.Models.Interfaces;
using Garage.Models.Records;

public sealed class AirplaneFactory : IVehicleFactory<Airplane>
{
    public Airplane Create(VehicleDetails details)
    {
        throw new NotImplementedException();
    }
}
