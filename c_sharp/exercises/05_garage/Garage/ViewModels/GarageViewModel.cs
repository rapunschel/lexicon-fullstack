using System;
using Garage.Models;
using Garage.Models.Vehicles;

namespace Garage.ViewModels;

public class GarageViewModel
{

    GarageCollection collection = default!;

    public bool CreateGarageCollection(int capacity)
    {
        try
        {
            collection = new GarageCollection(capacity);
            return true;
        }
        catch
        {
            return false;
        }
    }

    public bool AddVehicle(string brand, string regNumber, string color, int wheelCount, out string? error)
    {
        try
        {
            if (collection == null)
            {
                error = "Garage doesn't exist.";
                return false;
            }
            error = "";
            collection.Add(new Vehicle(brand: brand, regNumber: regNumber, color: color, wheelCount: wheelCount));
            return true;
        }
        catch (Exception ex)
        {
            error = ex.Message;
            return false;
        }

    }

    public string GetGarageInfo()
    {
        return collection.ToString();
    }
}

