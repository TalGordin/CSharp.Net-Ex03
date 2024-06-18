using System;
using System.Collections.Generic;
using System.Reflection;
using Ex03.GarageLogic;
using static Ex03.GarageLogic.Garage;

namespace Ex03.ConsoleUI
{
    public class UI
    {
        private Garage m_Garage;

        public UI(Garage i_Garage)
        {
            m_Garage = i_Garage;
        }

        public void MainMenu()
        {
            printMainMenu();
            while (true)
            {
                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        addOrEditVehicle();
                        break;
                    case "2":
                        DisplayVehicles();
                        break;
                    case "3":
                        InflateTires();
                        break;
                    case "4":
                        RefuelVehicle();
                        break;
                    case "5":
                        ChargeVehicle();
                        break;
                    case "6":
                        ChangeVehicleStatus();
                        break;
                    case "7":
                        DisplayVehicleDetails();
                        break;
                    default:
                        Console.WriteLine("Invalid choice. Please try again.");
                        continue;
                }
            }
        }

        private void printMainMenu()
        {
            Console.WriteLine("\n=======================================");
            Console.WriteLine("      Garage Management System");
            Console.WriteLine("=======================================");
            Console.WriteLine("1. Add a new vehicle");
            Console.WriteLine("2. Display all vehicles");
            Console.WriteLine("3. Inflate tires to max");
            Console.WriteLine("4. Refuel vehicle");
            Console.WriteLine("5. Charge vehicle battery");
            Console.WriteLine("6. Change vehicle status");
            Console.WriteLine("7. Display vehicle details");
            Console.WriteLine("=======================================");
            Console.Write("Please enter your choice: ");
        }

        private void addOrEditVehicle()
        {
            Console.WriteLine("Enter the vehicle license number:");
            string userInput = Console.ReadLine();

            bool shouldAddVehicle = m_Garage.CheckWhetherToAddVehicle(userInput);
            if (!shouldAddVehicle)
            {
                Console.WriteLine($"Vehicle with the same license number already exists in the garage- vehicle status updated to \"{eVehicleStatus.InRepair}\".");
            }
            else
            {
                addNewVehicle(userInput);
            }

                


        }

        private void addNewVehicle(string i_NewVehicleLicenseNumber)
        {
            Console.WriteLine("=======================================");
            Console.WriteLine("           Add a New Vehicle");
            Console.WriteLine("=======================================");

            Vehicle newVehicle = getEmptyVehicle(i_NewVehicleLicenseNumber);
            
            
        }

        private Vehicle getEmptyVehicle(string i_NewVehicleLicenseNumber)
        {
            Console.WriteLine("Choose vehicle type:");
            foreach (var type in Enum.GetValues(typeof(VehicleFactory.eVehicleType)))
            {
                Console.WriteLine($"{(int)type + 1}. {type}");
            }


            do
            {
                try
                {
                    int vehicleType = int.Parse(Console.ReadLine());
                    Vehicle newVehicle = VehicleFactory.CreateVehicle(vehicleType, i_NewVehicleLicenseNumber);
                    break;
                }
                catch (Exception exception) when (exception is ArgumentOutOfRangeException ||  exception is FormatException)
                {
                    printExceptionErrorMessage(exception);
                }
       
            } while (true);

            //return new Car(i_NewVehicleLicenseNumber);
        }

        //public void AddVehicle()
        //{
        //    Console.WriteLine("\n=======================================");
        //    Console.WriteLine("           Add a New Vehicle");
        //    Console.WriteLine("=======================================");

        //    Console.WriteLine()






        //    Console.WriteLine("Choose vehicle type:");
        //    foreach (var type in Enum.GetValues(typeof(VehicleFactory.eVehicleType)))
        //    {
        //        Console.WriteLine($"{(int)type + 1}. {type}");
        //    }

        //    try 
        //    {
        //        VehicleFactory.eVehicleType vehicleType = getVehicleChosen();

        //        //General info:
        //        Console.Write("Enter model name: ");
        //        string modelName = Console.ReadLine();
        //        Console.Write("Enter license number: ");
        //        string licenseNumber = Console.ReadLine();

        //        //Wheels:
        //        List<Wheel> wheels = getWheelsData();
        //        Console.Write("Enter number of wheels: ");
        //        int numberOfWheels = int.Parse(Console.ReadLine());

        //        for (int i = 0; i < numberOfWheels; i++)
        //        {
        //            Console.Write($"Enter manufacturer name for wheel {i + 1}: ");
        //            string manufacturerName = Console.ReadLine();

        //            Console.Write($"Enter current air pressure for wheel {i + 1}: ");
        //            float currentAirPressure = float.Parse(Console.ReadLine());

        //            Console.Write($"Enter max air pressure for wheel {i + 1}: ");
        //            float maxAirPressure = float.Parse(Console.ReadLine());

        //            wheels.Add(new Wheel(manufacturerName, currentAirPressure, maxAirPressure));
        //        }

        //        List<object> parameters = new List<object>();
        //        //Parameters by energy supply
        //        switch (vehicleType)
        //        {
        //            case VehicleFactory.eVehicleType.Truck:
        //                Console.Write("Is carrying hazardous materials (true/false): ");
        //                parameters.Add(bool.Parse(Console.ReadLine()));

        //                Console.Write("Enter cargo volume: ");
        //                parameters.Add(float.Parse(Console.ReadLine()));
        //                goto case VehicleFactory.eVehicleType.FuelCar;
        //            case VehicleFactory.eVehicleType.FuelMotorcycle:
        //            case VehicleFactory.eVehicleType.FuelCar:
        //                Console.Write("Enter fuel tank capacity: ");
        //                parameters.Add(float.Parse(Console.ReadLine()));

        //                Console.Write("Enter current fuel amount: ");
        //                parameters.Add(float.Parse(Console.ReadLine()));

        //                Console.WriteLine("Choose fuel type:");
        //                foreach (var type in Enum.GetValues(typeof(FuelEnergySource.eFuelType)))
        //                {
        //                    Console.WriteLine($"{(int)type}. {type}");
        //                }
        //                parameters.Add((FuelEnergySource.eFuelType)int.Parse(Console.ReadLine()));
        //                break;

        //            case VehicleFactory.eVehicleType.ElectricMotorcycle:
        //            case VehicleFactory.eVehicleType.ElectricCar:
        //                Console.Write("Enter battery capacity: ");
        //                parameters.Add(float.Parse(Console.ReadLine()));

        //                Console.Write("Enter current battery time: ");
        //                parameters.Add(float.Parse(Console.ReadLine()));
        //                break;

        //        }
        //        //Parameters by type
        //        switch (vehicleType)
        //        {
        //            case VehicleFactory.eVehicleType.FuelMotorcycle:
        //            case VehicleFactory.eVehicleType.ElectricMotorcycle:
        //                Console.WriteLine("Choose license type:");
        //                foreach (var type in Enum.GetValues(typeof(Motorcycle.eLicenseType)))
        //                {
        //                    Console.WriteLine($"{(int)type}. {type}");
        //                }
        //                parameters.Add((Motorcycle.eLicenseType)int.Parse(Console.ReadLine()));

        //                Console.Write("Enter engine capacity: ");
        //                parameters.Add(int.Parse(Console.ReadLine()));
        //                break;

        //            case VehicleFactory.eVehicleType.FuelCar:
        //            case VehicleFactory.eVehicleType.ElectricCar:
        //                Console.WriteLine("Choose car color:");
        //                foreach (var color in Enum.GetValues(typeof(Car.eColor)))
        //                {
        //                    Console.WriteLine($"{(int)color}. {color}");
        //                }
        //                parameters.Add((Car.eColor)int.Parse(Console.ReadLine()));

        //                Console.WriteLine("Choose number of doors:");
        //                foreach (var door in Enum.GetValues(typeof(Car.eNumOfDoors)))
        //                {
        //                    Console.WriteLine($"{(int)door}. {door}");
        //                }
        //                parameters.Add((Car.eNumOfDoors)int.Parse(Console.ReadLine()));
        //                break;
        //        }

        //    }
        //    catch (Exception ex)
        //    {
        //        Console.WriteLine($"Error completing operation: {ex.Message}");
        //        throw;
        //    }


        //    try
        //    {
        //        Vehicle vehicle = VehicleFactory.CreateVehicle(vehicleType, modelName, licenseNumber, wheels, parameters.ToArray());
        //        Console.Write("Enter owner name: ");
        //        string ownerName = Console.ReadLine();

        //        Console.Write("Enter owner phone: ");
        //        string ownerPhone = Console.ReadLine();

        //        m_Garage.AddVehicle(vehicle, ownerName, ownerPhone);
        //        Console.WriteLine("Vehicle added successfully!");
        //    }
        //    catch (Exception ex)
        //    {
        //        Console.WriteLine($"Failed to add vehicle: {ex.Message}");
        //    }
        //}

        //private List<Wheel> getWheelsData()
        //{
        //    List<Wheel> wheels = new List<Wheel>();
        //    Console.Write("Enter number of wheels: ");
        //    int numberOfWheels = int.Parse(Console.ReadLine());

        //    for (int i = 0; i < numberOfWheels; i++)
        //    {
        //        Console.Write($"Enter manufacturer name for wheel {i + 1}: ");
        //        string manufacturerName = Console.ReadLine();

        //        Console.Write($"Enter current air pressure for wheel {i + 1}: ");
        //        float currentAirPressure = float.Parse(Console.ReadLine());

        //        Console.Write($"Enter max air pressure for wheel {i + 1}: ");
        //        float maxAirPressure = float.Parse(Console.ReadLine());

        //        wheels.Add(new Wheel(manufacturerName, currentAirPressure, maxAirPressure));
        //    }

        //    return wheels;
        //}
        private VehicleFactory.eVehicleType getChosenVehicleType()
        {
            int input = int.Parse(Console.ReadLine());
            VehicleFactory.eVehicleType vehicleType = (VehicleFactory.eVehicleType)(input - 1);

            if (!Enum.IsDefined(typeof(VehicleFactory.eVehicleType), vehicleType))
            {
                throw new ArgumentOutOfRangeException("Invalid vehicle type chosen.");
            }

            return vehicleType;
        }

        public void DisplayVehicles()
        {
            Console.WriteLine("\n=======================================");
            Console.WriteLine("         All Vehicles in Garage");
            Console.WriteLine("=======================================");
            foreach (string licenseNumber in m_Garage.GetLicenseNumbers())
            {
                Console.WriteLine(licenseNumber);
            }
        }

        public void InflateTires()
        {
            Console.WriteLine("\n=======================================");
            Console.WriteLine("         Inflate Tires to Max");
            Console.WriteLine("=======================================");
            Console.Write("Enter license number: ");
            string licenseNumber = Console.ReadLine();

            try
            {
                m_Garage.InflateTiresToMax(licenseNumber);
                Console.WriteLine("Tires inflated to max successfully!");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Failed to inflate tires: {ex.Message}");
            }
        }

        public void RefuelVehicle()
        {
            Console.WriteLine("\n=======================================");
            Console.WriteLine("           Refuel Vehicle");
            Console.WriteLine("=======================================");
            Console.Write("Enter license number: ");
            string licenseNumber = Console.ReadLine();

            Console.Write("Enter amount of fuel to add: ");
            float amount = float.Parse(Console.ReadLine());

            Console.WriteLine("Choose fuel type:");
            foreach (var type in Enum.GetValues(typeof(FuelEnergySource.eFuelType)))
            {
                Console.WriteLine($"{(int)type}. {type}");
            }
            FuelEnergySource.eFuelType fuelType = (FuelEnergySource.eFuelType)int.Parse(Console.ReadLine());

            try
            {
                m_Garage.Refuel(licenseNumber, amount, fuelType);
                Console.WriteLine("Vehicle refueled successfully!");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Failed to refuel vehicle: {ex.Message}");
            }
        }

        public void ChargeVehicle()
        {
            Console.WriteLine("\n=======================================");
            Console.WriteLine("           Charge Vehicle");
            Console.WriteLine("=======================================");
            Console.Write("Enter license number: ");
            string licenseNumber = Console.ReadLine();

            Console.Write("Enter hours to charge: ");
            float hours = float.Parse(Console.ReadLine());

            try
            {
                m_Garage.ChargeBattery(licenseNumber, hours);
                Console.WriteLine("Vehicle charged successfully!");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Failed to charge vehicle: {ex.Message}");
            }
        }

        public void ChangeVehicleStatus()
        {
            Console.WriteLine("\n=======================================");
            Console.WriteLine("          Change Vehicle Status");
            Console.WriteLine("=======================================");
            Console.Write("Enter license number: ");
            string licenseNumber = Console.ReadLine();

            Console.WriteLine("Choose new status:");
            foreach (var status in Enum.GetValues(typeof(Garage.eVehicleStatus)))
            {
                Console.WriteLine($"{(int)status}. {status}");
            }
            Garage.eVehicleStatus newStatus = (Garage.eVehicleStatus)int.Parse(Console.ReadLine());

            try
            {
                m_Garage.ChangeVehicleStatus(licenseNumber, newStatus);
                Console.WriteLine("Vehicle status changed successfully!");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Failed to change vehicle status: {ex.Message}");
            }
        }

        public void DisplayVehicleDetails()
        {
            Console.WriteLine("\n=======================================");
            Console.WriteLine("          Vehicle Details");
            Console.WriteLine("=======================================");
            Console.Write("Enter license number: ");
            string licenseNumber = Console.ReadLine();

            try
            {
                string details = m_Garage.GetVehicleDetails(licenseNumber);
                Console.WriteLine(details);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Failed to get vehicle details: {ex.Message}");
            }
        }

        private void printExceptionErrorMessage(Exception i_Exception)
        {
            string errorMessage;
            if (i_Exception is ArgumentOutOfRangeException argumentOutOfRangeException)
            {
                errorMessage = argumentOutOfRangeException.ParamName;
            }
            else
            {
                errorMessage = i_Exception.Message;
            }

            Console.Write($"{i_Exception.GetType()}: {errorMessage} ");
            Console.WriteLine("Try again.");
        }
    }
}
