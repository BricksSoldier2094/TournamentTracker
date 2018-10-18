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
    public partial class frmCreateTeam : Form
    {

        private List<PersonModel> availableTeamMembers = GlobalConfig.Connection.GetPerson_All();
        private List<PersonModel> selectedTeamMembers = new List<PersonModel>();
        /// <summary>
        /// Store whatever is passed in to our constructor.
        /// </summary>
        private ITeamRequester callingForm;


        /// <summary>
        /// The Caller parameter allows us to know who is calling the form.
        /// It allow us to pass back the created team.
        /// </summary>
        /// <param name="caller"></param>
        public frmCreateTeam(ITeamRequester caller)
        {
            InitializeComponent();

            callingForm = caller;

            //CreateSampleData();

            WireUpLists();
        }

        #region Just a test
        /// <summary>
        /// Sample data to test a func.
        /// </summary>
        private void CreateSampleData()
        {
            availableTeamMembers.Add(new PersonModel { FirstName = "Henrique", LastName = "Martins" });
            availableTeamMembers.Add(new PersonModel { FirstName = "Vitor", LastName = "França" });

            selectedTeamMembers.Add(new PersonModel { FirstName = "Marie", LastName = "Martins" });
            selectedTeamMembers.Add(new PersonModel { FirstName = "Keyt", LastName = "Martins" });

        }
        #endregion

        /// <summary>
        /// Initialize the lists used in the form.
        /// </summary>
        private void WireUpLists()
        {
            //It makes the bind actually work and refresh the data
            selectTeamMemberDropDown.DataSource = null;

            selectTeamMemberDropDown.DataSource = availableTeamMembers;
            selectTeamMemberDropDown.DisplayMember = "FullName";
            selectTeamMemberDropDown.Refresh();

            //It makes the bind actually work and refresh the data
            teamMemberListBox.DataSource = null;

            teamMemberListBox.DataSource = selectedTeamMembers;
            teamMemberListBox.DisplayMember = "FullName";
            teamMemberListBox.Refresh();
        }

        private void createMemberButton_Click(object sender, EventArgs e)
        {
            if (ValidateForm())
            {
                PersonModel p = new PersonModel();
                p.FirstName = firstNameValue.Text;
                p.LastName = lastNameValue.Text;
                p.EmailAddress = emailValue.Text;
                p.CellPhoneNumber = cellPhoneValue.Text;

                p = GlobalConfig.Connection.CreatePerson(p);

                selectedTeamMembers.Add(p);

                WireUpLists();

                firstNameValue.Text = "";
                lastNameValue.Text = "";
                emailValue.Text = "";
                cellPhoneValue.Text = "";

                if (p.Id != 0)
                {
                    MessageBox.Show("Member Sucessfully created!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Information);
                
                }

            }
            else
            {
                MessageBox.Show("You need to fill in all of the fields to create a new team member.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Information);                
            }

            
        }

        private bool ValidateForm()
        {
            if (firstNameValue.Text.Length == 0)
            {
                return false;
            }
            if (lastNameValue.Text.Length == 0)
            {
                return false;
            }
            if (emailValue.Text.Length == 0)
            {
                return false;
            }
            if (cellPhoneValue.Text.Length == 0)
            {
                return false;
            }

            return true;
        }

        private void addMemberButton_Click(object sender, EventArgs e)
        {
            PersonModel p = (PersonModel)selectTeamMemberDropDown.SelectedItem;

            if (p != null)
            {
                availableTeamMembers.Remove(p);
                selectedTeamMembers.Add(p);
            }

            WireUpLists();
        }

        private void RemoveSelectedTeamButton_Click(object sender, EventArgs e)
        {
            PersonModel p = (PersonModel)teamMemberListBox.SelectedItem;

            if (p != null)
            {
                selectedTeamMembers.Remove(p);
                availableTeamMembers.Add(p);
            }

            WireUpLists();
        }

        private void createTeambutton_Click(object sender, EventArgs e)
        {
            TeamModel t = new TeamModel
            {
                TeamName = teamNameValue.Text,
                TeamMembers = selectedTeamMembers
            };

            GlobalConfig.Connection.CreateTeam(t);

            callingForm.TeamComplete(t);

            MessageBox.Show("Team sucessfully created!", "Team Created", MessageBoxButtons.OK, MessageBoxIcon.Information);

            this.Close();

        }
    }
}
