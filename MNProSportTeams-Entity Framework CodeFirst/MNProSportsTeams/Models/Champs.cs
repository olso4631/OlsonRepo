using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data.Entity;

namespace MNProSportsTeams.Models
{
    public class Champs
    {
        public int ChampsID { get; set; }
        public string ChampYear { get; set; }
        public string Title { get; set; }

        public int TeamID { get; set; }
        public virtual Team Team { get; set; }
    }
}
