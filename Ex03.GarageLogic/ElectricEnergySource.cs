using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex03.GarageLogic
{
    public class ElectricEnergySource : EnergySource
    {

        public void ChargeBattery(float i_HoursToAdd)
        {
            AddEnergy(i_HoursToAdd);
        }

        public override string ToString()
        {
            return string.Format("{0}", base.ToString());
        }
    }
}
