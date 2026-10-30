using System;
using Garage.Models.Interfaces;
using Garage.Models.Vehicles;
using Garage.ViewModels;

namespace Garage.Views.UI;

public class ConsoleUI<T> : IUI<T> where T : class, IVehicle
{

    GarageViewModel<T>? _gvm;
    public ConsoleUI(GarageViewModel<T> gvm)
    {
        _gvm = gvm;
    }


    public void CreateGarageView()
    {
        throw new NotImplementedException();
    }

    public void CreateVehicleView()
    {
        throw new NotImplementedException();
    }

    public void InitTestData()
    {
        throw new NotImplementedException();
    }

    public void ParkedVehiclesView()
    {
        throw new NotImplementedException();
    }

    public void ParkVehicleView()
    {
        throw new NotImplementedException();
    }

    public void SearchView()
    {
        throw new NotImplementedException();
    }

    public void UnparkedVehiclesView()
    {
        throw new NotImplementedException();
    }

    public void UnparkVehicleView()
    {
        throw new NotImplementedException();
    }
}
