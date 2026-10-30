using System.Diagnostics.CodeAnalysis;
using Garage.Views;

namespace Garage.ViewModels;

public class VehicleDetailsViewModel
{
    private VehicleDetailsFactory _factory = new();


    public bool GetFactory(string type, [NotNullWhen(true)] out IVehicleDetailsFactory? factory)
    {
        factory = _factory.Find(type);
        return factory != null;
    }
}
