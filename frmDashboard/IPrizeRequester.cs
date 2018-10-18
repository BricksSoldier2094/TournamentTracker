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
    ///we actually take a prize back from the frmCreatePrize
    /// </summary>
    public interface IPrizeRequester
    {
        /// <summary>
        /// A contract clausule. Implements the method used to help a form to call
        /// and take back a prize from the frmCreatePrize.
        /// </summary>
        /// <param name="model"></param>
        void PrizeComplete(PrizeModel model);
        
    }
}
