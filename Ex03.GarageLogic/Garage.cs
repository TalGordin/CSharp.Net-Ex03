﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex03.GarageLogic
{
    public class Garage
    {
        public enum eVehicleStatus
        {
            InRepair,
            Repaired,
            Paid
        }

        private Dictionary<string, VehicleData> m_Vehicles;

        public Garage()
        {
            m_Vehicles = new Dictionary<string, VehicleData>();
        }

        public void FindVehicleByLicense(string i_LicenseNumber)
        {
            bool foundVehicleInGarage = m_Vehicles.ContainsKey(i_LicenseNumber);

            if (foundVehicleInGarage)
            {
                m_Vehicles[i_LicenseNumber].Status = eVehicleStatus.InRepair;
                throw new Exception($"Vehicle with the same license number already exists in the garage- vehicle status updated to \"{eVehicleStatus.InRepair}\".");
            }
        }
        public void AddVehicle(Vehicle i_Vehicle, string i_OwnerName, string i_OwnerPhone)
        {
            bool wasNewVehicleAdded = m_Vehicles.ContainsKey(i_Vehicle.LicenseNumber);
            
            m_Vehicles[i_Vehicle.LicenseNumber] = new VehicleData(i_Vehicle, i_OwnerName, i_OwnerPhone, eVehicleStatus.InRepair);
            if (!wasNewVehicleAdded)
            {
                throw new ArgumentException($"Vehicle with the same license number already exists in the garage- vehicle data updated, status = \"{eVehicleStatus.InRepair}\".");
            }
        }

        public void InflateTiresToMax(string i_LicenseNumber)
        {
            if (!m_Vehicles.ContainsKey(i_LicenseNumber))
            {
                throw new ArgumentException("Vehicle with the given license number does not exist in the garage.");
            }

            m_Vehicles[i_LicenseNumber].Vehicle.InflateWheelsToMax();
        }

        public void Refuel(string i_LicenseNumber, float i_Amount, FuelEnergySource.eFuelType i_FuelType) // the instructions says diffrent functions
        {
            if (!m_Vehicles.ContainsKey(i_LicenseNumber))
            {
                throw new ArgumentException("Vehicle with the given license number does not exist in the garage.");
            }

            Vehicle vehicle = m_Vehicles[i_LicenseNumber].Vehicle;
            if (vehicle.EnergySource is FuelEnergySource fuelSource)
            {
                fuelSource.Refuel(i_Amount, i_FuelType);
            }
            else
            {
                throw new ArgumentException("Vehicle with the given license number is not a fuel vehicle.");
            }
        }

        public void ChargeBattery(string i_LicenseNumber, float i_HoursToAdd)// the instructions says diffrent functions
        {
            if (!m_Vehicles.ContainsKey(i_LicenseNumber))
            {
                throw new ArgumentException("Vehicle with the given license number does not exist in the garage.");
            }

            Vehicle vehicle = m_Vehicles[i_LicenseNumber].Vehicle;
            if (vehicle.EnergySource is ElectricEnergySource electricSource)
            {
                electricSource.ChargeBattery(i_HoursToAdd);
            }
            else
            {
                throw new ArgumentException("Vehicle with the given license number is not an electric vehicle.");
            }
        }

        public void ChangeVehicleStatus(string i_LicenseNumber, eVehicleStatus i_NewStatus)
        {
            if (!m_Vehicles.ContainsKey(i_LicenseNumber))
            {
                throw new ArgumentException("Vehicle with the given license number does not exist in the garage.");
            }

            m_Vehicles[i_LicenseNumber].Status = i_NewStatus;
        }

        public string GetVehicleDetails(string i_LicenseNumber)
        {
            if (!m_Vehicles.ContainsKey(i_LicenseNumber))
            {
                throw new ArgumentException("Vehicle with the given license number does not exist in the garage.");
            }

            VehicleData vehicleData = m_Vehicles[i_LicenseNumber];
            return string.Format(
                "Owner Name: {0}{1}Owner Phone: {2}{1}Status: {3}{1}{4}",
                vehicleData.OwnerName,
                Environment.NewLine,
                vehicleData.OwnerPhone,
                vehicleData.Status,
                vehicleData.Vehicle);
        }

        /*
        The GetLicenseNumbers method in the Garage class is responsible for retrieving a list of license numbers for all vehicles in the garage. 
        It also has the optional functionality to filter the license numbers based on the vehicle's status.*/
        public List<string> GetLicenseNumbers(eVehicleStatus? i_FilterStatus = null)
        {
            List<string> licenseNumbers = new List<string>();
            foreach (var vehicleData in m_Vehicles.Values)
            {
                if (!i_FilterStatus.HasValue || vehicleData.Status == i_FilterStatus.Value)
                {
                    licenseNumbers.Add(vehicleData.Vehicle.LicenseNumber);
                }
            }

            return licenseNumbers;
        }

        private class VehicleData
        {
            public Vehicle Vehicle { get; set; }
            public string OwnerName { get; set; }
            public string OwnerPhone { get; set; }
            public eVehicleStatus Status { get; set; }
            public VehicleData(Vehicle i_Vehicle, string i_OwnerName, string i_OwnerPhone, eVehicleStatus i_Status)
            {
                Vehicle = i_Vehicle;
                OwnerName = i_OwnerName;
                OwnerPhone = i_OwnerPhone;
                Status = i_Status;
            }
        }
    }
}
