using System;
namespace Garage.Models.Vehicles;

public class Car : Vehicle
{

    public int EngineCount
    {
        get;
        set
        {
            field = value >= 0 ? value
                : throw new ArgumentException("EngineCount must be a non-negative number");
        }
    }

    public Car(string brand, string regNumber, string color, int wheelCount) : base(brand, regNumber, color, wheelCount)
    {
    }


    public override string ToString()
    {
        return $"Car: {base.ToString()} | EngineCount: {EngineCount}";
    }
}
