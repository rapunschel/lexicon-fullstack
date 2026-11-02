using System;
using Garage.Models.Vehicles;
using Garage.Models.Interfaces;
using Garage.Models.Records;
namespace Garage.Models.Factories;



public sealed class AirplaneFactory : IVehicleFactory<Airplane>
{
    public static AirplaneFactory Instance { get; } = new();

    private AirplaneFactory() { }

    public Airplane Create(VehicleDetails details)
    {
        if (details is not AirplaneDetails airplaneDetails)
            throw new ArgumentException("AirplaneFactory requires AirplaneDetails.", nameof(details));

        return new Airplane(
            brand: airplaneDetails.Brand,
            regNumber: airplaneDetails.Registration,
            color: airplaneDetails.Color,
            wheelCount: airplaneDetails.WheelCount,
            speed: airplaneDetails.Speed);
    }
}
