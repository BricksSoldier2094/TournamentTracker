using System;
using System.Collections.Generic;
using System.Text;



namespace TournamentLibrary
{
    /// <summary>
    /// This class is used to create a prize inside our application
    /// </summary>
    public class PrizeModel
    {
        /// <summary>
        /// The unique identifier for the prize
        /// </summary>
        public int Id { get; set; } = 0;

        /// <summary>
        /// The number of a place of a prize
        /// </summary>
        public int PlaceNumber { get; set; } = 0;

        /// <summary>
        /// The name of a number of a place
        /// </summary>
        public string PlaceName { get; set; } = "NONE";

        /// <summary>
        /// The prizes' amount , considering an integer
        /// </summary>
        public decimal PrizeAmount { get; set; } = 0;

        /// <summary>
        /// The prizes' amount, considering a percentage above the total amount
        /// </summary>
        public double PrizePercentage { get; set; } = 0;

        public PrizeModel()
        {

        }

        public PrizeModel(string placeName, string placeNumber, string prizeAmount, string prizePercentage)
        {
            PlaceName = placeName;

            int placeNumberValue = 0;
            int.TryParse(placeNumber, out placeNumberValue);
            PlaceNumber = placeNumberValue;

            decimal prizeAmountValue = 0;
            decimal.TryParse(prizeAmount, out prizeAmountValue);
            PrizeAmount = prizeAmountValue;

            double prizePercentageValue = 0;
            double.TryParse(prizePercentage, out prizePercentageValue);
            PrizePercentage = prizePercentageValue;
        }
    }
}
