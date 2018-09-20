using System;
using System.Collections.Generic;
using System.Text;


namespace TournamentLibrary.Models
{
    /// <summary>
    /// This class represents the basical class of the application. And it means
    /// the team member that will fill the team.
    /// </summary>
    public class PersonModel
    {
        /// <summary>
        /// First name of the person
        /// </summary>
        public string FirstName { get; set; }

        /// <summary>
        /// Last name of the person
        /// </summary>
        public string LastName { get; set; }

        /// <summary>
        /// The email address of the person
        /// </summary>
        public string EmailAddress { get; set; }

        /// <summary>
        /// The CellPhone number of the person
        /// </summary>
        public string CellPhone { get; set; }



    }
}
