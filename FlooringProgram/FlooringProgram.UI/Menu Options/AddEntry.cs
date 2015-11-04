using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FlooringProgram.Models;
using FlooringProgram.UI.FormatAndValidate;
using FlooringProgram.Models.Interfaces;
using FlooringProgram.Data.Orders;
using System.IO;
using FlooringProgram.BLL;
namespace FlooringProgram.UI.Menu_Options
{
    public class AddEntry
    {
        public void AddOrder()
        {
            OrderOperations orderOperations = new OrderOperations();
            orderOperations.LoadOrdersFromFile();
            var request = new AddEntryRequest();
            CreateOrderRequest(request);///not quite sure on the order
            orderOperations.CreateOrder(request);
           
        }
        public void CreateOrderRequest(AddEntryRequest request)
        {
            Validate validate = new Validate();

            Console.WriteLine("ORDER ENTRY");
            Console.WriteLine();

          
            request.CustomerName = validate.PromptforString("Enter customer name: ");
            request.State = validate.PromptforState("Please enter the state abbreviation: IN, PA, MI, OH");
            request.ProductType = validate.PromptforProduct("Please Enter a product: Wood, Tile, Laminate, Carpet ");
            request.Area = validate.PromptforDecimal("Please Enter the Area:");
            request.OrderDate = validate.PromptforDate("Please enter the Date MM/DD/YYYY");
            Console.Clear();
            Console.WriteLine(@"Your order details are:
Customer Name: {0}
State: {1}
Product Type: {2}
Area: {3:n0}
Order Date: {4}", request.CustomerName, request.State, request.ProductType, request.Area, request.OrderDate);

            Console.WriteLine();

            Console.WriteLine("Would you like to add this order? (Y)es or (N)o?");
            string newOrderReply = Console.ReadLine().ToUpper();

            if (newOrderReply == "Y")
            {
                OrderOperations orderOperations = new OrderOperations();
                orderOperations.CreateOrder(request);
            }

            else
            {
                DisplayMenu returnToMain = new DisplayMenu();
                returnToMain.MainMenu();
            }
        }

    }
}
