using System;
using System.Text;
using Garage.Models.Vehicles;

namespace Garage.Models;

public class GarageCollection
{
    private Vehicle[] _storage;
    private int _pointer = 0;

    public GarageCollection(Vehicle[] items)
    {
        _storage = [.. items];
    }

    public GarageCollection(int capacity)
    {
        _storage = new Vehicle[capacity];
    }


    public Vehicle[] Search(Func<Vehicle, bool> matches)
    {
        Vehicle[] result = new Vehicle[_storage.Length];
        int count = 0;
        for (int i = 0; i < _storage.Length; i++)
        {
            if (_storage[i] == null) continue;
            if (matches(_storage[i]))
            {
                result[count++] = _storage[i];
            }
        }
        Array.Resize(ref result, count);
        return result;
    }

    public bool Add(Vehicle item)
    {
        if (_pointer >= _storage.Length) return false;

        // Ensure no clones can be added
        for (int i = 0; i < _storage.Length; i++)
        {
            if (_storage[i] == null) continue;
            if (string.Equals(_storage[i].RegNumber, item.RegNumber, StringComparison.OrdinalIgnoreCase)) return false;
        }
        _storage[_pointer++] = item;
        return true;
    }

    public bool Remove(string regNumber)
    {
        bool result = false;
        for (int i = 0; i < _storage.Length; i++)
        {
            if (result)
            {
                // Shift everything to the left
                _storage[i - 1] = _storage[i];

                // If last is shifted, set to default
                if (i == _storage.Length - 1)
                {
                    _storage[i] = default!;
                }
            }
            else if (_storage[i] != null && string.Equals(_storage[i].RegNumber, regNumber, StringComparison.OrdinalIgnoreCase))
            {
                result = true;
                _pointer--;
            }
        }

        return result;
    }

    public override string ToString()
    {
        StringBuilder sb = new();
        for (int i = 0; i < _storage.Length; i++)
        {
            if (_storage[i] == null)
            {
                sb.Append("empty parking slot \n");
                continue;
            }
            sb.Append($"{_storage[i].ToString()}\n");
        }

        return sb.ToString();
    }
}


