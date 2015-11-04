using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data.Entity;

namespace MNProSportsTeams.Models
{
    public class Venue
    {
        public int VenueID { get; set; }
        public string Building { get; set; }
        public string City { get; set; }
        public virtual List<Team> TeamList { get; set; }
    }
}