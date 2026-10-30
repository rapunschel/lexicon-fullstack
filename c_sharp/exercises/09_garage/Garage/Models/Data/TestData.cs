using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Garage.Models.Vehicles;

namespace Garage.Models.Data;

public static class TestData<T> where T : IVehicle
{

    public static IEnumerable<T> GetTestData()
    {
        IVehicle[] testData =
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

        return testData.OfType<T>();
    }
}
