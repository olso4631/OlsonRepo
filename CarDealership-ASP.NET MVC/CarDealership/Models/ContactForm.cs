using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CarDealership.Models
{
    public class ContactForm : IValidatableObject
    {


        public string Name { get; set; }
        public int PurchaseTimeFrameInMonths { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public decimal? Income { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            List<ValidationResult> errors = new List<ValidationResult>();

            if (Income == null || Income < 20000)
            {
                errors.Add(new ValidationResult("Income must not be blank or less than 20000", new string[] { "Income" }));
            }
            if (PhoneNumber == null || PhoneNumber[3] != '-' || PhoneNumber[7] != '-' || PhoneNumber.Length != 12)
            {
                errors.Add(new ValidationResult("Your phone Number is not valid xxx-xxx-xxxx", new string[] { "PhoneNumber" }));
            }
            if ((Email == null) || (!Email.Contains('@')))
            {
                errors.Add(new ValidationResult("That was not a valid email!", new string[] { "Email" }));
            }
            if (Name == null)
            {
                errors.Add(new ValidationResult("That was not a valid name!Please enter what people call you", new string[] { "Name" }));
            }
            return errors;
        }
    }
}