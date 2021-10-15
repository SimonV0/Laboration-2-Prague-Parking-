using System;

namespace Prague_Parking_1._0
{
    class Vehicles
    {

        public string VehicleType { get; set; }
        public string NumberPlate { get; set; }
        public DateTime Time { get; set; } // Skapade ny Time property.

        public Vehicles()
        {

            this.Time = DateTime.Now; // La till detta för att redan vid konstruktionen av fordonet så har den en tidstämpel.
                        
            int type = Program.RandomNumber(0, 8);

            if (type <= 5)
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
