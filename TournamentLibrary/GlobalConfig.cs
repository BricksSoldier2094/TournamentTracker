using System;
using System.Collections.Generic;
using System.Text;

namespace TournamentLibrary
{
    /// <summary>
    /// Implements a Global Configuration, visible everyone.
    /// Help to decide how kind of database source is available.
    /// </summary>
    public static class GlobalConfig
    {
        /// <summary>
        /// A list of Connections
        /// </summary>
        public static List<IDataConnection> Connections { get; private set; }

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
                //TODO Create the SQL Connection
            }

            if(textfiles)
            {
                //TODO Create the Text Connection
            }


        }
    }
}
