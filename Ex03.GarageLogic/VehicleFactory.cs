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
                    return new Motorcycle(i_LicenseNumber, new FuelEnergySource());
                case eVehicleType.ElectricMotorcycle:
                    return new Motorcycle(i_LicenseNumber, new ElectricEnergySource());
                case eVehicleType.FuelCar:
                    return new Car(i_LicenseNumber, new FuelEnergySource());
                case eVehicleType.ElectricCar:
                    return new Car(i_LicenseNumber, new ElectricEnergySource());
                case eVehicleType.Truck:
                    return new Truck(i_LicenseNumber, new FuelEnergySource());
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
