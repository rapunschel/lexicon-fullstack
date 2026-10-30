using Garage.Models.Records;
using Garage.Models.Vehicles;

namespace Garage.Models.Interfaces;

public interface IVehicleFactory<T> where T : IVehicle
{
    public T Create(VehicleDetails details);
}
