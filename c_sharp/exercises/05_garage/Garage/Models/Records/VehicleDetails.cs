using System;
using Garage.Models.Vehicles;

namespace Garage.Models.Records;


public record VehicleDetails
{
    public string Registration { get; set; } = "";
    public string Brand { get; set; } = "";
    public string Color { get; set; } = "";
    public int WheelCount { get; set; } = -1;

    public VehicleDetails(string Registration, string Brand, string Color, int WheelCount)
    {
        this.Registration = Registration;
        this.Brand = Brand;
        this.Color = Color;
        this.WheelCount = WheelCount;
    }

    public VehicleDetails()
    {

    }
    public virtual bool Matches(Vehicle item)
    {
        return (string.IsNullOrEmpty(Brand) || string.Equals(Brand, item.Brand, StringComparison.OrdinalIgnoreCase)) &&
               (string.IsNullOrEmpty(Color) || string.Equals(Color, item.Color, StringComparison.OrdinalIgnoreCase)) &&
               (string.IsNullOrEmpty(Registration) || string.Equals(Registration, item.RegNumber, StringComparison.OrdinalIgnoreCase)) &&
               (WheelCount == -1 || WheelCount == item.WheelCount);

    }
}
