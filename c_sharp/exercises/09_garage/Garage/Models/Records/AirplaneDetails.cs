namespace Garage.Models.Records;

public record AirplaneDetails(
    string Registration,
    string Brand,
    string Color,
    int WheelCount,
    int Speed) : VehicleDetails(Registration, Brand, Color, WheelCount);
