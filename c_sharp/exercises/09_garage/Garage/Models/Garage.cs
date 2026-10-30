using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Text;
using Garage.Models.Vehicles;
using Garage.Utils;

namespace Garage.Models;

public class GarageCollection<T> : IGarageCollection<T> where T : Vehicle
{
    private readonly List<T> _storage = new List<T>();
    private int _capacity = 0;

    public GarageCollection(List<T> items)
    {
        foreach (var item in items)
        {
            _storage.Add((T)item.ShallowCopy());
        }
    }

    public GarageCollection(int capacity)
    {
        _capacity = capacity;
    }


    public IEnumerable<T> Search(Func<T, bool> matches)
    {
        return (IEnumerable<T>)_storage.FindAll(item => matches(item)).ConvertAll(item => item.ShallowCopy());
    }

    public bool Add(T item)
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
            _storage.Remove((T)vehicle);
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
        string emptySlotsMsg = $"empty parking slots: {_capacity - _storage.Count}";
        if (_storage.Count == 0)
        {
            return emptySlotsMsg;
        }
        StringBuilder sb = TableFormatter.FormatTable(_storage);
        sb.Append(emptySlotsMsg);

        return sb.ToString();
    }
}


