using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrackerLibrary.Models
{
    /// <summary>
    /// This class represents the basical class of the application. And it means
    /// the team member that will fill the team.
    /// </summary>
    public class PersonModel
    {
        /// <summary>
        /// The unique identifier for the person
        /// </summary>
        public int Id { get; set; }

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
        public string CellPhoneNumber { get; set; }

    }
}
