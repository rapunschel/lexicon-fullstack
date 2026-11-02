using System;
using Garage.Models.Vehicles;

namespace Garage.Models.Interfaces;

public interface IUI
{
    public string Name { get; }
    public void CreateGarageView();
    public void CreateVehicleView();
    public void ParkVehicleView();
    public void UnparkVehicleView();
    public void SearchView();
    public void ParkedVehiclesView();
    public void UnparkedVehiclesView();
    public void InitTestData();
    public string GetGarageStorageType();
}
