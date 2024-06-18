using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex03.GarageLogic
{
    public class Car : Vehicle
    {
        public enum eColor
        {
            Red,
            White,
            Black,
            Yellow
        }

        public enum eNumOfDoors
        {
            Two = 2,
            Three,
            Four,
            Five
        }

        public const uint NumOfTires = 5;
        public const float MaxTiresAirPressure = 31;
        public eColor Color { get; set; }
        public eNumOfDoors NumberOfDoors { get; set; }

        public Car(string i_LicenseNumber, EnergySource i_EnergySource) : base(i_LicenseNumber, i_EnergySource, NumOfTires, MaxTiresAirPressure) { }

        public override Dictionary<string, Type> GetProperties()
        {
            Dictionary<string, Type> properties = base.GetProperties();
            properties.Add("car color", Color.GetType());
            properties.Add("number of doors", NumberOfDoors.GetType());
            return properties;
        }

        public override void SetProperty(string i_Property, object value)
        {
            try {
                if (i_Property == "model name")
                {
                    ModelName = value.ToString();
                }
                else if (i_Property == "car color")
                {
                    if (Enum.IsDefined(typeof(eColor), value))
                    {
                        Color = (eColor)value;
                    }
                    else
                    {
                        throw new ArgumentException("Invalid value for car color.");
                    }
                }
                else if (i_Property == "number of doors")
                {
                    if (Enum.IsDefined(typeof(eNumOfDoors), value))
                    {
                        NumberOfDoors = (eNumOfDoors)value;
                    }
                    else
                    {
                        throw new ArgumentException("Invalid value for number of doors.");
                    }
                }
                else
                {
                    throw new ArgumentException("Property not found in vehicle \"Car\".");
                }
            }
            catch (InvalidCastException)
            {
                throw new FormatException("Wrong data type for this property.");
            }
        }
        public override string ToString()
        {
            return string.Format("{0}Color: {1}{2}Number of Doors: {3}{2}",
                base.ToString(),
                Color,
                Environment.NewLine,
                NumberOfDoors);
        }
    }
}

