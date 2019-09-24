using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PayrollManager
{
    public partial class EditNewEmployee : Form //in the future use inheritance, like the timesheet class or add employee class 
    {
        //declaring public variables 
        public decimal[] hoursEachDay = new decimal[7];
        public decimal hourlyRate;
        public string firstName;
        public string lastName;

        public EditNewEmployee()
        {
            InitializeComponent();
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            TextBox[] textBoxes = new TextBox[] { txtSundayHours, txtMondayHours, txtTuesdayHours, txtWednesdayHours,
             txtThursdayHours, txtFridayHours, txtSaturdayHours};
            if (validateHours(textBoxes, hoursEachDay) && validateRate() && validateNames())
            {
                DialogResult = DialogResult.OK;
                Close();
            }
            else
            {
                Close();
            }
        }
        
        //display info to textbox that will be edited 
        public void updateForm(string firstName, string lastName, decimal hourlyPay, decimal[] hoursEachDay)
        {           
            txtFirstName.Text = firstName;
            txtLastName.Text = lastName;
            txtHourlyRate.Text = Convert.ToString(hourlyPay);

            TextBox[] textBoxes = new TextBox[] { txtSundayHours, txtMondayHours, txtTuesdayHours, txtWednesdayHours,
             txtThursdayHours, txtFridayHours, txtSaturdayHours};

            for (int i = 0; i < 7; i++)
            {
                textBoxes[i].Text = Convert.ToString(hoursEachDay[i]);
            }
        }

        //validate the info they just added to hours
        public bool validateHours(TextBox[] textBoxName, decimal[] HoursEachDay) //validate the hours entered 
        {
            for (int boxNum = 0; boxNum < 7; boxNum++)
            {
                try
                {
                    if (!(textBoxName[boxNum].Text.Length == 0))
                    {
                        HoursEachDay[boxNum] = decimal.Parse(textBoxName[boxNum].Text);
                        if (HoursEachDay[boxNum] < 0 || HoursEachDay[boxNum] > 24)
                        {
                            MessageBox.Show($"Please Enter a value between 0 and 24 for {textBoxName}", "Entry Error");
                            textBoxName[boxNum].Focus();
                            return false;
                        }
                    }
                    else
                    {
                        HoursEachDay[boxNum] = -1;
                    }
                }
                catch
                {
                    MessageBox.Show($"Please Enter a numeric value for {textBoxName}", "Entry Error");
                    textBoxName[boxNum].Focus();
                    return false;
                }

            }

            return true;
        }

        //valid the rate 
        public bool validateRate()
        {
            if (!(decimal.TryParse(txtHourlyRate.Text, out hourlyRate)))
            {
                MessageBox.Show("Please enter a numeric value for hourly rate", "Entry Error");
                txtHourlyRate.Focus();
                return false;
            }
            if (hourlyRate < 0)
            {
                MessageBox.Show("Please enter a positive value for hourly rate", "Entry Error");
                txtHourlyRate.Focus();
                return false;
            }
            if (hourlyRate > Math.Round(hourlyRate, 2)) //won't allow partial pennies 
            {
                MessageBox.Show("Please enter a valid currency value up to two decimal places", "Entry Error");
                txtHourlyRate.Focus();
                return false;
            }
            else
            {
                return true;
            }
            
        }

        //validate the names
        public bool validateNames()
        {
            if(txtFirstName.Text.Length == 0)
            {
                MessageBox.Show("Please enter a first name", "Entry Error");
                txtFirstName.Focus();
                return false;
            }
            if(txtLastName.Text.Length == 0)
            {
                MessageBox.Show("Please enter a last name", "Entry Error");
                txtLastName.Focus();
                return false;
            }
            else
            {
                firstName = txtFirstName.Text;
                lastName = txtLastName.Text;
                return true;
            }           
        }


        private void btnCancel_Click(object sender, EventArgs e)
        {
            Close(); //close if they hit cancel button
        }
    }
}
