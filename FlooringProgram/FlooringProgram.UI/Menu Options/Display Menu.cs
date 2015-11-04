using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlooringProgram.UI.Menu_Options
{
    public class DisplayMenu
    {

        public int HomeMenu()


        {
            int userSelection = 0;
            do
            {
                userSelection = MainMenu();
                DisplayOrder display = new DisplayOrder();
                AddEntry addorder = new AddEntry();
                RemoveOrder remove = new RemoveOrder();
                QuitDisplayMenu quit = new QuitDisplayMenu();
                EditEntry edit = new EditEntry();

                switch (userSelection)
                {
                    case 1:
                        Console.Clear();
                        display.askDay();
                        break;
                    case 2:
                        Console.Clear();
                        addorder.AddOrder(); // This adds order
                        break;
                    case 3:
                        Console.Clear();
                        edit.EditEntries();
                        break;
                    case 4:
                        Console.Clear();
                        remove.RemoveTheOrder();
                        break;
                    case 5:
                        quit.Quit();
                        break;

                };
            } while (userSelection != 5);

            return 0;
        }

        public int MainMenu()
        {
            int option = 0;
            bool validInput = false;
            bool alreadyDisplayed = false;
            do
            {
                Console.Clear();
                Console.WriteLine("Welcome to the Flooring Program");
                Console.WriteLine();
                Console.WriteLine("1. Display an Order");
                Console.WriteLine("2. Add Order");
                Console.WriteLine("3. Edit Order");
                Console.WriteLine("4. Delete Order");
                Console.WriteLine("5. Quit");
                Console.WriteLine();
                if (alreadyDisplayed)
                {
                    Console.Write("That's not a real thing, try again: ");
                }
                else
                {
                    Console.Write("Please make a selection: ");
                }

                string strOption = Console.ReadLine();
                validInput = int.TryParse(strOption, out option);

                alreadyDisplayed = true;

            } while (!(validInput && (option > 0 && option < 6)));

            return option;
        }


    }
}