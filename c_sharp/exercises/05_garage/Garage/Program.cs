using System;
using Garage.Models.Vehicles;



Vehicle car = new Car("Ford", "123a", "Red", 4);
Vehicle car2 = new Car("Ford", "123A", "Red", 4);


System.Console.WriteLine(car.ToString());
System.Console.WriteLine(car2.ToString());