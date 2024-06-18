using System;

namespace Ex03.GarageLogic
{
    public abstract class EnergySource
    {
        public float MaxCapacity { get; protected set; }
        public float CurrentAmount { get; protected set; }

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

