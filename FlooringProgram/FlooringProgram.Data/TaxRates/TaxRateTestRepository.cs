using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FlooringProgram.Models;
using FlooringProgram.Models.Interfaces;

namespace FlooringProgram.Data.TaxRates
{
    public class TaxRateTestRepository : ITaxRateRepository
    {
        public List<TaxRate> GetTaxRates()
        {
            return new List<TaxRate>()
            {
                new TaxRate() {State = "OH", TaxPercent = 0.10M},
                new TaxRate() {State = "MI", TaxPercent = 0.0625M},
                new TaxRate() {State = "PA", TaxPercent = 0.0675M},
                new TaxRate() {State = "IN", TaxPercent = 0.06M}
            };
        }
    }
}
