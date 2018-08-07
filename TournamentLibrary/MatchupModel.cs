﻿using System;
using System.Collections.Generic;
using System.Text;

/// <summary>
/// This class descibres the matchup that will fill the list of list of matchups and as we know
/// it means the rounds inside the tournament.
/// </summary>

namespace TournamentLibrary
{
    class MatchupModel
    {
        /// <summary>
        /// The list of Participants inside a tournament
        /// </summary>
        public List<MatchupEntryModel> Entries { get; set; }

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
