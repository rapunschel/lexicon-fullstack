using System;
using Garage.Models.Interfaces;
using Garage.Models.Records;
using Garage.Models.Vehicles;

namespace Garage.Models.Factories;

public sealed class VehicleFactory : IVehicleFactory<IVehicle>
{

    public static VehicleFactory Instance { get; } = new();

    private VehicleFactory() { }

    public IVehicle Create(VehicleDetails vehicleDetails)
    {

        switch (vehicleDetails)
        {
            case CarDetails carDetails:
                return CarFactory.Instance.Create(carDetails);

            case BusDetails busDetails:
                return BusFactory.Instance.Create(busDetails);

            case MotorcycleDetails motorcycleDetails:
                return MotorcycleFactory.Instance.Create(motorcycleDetails);

            case BoatDetails boatDetails:
                return BoatFactory.Instance.Create(boatDetails);

            case AirplaneDetails airplaneDetails:
                return AirplaneFactory.Instance.Create(airplaneDetails);

            default:
                throw new ArgumentException(
                    $"Unsupported vehicle details type: {vehicleDetails.GetType().Name}",
                    nameof(vehicleDetails));
        }
    }

}
