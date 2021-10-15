using System;

namespace Prague_Parking_1._0
{

    class Program
    {

        static void Main(string[] args)
        {

            Vehicles[] parkingGarage = new Vehicles[100]; 
            bool menuSwitch = true; 

            while (menuSwitch)
            {

                DisplayMenu(); 
                string menuSelection = Console.ReadLine(); 

                switch (menuSelection) 

                {
                    case "1": // Meny 1: Lägg till ett nytt fordon.

                        Vehicles vehicle = new Vehicles(); 
                        int parkingSpot = RandomNumber(0, 99); 

                        if (parkingGarage[parkingSpot] != null) 
                        {
                            ParkingOccupied(parkingGarage, vehicle); 
                        }
                        else
                        {
                            parkingGarage[parkingSpot] = vehicle; 
                            Console.WriteLine();
                            Console.WriteLine((parkingSpot + 1) + ". " + parkingGarage[parkingSpot].VehicleType + parkingGarage[parkingSpot].NumberPlate);  
                        }

                        break; 

                    case "2": // Meny 2: Visa alla parkerade fordon.
                        

                        Console.WriteLine();
                        int arrayEmpty = 0;

                        for (int i = 0; i < parkingGarage.Length; i++) 
                        {
                            if (parkingGarage[i] == null) 
                            {
                                arrayEmpty++;                                
                                continue;
                            }
                            else 
                            {
                                Console.WriteLine($"{i + 1}. {parkingGarage[i].VehicleType + parkingGarage[i].NumberPlate}");
                            }
                        }
                        if (arrayEmpty == 100) 
                        {
                            Console.WriteLine("Parkering är tom");
                        }

                        break;

                    case "3": // Meny 3: Flytta ett fordon.

                        
                        Console.WriteLine();
                        Console.WriteLine("Vilket fordon vill du flytta? Ange parkerings ruta.");
                        int currentParkingSpot = TryParse(Console.ReadLine());
                        Console.WriteLine();
                        Console.WriteLine("Vilken parkerings ruta vill du flytta fordonet till?");
                        int newParkingSpot = TryParse(Console.ReadLine());
                        Console.WriteLine();

                        MoveVehicle(parkingGarage, currentParkingSpot, newParkingSpot); 

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

                    case "6": // Meny 6: Avsluta programmet.

                        Console.WriteLine("Programmet avslutas!");
                        menuSwitch = false; 
                        break;

                    default: 
                        Console.WriteLine("Välj mellan 1-6 för att gå till någon meny.");
                        break;
                }
            }

        }

        // Case 3 METOD. 
        
        public static void MoveVehicle(Vehicles[] parkingGarage, int currentParkingSpot, int newParkingSpot) 
        {

            currentParkingSpot -= 1; 
            newParkingSpot -= 1; 

            try  
            {

                
                if (parkingGarage[newParkingSpot] == null && parkingGarage[currentParkingSpot] != null) 
                {
                    parkingGarage[newParkingSpot] = parkingGarage[currentParkingSpot]; 
                    Console.WriteLine($"Fordonet: {parkingGarage[currentParkingSpot].VehicleType + parkingGarage[currentParkingSpot].NumberPlate} från parkeringplats {currentParkingSpot + 1} har flyttats till parkeringplats nummer {newParkingSpot + 1}");
                    parkingGarage[currentParkingSpot] = null;  
                }
                else if (parkingGarage[newParkingSpot] == null && parkingGarage[currentParkingSpot] == null) 
                {                                                                                                       
                    Console.WriteLine($"Det finns inget fordon på parkeringplats nummer: {currentParkingSpot + 1}");
                }
                else if (parkingGarage[newParkingSpot] != null && parkingGarage[currentParkingSpot].VehicleType + parkingGarage[currentParkingSpot].NumberPlate != parkingGarage[newParkingSpot].VehicleType + parkingGarage[newParkingSpot].NumberPlate)
                { 
                    Console.WriteLine($"Parkeringplats: {newParkingSpot + 1} hade redan ett fordon: {parkingGarage[currentParkingSpot].VehicleType + parkingGarage[currentParkingSpot].NumberPlate}");
                }
                else if (parkingGarage[currentParkingSpot].VehicleType + parkingGarage[currentParkingSpot].NumberPlate == parkingGarage[newParkingSpot].VehicleType + parkingGarage[newParkingSpot].NumberPlate) 
                { 
                    Console.WriteLine("Fordonet står redan på den plats du försöker flytta den till."); 
                }
            }
            catch (Exception)
            {
                
               
                if (currentParkingSpot <= 0 || currentParkingSpot > parkingGarage.Length)
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
        public static void ParkingOccupied(Vehicles[] parkingGarage, Vehicles vehicle) 
        {

            bool parkingFull = true; 
            for (int i = 0; i < parkingGarage.Length; i++) 
            {
                if (parkingGarage[i] == null)  
                {
                    parkingGarage[i] = vehicle; 
                    Console.WriteLine();
                    Console.WriteLine((i + 1) + ". " + parkingGarage[i].VehicleType + parkingGarage[i].NumberPlate);
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
        public static void SearchVehicle(Vehicles[] parkingGarage, string searchedVehicle) 
        {

            searchedVehicle.Trim().ToLower(); 
            int arrayEmpty = 0; 
            for (int i = 0; i < parkingGarage.Length; i++)
            {

                if (parkingGarage[i] == null) 
                {
                    arrayEmpty++;
                    continue;
                }
                else 
                {
                    if (searchedVehicle == parkingGarage[i].NumberPlate) 
                    {
                        Console.WriteLine($"Fordonet {searchedVehicle} hittades på parkeringplats: {i + 1}"); 
                        break;
                    }
                    else if (searchedVehicle == parkingGarage[i].VehicleType + parkingGarage[i].NumberPlate) 
                    {
                        Console.WriteLine($"Fordonet {searchedVehicle} hittades på parkeringplats: {i + 1}");
                    }
                    else 
                    {
                        Console.WriteLine($"Fordonet med registreringsnummret {searchedVehicle} finns inte i parkeringen.");
                    }

                }

            }
            if (arrayEmpty == 100) 
            {
                Console.WriteLine("Parkeringen är tom");
            }

        }

        // Används i case 5:        
        public static void VehicleExit(Vehicles[] parkingGarage, string searchedVehicle) 
        {
            searchedVehicle.Trim().ToLower().ToLower(); 
            int arrayEmpty = 0; 
            for (int i = 0; i < parkingGarage.Length; i++) 
            {
              
                if (parkingGarage[i] == null)
                {
                    arrayEmpty++;
                    continue;
                }
                else
                {
                     
                    
                    if (searchedVehicle == parkingGarage[i].NumberPlate) 
                    {
                        Console.WriteLine($"Fordonet {searchedVehicle} lämnade parkeringplats: {i + 1}");
                        parkingGarage[i] = null; 
                        break;
                    }
                    else if (searchedVehicle == parkingGarage[i].VehicleType + parkingGarage[i].NumberPlate)
                    {
                        Console.WriteLine($"Fordonet {searchedVehicle} lämnade parkeringplats: {i + 1}");
                        parkingGarage[i] = null;
                    }
                    else
                    {
                        Console.WriteLine($"Fordonet med registreringsnummret {searchedVehicle} finns inte i parkeringen."); 
                    }

                }

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
                return result;             }
        }
                
        public static void DisplayMenu() 
        {
            Console.WriteLine();
            Console.WriteLine("Meny 1: Lägg till ett nytt fordon.");
            Console.WriteLine("Meny 2: Visa alla parkerade fordon.");
            Console.WriteLine("Meny 3: Flytta ett fordon.");
            Console.WriteLine("Meny 4: Leta efter ett fordon.");
            Console.WriteLine("Meny 5: Ta ut ett fordon.");
            Console.WriteLine("Meny 6: Avsluta programmet.");
            Console.WriteLine("Vilken meny vill du gå till?"); 
 
        }

    }

}



///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
                                                   // Kommenterad kod längre ner, går att ta bort kommentarerna och kommentera hela koden där uppe och bara köra direkt då allt innehåll finns med.

/*
using System;

namespace Prague_Parking_1._0
{

    class Program
    {

        static void Main(string[] args)
        {

            Vehicles[] parkingGarage = new Vehicles[100]; // Vi skapar en object array.
            bool menuSwitch = true; // Vi använder en bool switch för att köra vår meny.

            while (menuSwitch)
            {

                DisplayMenu(); // Kallar på vår Display metod.                              
                string menuSelection = Console.ReadLine(); // Tar inmatningen som vi sedan använder i våran switch.

                switch (menuSelection) // Använder utav en string selection för att göra det lättare att hantera felinmatningar. 

                {
                    case "1": // Meny 1: Lägg till ett nytt fordon.
                        Vehicles vehicle = new Vehicles(); // Skapar en ny vehicle object.
                        int parkingSpot = RandomNumber(0, 99); // Hittar en random parkering plats mellan 1 - 100 parkeringplatser.  

                        if (parkingGarage[parkingSpot] != null) // Har en kontrol för att kolla att parkeringsplatsen är tom eller inte.
                        {
                            ParkingOccupied(parkingGarage, vehicle); // Skickar den till vår Parkingoccupied metod. Beskriver den längre ner.
                        }
                        else
                        {
                            parkingGarage[parkingSpot] = vehicle; // Annars så lägger vi till fordonet i vår parking garage array.
                            Console.WriteLine();
                            Console.WriteLine((parkingSpot + 1) + ". " + parkingGarage[parkingSpot].VehicleType + parkingGarage[parkingSpot].NumberPlate); // Sedan så skriver vi ut det som vi precis hade sparat. Vi ökar också parkingSpot med 1
                                                                                                                                                           // när vi visar det för att undvika parkering plats 0. 

                        }

                        break; // Vi lämnar case 1 efter att hela grejen är klar. Och återvänder till menyn igen.

                    case "2": // Meny 2: Visa alla parkerade fordon.
                        

                        Console.WriteLine();
                        int arrayEmpty = 0; // Behövde något för att hantera ifall arrayen är tom. Försökte göra en metod men det lyckades inte så denna kod repeteras några gånger på olika ställen.

                        for (int i = 0; i < parkingGarage.Length; i++) // Loop genom hela arrayen.
                        {
                            if (parkingGarage[i] == null) // En if sats som hoppar över alla null platser i arrayen och samtidigt räknar på arrayEmpty.
                            {
                                arrayEmpty++;                                
                                continue;
                            }
                            else // Annars så skriver vi ut alla fordon som hittas med platsen + 1 (För att undvika plats nummer 0) och samtidigt så skriver den ut alla bilar som hittas.
                            {
                                Console.WriteLine($"{i + 1}. {parkingGarage[i].VehicleType + parkingGarage[i].NumberPlate}");
                            }
                        }
                        if (arrayEmpty == 100) // Om if satsen kördes 100 gånger så betyder det att arrayen var tom och då kan vi skriva ut att parkeringen är tom.
                        {
                            Console.WriteLine("Parkering är tom");
                        }

                        break;

                    case "3": // Meny 3: Flytta ett fordon.

                        // Lite lätta frågor och samtidigt så sparar vi in informationen för vår metod.
                        Console.WriteLine();
                        Console.WriteLine("Vilket fordon vill du flytta? Ange parkerings ruta.");
                        int currentParkingSpot = TryParse(Console.ReadLine());
                        Console.WriteLine();
                        Console.WriteLine("Vilken parkerings ruta vill du flytta fordonet till?");
                        int newParkingSpot = TryParse(Console.ReadLine());
                        Console.WriteLine();

                        MoveVehicle(parkingGarage, currentParkingSpot, newParkingSpot); // Vi skickar in de sparade värden in i vår metod och kör metoden. Allt görs inom metoden så kan vi bara lämna vårt Case 3.

                        break;
                    case "4": // Meny 4: Leta efter ett fordon.

                        // Enkel fråga och sedan så sparas informationen för vår metod igen precis som i case 3.
                        Console.WriteLine("Skriv in registreringsnummret på fordonet du söker efter:");  
                        string searchedVehicle = Console.ReadLine(); 
                        SearchVehicle(parkingGarage, searchedVehicle); // Vi skickar med parkeringsgarage arrayen och strängen där vi har sparat registreringsnummret.

                        break;

                    case "5": // Meny 5: Ta ut ett fordon.

                        // Samma sak görs här, en fråga ställs och sedan så går det till metoden.
                        Console.WriteLine("Skriv in registreringsnummret på fordonet som ska lämna.");
                        string exitingVehicle = Console.ReadLine();
                        VehicleExit(parkingGarage, exitingVehicle);
                        break;

                    case "6": // Meny 6: Avsluta programmet.

                        Console.WriteLine("Programmet avslutas!");
                        menuSwitch = false; // Vi sätter vår menuswitch till false vilket gör vår while loop till false och därmed avslutar programmet. 
                        break;

                    default: // En default case som skriver ut att det vara finns 6 alternativ.
                        Console.WriteLine("Välj mellan 1-6 för att gå till någon meny.");
                        break;
                }
            }

        }

        // Case 3 METOD. 
        // Vi skapar metoden och tar in vår Vehicles array vilket är parkinggarage, vi tar också in parkeringsplatsen fordonet vi vill flytta på står och det nya parkeringplatsen där den ska flyttas till.
        public static void MoveVehicle(Vehicles[] parkingGarage, int currentParkingSpot, int newParkingSpot) 
        {

            currentParkingSpot -= 1; // Vi minskar värdet med 1 för att vi har visat alla värden med + 1 överallt.
            newParkingSpot -= 1; // Vi gör samma sak med den nya parkeringsplatsen som väljs för att vi kommer visa det med +1 senare iallafall.

            try // Vi kör en try block för att undvika NullReferenceException. 
            {

                // Sedan så körs olika situationer i olika if och else if block.
                if (parkingGarage[newParkingSpot] == null && parkingGarage[currentParkingSpot] != null) // Vi flyttar fordonet från en parkeringsplats till en annan om nya parkeringsplatsen är tom.
                {
                    parkingGarage[newParkingSpot] = parkingGarage[currentParkingSpot]; // Vi flyttar bara den till nya array värdet och skriver ut det på nästa rad. 
                    Console.WriteLine($"Fordonet: {parkingGarage[currentParkingSpot].VehicleType + parkingGarage[currentParkingSpot].NumberPlate} från parkeringplats {currentParkingSpot + 1} har flyttats till parkeringplats nummer {newParkingSpot + 1}");
                    parkingGarage[currentParkingSpot] = null; // Sedan så sätter vi värdet på currentParkingspot till null, CurrentParkingSpot var där fordonet stod tidigare. 
                }
                else if (parkingGarage[newParkingSpot] == null && parkingGarage[currentParkingSpot] == null) // Här kollar vi om det verkligen finns ett fordon på platsen vi anger. NewParkingSpot behöver inte stå med men lämnade det iallafall.
                {                                                                                                       
                    Console.WriteLine($"Det finns inget fordon på parkeringplats nummer: {currentParkingSpot + 1}");
                }
                else if (parkingGarage[newParkingSpot] != null && parkingGarage[currentParkingSpot].VehicleType + parkingGarage[currentParkingSpot].NumberPlate != parkingGarage[newParkingSpot].VehicleType + parkingGarage[newParkingSpot].NumberPlate)
                { // Här kollar vi om det redan finns en bil på nya parkeringplatsen som har angetts och att det inte är samma bil som försöker flyttas till samma plats. 
                    Console.WriteLine($"Parkeringplats: {newParkingSpot + 1} hade redan ett fordon: {parkingGarage[currentParkingSpot].VehicleType + parkingGarage[currentParkingSpot].NumberPlate}");
                }
                else if (parkingGarage[currentParkingSpot].VehicleType + parkingGarage[currentParkingSpot].NumberPlate == parkingGarage[newParkingSpot].VehicleType + parkingGarage[newParkingSpot].NumberPlate) 
                { 
                    Console.WriteLine("Fordonet står redan på den plats du försöker flytta den till."); // Om det är samma fordon som försöker flyttas till samma plats så skriver vi ut det.
                }
            }
            catch (Exception)
            {
                
                // I catch blocked så hanterar vi felen som uppstod utan en try catch block.
                                
                // Om värdet som anges är större eller mindre än array längden så skriver vi ut att det antingen inte finns en parkeringplats som angavs i currentparkingspot för att det var därför vi hamnade i catch blocket..
                // Om värdet på currentparkingspot är okej så har vi hamnat där pga att newparkingspot hade för lågt eller högt värde och då så skriver vi ut det istället. 
                if (currentParkingSpot <= 0 || currentParkingSpot > parkingGarage.Length)
                {
                    Console.WriteLine($"Det finns ingen parkeringplats med nummer: {currentParkingSpot + 1}");
                }
                else
                {
                    Console.WriteLine($"Det finns ingen parkeringplats med nummer: {newParkingSpot + 1}");
                }
            }

        }

        public static int RandomNumber(int minValue, int maxValue) // En metod som underlättar skapandet av random nummer och som också tar med den sista siffran.
        {
            Random rnd = new Random();
            int randomNumber = rnd.Next(minValue, maxValue + 1);
            return randomNumber;
        }


        // Används i case 1: 
        // Vi kommer in i den här metoden om vårt random nummer blir det samma som förra gången och det redan står ett fordon där sedan tidigare. 
        // Då loopar vi istället igenom hela arrayen och hittar första lediga platsen och ställer fordonet där.

        public static void ParkingOccupied(Vehicles[] parkingGarage, Vehicles vehicle) // Vi tar in vilken array vi vill arbeta med, vilken vehicle vi analyserar
        {

            bool parkingFull = true; // Vi skapar en parkering full bool som kollar ifall parkeringen är full eller ej.
            for (int i = 0; i < parkingGarage.Length; i++) // Vi loopar igenom hela arrayen.
            {
                if (parkingGarage[i] == null)  // Kollar om platsen är null
                {
                    parkingGarage[i] = vehicle; // Om det är det så ställer vi fordonet där och skriver ut det.
                    Console.WriteLine();
                    Console.WriteLine((i + 1) + ". " + parkingGarage[i].VehicleType + parkingGarage[i].NumberPlate);
                    parkingFull = false; // Vi sätter också variabeln till false i loopen för att indikera att parkeringen inte är full så att det inte skriver ut att parkeringen var full.
                    break; // Sedan så lämnar vi loopen för att det inte finns någon mening att fortsätta loopa då vi redan har hittat en plats att ställa fordonet på.
                }

            }
            if (parkingFull) // Om parkeringen däremot skulle vara full så skriver vi ut det.
            {
                Console.WriteLine("Parkeringen är full");
            }

        }

        // Används i case 4:

        public static void SearchVehicle(Vehicles[] parkingGarage, string searchedVehicle) // Vi tar in parkeringsGarage in i vår metod där alla fordon är och searchedVehicle som innehåller registreringsnummret.
        {

            searchedVehicle.Trim().ToLower(); // Vi trimmar vår string så ifall den innehåller några blanka mellanslag så tas det bort och samtidigt kör en ToLower.
            int arrayEmpty = 0; // Vi skapar en till arrayEmpty för att se ifall arrayen är tom eller ej.
            for (int i = 0; i < parkingGarage.Length; i++)
            {

                if (parkingGarage[i] == null) // Vi plussar på vår arrayEmpty och samtidigt så skippar vi alla tomma element.
                {
                    arrayEmpty++;
                    continue;
                }
                else // Om elementet inte är tomt så börjar vi undersöka platsen.
                {
                    if (searchedVehicle == parkingGarage[i].NumberPlate) // Vi kolllar om numberplate matchar med sökningen. 
                    {
                        Console.WriteLine($"Fordonet {searchedVehicle} hittades på parkeringplats: {i + 1}"); // Om det gör det så skriver vi ut det och lämnar loopen.
                        break;
                    }
                    else if (searchedVehicle == parkingGarage[i].VehicleType + parkingGarage[i].NumberPlate) // Vi tar den här gången instället hela strängen med typ CAR#RegNummer och skriver ut det.
                    {
                        Console.WriteLine($"Fordonet {searchedVehicle} hittades på parkeringplats: {i + 1}");
                    }
                    else // Annars om det inte hittas så skriver vi helt enkelt bara ut det.
                    {
                        Console.WriteLine($"Fordonet med registreringsnummret {searchedVehicle} finns inte i parkeringen.");
                    }

                }

            }
            if (arrayEmpty == 100) // Om parkeringen är tom så skrivs det ut, vi avgör det med arrayEmpty++ som ökas i vår if sats innan den fortsätter i nästa varv i loopen.
            {
                Console.WriteLine("Parkeringen är tom");
            }

        }

        // Används i case 5:
        
        public static void VehicleExit(Vehicles[] parkingGarage, string searchedVehicle) // Tar emot parkingGarage arrayen och en string på fordonet som ska lämna parkeringsplatsen.
        {
            searchedVehicle.Trim().ToLower().ToLower(); // Vi trimmar och kör en lower på strängen igen som vi gjorde tidigare för att undvika att det blir fel.
            int arrayEmpty = 0; // Precis som tidigare så gör vi en arrayEmpty för att kolla om det är tomt eller ej.
            for (int i = 0; i < parkingGarage.Length; i++) 
            {
                // Samma sak händer här som i de tidigare funktionerna. Skippar varje null element och fortsätter.
                if (parkingGarage[i] == null)
                {
                    arrayEmpty++;
                    continue;
                }
                else
                {
                    // Koden är nästan det samma som searchVehicle metoden, bara att vi tar bort elementet om det hittas och skriver ut att fordonet har lämnats. Jag tror att det här skulle kunna lösas bättre om man hade använt interface? 
                    
                    if (searchedVehicle == parkingGarage[i].NumberPlate) 
                    {
                        Console.WriteLine($"Fordonet {searchedVehicle} lämnade parkeringplats: {i + 1}");
                        parkingGarage[i] = null; // Skillnaden är att vi tar bort fordonet om det hittas genom att sätta den till null så att det kan återanvändas. 
                        break;
                    }
                    else if (searchedVehicle == parkingGarage[i].VehicleType + parkingGarage[i].NumberPlate)
                    {
                        Console.WriteLine($"Fordonet {searchedVehicle} lämnade parkeringplats: {i + 1}");
                        parkingGarage[i] = null;
                    }
                    else
                    {
                        Console.WriteLine($"Fordonet med registreringsnummret {searchedVehicle} finns inte i parkeringen."); // Om det inte hittas så skriver vi ut att det inte fanns.
                    }

                }

            }
            if (arrayEmpty == 100) // Precis som tidigare så har vi också en kontroll för att se att parkeringen inte är tom...
            {
                Console.WriteLine("Parkeringen är tom");
            }


        }


        // En egen liten TryParse metod för att undvika ha alla out int variabler överallt vilket gör det lite lättare att samla alla incorrecta input på samma ställe och skriva ut att det är ogiltig inmatning.
        
        public static int TryParse(string value) 
        {
            bool success = int.TryParse(value, out int result); 

            if (success) 
            {
                return result; // Vi skickar tillbaka resultatet om vår TryParse metod går igenom och det var en giltig inmatning.
            }
            else
            {
                Console.WriteLine("Ogiltig inmatning"); // Annars så skriver vi ut att det var ogiltig inmatning. 
                return result; // Och det finns inte så mycket val och därför så skickar jag med result också för att jag måste returnera någonting. 
            }
        }
                
        public static void DisplayMenu() // En simple meny.
        {
            Console.WriteLine();
            Console.WriteLine("Meny 1: Lägg till ett nytt fordon.");
            Console.WriteLine("Meny 2: Visa alla parkerade fordon.");
            Console.WriteLine("Meny 3: Flytta ett fordon.");
            Console.WriteLine("Meny 4: Leta efter ett fordon.");
            Console.WriteLine("Meny 5: Ta ut ett fordon.");
            Console.WriteLine("Meny 6: Avsluta programmet.");
            Console.WriteLine("Vilken meny vill du gå till?"); 
 
        }

    }

}

*/