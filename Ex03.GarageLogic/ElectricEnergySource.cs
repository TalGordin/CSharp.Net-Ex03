using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex03.GarageLogic
{
    public class ElectricEnergySource : EnergySource
    {
        public ElectricEnergySource(float i_MaxTimeBeforeCharge) : base(i_MaxTimeBeforeCharge) { }

        public override Dictionary<string, Type> GetProperties()
        {
            Dictionary<string, Type> properties = base.GetProperties();
            properties.Add("current time until battery runs out", CurrentAmount.GetType());
            return properties;
        }

        public override void SetProperty(string i_Property, object value)
        {
            try
            {
                if (i_Property == "current time until battery runs out")
                {
                    CurrentAmount = (float)value;
                }
                else
                {
                    throw new ArgumentException("Property not found in \"Electric\" energy source.");
                }
            }
            catch (InvalidCastException)
            {
                throw new FormatException("Wrong data type for this property.");
            }
        }

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
