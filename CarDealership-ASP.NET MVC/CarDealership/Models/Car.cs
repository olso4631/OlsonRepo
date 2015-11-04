using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace CarDealership.Models
{
    public class Car : IValidatableObject
    {
        public int Id { get; set; }
        public string Make { get; set; }
        public string Model { get; set; }
        public string Year { get; set; }
        public string ImageUrl { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public decimal? Price { get; set; }
        public decimal SalesTax { get; set; }





        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            List<ValidationResult> carErrors = new List<ValidationResult>();

            if (Make == null)
            {
                carErrors.Add(new ValidationResult("You must enter a make!", new string[] { "Make" }));
            }
            if (Model == null)
            {
                carErrors.Add(new ValidationResult("You must enter a model!", new string[] { "Model" }));
            }
            if (Year == null) //need to fix the date
            {
                carErrors.Add(new ValidationResult("That was not a valid year!", new string[] { "Year" }));
            }
            else if (Year.Length != 4)
            {
                carErrors.Add(new ValidationResult("That was not a valid year!", new string[] { "Year" }));
            }
            if (Title == null)
            {
                carErrors.Add(new ValidationResult("That was not a valid name!", new string[] { "Title" }));
            }
            if (Price > 1000000 || Price == null)
            {
                carErrors.Add(new ValidationResult("Please provide a realistic price!", new string[] { "Price" }));
            }



            return carErrors;
        }
    }






}
