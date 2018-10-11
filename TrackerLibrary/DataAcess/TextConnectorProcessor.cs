using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrackerLibrary;
using TrackerLibrary.Models;



namespace TrackerLibrary.DataAcess.TextHelpers

{
    public static class TextConnectorProcessor
    {
        /// <summary>
        /// Return the path of the file based in the string passed to the method.
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns></returns>
        public static string FullFilePath(this string fileName) //PrizeModels.csv
        {
            // string C:\Data\TournamentTracker\PrizeModels.csv
            return $"{ConfigurationManager.AppSettings["filePath"] } \\ { fileName } ";
        }

        /// <summary>
        /// Takes the full file path and load the string
        /// </summary>
        /// <param name="file"></param>
        /// <returns></returns>
        public static List<string> LoadFile(this string file)
        {
            //validates if the file already exists
            if (!File.Exists(file))
            {
                return new List<string>();
            }

            //read all the lines in the file and create a list of strings with it
            return File.ReadAllLines(file).ToList();

        }

       /// <summary>
       /// Convert the text to a list of PrizeModels
       /// </summary>
       /// <param name="lines"></param>
       /// <returns></returns>
        public static List<PrizeModel> ConvertToPrizeModels(this List<string> lines)
        {
            List<PrizeModel> output = new List<PrizeModel>();

            foreach (string line in lines)
            {
                string[] cols = line.Split(',');

                PrizeModel p = new PrizeModel();
                p.Id = int.Parse(cols[0]);
                p.PlaceNumber = int.Parse(cols[1]);
                p.PlaceName = cols[2];
                p.PrizeAmount = decimal.Parse(cols[3]);
                p.PrizePercentage = double.Parse(cols[4]);
                output.Add(p);                
            }

            return output;

        }

        /// <summary>
        /// Convert the text to a list of Person Model
        /// </summary>
        /// <param name="lines"></param>
        /// <returns></returns>
        public static List<PersonModel> ConvertToPersonModels(this List<string> lines)
        {
            List<PersonModel> output = new List<PersonModel>();

            foreach (string line in lines)
            {
                string[] cols = line.Split(',');
                PersonModel p = new PersonModel();
                p.Id = int.Parse(cols[0]);
                p.FirstName = cols[1];
                p.LastName = cols[2];
                p.EmailAddress = cols[3];
                p.CellPhoneNumber = cols[4];
                output.Add(p);                
            }

            return output;

        }

        /// <summary>
        /// Saves the list of models, like a list of strings, in the file.
        /// </summary>
        /// <param name="models"></param>
        /// <param name="fileName"></param>
        public static void SaveToPrizeFile(this List<PrizeModel> models, string fileName)
        {
            List<string> lines = new List<string>();

            foreach (PrizeModel p in models)
            {
                lines.Add($"{ p.Id },{ p.PlaceNumber },{ p.PlaceName },{ p.PrizeAmount },{ p.PrizePercentage }");
            }

            File.WriteAllLines(fileName.FullFilePath(), lines);

        }

        /// <summary>
        /// Saves the list of Person, like a list of strings, in the file.
        /// </summary>
        /// <param name="models"></param>
        /// <param name="fileName"></param>
        public static void SavePeopleFile(this List<PersonModel> models, string fileName)
        {
            List<string> lines = new List<string>();

            foreach (PersonModel p in models)
            {
                lines.Add($"{ p.Id },{ p.FirstName },{ p.LastName },{ p.EmailAddress },{ p.CellPhoneNumber }");
            }

            File.WriteAllLines(fileName.FullFilePath(), lines);

        }

        /// <summary>
        /// Saves the list of Team, like a list of strings, in the file.
        /// </summary>
        /// <param name="models"></param>
        /// <param name="FileName"></param>
        public static void SaveToTeamsFile(this List<TeamModel> models, string FileName)
        {
            List<string> lines = new List<string>();

            foreach (TeamModel t in models)
            {
                lines.Add($"{ t.Id },{ t.TeamName },{ ConvertPeopleListToString(t.TeamMembers) }");
            }

            File.WriteAllLines(FileName.FullFilePath(),lines);

        }

        /// <summary>
        /// Helps the SaveToTeamsFile to extract the Ids in a list of person and return a list of them.
        /// </summary>
        /// <param name="People"></param>
        /// <returns></returns>
        private static string ConvertPeopleListToString(List<PersonModel> People)
        {
            string output = "";

            if(People.Count == 0)
            {
                return "";
            }

            foreach (PersonModel p in People)
            {
                output += $"{ p.Id }|";
            }

            //Removes the pipe character to the end of the list of people's id
            output = output.Substring(0, output.Length - 1);

            return output;
        }


        /// <summary>
        /// Saves the list of Teams and the list of TeamMembers' Id, like a list of strings, in the file.
        /// </summary>
        /// <param name="lines"></param>
        /// <returns></returns>
        public static List<TeamModel> ConvertToTeamModels(this List<string> lines, string peopleFileName)
        {
            List<TeamModel> Output = new List<TeamModel>();
            List<PersonModel> people = peopleFileName.FullFilePath().LoadFile().ConvertToPersonModels();

            foreach (string line in lines)
            {
                string[] cols = line.Split(',');

                TeamModel t = new TeamModel();

                t.Id = int.Parse(cols[0]);
                t.TeamName = cols[1];

                string[] personIds = cols[2].Split('|');

                //Take the list of people in the text file and search for it and filter WHERE the id of the person
                //in the list equals the id from the collection of person in the "TeamMemers" List, binded with the team
                foreach (string id in personIds)
                {                
                    t.TeamMembers.Add(people.Where(x => x.Id == int.Parse(id)).First());
                }

            }
                return Output;
        }

    }
}
