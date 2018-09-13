using System;
using System.Collections.Generic;
using System.Text;

namespace TournamentLibrary
{
    /// <summary>
    /// This class is used to create a team in our application
    /// </summary>
    public class TeamModel
    {
        /// <summary>
        /// A list of members that will fill the team
        /// </summary>    
        public List<PersonModel> TeamMembers { get; set; } = new List<PersonModel>();
        //This ability to declare a list by this way is something introduced in C# 6.0
        //before it we must declare a constructor that instantiate the list like a new list

        /// <summary>
        /// The Team's name.
        /// </summary>
        public string TeamName { get; set; }
    }
}
