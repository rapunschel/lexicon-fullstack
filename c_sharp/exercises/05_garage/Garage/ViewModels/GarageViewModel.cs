using System;
using Garage.Models;
using Garage.Models.Records;
using Garage.Models.Vehicles;

namespace Garage.ViewModels;

public class GarageViewModel
{

    GarageCollection collection = default!;

    public bool CreateGarageCollection(int capacity)
    {
        try
        {
            collection = new GarageCollection(capacity);
            return true;
        }
        catch
        {
            return false;
        }
    }

    public bool AddVehicle(VehicleDetails details, VehicleType type, out string? error)
    {
        error = "";
        try
        {
            if (collection == null)
            {
                error = "Garage doesn't exist.";
                return false;
            }

            switch (type)
            {
                case VehicleType.Car:
                    collection.Add(new Car(brand: details.Brand, regNumber: details.Registration,
                        color: details.Color, wheelCount: details.WheelCount, engineCount: ((CarDetails)details).EngineCount));
                    break;
                case VehicleType.Bus:
                    collection.Add(new Bus(brand: details.Brand, regNumber: details.Registration,
                        color: details.Color, wheelCount: details.WheelCount, length: ((BusDetails)details).Length));

                    break;
                case VehicleType.Motorcycle:
                    collection.Add(new Motorcycle(brand: details.Brand, regNumber: details.Registration,
                                     color: details.Color, wheelCount: details.WheelCount, ((MotorcycleDetails)details).Seats));
                    break;
                case VehicleType.Boat:
                    details = (BoatDetails)details;
                    collection.Add(new Boat(brand: details.Brand, regNumber: details.Registration,
                 color: details.Color, wheelCount: details.WheelCount, ((BoatDetails)details).Fuel));

                    break;
                case VehicleType.Airplane:
                    collection.Add(new Airplane(brand: details.Brand, regNumber: details.Registration,
                 color: details.Color, wheelCount: details.WheelCount, ((AirplaneDetails)details).Speed));
                    break;
                default:
                    collection.Add(new Vehicle(brand: details.Brand, regNumber: details.Registration,
                        color: details.Color, wheelCount: details.WheelCount));
                    break;

            }

            return true;
        }
        catch (Exception ex)
        {
            error = ex.Message;
            return false;
        }

    }

    public string GetGarageInfo()
    {
        return collection.ToString();
    }
}

