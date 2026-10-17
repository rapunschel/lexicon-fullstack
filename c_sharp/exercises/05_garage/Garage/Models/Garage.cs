using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Garage.Models.Vehicles;

namespace Garage.Models;

public class Garage<T> where T : Vehicle
{
    private T[] _storage;
    private int _pointer = 0;

    public Garage(T[] items)
    {
        _storage = [.. items];
    }

    public Garage(int capacity)
    {
        _storage = new T[capacity];
    }


    public IEnumerable<T> Search(Func<T, bool> matches)
    {
        return _storage.OfType<T>().Where(matches); // TODO return copy of storage and copies of each object
    }

    public bool Add(T item)
    {
        if (_pointer >= _storage.Length) return false;

        for (int i = 0; i < _storage.Length; i++)
        {
            if (EqualityComparer<T>.Default.Equals(_storage[i], item))
            {
                return false;
            }

        }
        _storage[_pointer++] = item;
        return true;
    }

    public bool Remove(T item)
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
                    _storage[i] = default;
                }
            }
            else if (EqualityComparer<T>.Default.Equals(_storage[i], item))
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


