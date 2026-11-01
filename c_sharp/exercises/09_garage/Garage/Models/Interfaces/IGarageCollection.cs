using System;
using System.Collections.Generic;
using Garage.Models.Enums;
using Garage.Models.Vehicles;

namespace Garage.Models;

public interface IGarageCollection<T>
{
    public CollectionResult Add(T item);
    public bool Remove(string regNumber);
    public IEnumerable<T> Search(Func<T, bool> matches);

    public string ToString();
}


