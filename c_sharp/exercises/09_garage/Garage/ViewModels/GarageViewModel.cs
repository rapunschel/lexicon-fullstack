using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Garage.Models;
using Garage.Models.Data;
using Garage.Models.Enums;
using Garage.Models.Interfaces;
using Garage.Models.Records;
using Garage.Models.Vehicles;
using Garage.Utils;

namespace Garage.ViewModels;

public class GarageViewModel<T> where T : class, IVehicle
{

    private IGarageCollection<T> _collection;
    private IEnumerable<T>? _testData;
    private IVehicleFactory<T> _factory;
    public GarageViewModel(IVehicleFactory<T> factory, int capacity)
    {
        _collection = new GarageCollection<T>(capacity);
        _factory = factory;
    }


    public bool CreateNewGarageCollection(int capacity)
    {
        _collection = new GarageCollection<T>(capacity);
        return true;
    }

    public ParkResult ParkVehicle(string registration)
    {

        if (VehicleRegister.TryGetVehicle(registration, out T? vehicle))
        {
            var result = _collection.Add(vehicle);
            return result switch
            {
                CollectionResult.Added => ParkResult.Parked,
                CollectionResult.AtCapacity => ParkResult.AtCapacity,
                CollectionResult.DuplicateRegistration => ParkResult.AlreadyParked,
                _ => throw new InvalidOperationException($"Unexpected operation from Add: {result}")
            };
        }
        return ParkResult.VehicleNotCreated;
    }

    public bool Search(VehicleDetails details, out IEnumerable<T> vehicles)
    {
        vehicles = _collection.Search(details.Matches);
        return true;
    }

    public ParkResult RemoveParkedVehicle(string registration)
    {
        var result = _collection.Remove(registration);
        return result switch
        {
            CollectionResult.Removed => ParkResult.Unparked,
            CollectionResult.NotFound => ParkResult.NotParked,
            _ => throw new InvalidOperationException($"Unexpected operation: {result}")
        };
    }


    public void CreateVehicle(VehicleDetails details)
    {
        VehicleRegister.Add(_factory.Create(details));
    }

    public string GetParkedCarsInfo()
    {
        return _collection.ToString();
    }
    public string GetNotParkedVehicles()
    {
        StringBuilder sb = TableFormatter<T>.FormatTable(VehicleRegister.GetUnparkedVehicles<T>());
        return sb.ToString();
    }

    public void InitTestData()
    {
        if (_testData is null)
            _testData = TestData<T>.GetTestData();

        for (int i = 0; i * 4 < _testData.Count(); i++)
        {
            int index = i * 3;
            _collection.Add(_testData.ElementAt(index));
        }
    }
}

