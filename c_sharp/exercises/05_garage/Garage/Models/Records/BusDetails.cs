namespace Garage.Models.Records;

public record BusDetails(
    string Registration,
    string Brand,
    string Color,
    int WheelCount,
    double Length) : VehicleDetails(Registration, Brand, Color, WheelCount);
