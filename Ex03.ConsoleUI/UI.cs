using System;
using System.Collections.Generic;
using System.Reflection;
using Ex03.GarageLogic;
using static Ex03.GarageLogic.Garage;
using static Ex03.GarageLogic.Motorcycle;

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

                printMainMenu();
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

        private void addOrEditVehicle() //DONE
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

            Vehicle newVehicle = getNewVehicle(i_NewVehicleLicenseNumber);
            VehiclePropertiesCollector properties = new VehiclePropertiesCollector(newVehicle);

            getPropertiesFromUser(properties, newVehicle);
            addVehicleToGarage(newVehicle);
        }

        private void addVehicleToGarage(Vehicle i_NewVehicle)
        {
            Console.WriteLine("Enter owner name: ");
            string ownerName = Console.ReadLine();
            Console.WriteLine("Enter owner phone number: ");
            do
            {
                string ownerPhone = Console.ReadLine();
                try
                {
                    m_Garage.AddVehicle(i_NewVehicle, ownerName, ownerPhone);
                    Console.WriteLine("Vehicle successfully added to garage!");
                    break;
                }
                catch (Exception exception) when (exception is ArgumentException || exception is FormatException)
                {
                    printExceptionErrorMessage(exception);
                }
            } while (true);
        }

        private void getPropertiesFromUser(VehiclePropertiesCollector properties, Vehicle newVehicle)
        {
            List<Dictionary<string, Type>> DictionariesOfProperties = new List<Dictionary<string, Type>>();

            DictionariesOfProperties.Add(properties.VehicleProperties);
            DictionariesOfProperties.Add(properties.EnergySourceProperties);
            foreach (var tire in properties.TiresProperties)
            {
                DictionariesOfProperties.Add(tire);
            }

            int currentTire = 0;

            foreach (var dictionary in DictionariesOfProperties)
            {
                foreach (var property in dictionary)
                {
                    do
                    {
                        try
                        {
                            if (dictionary == properties.VehicleProperties)
                            {
                            handleProperty(property, newVehicle);
                            }
                            else if (dictionary == properties.EnergySourceProperties)
                            {
                                handleProperty(property, newVehicle.EnergySource);
                            }
                            else
                            {
                                Console.Write($"Tire #{currentTire + 1} - ");
                                handleProperty(property, newVehicle.Tires[currentTire]);
                            }
                            break;
                        }
                        catch (Exception exception)
                        {
                            printExceptionErrorMessage(exception);
                        }
                    } while (true);
                }
                if (properties.TiresProperties.Contains(dictionary))
                {
                    currentTire++;
                }
            }
        }

        private void handleProperty(KeyValuePair<string, Type> i_Property, object obj)
        {
            Console.WriteLine($"Enter {i_Property.Key}:");
            string input = Console.ReadLine();
            object parsedValue;

            if (i_Property.Value == typeof(string))
            {
                parsedValue = input;
            }
            else if (i_Property.Value.IsEnum)
            {
                parsedValue = Enum.Parse(i_Property.Value, input);
            }
            else
            {
                MethodInfo parseMethod = i_Property.Value.GetMethod("Parse", new[] { typeof(string) });
                parsedValue = parseMethod.Invoke(null, new object[] { input });
            }
            //Console.WriteLine($"OBJECT TYPE = {obj.GetType()}");
            MethodInfo setPropertyMethod = obj.GetType().GetMethod("SetProperty", new[] { typeof(string), typeof(object) });
            
            try
            {
                setPropertyMethod.Invoke(obj, new object[] { i_Property.Key, parsedValue });
            }
            catch (TargetInvocationException ex)
            {
                // Handling the inner exception
                throw ex.InnerException;
            }

        }

        private Vehicle getNewVehicle(string i_NewVehicleLicenseNumber)
        {
            Console.WriteLine("Choose vehicle type:");
            foreach (var type in Enum.GetValues(typeof(VehicleFactory.eVehicleType)))
            {
                Console.WriteLine($"{(int)type + 1}. {type}");
            }

            Vehicle newVehicle;
            do
            {
                try
                {
                    int vehicleType = int.Parse(Console.ReadLine());
                    newVehicle = VehicleFactory.CreateVehicle(vehicleType - 1, i_NewVehicleLicenseNumber);
                    break;
                }
                catch (Exception exception) when (exception is ArgumentOutOfRangeException ||  exception is FormatException)
                {
                    printExceptionErrorMessage(exception);
                }
       
            } while (true);

            return newVehicle;
        }

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
