using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace BattleShip.UI
{
    class UserFiredShots
    {
        bool validShot = false;
        int xNum;
        int yNum;
        

        private void PromptUser()
        {
            do
            {
                Console.WriteLine("Where would you like to  fire, {0} /*current player name*/");
                string PlayerInput = Console.ReadLine().ToUpper();
                string letter = PlayerInput.Substring(0, 1);
                string number = (PlayerInput.Substring(1));

                if ((validShot = (letter == "A" || letter == "B" || letter == "C" || letter == "D" || letter == "E" || letter == "F" || letter == "G" || letter == "H" || letter == "I" || letter == "J") && (number == "1" || number == "2" || number == "3" || number == "4" || number == "5" || number == "6" || number == "7" || number == "8" || number == "9" || number == "10")))
                {
                  int xNum = ParseNumber(number);
                  int  yNum = ParseLetter(letter);
                    Coordinate coordinate = new Coordinate(xNum, yNum);
                    coordinate.XCoordinate = xNum;
                    coordinate.YCoordinate = yNum;

                    

                }

                else

                {
                    Console.WriteLine("That was not a valid choice, please try again! ");

                }
            } while (!validShot);
        }

        public int ParseNumber(string number)
        {
            switch (number)
            {
                case "1":
                    return 1;
                    break;
                case "2":
                    return 2;
                    break;
                case "3":
                    return 3;
                    break;
                case "4":
                    return 4;
                    break;
                case "5":
                    return 5;
                    break;
                case "6":
                    return 6;
                    break;
                case "7":
                    return 7;
                    break;
                case "8":
                    return 8;
                    break;
                case "9":
                    return 9;
                    break;
                case "10":
                    return 10;
                    break;

                default:
                    return 100;
            }
        }

        private int ParseLetter(string letter)
        {

            switch (letter)
            {

                case "A":
                    return 1;
                    break;
                case "B":
                    return 2;
                    break;
                case "C":
                    return 3;
                    break;
                case "D":
                    return 4;
                    break;
                case "E":
                    return 5;
                    break;
                case "F":
                    return 6;
                    break;
                case "G":
                    return 7;
                    break;
                case "H":
                    return 8;
                    break;
                case "I":
                    return 9;
                    break;
                case "J":
                    return 10;
                    break;
                default: return -100;
            }
        }

    }
}
        
    




























//    class UserFiredShots
//    {

//        public static string InputSplit()
//        { do
//            {

//                bool Translation;
//                string Player1;
//                string Player2;

//                Console.WriteLine("Where do you want to fire {0}?", Player1);//taking input where p1 wants to fire. step 1
//                string fireshot1 = Console.ReadLine().ToUpper(); //creating shotsfired1 variable with input from player
//                return fireshot1;
//            }
            

//            public static string Translation(string output)
//        {
//            string Translation = InputSplit().Substring(0, 1);//taking shotsfired and getting first letter from input
//            return Translation;
//        }

//          public static bool Input(string input)
//        {
//            bool ValidInput = false;
//            while (!ValidInput)
         

//            if (Translation == "A"
//                return true;
               
//            }
//            if (InputSplit == "A", "B", "C", "D", "E","F", "G", "H", "I", "J");
//            {
//                return true //I want it to send along to translator, a to 1, b to 2 etc.
//                break;
            
//        }while (!ValidInput);
//    }
//}






