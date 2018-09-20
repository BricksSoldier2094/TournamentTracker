using System;
using System.Collections.Generic;
using System.Text;
using TournamentLibrary.DataAcess;

namespace TournamentLibrary
{
    /// <summary>
    /// Implements a Global Configuration, visible everyone.
    /// Help to decide how kind of database source is available.
    /// </summary>
    public static class GlobalConfig
    {
        /// <summary>
        /// A list of Connections, holds everything that implements an IDataConnection Interface        
        /// </summary>
        public static List<IDataConnection> Connections { get; private set; } = new List<IDataConnection>();
        /// With that we actually can put in our list of connections any connection that implements this Interface


        /// <summary>
        /// Help the application to determine what kind of database source is
        /// actually available.
        /// </summary>
        /// <param name="database"></param>
        /// <param name="textfiles"></param>
        public static void InitializeConnections(bool database, bool textfiles)
        {
            if(database)
            {
                //TODO - Set up the SQL Connector properly
                SqlConnector MySqlConnector = new SqlConnector();
                Connections.Add(MySqlConnector);
                
            }

            if(textfiles)
            {
                //TODO Create the Text Connection
                TextConnector MyTextConnector = new TextConnector();
                Connections.Add(MyTextConnector);
            }


        }

        /// <summary>
        /// Contains the connection string available on the app.config file.
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public static string CnnString(string name)
        {
            return ConfigurationManager.ConnectionStrings[name].ConnectionString;
        }

    }
}
