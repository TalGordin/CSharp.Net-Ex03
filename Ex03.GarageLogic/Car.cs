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

        public eColor Color { get; set; }
        public eNumOfDoors NumberOfDoors { get; set; }

        public Car(string i_ModelName, string i_LicenseNumber, EnergySource i_EnergySource, List<Tire> i_Wheels, eColor i_Color, eNumOfDoors i_NumberOfDoors)
            : base(i_ModelName, i_LicenseNumber, i_EnergySource, i_Wheels)
        {
            Color = i_Color;
            NumberOfDoors = i_NumberOfDoors;
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

