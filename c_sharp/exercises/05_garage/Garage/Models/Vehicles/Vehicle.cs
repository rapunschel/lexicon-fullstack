using System;
using System.Collections.Generic;
namespace Garage.Models.Vehicles;

public abstract class Vehicle
{
    private static HashSet<string> set = new HashSet<string>();

    public string RegNumber
    {
        get;
        private set
        {
            value = value.ToUpper();
            if (set.Contains(value)) throw new ArgumentException("Reg number is not unique");

            field = string.IsNullOrEmpty(value) ? throw new ArgumentException("Registration number cant be empty")
                : value;

            set.Add(value);
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
}
