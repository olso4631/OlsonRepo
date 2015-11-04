using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FlooringProgram.Models;
using FlooringProgram.Models.Interfaces;

namespace FlooringProgram.Data.Orders
{
    class OrderFileRepositoryTest : IOrderFileRepository
    {
        public List<Order> LoadOrders(string Orderdate)
        {
            return new List<Order>()
            {
                new Order() {OrderNumber = 1, CustomerName = "Bob BlahBlah", State = "OH", TaxRate = 0.05M, ProductType = "TILE",
                    Area = 50, CostPerSquareFoot = 3.50M,  LaborCostPerSquareFoot = 4.15M, MaterialCost = 175.00M, LaborCost = 207.50M,
                    Tax = 19.13M, Total = 401.63M, OrderDate = "09/22/2015"},

                new Order() {OrderNumber = 1, CustomerName = "Joe BlahBlah", State = "OH", TaxRate = 0.05M, ProductType = "TILE",
                    Area = 50, CostPerSquareFoot = 3.50M,  LaborCostPerSquareFoot = 4.15M, MaterialCost = 175.00M, LaborCost = 207.50M,
                    Tax = 19.13M, Total = 401.63M, OrderDate = "09/25/2015"},

                new Order() {OrderNumber = 1, CustomerName = "Jen BlahBlah", State = "OH", TaxRate = 0.05M, ProductType = "TILE",
                    Area = 50, CostPerSquareFoot = 3.50M,  LaborCostPerSquareFoot = 4.15M, MaterialCost = 175.00M, LaborCost = 207.50M,
                    Tax = 19.13M, Total = 401.63M, OrderDate = "10/25/2015"},


            };

           
        }
    }
}
