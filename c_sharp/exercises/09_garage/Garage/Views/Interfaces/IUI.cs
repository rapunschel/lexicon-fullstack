using System;
using Garage.Models.Vehicles;

namespace Garage.Models.Interfaces;

public interface IUI<T>
{
    public void CreateGarageView();
    public void NewGarageView();
    public void CreateVehicleView();
    public void ParkVehicleView();
    public void UnparkVehicleView();
    public void SearchView();
    public void ParkedVehiclesView();
    public void UnparkedVehiclesView();
    public void InitTestData();
}
