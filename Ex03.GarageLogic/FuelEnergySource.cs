using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex03.GarageLogic
{
    public class FuelEnergySource : EnergySource
    {
        public enum eFuelType
        {
            Octan95,
            Octan96,
            Octan98,
            Soler
        }

        public eFuelType FuelType { get; set; }

        public FuelEnergySource(float i_MaxCapacity, float i_CurrentAmount, eFuelType i_FuelType)
            : base(i_MaxCapacity, i_CurrentAmount)
        {
            FuelType = i_FuelType;
        }

        public void Refuel(float i_Amount, eFuelType i_FuelType)
        {
            if (i_FuelType != FuelType)
            {
                throw new ArgumentException("Incorrect fuel type");
            }

            AddEnergy(i_Amount);
        }

        public override string ToString()
        {
            return string.Format("{0}{1}Fuel Type: {2}",
                base.ToString(),
                Environment.NewLine,
                FuelType);
        }
    }
}

