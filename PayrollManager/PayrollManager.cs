//////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
// Author: Akech Mabior 
// Verison: 1.0
// released: May 07, 2019
// Descripton: A payroll management application that allows the user to 
//
// Description of the functions:
// add: add new employees name, rate of pay, and hours they work and it calculates the payamount including overtime
// edit: edit exisiting employee information and updates the display
// edit the timesheet: edit the timesheet for an existing employee
// remove: remove an employee and all their information
// open: open an emp or other files to easily populate the system 
// save the file to a emp file or the file of the users chose
// save the file to a file name used to populate the application using save or save as to select the file name
//
///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace PayrollManager
{
    public partial class PayrollManager : Form
    {   
        //declaring public variables
        List<Employee> employees = new List<Employee>();
        string fileNameOpen = null;
        public string firstName;
        public string lastName;
        public decimal hourlyRate;
        public decimal[] hoursEachDay = new decimal[7];
        Employee employee;
        bool newOrOpen = false;

        public PayrollManager()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        //add employee info to a list so that is can be saved if user choses 
        public void AddtoList(string status, int index)
        {
            if(status == "new") //add a new employee to list
            {
                employee = new Employee(firstName, lastName, hourlyRate, hoursEachDay);
                employees.Insert(index, employee);
            }
            else if(status == "edit") //update the info for an exisiting employee
            {
                employee = new Employee(firstName, lastName, hourlyRate, hoursEachDay);
                employees.RemoveAt(index);
                employees.Insert(index, employee);
            }
        }

        //this creates the string that is displayed to the listbox
        public void DisplayInfo(string status, int index)
        {
            string displayInfo = null; //string that is displayed to listbox
            displayInfo = $"{firstName,-2} {lastName,-2} {employee.HourlyRate,4:C2}";
            for (int i = 0; i < 7; i++) //adds the hours if they are entered
            {
                if (!(employee.timeSheetData.HourEachDayOfWeek[i] == -1))
                    displayInfo += $"{employee.timeSheetData.HourEachDayOfWeek[i],4}";
                else
                    displayInfo += $"{" ",4}"; //leaves the space blank if nothing was entered
            }
            displayInfo += $"{employee.PayAmount,10:C2}";
            if(status == "new") //if it's new add to the beginning
            {
                listBoxInterface.Items.Insert(index, displayInfo);
            }
            else if(status == "edit") //if it edited add it back in the smae position
            {
                RemoveItem(status, index); // first remove the old entry
                listBoxInterface.Items.Insert(index, displayInfo); //add the new entry back in the same position
            }
        }

        //to open the add employee modal dialog
        private void addNewEmployeeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AddEmployee addEmployee = new AddEmployee();
            DialogResult add = addEmployee.ShowDialog();

            if (add == DialogResult.OK) //if the user pressed ok, use the information to edit the information
            {
                //gets the information entered from the add employee dialog to update the display and list
                firstName = addEmployee.firstName;
                lastName = addEmployee.lastName;
                hourlyRate = addEmployee.hourlyRate;
                hoursEachDay = addEmployee.hoursEachDay;
                AddtoList("new", 0);
                DisplayInfo("new", 0);
                newOrOpen = true;
            }
        }

        //opens the edit employee modal dialog 
        private void editNewEmployeeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            EditNewEmployee editNewEmployee = new EditNewEmployee();
            char[] delimiters = new char[] { '|', ' ' };
            int index = listBoxInterface.SelectedIndex;

            if (listBoxInterface.SelectedIndex == -1)
            {
                MessageBox.Show("Select an employee to edit");
                return;
            }
            else
            {
                //this loop gets the information from the employee list to popuplate the dialog 
                string tokens = employees.ElementAt(index).ToString();
                string[] items = tokens.Split(delimiters, StringSplitOptions.RemoveEmptyEntries);
                for (int i = 0; i < 4; i++)
                {
                    if (i == 0)
                        firstName = items[0].ToString();                        
                    if (i == 1)
                        lastName = items[1].ToString();
                    if (i == 2)
                        hourlyRate = decimal.Parse(items[2]);
                    if (i == 3)
                    {
                        for (int j = 0; j < 7; j++)
                        {
                            hoursEachDay[j] = decimal.Parse(items[j+i]);
                        }
                    }
                }

                editNewEmployee.updateForm(firstName, lastName, hourlyRate, hoursEachDay);

                DialogResult edit = editNewEmployee.ShowDialog();
                if (edit == DialogResult.OK)
                {
                    //use update info from dialog box to update the listbox and list 
                    firstName = editNewEmployee.firstName;
                    lastName = editNewEmployee.lastName;
                    hourlyRate = editNewEmployee.hourlyRate;
                    hoursEachDay = editNewEmployee.hoursEachDay;
                    AddtoList("edit", index);
                    DisplayInfo("edit", index);
                }
            }
        }

        private void saveAsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFileAs();
        }

        //method to save as
        public void SaveFileAs()
        {
            //user selects where to save file
            SaveFileDialog savefileDialog = new SaveFileDialog();
            savefileDialog.Title = "Save File";
            savefileDialog.DefaultExt = "emp";
            savefileDialog.Filter = "emp Files (*.emp) | *.emp| All Files (*.*)| *.*";
            savefileDialog.FilterIndex = 1;
            if (savefileDialog.ShowDialog() != DialogResult.OK)
            {
                //incase the user pressed can by accident they are again prompted and they can cancel from this point or save
                DialogResult button = MessageBox.Show("The file was not saved, would you like to save the file?", "Save As", MessageBoxButtons.YesNo);
                if (button == DialogResult.Yes)
                {
                    SaveFileAs();
                }
                else
                    return;
            }
            //the user pressed ok and the file is passed to be read
            else
            {
                writeToFile(savefileDialog.FileName); //success and file will be saved
            }
        }

        private void writeToFile(string fileName)
        {
            //writes to file specified by user 
            StreamWriter writer = new StreamWriter(fileName);

            foreach (Employee employee in employees)
            {
                writer.WriteLine(employee);
            }
            writer.Close(); //always close the file so it is available to others asap
        }

        private void listBoxInterface_SelectedIndexChanged(object sender, EventArgs e)
        {
            
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFile();
        }
        public void SaveFile()
        {

            //user selects where to save file if they did not already have a filename that they were using 
            SaveFileDialog savefileDialog = new SaveFileDialog();
            savefileDialog.Title = "Save File";
            savefileDialog.DefaultExt = "emp";
            savefileDialog.Filter = "emp Files (*.emp) | *.emp| All Files (*.*)| *.*";
            savefileDialog.FilterIndex = 1;
            if (!(fileNameOpen == null))
            {
                writeToFile(fileNameOpen);
            }
            else if (savefileDialog.ShowDialog() != DialogResult.OK)
            {
                DialogResult button = MessageBox.Show("The file was not saved, would you like to save the file?", "Save As", MessageBoxButtons.YesNo);
                if (button == DialogResult.Yes)
                {
                    SaveFileAs();
                }
                else
                    return;
            }
            //the user pressed ok and the file is passed to be read
            else
            {
                writeToFile(savefileDialog.FileName); //success again and file will be saved
            }
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            openFile();
        }
        public void openFile()
        {
            try
            {
                //Load the file to read
                //initialise openfiledialog 
                OpenFileDialog fileDialog = new OpenFileDialog();

                //set propperties of openfiledialog
                fileDialog.Title = "Open File";
                fileDialog.Filter = " Files (*.emp) | *.emp| All Files (*.*)| *.*";
                fileDialog.FilterIndex = 1;
                //compare the result of openfiledialog to the results Ok
                if (fileDialog.ShowDialog() != DialogResult.OK)
                {
                    return;
                }
                //the user pressed ok and the file is passed to be read
                else
                {
                    newOrOpen = true;
                    fileNameOpen = fileDialog.FileName;
                    ReadFile(fileDialog.FileName);
                }
            }
            catch
            {
                MessageBox.Show($"{fileNameOpen} could not be opened", "Error");
            }

        }

        public void ReadFile(string fileName)
        {
            StreamReader fileToRead;
            char[] delimiters = new char[] { '|', ' ' };

            try
            {
                //intialise the textreader
                fileToRead = new StreamReader(fileName);
            }
            catch
            {
                //a message showing the user that a error occured opening their file
                MessageBox.Show($"File {fileName} did not open.");
                return;
            }

            //read the file until there is no more to be read
            while (!fileToRead.EndOfStream)
            {
                string tokens = fileToRead.ReadLine(); //lines read save as tokens

                string[] employeeInfo = tokens.Split(delimiters, StringSplitOptions.RemoveEmptyEntries); //split line
                
                for(int i=0; i<4; i++)
                {
                    if (i == 0)
                        firstName = employeeInfo[0].ToString();
                    if (i== 1)
                        lastName = employeeInfo[1].ToString();
                    if (i == 2)
                        hourlyRate = decimal.Parse(employeeInfo[2]);
                    if(i==3)
                    {
                        for(int j=0;j<7;j++)
                        {
                            hoursEachDay[j] = decimal.Parse(employeeInfo[j+i]);
                        }
                    }
                }
                AddtoList("new", 0);
                DisplayInfo("new", 0);

            }
            //close the file
            fileToRead.Close();
        }

        private void newToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if(newOrOpen == true)
            {
                //they wish to open a new file
                SaveFileAs(); //first save the file they were working on if they wish
                listBoxInterface.Items.Clear(); //clear the display
                openFile(); //prompt to select the file they wish to open
            }
            else
            {
                openFile(); //they have no work and they really only need to open a new file
            }

        }

        private void closeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFileAs(); //they are closing prompt to save work
            listBoxInterface.Items.Clear(); //clear the display
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFileAs(); //they are completely exiting the application , prompt again to save
            Close(); //close the application gracefully
        }

        private void removeEmployeeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            RemoveItem("remove", listBoxInterface.SelectedIndex); //remove the employee they selected
        }

        public void RemoveItem(string status, int index)
        {
            if(index == -1)
            {
                MessageBox.Show("Select an item"); //if they didn't select an employee
                return;
            }
            if(status == "edit") //used to remove a display on listbox before displaying edited employee
            {
                listBoxInterface.Items.RemoveAt(index);
            }
            else if(status == "remove") //used to completely remove an employee
            {
                //first prompt in case they accidently pressed or want to change their mind 
                DialogResult button = MessageBox.Show("Would you like to remove this employee?", "Remove employee", MessageBoxButtons.YesNo);
                if (button == DialogResult.Yes)
                {
                    listBoxInterface.Items.RemoveAt(index);
                    employees.RemoveAt(index);
                }
            }

        }

        //opens the enter time sheet modal dialog 
        private void enterTimesheetDataToolStripMenuItem_Click(object sender, EventArgs e)
        {
            UpdateTimesheetData updateTimesheet = new UpdateTimesheetData();
            char[] delimiters = new char[] { '|', ' ' };
            int index = listBoxInterface.SelectedIndex;

            if (listBoxInterface.SelectedIndex == -1)
            {
                MessageBox.Show("Select an employee to edit");
                return;
            }
            else
            {
                string tokens = employees.ElementAt(index).ToString();
                string[] items = tokens.Split(delimiters, StringSplitOptions.RemoveEmptyEntries);
                for (int i = 0; i < 4; i++)
                {
                    if (i == 0)
                        firstName = items[0].ToString();
                    if (i == 1)
                        lastName = items[1].ToString();
                    if (i == 2)
                        hourlyRate = decimal.Parse(items[2]);
                    if (i == 3)
                    {
                        for (int j = 0; j < 7; j++)
                        {
                            hoursEachDay[j] = decimal.Parse(items[j + i]);
                        }
                    }
                }

                updateTimesheet.updateTimesheet(hoursEachDay);

                DialogResult update = updateTimesheet.ShowDialog();
                if (update == DialogResult.OK)
                {
                    hoursEachDay = updateTimesheet.hoursEachDay;
                    AddtoList("edit", index);
                    DisplayInfo("edit", index);
                }
            }
        }
    }
}
