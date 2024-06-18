using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Ex03.GarageLogic.Car;

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

        public const uint NumOfTires = 2;
        public const float MaxTiresAirPressure = 33;
        public eLicenseType LicenseType { get; set; }
        public int EngineDisplacement { get; set; }

        public Motorcycle(string i_LicenseNumber, EnergySource i_EnergySource) : base(i_LicenseNumber, i_EnergySource, NumOfTires, MaxTiresAirPressure) { }

        public override Dictionary<string, Type> GetProperties()
        {
            Dictionary<string, Type> properties = base.GetProperties();
            properties.Add("driving license type", LicenseType.GetType());
            properties.Add("engine displacement", LicenseType.GetType());
            return properties;
        }

        public override void SetProperty(string i_Property, object value)
        {
            try
            {
                if (i_Property == "model name")
                {
                    ModelName = value.ToString();
                }
                else if (i_Property == "driving license type")
                {
                    LicenseType = (eLicenseType)value;
                }
                else if (i_Property == "engine displacement")
                {
                    EngineDisplacement = (int)value;
                }
                else
                {
                    throw new ArgumentException("Property not found in vehicle \"Motorcycle\".");
                }
            }
            catch (InvalidCastException)
            {
                throw new FormatException("Wrong data type for this property.");
            }
        }

        public override string ToString()
        {
            return string.Format("{0}License Type: {1}{2}Engine Capacity: {3} cc",
                base.ToString(),
                LicenseType,
                Environment.NewLine,
                EngineDisplacement);
        }
    }
}

