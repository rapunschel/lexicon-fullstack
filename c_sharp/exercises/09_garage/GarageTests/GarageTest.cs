using Garage.Models;
using Garage.Models.Enums;
using Garage.Models.Vehicles;

namespace GarageTests;

public class GarageTest
{
    private static Airplane Plane(string regNumber) => new("Boeing", regNumber, "White", 3, 500);

    [Fact]
    public void Add_ReturnsAdded_WhenThereIsSpace()
    {
        var garage = new GarageCollection<IVehicle>(1);

        Assert.Equal(CollectionResult.Added, garage.Add(Plane("abc123")));
    }

    [Fact]
    public void Add_ReturnsDuplicateReg_AlreadyExist()
    {
        var garage = new GarageCollection<IVehicle>(2)
        {
            Plane("abc123")
        };

        Assert.Equal(CollectionResult.DuplicateRegistration, garage.Add(Plane("abc123")));
    }

    [Fact]
    public void Add_ReturnsAtCapacity_WhenFull()
    {
        var garage = new GarageCollection<IVehicle>(1)
        {
            Plane("aye123")
        };

        Assert.Equal(CollectionResult.AtCapacity, garage.Add(Plane("aye321")));
    }

    [Fact]
    public void Remove_ReturnsRemoved_WhenRegistrationExists()
    {
        var garage = new GarageCollection<IVehicle>(1)
        {
            Plane("naf321")
        };

        Assert.Equal(CollectionResult.Removed, garage.Remove("naf321"));
    }

    [Fact]
    public void Remove_ReturnsNotFound_WhenRegistrationDoesNotExist()
    {
        var garage = new GarageCollection<IVehicle>(1)
        {
            Plane("naf321")
        };
        Assert.Equal(CollectionResult.NotFound, garage.Remove("not999"));
    }


    [Fact]
    public void Search_ReturnsVehicle_WhenRegistrationExists()
    {
        var garage = new GarageCollection<IVehicle>(1);
        var reg = "abc123".ToUpper();
        garage.Add(Plane(reg));
        var result = garage.Search(vehicle => vehicle.RegNumber == reg);
        Assert.Equal(reg, result.First().RegNumber);
    }

    [Fact]
    public void Search_ReturnsAllRedVehicles()
    {
        var garage = new GarageCollection<IVehicle>(10)
        {
            new Car("ford", "abc123", "red", 5, 5),
            new Car("ford", "abc124", "red", 5, 5),
            new Car("ford", "abc125", "red", 5, 5),
            new Car("ford", "abc126", "red", 5, 5),
        };

        int countBefore = garage.Count();
        var result = garage.Search(vehicle => vehicle.Color == "red");
        Assert.Equal(4, result.Count());
    }

    [Fact]
    public void Search_ReturnsEmpty_WhenRegistrationDoesNotExist()
    {
        var garage = new GarageCollection<IVehicle>(4)
        {
            Plane("abc123"),
            Plane("abc125"),
            Plane("abc124"),
            Plane("abc124"),

        };
        var result = garage.Search(vehicle => vehicle.RegNumber == "abb123");

        Assert.Empty(result);
    }

    [Fact]
    public void Search_ReturnsNotEmpty_WhenRegistrationExist()
    {
        var garage = new GarageCollection<IVehicle>(1)
        {
            new Car("ford", "abc123", "red", 5, 5),

        };

        var result = garage.Search(vehicle => vehicle.RegNumber == "ABC123");

        Assert.NotEmpty(result);
    }
}
