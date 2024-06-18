using System;
using System.Collections.Generic;
using System.Runtime.Remoting.Services;

namespace Ex03.GarageLogic
{
    public abstract class Vehicle
    {
        public string ModelName { get; set; }
        public string LicenseNumber { get; set; }
        public EnergySource EnergySource { get; set; }
        public List<Tire> Tires { get; set; }

        public Vehicle(string i_LicenseNumber, EnergySource i_EnergySource, uint i_NumOfTires, float i_MaxTiresAirPressure)
        {
            LicenseNumber = i_LicenseNumber;
            EnergySource = i_EnergySource;
            Tires = new List<Tire>();

            for (int i = 0; i < i_NumOfTires; i++)
            {
                Tires.Add(new Tire(i_MaxTiresAirPressure));
            }
        }

        public virtual Dictionary<string, Type> GetProperties()
        {
            Dictionary<string, Type> properties = new Dictionary<string, Type>();

            properties.Add("model name", typeof(string));
 
            return properties;
        }

        public virtual void SetProperty(string i_Property, object value)
        {
            if (i_Property == "model name")
            {
                ModelName = value.ToString();
            }
            else
            {
                throw new ArgumentException("Property not found in vehicle.");
            }
        }
        public void InflateWheelsToMax()
        {
            foreach (Tire tire in Tires)
            {
                tire.InflateToMax();
            }
        }

        public override string ToString()
        {
            string tiresDetails = string.Join(Environment.NewLine, Tires);
            return string.Format(
                "Model Name: {0}{1}License Number: {2}{1}Energy Source: {3}{1}Tires:{1}{4}",
                ModelName,
                Environment.NewLine,
                LicenseNumber,
                EnergySource,
                tiresDetails);
        }
    }
    public class Tire
    {
        public string ManufacturerName { get; set; }
        public float CurrentAirPressure { get; set; }
        public float MaxAirPressure { get; }

        public Tire(float i_MaxAitPressure)
        {
            MaxAirPressure = i_MaxAitPressure;
        }

        public virtual Dictionary<string, Type> GetProperties()
        {
            Dictionary<string, Type> properties = new Dictionary<string, Type>();

            properties.Add("manufacturer name", typeof(string));
            properties.Add("current air pressure", CurrentAirPressure.GetType());

            return properties;
        }

        public virtual void SetProperty(string i_Property, object value)
        {
            try
            {
                if (i_Property == "manufacturer name")
                {
                    ManufacturerName = value.ToString();
                }
                else if (i_Property == "current air pressure")
                {
                    CurrentAirPressure = (float)value;
                }
                else
                {
                    throw new ArgumentException("Property not found in object \"tire\".");
                }
            }
            catch (InvalidCastException)
            {
                throw new FormatException("Wrong data type for this property.");
            }
        }

        public void InflateToMax()
        {
            CurrentAirPressure = MaxAirPressure;
        }

        public void AddAir(float i_AirToAdd)
        {
            if (CurrentAirPressure + i_AirToAdd > MaxAirPressure)
            {
                throw new ValueOutOfRangeException(0, MaxAirPressure - CurrentAirPressure, "Air pressure exceeds max limit.");
            }
            CurrentAirPressure += i_AirToAdd;
        }

        public override string ToString()
        {
            return string.Format(
                "Manufacturer Name: {0}, Current Air Pressure: {1}, Max Air Pressure: {2}",
                ManufacturerName,
                CurrentAirPressure,
                MaxAirPressure);
        }
    }
}
