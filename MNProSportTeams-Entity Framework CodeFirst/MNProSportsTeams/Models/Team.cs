using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;

namespace MNProSportsTeams.Models
{
    public class Team
    {
        public int TeamID { get; set; }
        public string TeamName { get; set; }
        public string sport { get; set; }
        public string Mascot { get; set; }
        public string Coach { get; set; }
        public string AverageSalary { get; set; }
        public bool Active { get; set; }
        public int ConvictionsCount { get; set; }


        public int VenueID { get; set; }
        public virtual Venue Venue { get; set; }

        public int YearsID { get; set; }
        public virtual Years Year { get; set; }

        public virtual List<Champs> Championships { get; set; }




    }
}