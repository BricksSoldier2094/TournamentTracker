using System;
using System.Collections.Generic;
using System.Text;

/// <summary>
/// This class is used to create a prize inside our application
/// </summary>

namespace TournamentLibrary
{
    class PrizeModel
    {
        /// <summary>
        /// The number of a place of a prize
        /// </summary>
        public int PlaceNumber { get; set; }

        /// <summary>
        /// The name of a number of a place
        /// </summary>
        public string PlaceName { get; set; }

        /// <summary>
        /// The prizes' amount , considering an integer
        /// </summary>
        public decimal PrizesAmount { get; set; }

        /// <summary>
        /// The prizes' amount, considering a percentage above the total amount
        /// </summary>
        public double PrizesPercentage { get; set; }
    }
}
