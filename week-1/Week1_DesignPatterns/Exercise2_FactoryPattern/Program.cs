using System;

// ============================================
// MAIN PROGRAM - Testing Factory Pattern
// ============================================

// We ask the factory to create vehicles for us
// We don't use "new Car()" or "new Bike()" directly

VehicleFactory carFactory = new CarFactory();
IVehicle car = carFactory.CreateVehicle();
car.Drive();

VehicleFactory bikeFactory = new BikeFactory();
IVehicle bike = bikeFactory.CreateVehicle();
bike.Drive();

VehicleFactory truckFactory = new TruckFactory();
IVehicle truck = truckFactory.CreateVehicle();
truck.Drive();

// ============================================
// STEP 1: Define the common interface
// All vehicles MUST have a Drive() method
// ============================================

public interface IVehicle
{
    void Drive();
}

// ============================================
// STEP 2: Create concrete vehicle classes
// Each one implements IVehicle
// ============================================

public class Car : IVehicle
{
    public void Drive()
    {
        Console.WriteLine("Car is driving on the road.");
    }
}

public class Bike : IVehicle
{
    public void Drive()
    {
        Console.WriteLine("Bike is riding on the street.");
    }
}

public class Truck : IVehicle
{
    public void Drive()
    {
        Console.WriteLine("Truck is hauling goods on the highway.");
    }
}

// ============================================
// STEP 3: Define the abstract factory class
// Every factory MUST have a CreateVehicle() method
// ============================================

public abstract class VehicleFactory
{
    // This method will be implemented by each specific factory
    public abstract IVehicle CreateVehicle();
}

// ============================================
// STEP 4: Create concrete factory classes
// Each factory knows how to create ONE type of vehicle
// ============================================

public class CarFactory : VehicleFactory
{
    public override IVehicle CreateVehicle()
    {
        Console.WriteLine("CarFactory: Creating a Car...");
        return new Car();
    }
}

public class BikeFactory : VehicleFactory
{
    public override IVehicle CreateVehicle()
    {
        Console.WriteLine("BikeFactory: Creating a Bike...");
        return new Bike();
    }
}

public class TruckFactory : VehicleFactory
{
    public override IVehicle CreateVehicle()
    {
        Console.WriteLine("TruckFactory: Creating a Truck...");
        return new Truck();
    }
}