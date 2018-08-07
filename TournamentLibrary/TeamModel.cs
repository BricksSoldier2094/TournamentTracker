using System;
using System.Collections.Generic;
using System.Text;
/// <summary>
/// This class is used to create a team, inside that we have:
/// </summary>
namespace TournamentLibrary
{
    public class TeamModel
    {
        /// <summary>
        /// A list of members that will fill the team
        /// </summary>
        public List<PersonModel> TeamMembers { get; set; } = new List<PersonModel>();

        /// <summary>
        /// The Team's name.
        /// </summary>
        public string TeamName { get; set; }
    }
}
