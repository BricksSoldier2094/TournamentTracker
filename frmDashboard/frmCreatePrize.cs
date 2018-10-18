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
using TrackerLibrary.DataAcess;

namespace frmDashboard
{
    public partial class frmCreatePrize : Form
    {
        /// <summary>
        /// Store whatever is passed in to our constructor.
        /// </summary>
        IPrizeRequester callingForm;

        /// <summary>
        /// The Caller parameter allows us to know who is calling the form.
        /// It allow us to pass back the created prize.
        /// </summary>
        /// <param name="caller"></param>
        public frmCreatePrize(IPrizeRequester caller)
        {
            InitializeComponent();

            callingForm = caller;
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

                GlobalConfig.Connection.CreatePrize(model);

                //Pass back the already created and vallidated form back from the caller.
                callingForm.PrizeComplete(model);

                MessageBox.Show("Prize created successfully!", "Prize created", MessageBoxButtons.OK, MessageBoxIcon.Information);
                CleanFormFields();

                this.Close();
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
