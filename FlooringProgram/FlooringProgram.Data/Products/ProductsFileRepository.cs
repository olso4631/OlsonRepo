using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FlooringProgram.Models;
using System.IO;
using FlooringProgram.Models.Interfaces;



namespace FlooringProgram.Data.Products
{
    public class ProductsFileRepository : IProductFileRepository
    {
        public List<Product> GetProducts()
        {
            List<Product> products = new List<Product>();

            string[] data = File.ReadAllLines(@"Data\Products.txt");
            for (int i = 1; i < data.Length; i++)
            {
                string[] row = data[i].Split(',');

                Product toAdd = new Product();
                toAdd.ProductType = row[0];
                toAdd.MaterialCost = decimal.Parse(row[1]);
                toAdd.LaborCost = decimal.Parse(row[2]);

                products.Add(toAdd);
                
            }

            return products;
        }

    }
}
