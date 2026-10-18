using System;
namespace Garage.Models.Vehicles;

public class Motorcycle : Vehicle
{

    public int Seats
    {
        get;
        set
        {
            field = value >= 0 ? value
                : throw new ArgumentException("Seats must be a non-negative number");
        }
    }



    public Motorcycle(string brand, string regNumber, string color, int wheelCount, int seats) : base(brand, regNumber, color, wheelCount)
    {
        Seats = seats;
    }


    public override string ToString()
    {
        return $"Motorcycle: {base.ToString()} | seats: {Seats}";
    }

    public override Motorcycle ShallowCopy() => (Motorcycle)MemberwiseClone();

}
