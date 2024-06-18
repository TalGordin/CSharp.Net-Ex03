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

        public Truck(string i_LicenseNumber, FuelEnergySource i_EnergySource) : base(i_LicenseNumber, i_EnergySource) { }

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

