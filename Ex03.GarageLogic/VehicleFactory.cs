using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Ex03.GarageLogic.VehicleFactory;


namespace Ex03.GarageLogic
{
    public static class VehicleFactory
    {
        public enum eVehicleType
        {
            FuelMotorcycle,
            ElectricMotorcycle,
            FuelCar,
            ElectricCar,
            Truck
        }

        public const FuelEnergySource.eFuelType MotorcycleFuel = FuelEnergySource.eFuelType.Octan98;
        public const FuelEnergySource.eFuelType CarFuel = FuelEnergySource.eFuelType.Octan95;
        public const FuelEnergySource.eFuelType TruckFuel = FuelEnergySource.eFuelType.Soler;
        public const float MotorcycleFuelTankCapacity = (float)5.5;
        public const float CarFuelTankCapacity = 45;
        public const float TruckTankCapacity = 120;
        public const float MotorcycleBatteryCapacity = (float)2.5;
        public const float CarBatteryCapacity = (float)3.5;

        public static Vehicle CreateVehicle(int i_requestedVehicleType, string i_LicenseNumber)
        {
            if (!Enum.IsDefined(typeof(eVehicleType), i_requestedVehicleType))
            {
                throw new ArgumentOutOfRangeException("Invalid vehicle type chosen.");
            }

            eVehicleType newVehicleType = (eVehicleType)i_requestedVehicleType;
            switch (newVehicleType)
            {
                case eVehicleType.FuelMotorcycle:
                    return new Motorcycle(i_LicenseNumber, new FuelEnergySource(MotorcycleFuel, MotorcycleFuelTankCapacity));
                case eVehicleType.ElectricMotorcycle:
                    return new Motorcycle(i_LicenseNumber, new ElectricEnergySource(MotorcycleBatteryCapacity));
                case eVehicleType.FuelCar:
                    return new Car(i_LicenseNumber, new FuelEnergySource(CarFuel, CarFuelTankCapacity));
                case eVehicleType.ElectricCar:
                    return new Car(i_LicenseNumber, new ElectricEnergySource(CarBatteryCapacity));
                case eVehicleType.Truck:
                    return new Truck(i_LicenseNumber, new FuelEnergySource(TruckFuel, TruckTankCapacity));
                default:
                    throw new ArgumentException("Unknown vehicle type.");
            }
        }

        //public static Vehicle CreateVehicle(eVehicleType i_VehicleType, string i_ModelName, string i_LicenseNumber, List<Tire> i_Wheels, params object[] i_Params)
        //{
        //    switch (i_VehicleType)
        //    {
        //        case eVehicleType.FuelMotorcycle:
        //            return new Motorcycle(i_ModelName, i_LicenseNumber, new FuelEnergySource((float)i_Params[0], (float)i_Params[1], (FuelEnergySource.eFuelType)i_Params[2]), i_Wheels, (Motorcycle.eLicenseType)i_Params[3], (int)i_Params[4]);
        //        case eVehicleType.ElectricMotorcycle:
        //            return new Motorcycle(i_ModelName, i_LicenseNumber, new ElectricEnergySource((float)i_Params[0], (float)i_Params[1]), i_Wheels, (Motorcycle.eLicenseType)i_Params[2], (int)i_Params[3]);
        //        case eVehicleType.FuelCar:
        //            return new Car(i_ModelName, i_LicenseNumber, new FuelEnergySource((float)i_Params[0], (float)i_Params[1], (FuelEnergySource.eFuelType)i_Params[2]), i_Wheels, (Car.eColor)i_Params[3], (Car.eNumOfDoors)i_Params[4]);
        //        case eVehicleType.ElectricCar:
        //            return new Car(i_ModelName, i_LicenseNumber, new ElectricEnergySource((float)i_Params[0], (float)i_Params[1]), i_Wheels, (Car.eColor)i_Params[2], (Car.eNumOfDoors)i_Params[3]);
        //        case eVehicleType.Truck:
        //            return new Truck(i_ModelName, i_LicenseNumber, i_Wheels, (FuelEnergySource.eFuelType)i_Params[0], (float)i_Params[1], (float)i_Params[2], (bool)i_Params[3], (float)i_Params[4]);
        //        default:
        //            throw new ArgumentException("Unknown vehicle type");
        //    }
        //}
    }
}
