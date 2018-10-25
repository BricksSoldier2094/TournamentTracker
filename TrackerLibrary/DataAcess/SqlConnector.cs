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
        /// Create a new Team in the TournamentTracker SQL DataBase, saves every person like a TeamMember, in the TeamMember's table
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public TeamModel CreateTeam(TeamModel model)
        {
            using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(GlobalConfig.CnnString(db)))
            {
                var p = new DynamicParameters();

                p.Add("@TeamName", model.TeamName);
                p.Add("@Id", 0, dbType: DbType.Int32, direction: ParameterDirection.Output);

                connection.Execute("dbo.spTeams_Insert", p, commandType: CommandType.StoredProcedure);

                model.Id = p.Get<int>("@Id");

                foreach (PersonModel tm in model.TeamMembers)
                {
                    p = new DynamicParameters();
                    p.Add("@TeamId", model.Id);
                    p.Add("@PersonId", tm.Id);

                    connection.Execute("dbo.spTeamMembers_Insert", p, commandType: CommandType.StoredProcedure);

                }

                return model;
            }
        }

        /// <summary>
        /// Create a new Tournament in the TournamentTracker SQL DataBase. Saves the related data about prizes and teams too.
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public void CreateTournament(TournamentModel model)
        {
            using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(GlobalConfig.CnnString(db)))
            {
                SaveTournament(connection, model);

                SaveTournamentPrizes(connection, model);

                SaveTournamentEntries(connection, model);             
                
            }
        }

        /// <summary>
        /// Saves the Tournament into the SQL Database
        /// </summary>
        /// <param name="connection"></param>
        /// <param name="model"></param>
        private void SaveTournament(IDbConnection connection ,TournamentModel model)
        {
            

            var p = new DynamicParameters();

            p.Add("@TournamentName", model.TournamentName);
            p.Add("@EntryFee", model.EntryFee);
            p.Add("@Id", 0, dbType: DbType.Int32, direction: ParameterDirection.Output);

            connection.Execute("dbo.spTournament_Insert", p, commandType: CommandType.StoredProcedure);

            //We are not returning the Model back because we've actually changed it, 
            //throught the "address" of the object it means, the object reference.
            model.Id = p.Get<int>("@Id");
        }

        /// <summary>
        /// Save the Tournament Prizes Entries into related SQL database.
        /// </summary>
        /// <param name="connection"></param>
        /// <param name="model"></param>
        private void SaveTournamentPrizes(IDbConnection connection, TournamentModel model)
        {
            foreach (PrizeModel pz in model.Prizes)
            {
                var p = new DynamicParameters();
                p.Add("@TournamentId", model.Id);
                p.Add("@PrizeId", pz.Id);
                p.Add("@Id", 0, dbType: DbType.Int32, direction: ParameterDirection.Output);

                connection.Execute("dbo.spTournamentPrize_Insert", p, commandType: CommandType.StoredProcedure);

            }
        }

        /// <summary>
        /// Save the Tournament Entries (Teams participating) into related SQL database.
        /// </summary>
        /// <param name="connection"></param>
        /// <param name="model"></param>
        private void SaveTournamentEntries(IDbConnection connection, TournamentModel model)
        {
            foreach (TeamModel tm in model.EnteredTeams)
            {
                var p = new DynamicParameters();
                p.Add("@TournamentId", model.Id);
                p.Add("@TeamId", tm.Id);
                p.Add("@Id", 0, dbType: DbType.Int32, direction: ParameterDirection.Output);

                connection.Execute("dbo.spTournamentEntries_Insert", p, commandType: CommandType.StoredProcedure);

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

        /// <summary>
        /// Get all the teams available in the team's table on SQL DataBase.
        /// </summary>
        /// <returns></returns>
        public List<TeamModel> GetTeam_All()
        {
            List<TeamModel> output;

            using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(GlobalConfig.CnnString(db)))
            {
                output = connection.Query<TeamModel>("dbo.spTeam_GetAll").ToList();

                foreach (TeamModel team in output)
                {
                    var p = new DynamicParameters();
                    p.Add("@TeamId", team.Id);

                    team.TeamMembers = connection.Query<PersonModel>("dbo.spTeamMembers_GetByTeam", p, commandType: CommandType.StoredProcedure).ToList();
                }

            }

            return output;
        }
    }
}
