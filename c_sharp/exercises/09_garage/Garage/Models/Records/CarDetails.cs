namespace Garage.Models.Records;

public record CarDetails(
    string Registration,
    string Brand,
    string Color,
    int WheelCount,
    int EngineCount) : VehicleDetails(Registration, Brand, Color, WheelCount);
