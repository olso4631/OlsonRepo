using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FlooringProgram.Models;
using FlooringProgram.Models.Interfaces;

namespace FlooringProgram.Data.Orders
{
    public class OrderFileRepositoryFactory  
    {
        public static IOrderFileRepository GetOrderRepository()
        {
            switch (ConfigurationSettings.GetMode())
            {
                case "Prod":
                    return new OrderFileRepository();
                case "Test":
                    return new OrderFileRepositoryTest();
            }

            return null;
        }
    }
}
