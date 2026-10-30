namespace Garage.Models.Vehicles;

public interface IVehicle
{
    string RegNumber { get; }
    string Color { get; set; }
    string Brand { get; set; }
    int WheelCount { get; set; }

    IVehicle ShallowCopy();
    string ToString();
}
