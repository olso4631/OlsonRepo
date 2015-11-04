using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FlooringProgram.UI.FormatAndValidate;
using FlooringProgram.Models;
using System.IO;
using FlooringProgram.BLL;



namespace FlooringProgram.UI.Menu_Options
{
    public class DisplayOrder
    {
        OrderOperations orderOperations = new OrderOperations();
        Validate validate = new Validate();


        bool success = false;
        bool foundOrder = false;


        public void askDay()//change this method name
        {
            Console.WriteLine("DISPLAY AN ORDER");
            Console.WriteLine();
            do
            {

                string orderDate = validate.PromptforDate("Please enter the Date of your Order MM/DD/YYYY");
                int orderNumber = Convert.ToInt32(validate.PromptforDecimal("Please enter your order Number"));


                bool foundOrder = orderOperations.DisplayMyGDOrder(orderDate);//calls method to find the order from the date and order number. 
                Console.WriteLine("Press enter to return to the main menu!");
                Console.ReadLine();

                if (foundOrder == false)
                {
                    Console.Clear();
                    Console.WriteLine("That was not a valid order, please try again! Press enter to try again or Q to return to the main menu!");
                    string display = Console.ReadLine().ToUpper();
                    if (success = (display.Length == 1) && display.Contains("Q"))
                    {
                        continue;
                    }
                }

            } while (success && !foundOrder);

        }
    }

}