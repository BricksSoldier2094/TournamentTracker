using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrackerLibrary.Models;

namespace TrackerLibrary.DataAcess
{
    //TODO - Wire up the CreatePrize for text files

    /// <summary>
    /// The TextConnector to access the TournamentTracker Text Database
    /// </summary>
    public class TextConnector : IDataConnection
    {

        /// <summary>
        /// Create a new prize in the TournamentTracker Text DataBase
        /// </summary>
        /// <param name="model">The prize information</param>
        /// <returns>The prize information, including the unique identifier</returns>
        public PrizeModel CreatePrize(PrizeModel model)
        {
            model.Id = 1;

            return model;
        }

    }
}
