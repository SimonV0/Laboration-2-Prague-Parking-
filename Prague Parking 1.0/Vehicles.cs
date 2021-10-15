using System;

namespace Prague_Parking_1._0
{
    class Vehicles
    {

        public string VehicleType { get; set; }
        public string NumberPlate { get; set; }
                  

        public Vehicles()
        {

            int type = Program.RandomNumber(0, 8);

            if (type <= 7)
            {
                this.VehicleType = "CAR#";
            }
            else
            {
                this.VehicleType = "MC#";
            }
                   
            this.NumberPlate = NumberPlates();
        }


        public string NumberPlates()
        {

            Random rnd = new Random();

            string allowedCharacters = "ABCDEFGHIJKLMNOPQRSTUVWXY123456790";
            string rareCharacters = "ÅÆÇÉÑÜÝßàáâãåæçèéêëñòóôùúûüÿ";
            //string rareCharacters = "ÀÁÂÃÄÅÆÇÈÉÊËÌÍÎÏÐÑÒÓÔÕÖØÙÚÛÜÝßàáâãäåæçèéêëðñòóôõöøùúûüýÿœŠšŸŽž"; // Vissa characters fungerade ej i consolen


            string numberplate = string.Empty; 
            int numberplateLength = rnd.Next(4, 11); 

            for (int i = 0; i < numberplateLength; i++)
            {
                numberplate += allowedCharacters[rnd.Next(allowedCharacters.Length)]; 
            }
            if (numberplate.Length >= 4 && numberplate.Length <= 5) 
            {
                for (int i = 0; i < 2; i++)
                {
                    numberplate += rareCharacters[rnd.Next(rareCharacters.Length)];
                }
            }

            return numberplate; 
        }



    }

}


/////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
                                                                // Kommenterad kod längre ner, går att ta bort kommentarerna och kommentera hela koden där uppe och bara köra direkt då allt innehåll finns med.


/*
using System;

namespace Prague_Parking_1._0
{
    class Vehicles
    {


        // Properties för vår vehicles, skapas så att man kan använda det senare med typ Vehicles.VehicleType och gör så att man kan använda och ändra saker till dessa
        // properties i konstruktorn, det används också för att komma åt en enskild objekts olika egenskaper och det är kanske den största huvud anledningen till att 
        // dessa finns.

        public string VehicleType { get; set; }
        public string NumberPlate { get; set; }


        // Constructor är där man anger olika start värden när ett objekt skapas.

        public Vehicles()
        {

            // Kör en random number från 0 till 8 där alla nummer mellan 0 till 7 skapar det bilar och ibland så kommer det en motorcyckel till parkeringen också.
            int type = Program.RandomNumber(0, 8);

            if (type <= 7)
            {
                this.VehicleType = "CAR#";
            }
            else
            {
                this.VehicleType = "MC#";
            }

            // Vi kör vår numberplate metod på alla våra fordon.
            this.NumberPlate = NumberPlates();
        }


        public string NumberPlates()
        {

            Random rnd = new Random();
            //Tillåtna characters som kan läggas in i vår numberplate.

            string allowedCharacters = "ABCDEFGHIJKLMNOPQRSTUVWXY123456790";
            string rareCharacters = "ÅÆÇÉÑÜÝßàáâãåæçèéêëñòóôùúûüÿ";
            //string rareCharacters = "ÀÁÂÃÄÅÆÇÈÉÊËÌÍÎÏÐÑÒÓÔÕÖØÙÚÛÜÝßàáâãäåæçèéêëðñòóôõöøùúûüýÿœŠšŸŽž"; // Vissa characters fungerade ej i consolen


            string numberplate = string.Empty; // Skapar en tom numberplate som sedan fylls på i vår for loop.
            int numberplateLength = rnd.Next(4, 11); // Skapar en slumpmässig längd på vår numberplate.

            for (int i = 0; i < numberplateLength; i++)
            {
                numberplate += allowedCharacters[rnd.Next(allowedCharacters.Length)]; // Sedan så börjar vi fylla den med alla allowed characters.
            }
            if (numberplate.Length >= 4 && numberplate.Length <= 5) // Om vår numberplate är mellan 4-5 characters lång så lägger vi till 2 extra rare characters. 
            {
                for (int i = 0; i < 2; i++)
                {
                    numberplate += rareCharacters[rnd.Next(rareCharacters.Length)];
                }
            }

            return numberplate; // Vi returnerar vår nyskapade plate.
        }



    }

}
*/