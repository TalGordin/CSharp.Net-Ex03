using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex03.GarageLogic
{
    public abstract class EnergySource
    {
        public float MaxCapacity { get; protected set; }
        public float CurrentAmount { get; protected set; }

        public EnergySource(float i_MaxCapacity, float i_CurrentAmount)
        {
            MaxCapacity = i_MaxCapacity;
            CurrentAmount = i_CurrentAmount;
        }

        public void AddEnergy(float i_Amount)
        {
            if (CurrentAmount + i_Amount > MaxCapacity)
            {
                throw new ValueOutOfRangeException(0, MaxCapacity - CurrentAmount, "Energy amount exceeds max capacity");
            }

            CurrentAmount += i_Amount;
        }

        public override string ToString()
        {
            return string.Format("Current Amount: {0}{1}Max Capacity: {2}",
                CurrentAmount,
                Environment.NewLine,
                MaxCapacity);
        }
    }
}

