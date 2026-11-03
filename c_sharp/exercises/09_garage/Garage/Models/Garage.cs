using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Text;
using Garage.Models.Enums;
using Garage.Models.Vehicles;
using Garage.Utils;

namespace Garage.Models;

public class GarageCollection<T> : IEnumerable<T>, IGarageCollection<T> where T : IVehicle
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
        return _storage.FindAll(item => matches(item)).ConvertAll(item => (T)item.ShallowCopy());
    }

    public CollectionResult Add(T item)
    {
        if (FindVehicle(item.RegNumber, out IVehicle? _))
            return CollectionResult.DuplicateRegistration;

        if (_storage.Count >= _capacity) return CollectionResult.AtCapacity;


        _storage.Add(item);
        return CollectionResult.Added;
    }

    public CollectionResult Remove(string regNumber)
    {
        if (FindVehicle(regNumber, out IVehicle? vehicle))
        {
            _storage.Remove((T)vehicle);
            return CollectionResult.Removed;
        }
        return CollectionResult.NotFound;
    }
    private bool FindVehicle(string regNumber, [NotNullWhen(true)] out IVehicle? vehicle)
    {
        vehicle = _storage.Find(vehicle => string.Equals(vehicle.RegNumber, regNumber, StringComparison.OrdinalIgnoreCase));
        if (vehicle == null) return false;
        return true;
    }

    public override string ToString()
    {
        string emptySlotsMsg = $"empty parking slots: {_capacity - _storage.Count}";

        StringBuilder sb = TableFormatter<T>.FormatTable(_storage);
        sb.Append(emptySlotsMsg);

        return sb.ToString();
    }

    public IEnumerator<T> GetEnumerator()
    {
        return _storage.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}


