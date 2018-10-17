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
        /// The name of the file that keeps the data of the prizes created
        /// </summary>
        private const string PrizesFile = "PrizeModels.csv";

        /// <summary>
        /// The name of the file that keeps the data of the people created
        /// </summary>
        private const string PeopleFile = "PersonModels.csv";

        /// <summary>
        /// The name of the file that keeps the data of the Teams created
        /// </summary>
        private const string TeamFile = "TeamModels.csv";

        /// <summary>
        /// Create a new person in the TournamentTracker Text DataBase
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public PersonModel CreatePerson(PersonModel model)
        {
            List<PersonModel> people = PeopleFile.FullFilePath().LoadFile().ConvertToPersonModels();

            int currentId = 1;

            if(people.Count > 0)
            {
                currentId = currentId = people.OrderByDescending(x => x.Id).First().Id + 1;
            }

            model.Id = currentId;

            people.Add(model);

            people.SavePeopleFile(PeopleFile);

            return model;

        }

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

        /// <summary>
        /// Create a new Team in the TournamentTracker Text DataBase
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public TeamModel CreateTeam(TeamModel model)
        {
            List<TeamModel> Teams = TeamFile.FullFilePath().LoadFile().ConvertToTeamModels(PeopleFile);

            //Find Max ID
            int currentID = 1;

            if (Teams.Count > 0)
            {
                currentID = Teams.OrderByDescending(x => x.Id).First().Id + 1;
            }

            model.Id = currentID;

            Teams.Add(model);

            Teams.SaveToTeamsFile(TeamFile);

            return model;
        }

        /// <summary>
        /// Get all the People available in the Members file on Text DataBase.
        /// </summary>
        /// <returns></returns>
        public List<PersonModel> GetPerson_All()
        {
            return PeopleFile.FullFilePath().LoadFile().ConvertToPersonModels();
        }

        public List<TeamModel> GetTeam_All()
        {
            throw new NotImplementedException();
        }
    }
}
