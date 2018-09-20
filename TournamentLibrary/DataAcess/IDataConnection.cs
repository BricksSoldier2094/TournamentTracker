using System;
using System.Collections.Generic;
using System.Text;
using TournamentLibrary.Models;

namespace TournamentLibrary.DataAcess

{
    /// <summary>
    /// Implements a contract to help our application to standardize how
    /// we actually do the things whatever the database is selected.
    /// </summary>
    public interface IDataConnection
    {
        /// <summary>
        /// A contract clausule. Implements the method to create a new prize in the database
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        PrizeModel CreatePrize(PrizeModel model); 
    }
}
