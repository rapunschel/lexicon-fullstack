using System;
using System.Collections.Generic;
using System.Linq;
using Garage.Models.Records;
using Garage.Models.Utilities;
namespace Garage.Views;

public interface IVehicleDetailsFactory
{
    public string Type { get; }
    VehicleDetails Create(VehicleDetails common);
}



public sealed class VehicleDetailsFactory
{
    private IEnumerable<IVehicleDetailsFactory> _factories;

    public VehicleDetailsFactory()
    {
        _factories = [.. AssemblyScanner
            .FindConcreteSubtypesOfT(typeof(IVehicleDetailsFactory))
            .Select(type =>
                (IVehicleDetailsFactory)(Activator.CreateInstance(type)
                    ?? throw new InvalidOperationException(
                        $"Could not create factory {type.Name}.")))];
    }

    public IVehicleDetailsFactory? Find(string type) =>
        _factories.FirstOrDefault(factory =>
            string.Equals(
                factory.Type,
                type.Trim(),
                StringComparison.OrdinalIgnoreCase));
}
public sealed class BusDetailsFactory : IVehicleDetailsFactory
{
    public string Type => "bus";

    public VehicleDetails Create(VehicleDetails common) =>
        new BusDetails(
            common.Registration,
            common.Brand,
            common.Color,
            common.WheelCount,
            Length: InputReader.ReadPositiveDouble("Enter length of bus:"));
}

public sealed class MotorcycleDetailsFactory : IVehicleDetailsFactory
{
    public string Type => "motorcycle";

    public VehicleDetails Create(VehicleDetails common) =>
        new MotorcycleDetails(
            common.Registration,
            common.Brand,
            common.Color,
            common.WheelCount,
            Seats: InputReader.ReadPositiveInt("How many seats:"));
}

public sealed class BoatDetailsFactory : IVehicleDetailsFactory
{
    public string Type => "boat";

    public VehicleDetails Create(VehicleDetails common) =>
        new BoatDetails(
            common.Registration,
            common.Brand,
            common.Color,
            common.WheelCount,
            Fuel: InputReader.ReadFuelTypeEnum("Fuel type (Gasoline / Diesel): "));
}

public sealed class AirplaneDetailsFactory : IVehicleDetailsFactory
{
    public string Type => "airplane";

    public VehicleDetails Create(VehicleDetails common) =>
        new AirplaneDetails(
            common.Registration,
            common.Brand,
            common.Color,
            common.WheelCount,
            Speed: InputReader.ReadPositiveInt("Enter speed:"));
}