using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MNProSportsTeams.Models;
using MNProSportsTeams.Operations;
using System.Data.Entity;

namespace MNProSportsTeams
{
    public class Program
    {

        static void Main(string[] args)
        {
            TeamOperations teamOps = new TeamOperations();

            Console.WriteLine("What would you like to do? Please enter a number");
            Console.WriteLine("1.Check out our current teams?");
            Console.WriteLine("2.Want to add a prospective team for us to Consider?");
            Console.WriteLine("3.Find a specific team by their TeamID");
            string userInput = Console.ReadLine();

            if (userInput == "2")
            {

                teamOps.AddTeam();
            }
            if (userInput == "1")
            {
                teamOps.DisplayTeams();
                Console.ReadLine();
            }

            if (userInput == "3") 
            {
                Console.Clear();
                Console.WriteLine("What is their ID?");
                int result = int.Parse(Console.ReadLine());
                Team team = teamOps.GetTeamByID(result);

                Console.WriteLine("Team Name: {0}", team.TeamName);
                Console.WriteLine("Active Team?: {0}", team.Active);
                Console.WriteLine("Average Salary: {0}", team.AverageSalary);
                Console.WriteLine("Head Coach Name: {0}", team.Coach);
                Console.WriteLine("Number of CONVICTIONS: {0}", team.ConvictionsCount);
                Console.WriteLine("Name of Mascot: {0}", team.Mascot);
                Console.WriteLine("Name of Sport: {0}", team.sport);
                Console.WriteLine("Name of Stadium: {0}", team.Venue.Building);
                Console.WriteLine("Year\n", team.Year);
                Console.ReadLine();

            }



        }



    }
}



//Console.WriteLine("Would you like to see a team? Y?");
//string response = Console.ReadLine().ToUpper();

//if (response == "Y")
//{
//    Program p = new Program();
//    p.DisplayTeams();
//}

//using (var TC = new TeamContext())//////////////////////
//{

//    var newTeam = new Team()
//    {
//        TeamName = "Minnesota Timberwolves",
//        Active = true,
//        AverageSalary = "600000",
//        Coach = "Sam Mitchel",
//        ConvictionsCount = 0,
//        Mascot = "Crunch",
//        sport = "Basketball",
//    };

//    var years = new Years()
//    {
//        YearsFounded = "1965",
//        YearDisbanded = null
//    };

//    var venue = new Venue()
//    {
//        Building = "Target Center",
//        City = "Minneapolis",
//    };


//    var championships = new Champs()
//    {
//        ChampYear = "2015",
//        Title = "NBA Championship"
//    };

//    TC.Championships.Add(championships);
//    TC.Teams.Add(newTeam);
//    TC.Venues.Add(venue);
//    TC.Years.Add(years);
