using System;
using System.Collections.Generic;
using System.Text;
using Garage.Models;
using Garage.Models.Records;
using Garage.Models.Vehicles;
using Garage.Utils;

namespace Garage.ViewModels;

public class GarageViewModel
{
    Vehicle[] testData =
    {
    new Airplane("Boeing", "AIR101", "White", 6, 850),
    new Airplane("Airbus", "AIR102", "Blue", 6, 900),
    new Airplane("Cessna", "AIR103", "White", 3, 300),
    new Airplane("Embraer", "AIR104", "Red", 6, 870),
    new Airplane("Bombardier", "AIR105", "Blue", 8, 920),

    new Boat("Yamaha", "BOA101", "White", 0, FuelType.Gasoline),
    new Boat("Bayliner", "BOA102", "Blue", 0, FuelType.Diesel),
    new Boat("Sea Ray", "BOA103", "Red", 0, FuelType.Gasoline),
    new Boat("Jeanneau", "BOA104", "White", 0, FuelType.Diesel),
    new Boat("Boston Whaler", "BOA105", "Blue", 0, FuelType.Gasoline),

    new Bus("Volvo", "BUS101", "Yellow", 6, 12.0),
    new Bus("Mercedes-Benz", "BUS102", "Blue", 6, 10.5),
    new Bus("Scania", "BUS103", "White", 8, 14.0),
    new Bus("MAN", "BUS104", "Red", 6, 11.2),
    new Bus("Iveco", "BUS105", "Yellow", 4, 9.8),

    new Car("Toyota", "CAR101", "Red", 4, 1),
    new Car("Volvo", "CAR102", "Blue", 4, 1),
    new Car("Ford", "CAR103", "White", 4, 1),
    new Car("BMW", "CAR104", "Black", 4, 1),
    new Car("Honda", "CAR105", "Red", 4, 2),

    new Motorcycle("Honda", "MOT101", "Black", 2, 2),
    new Motorcycle("Yamaha", "MOT102", "Blue", 2, 2),
    new Motorcycle("Ducati", "MOT103", "Red", 2, 1),
    new Motorcycle("Kawasaki", "MOT104", "Green", 2, 2),
    new Motorcycle("Suzuki", "MOT105", "Black", 2, 1)
};
    private GarageCollection _collection;
    private List<Vehicle> _createdVehicles = [];
    private bool _testDataLoaded = false;


    public GarageViewModel(int capacity)
    {
        _collection = new GarageCollection(capacity);
    }


    public bool CreateNewGarageCollection(int capacity)
    {
        _collection = new GarageCollection(capacity);
        return true;
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

    public bool Search(VehicleDetails details, out List<Vehicle> vehicles)
    {
        vehicles = _collection.Search(details.Matches);
        return true;
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
            _createdVehicles.Add(vehicle);
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
        StringBuilder sb = TableFormatter.FormatTable(_createdVehicles);
        return sb.ToString();
    }

    public void InitTestData()
    {
        if (!_testDataLoaded)
        {
            _testDataLoaded = true;
            foreach (var v in testData)
            {
                _createdVehicles.Add(v);
            }
        }

        for (int i = 0; i * 4 < testData.Length; i++)
        {
            _collection.Add(testData[i * 3]);
        }
    }
}

