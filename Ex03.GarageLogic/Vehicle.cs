using System;
using System.Collections.Generic;

namespace Ex03.GarageLogic
{
    public abstract class Vehicle
    {
        public string ModelName { get; set; }
        public string LicenseNumber { get; set; }
        public EnergySource EnergySource { get; set; }
        public List<Tire> Tires { get; set; }

        public Vehicle(string i_ModelName, string i_LicenseNumber, EnergySource i_EnergySource, List<Tire> i_Wheels)
        {
            ModelName = i_ModelName;
            LicenseNumber = i_LicenseNumber;
            EnergySource = i_EnergySource;
            Tires = i_Wheels;
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
        public float MaxAirPressure { get; set; }

        public Tire(string i_ManufacturerName, float i_CurrentAirPressure, float i_MaxAirPressure)
        {
            ManufacturerName = i_ManufacturerName;
            CurrentAirPressure = i_CurrentAirPressure;
            MaxAirPressure = i_MaxAirPressure;
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
    public class ValueOutOfRangeException : Exception
    {
        public float MinValue { get; private set; }
        public float MaxValue { get; private set; }

        public ValueOutOfRangeException(float i_MinValue, float i_MaxValue, string i_Message)
            : base(i_Message)
        {
            MinValue = i_MinValue;
            MaxValue = i_MaxValue;
        }
    }
}
