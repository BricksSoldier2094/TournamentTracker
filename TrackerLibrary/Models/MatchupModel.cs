using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrackerLibrary.Models
{
    /// <summary>
    /// This class descibres the matchup that will fill the list of list of matchups and as we know
    /// it means the rounds inside the tournament.
    /// </summary>
    public class MatchupModel
    {

        /// <summary>
        /// The unique identifier for the Matchup
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// The list of Participants inside a tournament
        /// </summary>
        public List<MatchupEntryModel> Entries { get; set; } = new List<MatchupEntryModel>();

        /// <summary>
        /// The winner of a tournament
        /// </summary>
        public TeamModel Winner { get; set; }

        /// <summary>
        /// The rounds' number
        /// </summary>
        public int MatchupRound { get; set; }

    }
}
