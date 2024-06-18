using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex03.GarageLogic
{
    public class Truck : Vehicle
    {
        public bool IsCarryingHazardousMaterials { get; set; }
        public float CargoVolume { get; set; }
        public FuelEnergySource FuelSource { get; set; }

        public Truck(string i_ModelName, string i_LicenseNumber, List<Tire> i_Wheels, FuelEnergySource.eFuelType i_FuelType, float i_FuelTankCapacity, float i_CurrentFuelAmount, bool i_IsCarryingHazardousMaterials, float i_CargoVolume)
            : base(i_ModelName, i_LicenseNumber, new FuelEnergySource(i_FuelTankCapacity, i_CurrentFuelAmount, i_FuelType), i_Wheels)
        {
            FuelSource = new FuelEnergySource(i_FuelTankCapacity, i_CurrentFuelAmount, i_FuelType); // to ensure it will alwys be FuelEnergy source
            IsCarryingHazardousMaterials = i_IsCarryingHazardousMaterials;
            CargoVolume = i_CargoVolume;
        }

        public void Refuel(float i_AmountToAdd, FuelEnergySource.eFuelType i_FuelType)
        {
            FuelSource.Refuel(i_AmountToAdd, i_FuelType);
        }

        public override string ToString()
        {
            return string.Format("{0}Carrying Hazardous Materials: {1}{2}Cargo Volume: {3} cubic meters{2}{4}",
                base.ToString(),
                IsCarryingHazardousMaterials,
                Environment.NewLine,
                CargoVolume,
                FuelSource);
        }
    }
}

