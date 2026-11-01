using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
    private List<T> _createdVehicles = [];
    private bool _testDataLoaded = false;
    private IEnumerable<T> _testData = TestData<T>.GetTestData();
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
        T? vehicle = _createdVehicles.FirstOrDefault(item =>
            string.Equals(item.RegNumber, registration, StringComparison.OrdinalIgnoreCase));

        if (vehicle == null) return ParkResult.VehicleNotCreated;

        var result = _collection.Add(vehicle);
        return result switch
        {
            CollectionResult.Added => ParkResult.Parked,
            CollectionResult.AtCapacity => ParkResult.AtCapacity,
            CollectionResult.DuplicateRegistration => ParkResult.AlreadyParked,
            _ => throw new InvalidOperationException($"Unexpected operation from Add: {result}")
        };
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


    public bool CreateVehicle(VehicleDetails details)
    {

        _createdVehicles.Add(_factory.Create(details));
        return true;
    }



    public string GetParkedCarsInfo()
    {
        return _collection.ToString();
    }
    public string GetNotParkedVehicles()
    {
        StringBuilder sb = TableFormatter<T>.FormatTable(_createdVehicles);
        return sb.ToString();
    }

    public void InitTestData()
    {
        if (!_testDataLoaded)
        {
            _testDataLoaded = true;
            foreach (var v in _testData)
            {
                _createdVehicles.Add(v);
            }
        }

        for (int i = 0; i * 4 < _testData.Count(); i++)
        {
            _collection.Add(_testData.ElementAt(i * 3));
        }
    }
}

