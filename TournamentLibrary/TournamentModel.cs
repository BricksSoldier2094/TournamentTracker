using System;
using System.Collections.Generic;
using System.Text;

/// <summary>
/// This class represents a tournament unit inside our tournament tracker
/// </summary>

namespace TournamentLibrary
{
    class TournamentModel
    {
        /// <summary>
        /// The tournament's name
        /// </summary>
        public string TournamentName { get; set; }

        /// <summary>
        /// The amount of the entry fee that the members of the tournament need to pay
        /// </summary>
        public decimal EntryFee { get; set; }


        /// <summary>
        /// A lista of team that will participate
        /// </summary>
        public List<TeamModel> EnteredTeam { get; set; } = new List<TeamModel>();

        /// <summary>
        /// The list of prizes offered for the participants of the tournament
        /// </summary>
        public List<PrizeModel> Prizes { get; set; } = new List<PrizeModel>();

        /// <summary>
        /// The list of list of Matchup Entries, that means the rounds of the tournament
        /// </summary>
        public List<List<MatchupEntryModel>> Rounds { get; set; } = new List<List<MatchupEntryModel>>();



    }
}
