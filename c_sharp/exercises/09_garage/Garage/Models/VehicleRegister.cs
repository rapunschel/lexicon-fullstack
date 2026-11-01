using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text.RegularExpressions;
using Garage.Models.Vehicles;

namespace Garage.Models;


public static class VehicleRegister
{

    private static List<IVehicle> _createdVehicles = [];


    public static bool TryGetVehicle<T>(string reg, [NotNullWhen(true)] out T? result) where T : class, IVehicle
    {
        result = null;

        IVehicle? vehicle = _createdVehicles.FirstOrDefault(item =>
               string.Equals(item.RegNumber, reg, StringComparison.OrdinalIgnoreCase));

        if (vehicle is T typed)
        {
            result = typed;
        }
        return result != null;
    }
    public static bool IsValidRegNumber(string reg)
    {
        return Regex.IsMatch(reg.Trim().ToUpperInvariant(), @"\A[A-Z]{3}[0-9]{3}\z");
    }

    public static bool IsUnique(string reg)
    {
        return !_createdVehicles.Any(item => string.Equals(item.RegNumber, reg, StringComparison.OrdinalIgnoreCase));
    }


    public static void Add<T>(T vehicle) where T : class, IVehicle
    {
        _createdVehicles.Add(vehicle);
    }

    public static IEnumerable<T> GetUnparkedVehicles<T>() where T : class, IVehicle
    {
        return _createdVehicles.OfType<T>();
    }

    public static int Count()
    {
        return _createdVehicles.Count();
    }
}
