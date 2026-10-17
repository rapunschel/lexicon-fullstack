namespace Garage.Models.Vehicles;

public class Boat : Vehicle
{

    public FuelType Fuel
    {
        get;
        set;
    }

    public Boat(string brand, string regNumber, string color, int wheelCount, FuelType fuel) : base(brand, regNumber, color, wheelCount)
    {
        Fuel = fuel;
    }


    public override string ToString()
    {
        return $"Boat: {base.ToString()} | Fuel: {Fuel}";
    }
}