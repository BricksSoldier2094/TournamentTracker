using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TrackerLibrary;
using TrackerLibrary.Models;

namespace frmDashboard
{
    public partial class frmCreateTournament : Form
    {
        List<TeamModel> availableTeams = GlobalConfig.Connection.GetTeam_All();
        List<TeamModel> selectedTeam = new List<TeamModel>();
        List<PrizeModel> selectedPrizes = new List<PrizeModel>();

        public frmCreateTournament()
        {
            InitializeComponent();

            WireUpLists();
        }

        /// <summary>
        /// Initialize the lists used in the form.
        /// </summary>
        private void WireUpLists()
        {
            selectTeamDropDown.DataSource = null;
            selectTeamDropDown.DataSource = availableTeams;
            selectTeamDropDown.DisplayMember = "TeamName";

            tournamentTeamsListBox.DataSource = null;
            tournamentTeamsListBox.DataSource = selectedTeam;
            tournamentTeamsListBox.DisplayMember = "TeamName";

            PrizesListBox.DataSource = null;
            PrizesListBox.DataSource = selectedPrizes;
            PrizesListBox.DisplayMember = "PlaceNumber";
        }

        
        private void addTeamButton_Click(object sender, EventArgs e)
        {            
            TeamModel t = (TeamModel)selectTeamDropDown.SelectedItem;

            if(t != null)
            {
                availableTeams.Remove(t);
                selectedTeam.Add(t);

                WireUpLists();
            }

        }
    }
}
