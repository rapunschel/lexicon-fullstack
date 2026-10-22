using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Text;
using Garage.Models.Vehicles;

namespace Garage.Models;

public class GarageCollection
{
    private List<Vehicle> _storage = new List<Vehicle>();
    private int _capacity = 0;

    public GarageCollection(List<Vehicle> items)
    {
        foreach (var item in items)
        {
            _storage.Add(item.ShallowCopy());
        }
    }

    public GarageCollection(int capacity)
    {
        _capacity = capacity;
    }


    public List<Vehicle> Search(Func<Vehicle, bool> matches)
    {
        return _storage.FindAll(item => matches(item)).ConvertAll(item => item.ShallowCopy());
    }

    public bool Add(Vehicle item)
    {
        if (_storage.Count >= _capacity) return false;

        if (FindVehicle(item.RegNumber, out Vehicle? _))
        {
            return false;
        }

        _storage.Add(item);
        return true;
    }

    public bool Remove(string regNumber)
    {
        if (FindVehicle(regNumber, out Vehicle? vehicle))
        {
            _storage.Remove(vehicle);
            return true;
        }
        return false;
    }
    private bool FindVehicle(string regNumber, [NotNullWhen(true)] out Vehicle? vehicle)
    {
        vehicle = _storage.Find(vehicle => string.Equals(vehicle.RegNumber, regNumber, StringComparison.OrdinalIgnoreCase));
        if (vehicle == null) return false;
        return true;
    }

    public override string ToString()
    {
        StringBuilder sb = new();
        foreach (var item in _storage)
        {
            sb.Append($"{item.ToString()}\n");

        }
        for (int i = 0; i < _capacity - _storage.Count; i++)
        {
            sb.Append("empty parking slot \n");
        }

        return sb.ToString();
    }
}


