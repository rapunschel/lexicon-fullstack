using System;
using System.Text;
using Garage.Models;
using Garage.Models.Records;
using Garage.Models.Vehicles;

namespace Garage.ViewModels;

public class GarageViewModel
{

    private GarageCollection<Vehicle> _collection = default!;
    private Vehicle[] _createdVehicles = new Vehicle[500]; // Too lazy to manually expand will just refactor to list in the future anyway
    private int _pointer = 0;
    public bool CreateGarageCollection(int capacity)
    {
        try
        {
            _collection = new GarageCollection<Vehicle>(capacity);
            return true;
        }
        catch
        {
            return false;
        }
    }

    public bool ParkVehicle(string registration)
    {

        foreach (var item in _createdVehicles)
        {
            if (string.Equals(item.RegNumber, registration, StringComparison.OrdinalIgnoreCase))
            {
                if (_collection.Add(item))
                {
                    return true;
                }
                break; // Can only ever be 1 registration in _createdVehicles
            }
        }

        return false;
    }

    public bool RemoveParkedVehicle(string registration)
    {
        return _collection.Remove(registration);
    }


    public bool CreateVehicle(VehicleDetails details, VehicleType type, out string? error)
    {
        error = "";
        Vehicle vehicle;
        try
        {
            switch (type)
            {
                case VehicleType.Car:
                    vehicle = new Car(brand: details.Brand, regNumber: details.Registration,
                        color: details.Color, wheelCount: details.WheelCount, engineCount: ((CarDetails)details).EngineCount);
                    break;
                case VehicleType.Bus:
                    vehicle = new Bus(brand: details.Brand, regNumber: details.Registration,
                        color: details.Color, wheelCount: details.WheelCount, length: ((BusDetails)details).Length);
                    break;
                case VehicleType.Motorcycle:
                    vehicle = new Motorcycle(brand: details.Brand, regNumber: details.Registration,
                                     color: details.Color, wheelCount: details.WheelCount, ((MotorcycleDetails)details).Seats);
                    break;
                case VehicleType.Boat:
                    vehicle = new Boat(brand: details.Brand, regNumber: details.Registration,
                 color: details.Color, wheelCount: details.WheelCount, ((BoatDetails)details).Fuel);

                    break;
                case VehicleType.Airplane:
                    vehicle = new Airplane(brand: details.Brand, regNumber: details.Registration,
                 color: details.Color, wheelCount: details.WheelCount, ((AirplaneDetails)details).Speed);
                    break;
                default:
                    vehicle = new Vehicle(brand: details.Brand, regNumber: details.Registration,
                        color: details.Color, wheelCount: details.WheelCount);
                    break;

            }
            _createdVehicles[_pointer++] = vehicle;
            return true;
        }
        catch (Exception ex)
        {
            error = ex.Message;
            return false;
        }

    }

    public string GetParkedCarsInfo()
    {
        return _collection.ToString();
    }
    public string GetNotParkedVehicles()
    {
        StringBuilder sb = new();

        foreach (Vehicle item in _createdVehicles)
        {
            if (item == null) continue;
            sb.Append($"{item.ToString()}\n");
        }
        return sb.ToString();
    }

}

