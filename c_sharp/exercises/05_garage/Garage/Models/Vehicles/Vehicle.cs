using System;
namespace Garage.Models.Vehicles;

public abstract class Vehicle
{
    string RegNumber
    {
        get;
        set
        {
            field = string.IsNullOrEmpty(value) ? throw new ArgumentException("Registration number cant be empty")
                : value;
        }
    }
    string Color
    {
        get;
        set
        {
            field = string.IsNullOrEmpty(value) ? throw new ArgumentException("Color cant be empty")
                : value;
        }
    }
    string Brand
    {
        get;
        set
        {
            field = string.IsNullOrEmpty(value) ? throw new ArgumentException("brand cant be empty")
                : value;
        }
    }

    int WheelCount
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
        RegNumber = regNumber;
        Color = color;
        WheelCount = wheelCount;
        Brand = brand;
    }

    public override string ToString()
    {
        return $"Brand: {Brand} | Reg number: {RegNumber} | Color: {Color} | Wheels: {WheelCount}";
    }
}
