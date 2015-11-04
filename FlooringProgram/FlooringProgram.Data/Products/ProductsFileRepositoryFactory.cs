using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FlooringProgram.Models.Interfaces;
using FlooringProgram.Models;

namespace FlooringProgram.Data.Products
{
    class ProductsFileRepositoryFactory
    {
        public static IProductFileRepository GetOrderRepository()
        {
            switch (ConfigurationSettings.GetMode())
            {
                case "Prod":
                    return new ProductsFileRepository();
                case "Test":
                    return new ProductFileRepositoryTest();
            }

            return null;
        }
    }
}
