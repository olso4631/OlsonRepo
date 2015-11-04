using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FlooringProgram.Models;
using FlooringProgram.UI.Menu_Options;


namespace FlooringProgram.UI.FormatAndValidate
{
    public class OrderFormat
    {

        public void OrderDisplay(Order order)
        {
            Console.WriteLine(@"Order number: {0}
             Customer Name: {1} 
             State: {2}
             Taxrate: {3:c}
             Product Type: {4}
             Material Cost Per Square Foot: {5:c}
             Labor Cost Per Square Foot: {6:c}
             Area: {7}
             Material Cost: {8:c}
             Labor Cost: {9:c}
             Tax: {10:C}
             Total: {11:C}"
, order.OrderNumber, order.CustomerName, order.State, order.TaxRate, order.ProductType,
order.MaterialCost, order.LaborCostPerSquareFoot, order.Area, order.MaterialCost,
order.LaborCost, order.Tax, order.Total);

            Console.WriteLine("Press enter to return to main menu:");
            string we = Console.ReadLine();

            if (string.IsNullOrEmpty(we))
            {

                DisplayMenu m = new DisplayMenu();
                m.MainMenu();
            }

        }

    }
}
