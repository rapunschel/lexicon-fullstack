using Garage.Models.Vehicles;

namespace Garage.Models.Records;

public record BoatDetails(
    string Registration,
    string Brand,
    string Color,
    int WheelCount,
    FuelType Fuel) : VehicleDetails(Registration, Brand, Color, WheelCount);
