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
    public partial class frmCreateTournament : Form, IPrizeRequester, ITeamRequester
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
            PrizesListBox.DisplayMember = "PlaceName";
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

        private void createPrizeButton_Click(object sender, EventArgs e)
        {
            //Call the CreatePrizeForm
            frmCreatePrize frm = new frmCreatePrize(this);
            frm.ShowDialog();            
        }

        public void PrizeComplete(PrizeModel model)
        {
            //Get back from the form a PrizeModel 
            //Take the PrizeModel and put it into our list of selected prizes
            selectedPrizes.Add(model);
            WireUpLists();
        }

        public void TeamComplete(TeamModel model)
        {
            selectedTeam.Add(model);
            WireUpLists();
        }

        private void newTeamLinkLabel_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            frmCreateTeam frm = new frmCreateTeam(this);
            frm.ShowDialog();
        }

        private void removeSelectedPlayerButton_Click(object sender, EventArgs e)
        {
            TeamModel t = (TeamModel)tournamentTeamsListBox.SelectedItem;

            if(t != null)
            {
                selectedTeam.Remove(t);
                availableTeams.Add(t);
            }

            WireUpLists();

        }

        private void removeSelectedPrizeButton_Click(object sender, EventArgs e)
        {
            PrizeModel p = (PrizeModel)PrizesListBox.SelectedItem;

            if(p != null)
            {
                selectedPrizes.Remove(p);                
            }

            WireUpLists();

        }

        private void createTournamentbutton_Click(object sender, EventArgs e)
        {
            //Validate data
            decimal fee = 0;
            bool feeAcceptable = decimal.TryParse(entryFeeValue.Text, out fee);

            if (!feeAcceptable)
            {
                MessageBox.Show("You need to enter a valid Entry Fee", "Invalid Fee", MessageBoxButtons.OK, MessageBoxIcon.Error);
                entryFeeValue.Focus();
                return;
            }

            //Create tournament model
            TournamentModel tm = new TournamentModel();

            tm.TournamentName = tournamentNameValue.Text;
            tm.EntryFee = fee;

            tm.Prizes = selectedPrizes;
            tm.EnteredTeams = selectedTeam;

            //Wire our matchups

            //Create tournament Entry
            //Create all of the prizes entries
            //create all of team entries
            GlobalConfig.Connection.CreateTournament(tm);

        }
    }
}
