using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FlooringProgram.Models;
using FlooringProgram.UI.FormatAndValidate;
using System.IO;
using FlooringProgram.BLL;


namespace FlooringProgram.UI.Menu_Options
{
    public class EditEntry

    {
        DisplayOrder displayorders = new DisplayOrder();

        string userInput;
        string propertyEdit;

        public void EditEntries()
        {

            OrderOperations orderOperations = new OrderOperations();
            Validate validate = new Validate();
            bool goodDate = false;

            Console.WriteLine("EDIT ORDER");
            Console.WriteLine();

            do
            {   

                string OrderDate = validate.PromptforDate("Please enter the date of the order you want to change MM/DD/YYYY");
                int orderNumber = Convert.ToInt32(validate.PromptforDecimal("Please enter your order number"));

                goodDate = orderOperations.FindOrder(OrderDate, orderNumber);

                if (goodDate == true)
                {

                    Console.WriteLine("What would you like to change, Customer Name, State, Area, or Product?");//propertyEdit
                    propertyEdit = Console.ReadLine().ToUpper();//valid info continue, one of these

                    if ((propertyEdit == "CUSTOMER NAME" || propertyEdit == "STATE" || propertyEdit == "AREA" || propertyEdit == "AREA" || propertyEdit == "PRODUCT"))
                    {
                        if (propertyEdit == "CUSTOMER NAME")
                        {
                            Console.WriteLine("What would you like to change it to?");
                            userInput = Console.ReadLine().ToUpper();
                        }

                        if (propertyEdit == "STATE")
                        {
                            userInput = validate.PromptforState("What would you like to change it to? IN, OH, PA, or MI");
                        }

                        if (propertyEdit == "AREA")
                        {
                            decimal userArea;

                            userArea = validate.PromptforDecimal("What would you like to change it to?");
                            userInput = userArea.ToString();
                           
                        }

                        if (propertyEdit == "PRODUCT")
                        {
                            userInput = validate.PromptforProduct("What would you like to change it to? Wood, Tile, Carpet, or Laminate");
                          
                        }
                    }
                    else
                    {

                        Console.WriteLine("That was not a valid choice, please try again!");
                    }
                    orderOperations.EditOrder(OrderDate, propertyEdit, userInput);

                    Console.Clear();
                    Console.WriteLine("Your order has been successfully updated! Press Enter to continue!");
                    string edits = Console.ReadLine().ToUpper();
                }

                else
                {

                    Console.WriteLine("That is not a valid order, please try again!");
                }

            } while (!goodDate);
        }

    }
}


