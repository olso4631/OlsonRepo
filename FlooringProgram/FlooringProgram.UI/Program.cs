using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FlooringProgram.Models;
using FlooringProgram.BLL;
using FlooringProgram.UI.Menu_Options;

namespace FlooringProgram.UI
{
    class Program
    {
        static void Main(string[] args)
        {
      
            DisplayMenu m = new DisplayMenu();
            m.HomeMenu();
          

           

      /* Console.WriteLine("What day do you want to see the sales of?")

        {
            Console.Write("Enter a state: ");
            string state = Console.ReadLine();

            TaxRateOperations taxOps = new TaxRateOperations();
            if (taxOps.IsAllowedState(state))
            {
                Console.WriteLine("That is a valid state");
                TaxRate rate = taxOps.GetTaxRateFor(state);

                Console.WriteLine("The tax rate for {0} is {1:p}", rate.State, rate.TaxPercent);
            }
            else
            {
                Console.WriteLine("That is not a valid state");
            }

            Console.ReadLine();*/
        }
    }
}
