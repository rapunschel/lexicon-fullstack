using System;
using System.Reflection;
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


    public void InitTestData()
    {
        Vehicle[] vehicles =
            {
                new Airplane("Boeing", "AIR-101", "White", 6, 850),
                new Airplane("Airbus", "AIR-102", "Blue", 6, 900),
                new Airplane("Cessna", "AIR-103", "White", 3, 300),
                new Airplane("Embraer", "AIR-104", "Red", 6, 870),
                new Airplane("Bombardier", "AIR-105", "Blue", 8, 920),

                new Boat("Yamaha", "BOAT-201", "White", 0, FuelType.Gasoline),
                new Boat("Bayliner", "BOAT-202", "Blue", 0, FuelType.Diesel),
                new Boat("Sea Ray", "BOAT-203", "Red", 0, FuelType.Gasoline),
                new Boat("Jeanneau", "BOAT-204", "White", 0, FuelType.Diesel),
                new Boat("Boston Whaler", "BOAT-205", "Blue", 0, FuelType.Gasoline),

                new Bus("Volvo", "BUS-301", "Yellow", 6, 12.0),
                new Bus("Mercedes-Benz", "BUS-302", "Blue", 6, 10.5),
                new Bus("Scania", "BUS-303", "White", 8, 14.0),
                new Bus("MAN", "BUS-304", "Red", 6, 11.2),
                new Bus("Iveco", "BUS-305", "Yellow", 4, 9.8),

                new Car("Toyota", "CAR-401", "Red", 4, 1),
                new Car("Volvo", "CAR-402", "Blue", 4, 1),
                new Car("Ford", "CAR-403", "White", 4, 1),
                new Car("BMW", "CAR-404", "Black", 4, 1),
                new Car("Honda", "CAR-405", "Red", 4, 2),

                new Motorcycle("Honda", "MOTO-501", "Black", 2, 2),
                new Motorcycle("Yamaha", "MOTO-502", "Blue", 2, 2),
                new Motorcycle("Ducati", "MOTO-503", "Red", 2, 1),
                new Motorcycle("Kawasaki", "MOTO-504", "Green", 2, 2),
                new Motorcycle("Suzuki", "MOTO-505", "Black", 2, 1)
            };
        foreach (var v in vehicles)
        {
            _createdVehicles[_pointer++] = v;
        }
    }

}

