using System;
namespace Garage.Models.Vehicles;

public class Airplane : Vehicle
{

    public int Speed
    {
        get;
        set
        {
            field = value >= 0 ? value
                : throw new ArgumentException("Speed must be a non-negative number");
        }
    }



    public Airplane(string brand, string regNumber, string color, int wheelCount, int speed) : base(brand, regNumber, color, wheelCount)
    {
        Speed = speed;
    }


    public override string ToString()
    {
        return $"Airplane: {base.ToString()} | Speed: {Speed}";
    }

    public override Airplane ShallowCopy() => (Airplane)MemberwiseClone();

}
