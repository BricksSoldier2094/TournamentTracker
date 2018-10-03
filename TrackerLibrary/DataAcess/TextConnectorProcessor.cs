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
        /// Return the path of the file based database
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

    }
}
