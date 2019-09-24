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
    public partial class AddEmployee : Form
    {
        public decimal[] hoursEachDay = new decimal[7];
        public decimal hourlyRate;
        public string firstName;
        public string lastName;

        public AddEmployee()
        {
            InitializeComponent();
        }

        //the add button event handler
        private void btnAdd_Click(object sender, EventArgs e)
        {
            //textboxes for the textboxes on display
            TextBox[] textBoxes = new TextBox[] { txtSundayHours, txtMondayHours, txtTuesdayHours, txtWednesdayHours,
             txtThursdayHours, txtFridayHours, txtSaturdayHours };

            if (validateHours(textBoxes, hoursEachDay) && validateRate() && validateNames()) //if everything is valid after the press ok
            {
                DialogResult = DialogResult.OK; //set results to ok 
                Close(); // and close the modal dialog to return to payroll listbox 
            }
        }
       


        public bool validateHours(TextBox[] textBoxName, decimal[] HoursEachDay) //valid the hours 
        {
            for(int boxNum = 0; boxNum<7; boxNum++)
            {
                try
                {
                    if (!(textBoxName[boxNum].Text.Length == 0)) //if they are not empty
                    {
                        HoursEachDay[boxNum] = decimal.Parse(textBoxName[boxNum].Text);
                        if (HoursEachDay[boxNum] < 0 || HoursEachDay[boxNum] > 24) //if they are greater than hours in a day
                        {
                            MessageBox.Show($"Please Enter a value between 0 and 24 for {textBoxName}", "Entry Error");
                            textBoxName[boxNum].Focus();
                            return false;
                        }
                    }
                    else
                    {
                        HoursEachDay[boxNum] = -1; //set to -1 so that it's placed as empty and can be left empty when displayed 
                    }
                }
                catch
                {
                    //catch if a non-decimal and cannot be parsed 
                    MessageBox.Show($"Please Enter a numeric value for {textBoxName}", "Entry Error");
                    textBoxName[boxNum].Focus();
                    return false;
                }

            }

            return true;
        }

        public bool validateRate() //validate what they be payed 
        {
            if (!(decimal.TryParse(txtHourlyRate.Text, out hourlyRate))) //number cannot be parsed 
            {
                MessageBox.Show("Please enter a numeric value for hourly rate", "Entry Error");
                txtHourlyRate.Focus();
                return false;
            }
            if (hourlyRate < 0) //trying to pay them negative value
            {
                MessageBox.Show("Please enter a positive value for hourly rate", "Entry Error");
                txtHourlyRate.Focus();
                return false;
            }
            if(hourlyRate > Math.Round(hourlyRate,2)) //won't allow partial pennies 
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

        public bool validateNames()  //valid the name 
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
            Close(); //if they can, it closes the modal dialog
        }

        private void AddEmployee_Load(object sender, EventArgs e)
        {

        }
    }
}
