using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FlooringProgram.Models.Interfaces;
using System.IO;
using FlooringProgram.Models;




namespace FlooringProgram.Data.Orders
{
    public class OrderFileRepository : IOrderFileRepository
    {
        public List<Order> LoadOrders(string Orderdate)

        {
            List<Order> order = new List<Order>();

            string[] data = File.ReadAllLines("Orders.txt");
            for (int i = 1; i < data.Length; i++)
            {
                string[] row = data[i].Split(',');

                Order toAdd = new Order();
                toAdd.OrderDate = row[0];
                toAdd.OrderNumber = Convert.ToInt32(row[1]);
                toAdd.CustomerName = row[2];
                toAdd.State = row[3];
                toAdd.TaxRate = decimal.Parse(row[4]);
                toAdd.Tax = decimal.Parse(row[5]);
                toAdd.ProductType = row[6];
                toAdd.Area = decimal.Parse(row[7]);
                toAdd.CostPerSquareFoot = decimal.Parse(row[8]);
                toAdd.LaborCostPerSquareFoot = decimal.Parse(row[9]);
                toAdd.MaterialCost = decimal.Parse(row[10]);
                toAdd.LaborCost = decimal.Parse(row[11]);
                toAdd.Total = decimal.Parse(row[12]);



                order.Add(toAdd);
            }

            return order;
        }
        /*public void AddOrder(Order neworder)
        {
            public List<Order> _allOrders = new List<Order>();
        _allOrders.Add(neworder);
           */
    }
}