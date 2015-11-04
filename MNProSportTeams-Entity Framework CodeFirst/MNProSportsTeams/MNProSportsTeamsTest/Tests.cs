using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MNProSportsTeams.Models;
using NUnit.Framework;
using MNProSportsTeams.Operations;

namespace MNSportsTest
{
    [TestFixture]
    public class Tests
    {
        [TestCase(1, "Minnesota Timberwolves")]
        public void GetTeambyIDTEST(int ID, string expectedTeamName)
        {
            TeamOperations teamOps = new TeamOperations();

            Team testTeam = teamOps.GetTeams().SingleOrDefault(m => m.TeamID == ID);
            string team = testTeam.TeamName;

            Assert.AreEqual(team, expectedTeamName);
        }




    }
}