using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PayrollManager
{
    class Employee
    {
        //set the private variables
        private string firstName;
        private string lastName;
        private decimal hourlyRate;

        //set the public varibales
        public string  fullName;
        public decimal payAmount;
        public TimeSheetData timeSheetData = new TimeSheetData();

        //firstname property 
        public string FirstName
        {
            get { return firstName; }
            set
            {
                //exception handling for case where nothing is entered 
                if (value.Length == 0)
                {
                    throw new Exception("Please enter a first name, last name has a length of zero");
                }
                else
                {
                    firstName = value;
                }
            }
        }

        //last name property
        public string LastName
        {
            get { return lastName;}
            set
            {
                //exception handling for case where nothing is entered 
                if (value.Length == 0)
                {
                    throw new Exception("Please enter a last name, last name has a length of zero");
                }
                else
                {
                    LastName = value;
                }
            }
        }

        //hourly rate property 
        public decimal HourlyRate
        {
            get { return hourlyRate; }
            set
            {
                if (hourlyRate <0) //validated that the pay is not negative
                {
                    throw new Exception("Please enter a positive value");
                }
                if (hourlyRate > Math.Round(hourlyRate, 2))  //validates that the pay is not partial pennies
                {
                    throw new Exception("Please enter a valid dollar value");
                }
                else //sets the value
                {
                    hourlyRate = value;
                }                
            }
        }

        //fullname readonly property
        public string FullName
        {
            get { return fullName = $"{firstName} {lastName}"; }
        }

        //payamount readonly property
        public decimal PayAmount
        {
            get { return payAmount;}                
        }

        //null constructor 
        public Employee()
        {
            firstName = null;
            lastName = null;
            hourlyRate = 0;
        }

        //constructor for three parameters 
        public Employee(string firstName, string lastName, decimal hourlyRate)
        {
            this.firstName = firstName;
            this.lastName = lastName;
            this.hourlyRate = hourlyRate;
        }

        //added the timesheet data reference
        public Employee(TimeSheetData timeSheetData)
        {
            this.timeSheetData = timeSheetData;
        }
        
        //constructor that also takes the hours each day
        public Employee(string firstName, string lastName, decimal hourlyRate, decimal[] hoursEachDay)
        {
            this.firstName = firstName;
            this.lastName = lastName;
            this.hourlyRate = hourlyRate;
            timeSheetData = new TimeSheetData(hoursEachDay);
            if(timeSheetData.totalHours > (decimal)37.5)
            {
                payAmount = ((decimal)37.5  * hourlyRate) + (timeSheetData.OverTimeHours*(hourlyRate*(decimal)1.5));
            }
            else
            {
                payAmount = timeSheetData.totalHours * hourlyRate;
            }
        }

        //constructor to calculate the payamount 
        public decimal CalculatePay(decimal regularHours, decimal overtimeRate)
        {
            return payAmount = regularHours * overtimeRate;
        }

        //tostring constructor to create the employee list 
        public override string ToString()
        {
            return $"{firstName.Trim(' ')}|{lastName.Trim(' ')}|{hourlyRate.ToString().Trim(' ')}|{timeSheetData.ToString().Trim(' ')}";
        }
    }
}
