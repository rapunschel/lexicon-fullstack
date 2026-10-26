using System;
using Garage.Models.Vehicles;
using System.Text;
using System.Collections.Generic;

namespace Garage.Utils;

public static class TableFormatter
{
    public static StringBuilder FormatTable(List<Vehicle> vehicles)
    {
        StringBuilder sb = new();
        const int offset = -15;
        sb.Append($"{"Type",offset} {"Brand",offset} {"Registration",offset} {"Color",offset} {"Wheels",offset} {"Details",offset}\n");

        foreach (var item in vehicles)
        {
            string typeName = item switch
            {
                Airplane _ => nameof(Airplane),
                Boat _ => nameof(Boat),
                Bus _ => nameof(Bus),
                Car _ => nameof(Car),
                Motorcycle _ => nameof(Motorcycle),
                Vehicle _ => nameof(Vehicle),
                _ => "Unknown"
            };
            string details = item switch
            {
                Airplane airplane => $"Speed: {airplane.Speed}",
                Boat boat => $"Fuel: {boat.Fuel}",
                Bus bus => $"Length: {bus.Length}",
                Car car => $"EngineCount: {car.EngineCount}",
                Motorcycle motorcycle => $"Seats: {motorcycle.Seats}",
                Vehicle _ => "",
                _ => ""
            };
            sb.Append($"{typeName,offset} {item.Brand,offset} {item.RegNumber,offset} {item.Color,offset} {item.WheelCount,offset} {details,offset}\n");

        }

        return sb;
    }
}
