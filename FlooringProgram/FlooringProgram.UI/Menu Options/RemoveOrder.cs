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
    public class RemoveOrder
    {
        string OrderNumber;
        string OrderDate;
        int output;
        bool goodNumber;

        public void RemoveTheOrder()
        {
            OrderOperations orderOperations = new OrderOperations();

            Validate validate = new Validate();
            AddEntry display = new AddEntry();
            //bool alreadydisplayed = false;
            bool validinput = false;
            bool goodDate = false;

            Console.WriteLine("REMOVE ORDER");
            Console.WriteLine();


            do
            { 
                OrderDate = validate.PromptforDate("Please enter the Date of the order MM/DD/YYYY");
                Console.WriteLine("Enter Order Number:");
                OrderNumber = Console.ReadLine();
                goodNumber = int.TryParse(OrderNumber, out output);
                if (validinput = (goodNumber == true))
                {
                    goodDate = (orderOperations.FindOrder(OrderDate, output));
                }
               if (goodDate == false)
                {
                    Console.WriteLine("That was not a valid order!");
                    //Console.ReadLine();
                }

            } while (!goodDate && !goodNumber);

            if (goodDate == true)
            {
                Console.WriteLine("Would you like to delete order? y/n");
                string answer;
                answer = Console.ReadLine();
                if (answer == "y")
                {
                    //string OrderDate = validate.PromptforDate("Please enter the Date MM/DD/YYYY");
                    //decimal ordnum = validate.PromptforDecimal("EnterOrder Number: ");
                    List<Order> removeO = orderOperations.LoadOrdersFromFile();
                    var remove = orderOperations.LoadOrdersFromFile();
                    var resultsr = from order in removeO
                                   where order.OrderDate != OrderDate || order.OrderNumber != output
                                   select new
                                   {
                                       order.OrderDate,
                                       order.OrderNumber,
                                       order.CustomerName,
                                       order.State,
                                       order.TaxRate,
                                       order.Tax,
                                       order.ProductType,
                                       order.Area,
                                       order.CostPerSquareFoot,
                                       order.LaborCost,
                                       order.LaborCostPerSquareFoot,
                                       order.MaterialCost,
                                       order.Total
                                   };

                    using (StreamWriter ord = new StreamWriter("Orders.txt"))
                    {
                        foreach (var order in resultsr)
                        {
                            ord.WriteLine(order.OrderDate + "," + order.OrderNumber + "," + order.CustomerName + "," + order.State + "," +
                                +order.TaxRate + "," + order.Tax + "," + order.ProductType + "," + order.Area + "," + order.CostPerSquareFoot + "," + order.LaborCost + "," + order.LaborCostPerSquareFoot + "," +
                                 order.MaterialCost + "," + order.Total);
                        }
                        Console.WriteLine("Your order has been removed!");
                        Console.ReadLine();
                    }
                }
                else
                {
                    DisplayMenu m = new DisplayMenu();
                    m.HomeMenu();
                }
            }
            else
            {
                Console.WriteLine("That is not a valid order!");
            }
        }
    }
}


