using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex03.GarageLogic
{
    public class Motorcycle : Vehicle
    {
        public enum eLicenseType
        {
            A,
            A1,
            AA,
            B1
        }

        public eLicenseType LicenseType { get; set; }
        public int EngineCapacity { get; set; }

        public Motorcycle(string i_ModelName, string i_LicenseNumber, EnergySource i_EnergySource, List<Tire> i_Wheels, eLicenseType i_LicenseType, int i_EngineCapacity)
            : base(i_ModelName, i_LicenseNumber, i_EnergySource, i_Wheels)
        {
            LicenseType = i_LicenseType;
            EngineCapacity = i_EngineCapacity;
        }

        public override string ToString()
        {
            return string.Format("{0}License Type: {1}{2}Engine Capacity: {3} cc",
                base.ToString(),
                LicenseType,
                Environment.NewLine,
                EngineCapacity);
        }
    }
}

