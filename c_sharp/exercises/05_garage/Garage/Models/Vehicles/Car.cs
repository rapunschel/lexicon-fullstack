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

    public Car(string brand, string regNumber, string color, int wheelCount, int engineCount) : base(brand, regNumber, color, wheelCount)
    {
        EngineCount = engineCount;
    }


    public override string ToString()
    {
        return $"Car: {base.ToString()} | EngineCount: {EngineCount}";
    }
    public override Car ShallowCopy() => (Car)MemberwiseClone();

}
