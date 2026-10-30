using System;
using System.Collections.Generic;

namespace Garage.Models;

public interface IGarageCollection<T>
{
    public bool Add(T item);
    public bool Remove(string regNumber);
    public IEnumerable<T> Search(Func<T, bool> matche);

    public string ToString();
}


