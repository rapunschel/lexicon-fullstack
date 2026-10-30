using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
namespace Garage.Models.Vehicles;

public abstract class Vehicle : IVehicle
{
    private static HashSet<string> _set = [];


    public string RegNumber
    {
        get;
        private set
        {
            string normalized = value.Trim().ToUpperInvariant();

            if (!Regex.IsMatch(normalized, @"\A[A-Z]{3}[0-9]{3}\z"))
                throw new ArgumentException(
                    "Registration number must be 3 letters followed by 3 digits. Example: abc123");

            if (_set.Contains(normalized))
                throw new ArgumentException("Reg number is not unique");

            field = normalized;
            _set.Add(normalized);
        }
    }
    public string Color
    {
        get;
        set
        {
            field = string.IsNullOrEmpty(value) ? throw new ArgumentException("Color cant be empty")
                : value;
        }
    }
    public string Brand
    {
        get;
        set
        {
            field = string.IsNullOrEmpty(value) ? throw new ArgumentException("brand cant be empty")
                : value;
        }
    }

    public int WheelCount
    {
        get;
        set
        {
            field = value >= 0 ? value
                : throw new ArgumentException("Wheel count must be a non-negative number");
        }
    }

    public Vehicle(string brand, string regNumber, string color, int wheelCount)
    {
        Color = color;
        WheelCount = wheelCount;
        Brand = brand;
        RegNumber = regNumber;
    }

    public override string ToString()
    {
        return $"Brand: {Brand} | Reg number: {RegNumber} | Color: {Color} | Wheels: {WheelCount}";
    }

    public virtual IVehicle ShallowCopy()
    {
        return (IVehicle)MemberwiseClone();
    }
}
