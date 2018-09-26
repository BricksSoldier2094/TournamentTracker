using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrackerLibrary.Models;
using TrackerLibrary.DataAcess.TextHelpers;

namespace TrackerLibrary.DataAcess
{


    /// <summary>
    /// The TextConnector to access the TournamentTracker Text Database
    /// </summary>
    public class TextConnector : IDataConnection
    {
        /// <summary>
        /// The nome of the file that keeps the data of the prizes created
        /// </summary>
        private const string PrizesFile = "PrizeModels.csv";

        /// <summary>
        /// Create a new prize in the TournamentTracker Text DataBase
        /// </summary>
        /// <param name="model">The prize information</param>
        /// <returns>The prize information, including the unique identifier</returns>
        public PrizeModel CreatePrize(PrizeModel model)
        {
            //Load the text file
            //Convert the text file to a List<PrizeModel>
            List<PrizeModel> prizes = PrizesFile.FullFilePath().LoadFile().ConvertToPrizeModels();

            // Find the max ID
            int currentId = 1;

            if(prizes.Count > 0)
            {
                currentId = currentId = prizes.OrderByDescending(x => x.Id).First().Id + 1;
            }

            model.Id = currentId;

            // Add the new record with the new ID ( max ID + 1)
            prizes.Add(model);

            // Convert the Prizes to a list of a List<string>
            // Save the List<string> to the text file
            prizes.SaveToPrizeFile(PrizesFile);

            return model;
        }

    }
}
