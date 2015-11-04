using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlooringProgram.UI.Menu_Options
{
    class QuitDisplayMenu
    {
        public void Quit()
        {


            Console.WriteLine("Would you like to exit the program?");
            string quitProgram = Console.ReadLine().ToUpper();

            if (quitProgram == "Y")
            {
                Console.WriteLine("Hit the enter key to exit:");
                Console.ReadLine();
            }
            else
            {
                DisplayMenu m = new DisplayMenu();
                m.HomeMenu();
            }
                     
        }
    }
}
