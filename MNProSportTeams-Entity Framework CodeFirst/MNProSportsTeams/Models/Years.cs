using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data.Entity;

namespace MNProSportsTeams.Models
{
    public class Years
    {
        public int YearsID { get; set; }
        public string YearsFounded { get; set; }
        public string YearDisbanded { get; set; }

        public virtual List<Team> teams { get; set; }
    }
}
