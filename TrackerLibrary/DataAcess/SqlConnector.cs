using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
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
        /// <summary>
        /// The constant that represents our CnnString Value. Used to avoid magic strings.
        /// </summary>
        private const string db = "Tournaments";

        /// <summary>
        /// Create a new person entry in the TournamentTracker SQL DataBase
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public PersonModel CreatePerson(PersonModel model)
        {
            //Stabilish the use of a connection inside the Using statement
            //When this block of code is already executed then this connection
            //will be automatic destroyed
            using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(GlobalConfig.CnnString(db)))
            {
                var p = new DynamicParameters();
                p.Add("@FirstName", model.FirstName);
                p.Add("@LastName", model.LastName);
                p.Add("@EmailAddress", model.EmailAddress);
                p.Add("@CellPhoneNumber", model.CellPhoneNumber);

                p.Add("@Id", 0, dbType: DbType.Int32, direction: ParameterDirection.Output);

                connection.Execute("dbo.spPeople_Insert", p, commandType: CommandType.StoredProcedure);

                model.Id = p.Get<int>("@Id");

                return model;
            }
        }

        /// <summary>
        /// Create a new prize in the TournamentTracker SQL DataBase
        /// </summary>
        /// <param name="model">The prize information</param>
        /// <returns>The prize information, including the unique identifier</returns>
        public PrizeModel CreatePrize(PrizeModel model)
        {            
            using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(GlobalConfig.CnnString(db)))
            {
                var p = new DynamicParameters();
                p.Add("@PlaceNumber", model.PlaceNumber);
                p.Add("@PlaceName", model.PlaceName);
                p.Add("@PrizeAmount", model.PrizeAmount);
                p.Add("@PrizePercentage", model.PrizePercentage);
                

                //This line allow us to modify the ParameterDirection to get the ID back after
                // the query execution on to database
                p.Add("@Id", 0, dbType: DbType.Int32, direction: ParameterDirection.Output);

                connection.Execute("dbo.spPrize_Insert", p, commandType: CommandType.StoredProcedure);

                //Get the ID in the parameter list after it is filled out by the query
                model.Id = p.Get<int>("@Id");

                return model;
            }
        }

        /// <summary>
        /// Get all the People available in the Members table on SQL DataBase.
        /// </summary>
        /// <returns></returns>
        public List<PersonModel> GetPerson_All()
        {
            List<PersonModel> output;

            using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(GlobalConfig.CnnString(db)))
            {
                output = connection.Query<PersonModel>("dbo.spPeople_GetAll").ToList();
            }

            return output;

        }

    }
}
