using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrackerLibrary.Models;

namespace frmDashboard
{
    /// <summary>
    ///Implements a contract to help our application to standardize how
    ///we actually take back from the frmCreateTeam the team created.
    /// </summary>
    public interface ITeamRequester
    {
        /// <summary>
        /// A contract clausule. Implements the method used to help a form to call
        /// and take back the team from the frmCreateTeam.
        /// </summary>
        /// <param name="model"></param>
        void TeamComplete(TeamModel model);

    }
}
