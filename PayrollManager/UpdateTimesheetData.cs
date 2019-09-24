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
    public partial class UpdateTimesheetData : Form //future base class for add and edit employee 
    {
        public decimal[] hoursEachDay = new decimal[7];

        public UpdateTimesheetData()
        {
            InitializeComponent();
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            TextBox[] textBoxes = new TextBox[] {txtSundayHours, txtMondayHours, txtTuesdayHours, txtWednesdayHours,
             txtThursdayHours, txtFridayHours, txtSaturdayHours};
            if (validateHours(textBoxes, hoursEachDay))
            {
                DialogResult = DialogResult.OK;
                Close();
            }
        }

        //add hours to timesheet, prepopulate the timesheet 
        public void updateTimesheet(decimal[] hoursEachDay)
        {
            TextBox[] textBoxes = new TextBox[] {txtSundayHours, txtMondayHours, txtTuesdayHours, txtWednesdayHours,
             txtThursdayHours, txtFridayHours, txtSaturdayHours};

            for (int i = 0; i < 7; i++)
            {
                textBoxes[i].Text = Convert.ToString(hoursEachDay[i]);
            }
        }

        //validate the hours
        public bool validateHours(TextBox[] textBoxName, decimal[] HoursEachDay)
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


        private void btnCancel_Click_1(object sender, EventArgs e)
        {
            Close(); //close the modal dialog 
        }
    }
}
