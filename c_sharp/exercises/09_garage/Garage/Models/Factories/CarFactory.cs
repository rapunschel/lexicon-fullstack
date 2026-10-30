using System;
using Garage.Models.Vehicles;
namespace Garage.Models.Factories;

using Garage.Models.Interfaces;
using Garage.Models.Records;

public sealed class CarFactory : IVehicleFactory<Car>
{
    public Car Create(VehicleDetails details)
    {
        throw new NotImplementedException();
    }
}
