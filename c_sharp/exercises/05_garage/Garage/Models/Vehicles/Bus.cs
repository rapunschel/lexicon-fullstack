using System;
namespace Garage.Models.Vehicles;

public class Bus : Vehicle
{

    public double Length
    {
        get;
        set
        {
            field = value >= 0 ? value
                : throw new ArgumentException("Length must be a non-negative number");
        }
    }



    public Bus(string brand, string regNumber, string color, int wheelCount, double length) : base(brand, regNumber, color, wheelCount)
    {
        Length = length;
    }


    public override string ToString()
    {
        return $"Bus: {base.ToString()} | Length: {Length}";
    }
}
