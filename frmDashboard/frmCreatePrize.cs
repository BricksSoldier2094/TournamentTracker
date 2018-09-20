using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TournamentLibrary;
using TournamentLibrary.DataAcess;
using TournamentLibrary.Models;

namespace frmDashboard
{
    public partial class frmCreatePrize : Form
    {
        public frmCreatePrize()
        {
            InitializeComponent();
        }

        private void createPrizebutton_Click(object sender, EventArgs e)
        {
            if (ValidateForm())
            {
                PrizeModel model = new PrizeModel(

                    placeNameValue.Text,
                    placeNumberValue.Text,
                    prizeAmountValue.Text,
                    prizePercentageValue.Text);

                foreach (IDataConnection db in GlobalConfig.Connections)
                {
                    db.CreatePrize(model);
                }

                MessageBox.Show("Prize created successfully!", "Prize created", MessageBoxButtons.OK, MessageBoxIcon.Information);
                CleanFormFields();

            }
            else
            {
                MessageBox.Show("An error ocurr during the attempt to save the prize.\nReview the information and try again.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        #region Method to validate the form
        /// <summary>
        /// Validates the form assuring the information is correctly.
        /// </summary>
        private bool ValidateForm()
        {
            bool output = true;

            int placeNumber = 0;
            bool placeNumberValidNumber = int.TryParse(placeNumberValue.Text, out placeNumber);

            if (!placeNumberValidNumber)
            {
                output = false;
                placeNumberValue.Focus();
            }

            if (placeNumber < 1)
            {
                output = false;
                placeNumberValue.Focus();
            }

            decimal prizeAmount = 0;
            double prizePercentage = 0;


            bool prizeAmountValid = decimal.TryParse(prizeAmountValue.Text, out prizeAmount);
            bool prizePercentageValid = double.TryParse(prizePercentageValue.Text, out prizePercentage);

            if (!prizeAmountValid || !prizePercentageValid)
            {
                output = false;
                prizeAmountValue.Focus();
            }

            if (prizeAmount <= 0 && prizePercentage <= 0)
            {
                output = false;
                prizePercentageValue.Focus();
            }

            if (prizePercentage < 0 || prizePercentage > 100)
            {
                output = false;
            }

            return output;
        }
        #endregion

        #region Method to Clean the text fields of the form

        private void CleanFormFields()
        {
            placeNameValue.Clear();
            placeNumberValue.Clear();
            prizeAmountValue.Text = "0";
            prizePercentageValue.Text = "0";
            
        }

        #endregion
    }
}
