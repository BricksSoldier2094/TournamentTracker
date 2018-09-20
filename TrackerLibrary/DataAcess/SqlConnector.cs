using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrackerLibrary.Models;

namespace TrackerLibrary.DataAcess
{
    /// <summary>
    /// The SQLConnector to access the TournamentTracker SQL Database
    /// </summary>
    public class SqlConnector : IDataConnection
    {
        // TODO - Make the create prize method actually save to the database

        /// <summary>
        /// Create a new prize in the TournamentTracker SQL DataBase
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
