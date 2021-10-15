using System;

namespace Prague_Parking_1._0
{


/* Jag försökte har försökt att skriva nytt i 1.1 på alla ställen där allt som har lagts till är nytt, 
 * jag har heller inte kommenterat så mycket för att inte 
 * kladda ner allting men det är lite som är nytt och på 
 * några ställen så har jag återanvänt saker som jag använde tidigare*/ 

    class Program
    {

        static void Main(string[] args)
        {

            Vehicles[,] parkingGarage = new Vehicles[100, 2];
            bool menuSwitch = true;
            int totalMC = 0; // La till en MC counter
            while (menuSwitch)
            {

                DisplayMenu();
                string menuSelection = Console.ReadLine();

                switch (menuSelection)

                {
                    case "1": // Meny 1: Lägg till ett nytt fordon.

                        Vehicles vehicle = new Vehicles();

                        if (vehicle.VehicleType == "MC#")
                        {
                            totalMC++; // Skapade en ny kontroll utav hur många motorcycklar som finns
                        }

                        int parkingSpot = RandomNumber(0, 99);

                        if (parkingGarage[parkingSpot, 0] != null) // La till ",0" här
                        {
                            ParkingOccupied(parkingGarage, vehicle);
                        }
                        else
                        {
                            parkingGarage[parkingSpot, 0] = vehicle; // La till ",0" här
                            Console.WriteLine();
                            Console.WriteLine((parkingSpot + 1) + ". " + parkingGarage[parkingSpot, 0].VehicleType + parkingGarage[parkingSpot, 0].NumberPlate + " - " + parkingGarage[parkingSpot, 0].Time); // Lagt till + .Time här

                        }

                        break;

                    case "2": // Meny 2: Visa alla parkerade fordon.


                        Console.WriteLine();
                        int arrayEmpty = 0;

                        for (int i = 0; i < parkingGarage.GetLength(0); i++)
                        {
                            if (parkingGarage[i, 0] == null) // La till ",0" här
                            {
                                arrayEmpty++;
                                continue;
                            }
                            else if (parkingGarage[i, 1] != null) // La till det här else if för att visa MC också.
                            {
                                Console.WriteLine($"{i + 1}. {parkingGarage[i, 0].VehicleType + parkingGarage[i, 0].NumberPlate}|{parkingGarage[i, 1].VehicleType + parkingGarage[i, 1].NumberPlate}");
                            }
                            else
                            {
                                Console.WriteLine($"{i + 1}. {parkingGarage[i, 0].VehicleType + parkingGarage[i, 0].NumberPlate}"); // La till ",0" här
                            }
                        }
                        if (arrayEmpty == 100)
                        {
                            Console.WriteLine("Parkering är tom");
                        }

                        break;

                    case "3": // Meny 3: Flytta ett fordon.


                        Console.WriteLine();

                        //Console.WriteLine("Vilket fordon vill du flytta? Ange parkerings ruta.");
                        //int currentParkingSpot = TryParse(Console.ReadLine());

                        Console.Write("Skriv in registreringsnummret på fordonet som du vill flytta på: ");
                        string numberPlate = Console.ReadLine(); // NYTT I 1.1
                        Console.WriteLine();
                        Console.Write("Vilken parkerings ruta vill du flytta fordonet till: ");
                        int newParkingSpot = TryParse(Console.ReadLine());
                        Console.WriteLine();
                        MoveVehiclPlates(parkingGarage, numberPlate, newParkingSpot);
                        //MoveVehicle(parkingGarage, currentParkingSpot, newParkingSpot);

                        break;
                    case "4": // Meny 4: Leta efter ett fordon.


                        Console.WriteLine("Skriv in registreringsnummret på fordonet du söker efter:");
                        string searchedVehicle = Console.ReadLine();
                        SearchVehicle(parkingGarage, searchedVehicle);

                        break;

                    case "5": // Meny 5: Ta ut ett fordon.

                        Console.WriteLine("Skriv in registreringsnummret på fordonet som ska lämna.");
                        string exitingVehicle = Console.ReadLine();
                        VehicleExit(parkingGarage, exitingVehicle);
                        break;

                    case "6": // Meny 6: Optimering av parkeringshuset.

                        OptimizeParking(parkingGarage, totalMC);

                        break;
                    case "7": // Meny 6: Avsluta programmet.

                        Console.WriteLine("Programmet avslutas!");
                        menuSwitch = false;
                        break;

                    default:
                        Console.WriteLine("Välj mellan 1-6 för att gå till någon meny.");
                        break;
                }
            }

        }

        // Case 3 METOD. Används inte längre, MoveVehiclPlates används istället.

        public static void MoveVehicle(Vehicles[,] parkingGarage, int currentParkingSpot, int newParkingSpot)
        {

            currentParkingSpot -= 1;
            newParkingSpot -= 1;

            try
            {


                if (parkingGarage[newParkingSpot, 0] == null && parkingGarage[currentParkingSpot, 0] != null) // La till ",0" här
                {
                    parkingGarage[newParkingSpot, 0] = parkingGarage[currentParkingSpot, 0]; // La till ",0" här
                    Console.WriteLine($"Fordonet: {parkingGarage[currentParkingSpot, 0].VehicleType + parkingGarage[currentParkingSpot, 0].NumberPlate} från parkeringplats {currentParkingSpot + 1} har flyttats till parkeringplats nummer {newParkingSpot + 1}"); // La till ",0" här
                    parkingGarage[currentParkingSpot, 0] = null; // La till ",0" här
                }
                else if (parkingGarage[newParkingSpot, 0] == null && parkingGarage[currentParkingSpot, 0] == null) // La till ",0" här
                {
                    Console.WriteLine($"Det finns inget fordon på parkeringplats nummer: {currentParkingSpot + 1}");
                }
                else if (parkingGarage[newParkingSpot, 0] != null && parkingGarage[currentParkingSpot, 0].VehicleType + parkingGarage[currentParkingSpot, 0].NumberPlate != parkingGarage[newParkingSpot, 0].VehicleType + parkingGarage[newParkingSpot, 0].NumberPlate) // La till ",0" här
                {
                    Console.WriteLine($"Parkeringplats: {newParkingSpot + 1} hade redan ett fordon: {parkingGarage[currentParkingSpot, 0].VehicleType + parkingGarage[currentParkingSpot, 0].NumberPlate}"); // La till ",0" här
                }
                else if (parkingGarage[currentParkingSpot, 0].VehicleType + parkingGarage[currentParkingSpot, 0].NumberPlate == parkingGarage[newParkingSpot, 0].VehicleType + parkingGarage[newParkingSpot, 0].NumberPlate) // La till ",0" här
                {
                    Console.WriteLine("Fordonet står redan på den plats du försöker flytta den till.");
                }
            }
            catch (Exception)
            {


                if (currentParkingSpot <= 0 || currentParkingSpot > parkingGarage.GetLength(0))
                {
                    Console.WriteLine($"Det finns ingen parkeringplats med nummer: {currentParkingSpot + 1}");
                }
                else
                {
                    Console.WriteLine($"Det finns ingen parkeringplats med nummer: {newParkingSpot + 1}");
                }
            }

        }

        public static int RandomNumber(int minValue, int maxValue)
        {
            Random rnd = new Random();
            int randomNumber = rnd.Next(minValue, maxValue + 1);
            return randomNumber;
        }


        // Används i case 1: 
        public static void ParkingOccupied(Vehicles[,] parkingGarage, Vehicles vehicle)
        {

            bool parkingFull = true;
            for (int i = 0; i < parkingGarage.GetLength(0); i++)
            {
                if (parkingGarage[i, 0] == null) // La till ",0" här
                {
                    parkingGarage[i, 0] = vehicle; // La till ",0" här
                    Console.WriteLine();
                    Console.WriteLine((i + 1) + ". " + parkingGarage[i, 0].VehicleType + parkingGarage[i, 0].NumberPlate + " - " + parkingGarage[i, 0].Time); // Lagt till .Time här.
                    parkingFull = false;
                    break;
                }

            }
            if (parkingFull)
            {
                Console.WriteLine("Parkeringen är full");
            }

        }

        // Används i case 4:
        public static void SearchVehicle(Vehicles[,] parkingGarage, string searchedVehicle) // La till komma på Vehicles
        {

            searchedVehicle.Trim().ToLower();
            int arrayEmpty = 0;
            bool found = true; // NYTT I 1.1
            for (int i = 0; i < parkingGarage.GetLength(0); i++)
            {

                if (parkingGarage[i, 0] == null)
                {
                    arrayEmpty++;
                    continue;
                }
                else
                {
                    if (searchedVehicle == parkingGarage[i, 0].NumberPlate) // La till ",0" här
                    {
                        found = false;
                        Console.WriteLine($"Fordonet {searchedVehicle} hittades på parkeringplats: {i + 1}");
                        break;
                    }
                    else if (searchedVehicle == parkingGarage[i, 0].VehicleType + parkingGarage[i, 0].NumberPlate) // La till ",0" här
                    {
                        found = false;
                        Console.WriteLine($"Fordonet {searchedVehicle} hittades på parkeringplats: {i + 1}");
                        break;
                    }
                    else if (parkingGarage[i,1] != null && searchedVehicle == parkingGarage[i, 1].VehicleType + parkingGarage[i, 1].NumberPlate) // NYTT i 1.1
                    {
                        found = false;
                        Console.WriteLine($"Fordonet {searchedVehicle} hittades på parkeringplats: {i + 1} men det innehåller 2 fordon");
                        Console.WriteLine($"{i + 1}. { parkingGarage[i, 0].VehicleType + parkingGarage[i, 0].NumberPlate}|{ parkingGarage[i, 1].VehicleType + parkingGarage[i, 1].NumberPlate}");
                        break;
                    }
                    else if (parkingGarage[i, 1] != null && searchedVehicle == parkingGarage[i, 1].NumberPlate) // NYTT i version 1.1
                    {
                        found = false;
                        Console.WriteLine($"Fordonet {searchedVehicle} hittades på parkeringplats: {i + 1} men det innehåller 2 fordon");
                        Console.WriteLine($"{i + 1}. { parkingGarage[i, 0].VehicleType + parkingGarage[i, 0].NumberPlate}|{ parkingGarage[i, 1].VehicleType + parkingGarage[i, 1].NumberPlate}");
                        break;

                    }

                }

            }
            if (found) // NYTT i 1.1
            {
                Console.WriteLine($"Fordonet du sökte efter {searchedVehicle} finns inte i parkeringen.");
            }
            if (arrayEmpty == 100)
            {
                Console.WriteLine("Parkeringen är tom");
            }

        }

        // Används i case 5:        
        public static void VehicleExit(Vehicles[,] parkingGarage, string searchedVehicle)
        {

            DateTime currentTime = DateTime.Now; // Håller koll på tiden just nu.

            searchedVehicle.Trim().ToLower().ToLower();
            int arrayEmpty = 0;
            bool found = true;
            for (int i = 0; i < parkingGarage.GetLength(0); i++)
            {

                if (parkingGarage[i, 0] == null)
                {
                    arrayEmpty++;
                    continue;
                }
                else
                {

                    TimeSpan timeSpan = currentTime - parkingGarage[i, 0].Time; // Skapar en skillnad på tiden som bilen parkerades och just nu.


                    if (searchedVehicle == parkingGarage[i, 0].NumberPlate)
                    {
                        found = false; // NYTT
                        Console.WriteLine($"Fordonet {searchedVehicle} lämnade parkeringplats: {i + 1}");
                        Console.WriteLine($"Fordonet stod parkerat i: {timeSpan.ToString(@"hh\:mm\:ss")}"); // Skriver ut tiden som fordonet stod parkerat. 
                        parkingGarage[i, 0] = null; // La till ",0" här
                        break;
                    }
                    else if (parkingGarage[i, 1] != null && searchedVehicle == parkingGarage[i, 1].NumberPlate) // NYTT I 1.1
                    {
                        found = false; // NYTT 
                        Console.WriteLine($"Fordonet {searchedVehicle} lämnade parkeringplats: {i + 1}");
                        Console.WriteLine($"Fordonet stod parkerat i: {timeSpan.ToString(@"hh\:mm\:ss")}"); // Skriver ut tiden som fordonet stod parkerat. 
                        parkingGarage[i, 1] = null; // La till ",1" här // NYTT
                        break;
                    }
                    else if (searchedVehicle == parkingGarage[i, 0].VehicleType + parkingGarage[i, 0].NumberPlate)
                    {
                        found = false; // NYTT 
                        Console.WriteLine($"Fordonet {searchedVehicle} lämnade parkeringplats: {i + 1}");
                        Console.WriteLine($"Fordonet stod parkerat i: {timeSpan.ToString(@"hh\:mm\:ss")}"); // Skriver ut tiden som fordonet stod parkerat. 
                        parkingGarage[i, 0] = null;
                    }
                    else if (parkingGarage[i, 1] != null && searchedVehicle == parkingGarage[i, 1].VehicleType + parkingGarage[i, 1].NumberPlate)
                    {
                        found = false; // NYTT 
                        Console.WriteLine($"Fordonet {searchedVehicle} lämnade parkeringplats: {i + 1}");
                        Console.WriteLine($"Fordonet stod parkerat i: {timeSpan.ToString(@"hh\:mm\:ss")}"); // Skriver ut tiden som fordonet stod parkerat. 
                        parkingGarage[i, 1] = null;
                    }
                    
                }

            }
            if (found) // NYTT I 1.1
            {
                Console.WriteLine($"Fordonet du sökte efter {searchedVehicle} finns inte i parkeringen.");
            }
            if (arrayEmpty == 100)
            {
                Console.WriteLine("Parkeringen är tom");
            }


        }



        public static int TryParse(string value)
        {
            bool success = int.TryParse(value, out int result);

            if (success)
            {
                return result;
            }
            else
            {
                Console.WriteLine("Ogiltig inmatning");
                return result;
            }
        }

        public static void DisplayMenu()
        {
            Console.WriteLine();
            Console.WriteLine("Meny 1: Lägg till ett nytt fordon.");
            Console.WriteLine("Meny 2: Visa alla parkerade fordon.");
            Console.WriteLine("Meny 3: Flytta ett fordon.");
            Console.WriteLine("Meny 4: Leta efter ett fordon.");
            Console.WriteLine("Meny 5: Ta ut ett fordon.");
            Console.WriteLine("Meny 6: Optimera parkeringshuset.");
            Console.WriteLine("Meny 7: Avsluta programmet.");
            Console.WriteLine("Vilken meny vill du gå till?");

        }


        // Hela metoden är nytt i 1.1
        public static void OptimizeParking(Vehicles[,] parkingGarage, int totalMC)
        {
            int arrayEmpty = 0;

            for (int i = 0; i < parkingGarage.GetLength(0); i++)
            {
                if (parkingGarage[i, 0] != null && parkingGarage[i, 1] != null)
                {
                    totalMC -= 2;
                }
                else if (parkingGarage[i, 0] == null && parkingGarage[i, 1] == null)
                {
                    arrayEmpty++;
                    continue;
                }
            }
            if (arrayEmpty == 100)
            {
                Console.WriteLine("Parkeringen är tom");
            }
            else if (totalMC <= 1)
            {
                Console.WriteLine("Parkeringen behöver inte optimeras");
            }

            for (int i = 0; i < parkingGarage.GetLength(0); i++)
            {

                if (parkingGarage[i, 0] == null && parkingGarage[i, 1] == null)
                {
                    continue;
                }
                else if (parkingGarage[i, 0].VehicleType == "MC#" && totalMC >= 2)
                {
                    for (int j = i + 1; j < parkingGarage.GetLength(0); j++)
                    {
                        if (parkingGarage[j, 0] == null)
                        {
                            continue;
                        }
                        else if (parkingGarage[j, 0].VehicleType == "MC#" && parkingGarage[j, 1] == null && parkingGarage[i, 1] == null)
                        {

                            parkingGarage[i, 1] = parkingGarage[j, 0];
                            Console.WriteLine($"{i + 1}. {parkingGarage[i, 0].VehicleType + parkingGarage[i, 0].NumberPlate}|({j + 1}). {parkingGarage[i, 1].VehicleType + parkingGarage[i, 1].NumberPlate}");

                            totalMC -= 2;
                            parkingGarage[j, 0] = null;
                            if (totalMC <= 1)
                            {
                                Console.WriteLine("Parkeringen har blivit optimerad.");
                                break;
                            }
                            break;
                        }

                    }

                }


            }

        }

        // Hela metoden är nytt i 1.1
        private static void MoveVehiclPlates(Vehicles[,] parkingGarage, string numberPlate, int newParkingSpot)
        {

            newParkingSpot -= 1;
            numberPlate.Trim().ToLower();
            try
            {

                int arrayEmpty = 0;
                bool found = true;
                for (int i = 0; i < parkingGarage.GetLength(0); i++)
                {

                    if (parkingGarage[i, 0] == null)
                    {
                        arrayEmpty++;
                        continue;
                    }

                    else if (numberPlate == parkingGarage[i, 0].VehicleType + parkingGarage[i, 0].NumberPlate || numberPlate == parkingGarage[i, 0].NumberPlate) // HÄR
                    {
                        found = false;

                        // Tar hand om bilar som flyttas till tomma rutor 
                        if (parkingGarage[i, 0].VehicleType == "CAR#" && parkingGarage[newParkingSpot, 0] == null)
                        {
                            parkingGarage[newParkingSpot, 0] = parkingGarage[i, 0];
                            parkingGarage[i, 0] = null;
                            Console.WriteLine($"Fordonet CAR#{parkingGarage[newParkingSpot, 0].NumberPlate} har flyttas till plats {newParkingSpot + 1}.");
                            break;
                        }
                        // Tar hand om motorcycklar som flyttas till en tom plats.
                        else if ((parkingGarage[newParkingSpot, 0] == null && parkingGarage[i, 0].VehicleType == "MC#"))
                        {
                            parkingGarage[newParkingSpot, 0] = parkingGarage[i, 0];
                            parkingGarage[i, 0] = null;
                            Console.WriteLine("Motorcyckeln har flyttas till plats {0}.", newParkingSpot + 1);
                            Console.WriteLine($"{newParkingSpot + 1}. {parkingGarage[newParkingSpot, 0].VehicleType + parkingGarage[newParkingSpot, 0].NumberPlate}");
                            break;
                        }

                        // Tar hand om det redan står 2 motorcyklar redan.
                        else if ((parkingGarage[newParkingSpot, 0] != null && parkingGarage[newParkingSpot, 1] != null))
                        {

                            Console.WriteLine($"Det står två motorcyklar där: {parkingGarage[newParkingSpot, 0].VehicleType + parkingGarage[newParkingSpot, 0].NumberPlate}" +
                                   $"|{parkingGarage[newParkingSpot, 1].VehicleType + parkingGarage[newParkingSpot, 1].NumberPlate}");
                            Console.WriteLine($"Fordonet du försökte flytta från plats nummer {i + 1}, kunde ej flyttas.");
                            break;
                        }

                        // Tar hand om man försöker flytta en bil eller motorcykel till en plats som redan har en bil där.
                        else if (parkingGarage[newParkingSpot, 0] != null && parkingGarage[newParkingSpot, 0].VehicleType == "CAR#")
                        {
                            Console.WriteLine($"Det står redan ett fordon där: {parkingGarage[newParkingSpot, 0].VehicleType + parkingGarage[newParkingSpot, 0].NumberPlate}");
                            Console.WriteLine($"Bilen du försökte flytta på från plats nummer {i + 1}, kunde ej flyttas.");
                            break;
                        }

                        // Tar hand om motorcyklar och dubbelparkerar dem om det behövs. 
                        else if (parkingGarage[newParkingSpot, 0].VehicleType != "CAR#" && (parkingGarage[newParkingSpot, 1] == null && parkingGarage[i, 0].VehicleType == "MC#"))
                        {
                            parkingGarage[newParkingSpot, 1] = parkingGarage[i, 0];
                            parkingGarage[i, 0] = null;
                            Console.WriteLine("Motorcyckeln har flyttas till plats {0}.", newParkingSpot + 1);
                            Console.WriteLine($"{newParkingSpot + 1}. {parkingGarage[newParkingSpot, 0].VehicleType + parkingGarage[newParkingSpot, 0].NumberPlate}|{parkingGarage[newParkingSpot, 1].VehicleType + parkingGarage[newParkingSpot, 1].NumberPlate}");
                            break;
                        }

                        // Tar hand om alla andra situationer som kanske har glömts men ändå innehåller ett fordon.
                        else
                        {
                            Console.WriteLine($"Det står redan ett fordon där: {parkingGarage[newParkingSpot, 0].VehicleType + parkingGarage[newParkingSpot, 0].NumberPlate}");
                            Console.WriteLine($"Fordonet du försökte flytta på från plats nummer {i + 1}, kunde ej flyttas.");
                        }
                    }
                }

                if (arrayEmpty == 100)
                {
                    Console.WriteLine("Parkeringen är tom");
                }
                else if (found)
                {
                    Console.WriteLine($"{numberPlate} finns inte i parkeringen");
                }
            }
            catch (Exception)
            {
                if (newParkingSpot <= 0 || newParkingSpot > parkingGarage.GetLength(0))
                {
                    Console.WriteLine($"Det finns ingen parkeringplats med nummer: {newParkingSpot + 1}");
                }
            }

        }
    }

}


