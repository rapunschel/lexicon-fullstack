using System;
using System.Collections.Generic;
using Garage.Models.Vehicles;

namespace Garage.Models;

public interface IGarageCollection<T>
{
    public bool Add(T item);
    public bool Remove(string regNumber);
    public IEnumerable<T> Search(Func<T, bool> matches);

    public string ToString();
}


