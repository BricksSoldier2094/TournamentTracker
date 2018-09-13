using System;
using System.Collections.Generic;
using System.Text;

namespace TournamentLibrary
{
    /// <summary>
    /// Implements a contract to help our application to standardize how
    /// we actually do the things whatever the database is selected.
    /// </summary>
    public interface IDataConnection
    {
        /// <summary>
        /// A contract clausule to standardize the method to create a prize the data connections.
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        PrizeModel CreatePrize(PrizeModel model); 
    }
}
