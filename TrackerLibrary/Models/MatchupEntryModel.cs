using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrackerLibrary.Models
{
    /// <summary>
    /// This class represents the entry of a team participating on to the tournament.)
    /// </summary>
    public class MatchupEntryModel
    {

        /// <summary>
        /// The team that will participate in a tournament
        /// </summary>
        public TeamModel TeamCompeting { get; set; }

        /// <summary>
        /// The Score acquired in a matchup
        /// </summary>
        public double Score { get; set; }

        /// <summary>
        /// I don't kown, need to review at Tim Corey's videos.
        /// </summary>
        public MatchupModel ParentMatchup { get; set; }

    }
}
