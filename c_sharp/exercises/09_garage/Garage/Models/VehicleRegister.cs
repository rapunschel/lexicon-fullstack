using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace Garage.Models;


public class VehicleRegister
{
    private static HashSet<string> _set = [];


    public static bool IsValidRegNumber(string reg)
    {
        return Regex.IsMatch(reg.Trim().ToUpperInvariant(), @"\A[A-Z]{3}[0-9]{3}\z");
    }

    public static bool IsUnique(string reg)
    {
        return !_set.Contains(reg.Trim().ToUpperInvariant());
    }

    public static void Add(string reg)
    {
        _set.Add(reg.Trim().ToUpperInvariant());
    }
}
