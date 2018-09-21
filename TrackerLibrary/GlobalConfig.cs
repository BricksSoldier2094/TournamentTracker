using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrackerLibrary.DataAcess;

namespace TrackerLibrary
{
    /// <summary>
    /// Implements a Global Configuration, visible everyone.
    /// Help to decide how kind of database source is available.
    /// </summary>
    public class GlobalConfig
    {

        /// <summary>
        /// A connection established in the app.config file.    
        /// </summary>
        public static IDataConnection Connection { get; private set; }
        /// With that we actually can put in our list of connections any connection that implements this Interface


        /// <summary>
        /// Help the application to determine what kind of database source is
        /// actually available.
        /// </summary>
        /// <param name="database"></param>
        /// <param name="textfiles"></param>
        public static void InitializeConnections(DataBaseType db)
        {          

            if (db == DataBaseType.Sql)
            {
                
                SqlConnector MySqlConnector = new SqlConnector();
                Connection = MySqlConnector;

            }

            else if (db == DataBaseType.TextFile)
            {
                //TODO Create the Text Connection
                TextConnector MyTextConnector = new TextConnector();
                Connection = MyTextConnector;
            }


        }

        /// <summary>
        /// Contains the connection string available on the app.config file.
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public static string CnnString(string name)
        {
            return ConfigurationManager.ConnectionStrings["Tournaments"].ConnectionString;
        }

    }
}
