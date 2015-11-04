using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MNProSportsTeams.Models;
using System.Data.Entity;

namespace MNProSportsTeams.Operations
{
    public class TeamOperations
    {
        public List<Team> GetTeams()
        {
            List<Team> teamList = new List<Team>();

            using (TeamContext Contexts = new TeamContext())
            {
                foreach (var team in Contexts.Teams)
                {
                    Team newTeam = new Team();
                    newTeam.Active = team.Active;
                    newTeam.AverageSalary = team.AverageSalary;
                    newTeam.Championships = team.Championships;
                    newTeam.Coach = team.Coach;
                    newTeam.ConvictionsCount = team.ConvictionsCount;
                    newTeam.Mascot = team.Mascot;
                    newTeam.sport = team.sport;
                    newTeam.TeamID = team.TeamID;
                    newTeam.TeamName = team.TeamName;
                    newTeam.Venue = team.Venue;
                    newTeam.VenueID = team.VenueID;
                    newTeam.YearsID = team.YearsID;
                    newTeam.Year = team.Year;

                    teamList.Add(newTeam);


                }
                return teamList;



            }

        }
        public void DisplayTeams()
        {
            List<Team> ListOfTeams = new List<Team>();

            ListOfTeams = GetTeams();

            foreach(var team in ListOfTeams)
            {
                Console.WriteLine("Team Name: {0}", team.TeamName);
                Console.WriteLine("Active Team?: {0}", team.Active);
                Console.WriteLine("Average Salary: {0}", team.AverageSalary);
                Console.WriteLine("Head Coach Name: {0}", team.Coach);
                Console.WriteLine("Number of CONVICTIONS: {0}", team.ConvictionsCount);
                Console.WriteLine("Name of Mascot: {0}", team.Mascot);
                Console.WriteLine("Name of Sport: {0}", team.sport);
                Console.WriteLine("Name of Stadium: {0}", team.Venue.Building);
                Console.WriteLine("Year\n", team.Year);

            }


        }

        public void AddTeam()
        {
            using (var TC = new TeamContext())
            {

                Console.Clear();
                TeamOperations teamOps = new TeamOperations();
                Team newTeam = new Team();
                Venue venue = new Venue();


                Console.WriteLine("What would you like them to be called?");
                newTeam.TeamName = Console.ReadLine();
                Console.WriteLine("What sport do they play?");
                newTeam.sport = Console.ReadLine();
                Console.WriteLine("Where in Minnesota would they be located?");
                venue.City = Console.ReadLine();
                Console.WriteLine("What about a mascot?");
                newTeam.Mascot = Console.ReadLine();

               
   
                var years = new Years()
                {
                    YearsFounded = null,
                    YearDisbanded = null
                };

                var championships = new Champs()
                {
                    ChampYear = null,
                    Title = null
                };

                TC.Championships.Add(championships);
                TC.Years.Add(years);
                TC.Venues.Add(venue);
                TC.Teams.Add(newTeam);

                TC.SaveChanges();

            }
        }




        public Team GetTeamByID(int ID)
        {

            return GetTeams().SingleOrDefault(m => m.TeamID == ID);

        }
    }

}