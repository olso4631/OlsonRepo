using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data.Entity;

namespace MNProSportsTeams.Models
{
    public class TeamContext : DbContext
    {
        public DbSet<Team> Teams { get; set; }
        public DbSet<Years> Years { get; set; }
        public DbSet<Champs> Championships { get; set; }
        public DbSet<Venue> Venues { get; set; }

    }
}