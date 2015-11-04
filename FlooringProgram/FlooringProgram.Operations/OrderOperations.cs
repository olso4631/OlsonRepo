using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FlooringProgram.Models;
using FlooringProgram.Models.Interfaces;
using FlooringProgram.Data.Orders;
using System.IO;


//can't use UI. have to send it here.

namespace FlooringProgram.BLL
{
    public class OrderOperations
    {
        List<Order> ListofOrders = new List<Order>();
        TaxRateOperations taxRateOperations = new TaxRateOperations();
        //IOrderFileRepository _repo = new OrderFileRepositoryFactory();



        public List<Order> LoadOrdersFromFile()
        {
            ListofOrders.Clear();




            using (StreamReader sr = new StreamReader("Orders.txt"))
            {
                string aLine = sr.ReadLine();
                while (aLine != null && aLine != "")
                {

                    string[] cDetails = aLine.Split(',');//splitting file on commas
                    if (cDetails == null)
                    {
                        break;
                    }
                    Order ordersfromfile = new Order()
                    {
                        OrderDate = cDetails[0],
                        OrderNumber = int.Parse(cDetails[1]),
                        CustomerName = cDetails[2],
                        State = cDetails[3],
                        TaxRate = decimal.Parse(cDetails[4]),
                        Tax = decimal.Parse(cDetails[5]),
                        ProductType = cDetails[6],
                        Area = decimal.Parse(cDetails[7]),
                        CostPerSquareFoot = decimal.Parse(cDetails[8]),
                        LaborCost = decimal.Parse(cDetails[9]),
                        LaborCostPerSquareFoot = decimal.Parse(cDetails[10]),
                        MaterialCost = decimal.Parse(cDetails[11]),
                        Total = decimal.Parse(cDetails[12])//elements that were split from commas are set to the order properties
                    };

                    ListofOrders.Add(ordersfromfile);//loaded list from the file "Order.txt" and writing it to our list "listoforders"

                    aLine = sr.ReadLine(); //continue to the next line in the code and continue

                }

            }
            return ListofOrders;
        }

        public bool DisplayMyGDOrder(string orderDate)//10/23/2015-this now loa
        {
            IOrderFileRepository _repo = OrderFileRepositoryFactory.GetOrderRepository();
            var DisplayOrder = _repo.LoadOrders(orderDate);

            foreach (Order order in DisplayOrder)
            {
                Console.Clear();
                Console.WriteLine("----------------------------------------------------------------------");
                Console.WriteLine("Order Number: {0}", order.OrderNumber);
                Console.WriteLine("Customer Name: {0}", order.CustomerName);
                Console.WriteLine("State: {0}", order.State);
                Console.WriteLine("Tax Rate: {0:p2}", order.TaxRate);
                Console.WriteLine("Tax: {0:c}", order.Tax);
                Console.WriteLine("Product Type: {0}", order.ProductType);
                Console.WriteLine("Area: {0:n0}", order.Area);
                Console.WriteLine("Cost Per Square Foot: {0:c}", order.CostPerSquareFoot);
                Console.WriteLine("Labor Cost: {0:c}", order.LaborCost);
                Console.WriteLine("Labor Cost Per Square Foot: {0:c}", order.LaborCostPerSquareFoot);
                Console.WriteLine("Material Cost: {0:c}", order.MaterialCost);
                Console.WriteLine("Total: {0:c}", order.Total);
                Console.WriteLine("Order Date: {0}", order.OrderDate);
                Console.WriteLine("");
            }
            return true;//probably need an IF STATEMENT if there's actually an order. etc.
        }






        public void CreateOrder(AddEntryRequest request)//takes new order from prompts in UI and assigns the properties to it
        {
            Order newOrder = new Order();
            newOrder.OrderDate = request.OrderDate;
            newOrder.CustomerName = request.CustomerName;
            newOrder.State = request.State;
            //newOrder.TaxRate=a method and conditional
            newOrder.ProductType = request.ProductType;

            if (request.ProductType == "WOOD")  ////changed to newOrder.Product Type
            {
                newOrder.CostPerSquareFoot = 5.15m;
                newOrder.LaborCostPerSquareFoot = 4.75m;
            }
            else if (request.ProductType == "TILE")  // added else if; CHANGED PRODUCT NAMES TO CAPS 
            {
                newOrder.CostPerSquareFoot = 3.50m;
                newOrder.LaborCostPerSquareFoot = 4.15m;
            }
            else if (request.ProductType == "CARPET") // added else if; CHANGED PRODUCT NAMES TO CAPS 
            {
                newOrder.CostPerSquareFoot = 2.25m;
                newOrder.LaborCostPerSquareFoot = 2.10m;
            }
            else
            {
                newOrder.CostPerSquareFoot = 1.75m;
                newOrder.LaborCostPerSquareFoot = 2.10m;
            }

            newOrder.Area = request.Area;
            /////////////////////////////
            if (request.State == "OH")
            {
                newOrder.TaxRate = 0.0625M;
                // taxRateOperations.IsAllowedState("OH");


            }
            else if (request.State == "PA")///made them else if
            {
                newOrder.TaxRate = 0.0675M;
            }
            else if (request.State == "MI")// made this else if
            {
                newOrder.TaxRate = 0.0575M;
            }
            else
            {
                newOrder.TaxRate = 0.06M;
            }
            newOrder.MaterialCost = (newOrder.Area * newOrder.CostPerSquareFoot);
            newOrder.LaborCost = (newOrder.Area * newOrder.LaborCostPerSquareFoot);
            newOrder.Tax = (newOrder.Area * newOrder.TaxRate);//Addded this line to calculate total tax
            newOrder.Total = ((newOrder.MaterialCost + newOrder.LaborCost + newOrder.Tax));//changed this line to incorporate tax in the total


            ListofOrders.Add(newOrder);//add my newly created order to my list.
            WriteToFile(ListofOrders);//send my new list with orders from the file and newly added orders and send it my
                                      //writetofile to send my list to the file again.



        }

        public void WriteToFile(List<Order> ListofOrders)//take my list and write it to the file
        {
            //LoadOrdersFromFile();

            using (StreamWriter ord = new StreamWriter("Orders.txt"))
            {
                foreach (Order order in ListofOrders)
                {
                    ord.WriteLine(order.OrderDate + "," + (order.OrderNumber + 1) + "," + order.CustomerName + "," + order.State + "," +
                        +order.TaxRate + "," + order.Tax + "," + order.ProductType + "," + order.Area + "," + order.CostPerSquareFoot + "," + order.LaborCost + "," + order.LaborCostPerSquareFoot + "," +
                         order.MaterialCost + "," + order.Total);

                }

            }
        }
        public bool FindOrder(string orderDate, int orderNumber)
        {
            var orders = LoadOrdersFromFile();

            var results = (from order in orders
                           where order.OrderDate == orderDate && (order.OrderNumber == orderNumber)
                           select order).ToList();

            //return results;
            if (results.Count != 0)
            {
                DisplayOrder(results);
                return true;
            }
            else
            {
                return false;
            }
        }



        public bool DisplayOrder(List<Order> results)
        {
            // bool validDate = false;

            //if (validDate = (results.Count != 0))
            // {

            foreach (var order in results)
            {
                Console.Clear();
                Console.WriteLine("----------------------------------------------------------------------");
                Console.WriteLine("Order Date: {0}", order.OrderDate);
                Console.WriteLine("Order Number: {0}", order.OrderNumber);
                Console.WriteLine("Customer Name: {0}", order.CustomerName);
                Console.WriteLine("State: {0}", order.State);
                Console.WriteLine("Tax Rate: {0:p2}", order.TaxRate);
                Console.WriteLine("Tax: {0:c}", order.Tax);
                Console.WriteLine("Product Type: {0}", order.ProductType);
                Console.WriteLine("Area: {0:n0}", order.Area);
                Console.WriteLine("Cost Per Square Foot: {0:c}", order.CostPerSquareFoot);
                Console.WriteLine("Labor Cost: {0:c}", order.LaborCost);
                Console.WriteLine("Labor Cost Per Square Foot: {0:c}", order.LaborCostPerSquareFoot);
                Console.WriteLine("Material Cost: {0:c}", order.MaterialCost);
                Console.WriteLine("Total: {0:c}", order.Total);
                Console.WriteLine("");


            };
            return true;


        }


        public bool EditOrder(string OrderDate, string PropertyEdit, string userInput)//gonna need the string of orderDate
        {
            Order originalOrder = new Order();
            Order EditedOrder = new Order();

            var orders = LoadOrdersFromFile();//reading the file and putting it into a list

            var results = (from order in orders
                           where order.OrderDate == OrderDate
                           select order).ToList();//create a new list of the order to edit that is selected from the orderdate.

            if (results.Count != 0)//if the list has soemthing in it proceed
            {
                originalOrder = results[0];//set my instantiated (above) order object to the first object in my list (index 1, or [0]).


                if (PropertyEdit == "CUSTOMER NAME")
                {
                    EditedOrder.CustomerName = userInput;

                    EditedOrder.ProductType = originalOrder.ProductType;
                    EditedOrder.OrderDate = originalOrder.OrderDate;
                    EditedOrder.OrderNumber = originalOrder.OrderNumber - 1;
                    EditedOrder.State = originalOrder.State;
                    EditedOrder.Tax = originalOrder.Tax;
                    EditedOrder.TaxRate = originalOrder.TaxRate;
                    EditedOrder.Area = originalOrder.Area;
                    EditedOrder.CostPerSquareFoot = originalOrder.CostPerSquareFoot;
                    EditedOrder.LaborCost = originalOrder.LaborCost;
                    EditedOrder.MaterialCost = originalOrder.MaterialCost;
                    EditedOrder.LaborCostPerSquareFoot = originalOrder.LaborCostPerSquareFoot;
                    EditedOrder.Total = originalOrder.Total;
                    EditedOrder.OrderDate = originalOrder.OrderDate;
                }

                if (PropertyEdit == "STATE")//need to change since the taxes are going to be different
                {
                    if (userInput == "OH")
                    {
                        EditedOrder.TaxRate = 0.0625M;
                        EditedOrder.State = userInput;



                    }
                    else if (userInput == "PA")///made them else if
                    {
                        EditedOrder.TaxRate = 0.0675M;
                        EditedOrder.State = userInput;
                    }
                    else if (userInput == "MI")// made this else if
                    {
                        EditedOrder.TaxRate = 0.0575M;
                        EditedOrder.State = userInput;
                    }
                    else
                    {
                        EditedOrder.TaxRate = 0.06M;
                        EditedOrder.State = userInput;
                    }


                    EditedOrder.CustomerName = originalOrder.CustomerName;
                    EditedOrder.ProductType = originalOrder.ProductType;
                    EditedOrder.OrderDate = originalOrder.OrderDate;
                    EditedOrder.OrderNumber = originalOrder.OrderNumber - 1;
                    EditedOrder.Area = originalOrder.Area;
                    EditedOrder.Tax = (originalOrder.Area * EditedOrder.TaxRate);
                    EditedOrder.CostPerSquareFoot = originalOrder.CostPerSquareFoot;
                    EditedOrder.LaborCost = originalOrder.LaborCost;
                    EditedOrder.MaterialCost = originalOrder.MaterialCost;
                    EditedOrder.LaborCostPerSquareFoot = originalOrder.LaborCostPerSquareFoot;
                    EditedOrder.Total = (originalOrder.MaterialCost + EditedOrder.LaborCost + EditedOrder.Tax);
                    EditedOrder.OrderDate = originalOrder.OrderDate;
                }

                if (PropertyEdit == "AREA")
                {
                    decimal newArea;

                    bool dec = decimal.TryParse(userInput, out newArea);
                    if (dec == true)
                    {
                        EditedOrder.Area = newArea;

                        EditedOrder.CustomerName = originalOrder.CustomerName;
                        EditedOrder.ProductType = originalOrder.ProductType;
                        EditedOrder.OrderDate = originalOrder.OrderDate;
                        EditedOrder.OrderNumber = originalOrder.OrderNumber - 1;
                        EditedOrder.Tax = originalOrder.Tax;
                        EditedOrder.TaxRate = originalOrder.TaxRate;
                        EditedOrder.LaborCostPerSquareFoot = originalOrder.LaborCostPerSquareFoot;
                        EditedOrder.CostPerSquareFoot = originalOrder.CostPerSquareFoot;
                        EditedOrder.LaborCost = (EditedOrder.Area * originalOrder.LaborCostPerSquareFoot);
                        EditedOrder.MaterialCost = (EditedOrder.Area * originalOrder.CostPerSquareFoot);
                        EditedOrder.Total = (EditedOrder.MaterialCost + EditedOrder.LaborCost + EditedOrder.Tax);
                        EditedOrder.State = originalOrder.State;

                    }
                    else
                    {
                        Console.WriteLine("That won't do");
                    }

                }

                if (PropertyEdit == "PRODUCT")
                {
                    if (userInput == "WOOD")
                    {
                        EditedOrder.CostPerSquareFoot = 5.15m;
                        EditedOrder.LaborCostPerSquareFoot = 4.75m;
                        EditedOrder.ProductType = userInput;
                    }
                    else if (userInput == "TILE")
                    {
                        EditedOrder.CostPerSquareFoot = 3.50m;
                        EditedOrder.LaborCostPerSquareFoot = 4.15m;
                        EditedOrder.ProductType = userInput;
                    }
                    else if (userInput == "CARPET")
                    {
                        EditedOrder.CostPerSquareFoot = 2.25m;
                        EditedOrder.LaborCostPerSquareFoot = 2.10m;
                        EditedOrder.ProductType = userInput;
                    }
                    else
                    {
                        EditedOrder.CostPerSquareFoot = 1.75m;
                        EditedOrder.LaborCostPerSquareFoot = 2.10m;
                        EditedOrder.ProductType = userInput;
                    }
                    EditedOrder.ProductType = userInput;

                    EditedOrder.CustomerName = originalOrder.CustomerName;
                    EditedOrder.OrderDate = originalOrder.OrderDate;
                    EditedOrder.OrderNumber = originalOrder.OrderNumber - 1;
                    EditedOrder.Area = originalOrder.Area;
                    EditedOrder.State = originalOrder.State;
                    EditedOrder.TaxRate = originalOrder.TaxRate;
                    EditedOrder.LaborCost = (originalOrder.Area * EditedOrder.LaborCostPerSquareFoot);
                    EditedOrder.MaterialCost = (originalOrder.Area * EditedOrder.CostPerSquareFoot);
                    EditedOrder.Tax = (originalOrder.Area * originalOrder.TaxRate);
                    EditedOrder.Total = (EditedOrder.MaterialCost + EditedOrder.LaborCost + originalOrder.Tax);


                }



                orders.Add(EditedOrder);
                orders.Remove(originalOrder);
                WriteToFile(orders);

                return true;
            }

            else
            {
                return false;
            }
        }
    }
}