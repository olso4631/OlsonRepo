using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FlooringProgram.Models;
using FlooringProgram.Models.Interfaces;

namespace FlooringProgram.Data.Products
{
    class ProductFileRepositoryTest : IProductFileRepository    

    {
        public List<Product> GetProducts()
        {
            return new List<Product>()
            {
                new Product() { ProductType = "WOOD", LaborCost = 4.75M, MaterialCost = 5.15M  },
                new Product() { ProductType = "TILE", LaborCost = 4.15M, MaterialCost = 3.50M  },
                new Product() { ProductType = "CARPET", LaborCost = 2.10M, MaterialCost = 2.25M  },
                new Product() { ProductType = "LAMINATE", LaborCost = 2.10M, MaterialCost = 1.75M  }


            };

        }
    }
}
