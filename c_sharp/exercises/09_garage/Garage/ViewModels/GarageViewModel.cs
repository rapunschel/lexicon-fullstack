using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Garage.Models;
using Garage.Models.Data;
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

    public bool Search(VehicleDetails details, out IEnumerable<T> vehicles)
    {
        vehicles = _collection.Search(details.Matches);
        return true;
    }

    public bool RemoveParkedVehicle(string registration)
    {
        return _collection.Remove(registration);
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

