using System;
using Garage.Models.Interfaces;
using Garage.Models.Records;
using Garage.Models.Vehicles;

namespace Garage.Models.Factories;

public sealed class VehicleFactory : IVehicleFactory<IVehicle>
{
    public IVehicle Create(VehicleDetails vehicleDetails)
    {

        switch (vehicleDetails)
        {
            case CarDetails carDetails:
                return new Car(
                    brand: carDetails.Brand,
                    regNumber: carDetails.Registration,
                    color: carDetails.Color,
                    wheelCount: carDetails.WheelCount,
                    engineCount: carDetails.EngineCount);

            case BusDetails busDetails:
                return new Bus(
                    brand: busDetails.Brand,
                    regNumber: busDetails.Registration,
                    color: busDetails.Color,
                    wheelCount: busDetails.WheelCount,
                    length: busDetails.Length);

            case MotorcycleDetails motorcycleDetails:
                return new Motorcycle(
                    brand: motorcycleDetails.Brand,
                    regNumber: motorcycleDetails.Registration,
                    color: motorcycleDetails.Color,
                    wheelCount: motorcycleDetails.WheelCount,
                    motorcycleDetails.Seats);

            case BoatDetails boatDetails:
                return new Boat(
                    brand: boatDetails.Brand,
                    regNumber: boatDetails.Registration,
                    color: boatDetails.Color,
                    wheelCount: boatDetails.WheelCount,
                    boatDetails.Fuel);

            case AirplaneDetails airplaneDetails:
                return new Airplane(
                    brand: airplaneDetails.Brand,
                    regNumber: airplaneDetails.Registration,
                    color: airplaneDetails.Color,
                    wheelCount: airplaneDetails.WheelCount,
                    airplaneDetails.Speed);

            default:
                throw new ArgumentException(
                    $"Unsupported vehicle details type: {vehicleDetails.GetType().Name}",
                    nameof(vehicleDetails));
        }
    }

}
