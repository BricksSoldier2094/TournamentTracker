using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrackerLibrary.Models;

namespace TrackerLibrary.DataAcess
{
    /// <summary>
    /// Implements a contract to help our application to standardize how
    /// we actually do the things whatever the database is selected.
    /// </summary>
    public interface IDataConnection
    {

        /// <summary>
        /// A contract clausule. Implements the method to create a new prize in the database.
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        PrizeModel CreatePrize(PrizeModel model);

        /// <summary>
        /// A contract clausule. Implements the method to create a new person in the database.
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        PersonModel CreatePerson(PersonModel model);

        /// <summary>
        /// Get all the person available in the data base.
        /// </summary>
        /// <returns></returns>
        List<PersonModel> GetPerson_All();

        
    }
}
