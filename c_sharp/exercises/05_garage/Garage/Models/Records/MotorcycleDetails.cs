namespace Garage.Models.Records;

public record MotorcycleDetails(
    string Registration,
    string Brand,
    string Color,
    int EngineCount,
    int WheelCount,
    int Seats) : VehicleDetails(Registration, Brand, Color, WheelCount);