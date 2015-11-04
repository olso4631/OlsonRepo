using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FlooringProgram.BLL;

namespace FlooringProgram.UI.FormatAndValidate
{
    public class Validate
    {
        TaxRateOperations taxRateOperations = new TaxRateOperations();

        public string PromptforString(string message)
        {
            

            bool validInput = false;
            string output = "";

            while (!validInput)
            {
                Console.WriteLine(message);
                output = Console.ReadLine().ToUpper(); ;

                if (string.IsNullOrEmpty(output))
                {
                    Console.WriteLine("Please enter data!");
                }
                else
                {
                    validInput = true;
                }
            }
            return output;

        }

        public string PromptforState(string message)
        {
            

            bool validInput = false;
            string output = "";

            while (!validInput)
            {
                Console.WriteLine(message);
                output = Console.ReadLine().ToUpper();

                if (validInput = (output == "MI" || output == "OH" || output == "PA" | output == "IN"))
                {

                    return output;
                    
                }
                else
                {
                    Console.WriteLine("Please enter a valid State!");
                }
            }
            return "a";
        }

        public string PromptforProduct(string message)
        {
            bool validInput = false;
            string output = "";

            while (!validInput)
            {
                Console.WriteLine(message);
                output = Console.ReadLine().ToUpper();

                if (validInput = (output == "TILE" || output == "WOOD" || output == "CARPET" || output == "LAMINATE"))
                {
                    return output;
                }
                else
                {
                    Console.WriteLine("That was not valid input! Please try again!");
                }
            }
            return output;
        }

        public decimal PromptforDecimal(string message)
        {
            bool validInput = false;
            decimal output = 0;

            while (!validInput)
            {
                Console.WriteLine(message);
                validInput = (decimal.TryParse(Console.ReadLine(), out output));
                //put limits on 
                if (validInput == true)
                {
                    return output;
                }
                else
                {
                    Console.WriteLine("That is not a valid decimal!"); 
                    }
            }

            return output;
        }

        public string PromptforDate(string message)
        {
            bool validinput = false;
            string output = "";
            DateTime time;

            do
            {
                Console.WriteLine(message);
                output = Console.ReadLine();
                bool datetime = DateTime.TryParse(output, out time);
                if (validinput = ((output.Length == 10)
                            && (output.Substring(0, 1) == "0" || output.Substring(0, 1) == "1")
                            && output.Substring(2, 1) == "/"
                            && output.Substring(5, 1) == "/") && datetime == true)
                {
                    return output;
                }
                else
                {
                    Console.WriteLine("That was not a valid date! Please try again MM/DD/YYYY");
                }

            } while (!validinput);
            return "A";
        }
    }  
}



