using System;
using Garage.Models.Vehicles;
using System.Text;
using System.Collections.Generic;

namespace Garage.Utils;

public static class TableFormatter<T> where T : IVehicle
{
    public static StringBuilder FormatTable(IEnumerable<T> vehicles)
    {
        StringBuilder sb = new();
        const int offset = -20;
        sb.Append(new string('=', Math.Abs(offset * 6)));
        sb.Append("\n");
        sb.Append($"{"Type",offset} {"Brand",offset} {"Registration",offset} {"Color",offset} {"Wheels",offset} {"Details",offset}\n");
        sb.Append(new string('=', Math.Abs(offset * 6)));
        sb.Append("\n");
        foreach (var item in vehicles)
        {
            string typeName = item switch
            {
                Airplane _ => nameof(Airplane),
                Boat _ => nameof(Boat),
                Bus _ => nameof(Bus),
                Car _ => nameof(Car),
                Motorcycle _ => nameof(Motorcycle),
                IVehicle _ => nameof(IVehicle),
                _ => "Unknown"
            };
            string details = item switch
            {
                Airplane airplane => $"Speed: {airplane.Speed}",
                Boat boat => $"Fuel: {boat.Fuel}",
                Bus bus => $"Length: {bus.Length}",
                Car car => $"EngineCount: {car.EngineCount}",
                Motorcycle motorcycle => $"Seats: {motorcycle.Seats}",
                IVehicle _ => "",
                _ => ""
            };
            sb.Append($"{typeName,offset} {item.Brand,offset} {item.RegNumber,offset} {item.Color,offset} {item.WheelCount,offset} {details,offset}\n");

        }

        return sb;
    }
}
