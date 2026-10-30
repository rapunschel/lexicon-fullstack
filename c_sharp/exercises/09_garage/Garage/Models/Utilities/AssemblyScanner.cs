using System;
using System.Collections.Generic;
using System.Linq;

namespace Garage.Models.Utilities;

public static class AssemblyScanner
{
    // Thanks stackoverflow: https://stackoverflow.com/questions/857705/get-all-derived-types-of-a-type
    public static IEnumerable<Type> FindConcreteSubtypesOfT(Type type)
    {
        Type baseType = type;

        return AppDomain.CurrentDomain.GetAssemblies()
            .SelectMany(assembly => assembly.GetTypes())
            .Where(type =>
                !type.IsAbstract &&
                !type.IsInterface &&
                baseType.IsAssignableFrom(type));
    }
}
