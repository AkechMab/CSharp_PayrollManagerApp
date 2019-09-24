using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PayrollManager
{
    class TimeSheetData
    {
        //declare the varibales
        public decimal sundayHours;
        public decimal mondayHours;
        public decimal tuesdayHours;
        public decimal wednesdayHours;
        public decimal thursdayHours;
        public decimal fridayHours;
        public decimal saturdayHours;
        public decimal totalHours;
        public decimal overtimeHours;
        public decimal[] hourEachDayOfWeek = new decimal[7];
        public decimal standardOvertime = (decimal)37.5;

        //accessor properties of sundayHours
        public decimal SundayHours
        {
            get { return sundayHours; }
            set
            {
                if(value < 0 || value >24) //throw an exception if the hours are less or greater than the hours in a day
                {
                    throw new Exception("Please enter a number of hours between 0 and 24");
                }
                else
                {
                    sundayHours = value; //assigned the value being assigned to the property
                }
            }
        }

        //accessor properties of mondayHours
        public decimal MondayHours
        {
            get { return mondayHours; }
            set
            {
                if (value < 0 || value > 24) //throw an exception if the hours are less or greater than the hours in a day
                {
                    throw new Exception("Please enter a number of hours between 0 and 24");
                }
                else
                {
                    mondayHours = value; //assigned the value being assigned to the property
                }
            }
        }

        //accessor properties of tuesdayHours
        public decimal TuesdayHours
        {
            get { return TuesdayHours; }
            set
            {
                if (value < 0 || value > 24) //throw an exception if the hours are less or greater than the hours in a day
                {
                    throw new Exception("Please enter a number of hours between 0 and 24");
                }
                else
                {
                    TuesdayHours = value; //assigned the value being assigned to the property
                }
            }
        }

        //accessor properties of wednesdayHours
        public decimal WednesdayHours
        {
            get { return wednesdayHours; }
            set
            {
                if (value < 0 || value > 24) //throw an exception if the hours are less or greater than the hours in a day
                {
                    throw new Exception("Please enter a number of hours between 0 and 24");
                }
                else
                {
                    wednesdayHours = value; //assigned the value being assigned to the property
                }
            }
        }

        //accessor properties of thursdayHours
        public decimal ThursdayHours
        {
            get { return thursdayHours; }
            set
            {
                if (value < 0 || value > 24) //throw an exception if the hours are less or greater than the hours in a day
                {
                    throw new Exception("Please enter a number of hours between 0 and 24");
                }
                else
                {
                    thursdayHours = value; //assigned the value being assigned to the property
                }
            }
        }

        //accessor properties of fridayHours
        public decimal FridayHours
        {
            get { return fridayHours; }
            set
            {
                if (value < 0 || value > 24) //throw an exception if the hours are less or greater than the hours in a day
                {
                    throw new Exception("Please enter a number of hours between 0 and 24");
                }
                else
                {
                    fridayHours = value; //assigned the value being assigned to the property
                }
            }
        }

        //accessor properties of saturdayHours
        public decimal SaturdayHours
        {
            get { return saturdayHours; }
            set
            {
                if (value < 0 || value > 24) //throw an exception if the hours are less or greater than the hours in a day
                {
                    throw new Exception("Please enter a number of hours between 0 and 24");
                }
                else
                {
                    saturdayHours = value; //assigned the value being assigned to the property
                }
            }
        }

        //accessor properties of hours worked each day
        public decimal[] HourEachDayOfWeek
        {

            get { return hourEachDayOfWeek; }
            set
            {
                hourEachDayOfWeek = value;
            }
        }

        //accessor properties of totalHours
        public decimal TotalHours
        {
            get
            {
                return totalHours;
            }
        }

        //accessor properties of overtimeHours
        public decimal OverTimeHours
        {
            get
            {
                return overtimeHours;
            }
            set
            {
                if (totalHours > standardOvertime)
                {
                    overtimeHours = totalHours - standardOvertime;
                }
                else
                {
                    overtimeHours =  0;
                }
            }
        }

        //null constructor 
        public TimeSheetData()
        {
            sundayHours = 0;
            mondayHours = 0;
            tuesdayHours = 0;
            wednesdayHours = 0;
            thursdayHours = 0;
            fridayHours = 0;
            saturdayHours = 0;;
            totalHours = sundayHours + mondayHours + tuesdayHours + wednesdayHours + thursdayHours + fridayHours + saturdayHours;
            if (totalHours > standardOvertime)
            {
                overtimeHours = totalHours - standardOvertime;
            }
            else
            {
                overtimeHours = 0;
            }
        }

        //sets the sunday and saturday hours to zero
        public TimeSheetData(decimal mondayHours, decimal tuesdayHours, decimal wednesdayHours, decimal thursdayHours, decimal fridayHours)
        {
            sundayHours = 0;
            this.mondayHours = mondayHours;
            this.tuesdayHours = tuesdayHours;
            this.wednesdayHours = wednesdayHours;
            this.thursdayHours = thursdayHours;
            this.fridayHours = fridayHours;
            saturdayHours = 0;
            totalHours = sundayHours + mondayHours + tuesdayHours + wednesdayHours + thursdayHours + fridayHours + saturdayHours;
            if (totalHours > standardOvertime)
            {
                overtimeHours = totalHours - standardOvertime;
            }
            else
            {
                overtimeHours = 0;
            }
        }

        //seven parameter constructor to set the hours each day
        public TimeSheetData(decimal sundayHours, decimal mondayHours, decimal tuesdayHours, decimal wednesdayHours, decimal thursdayHours, decimal fridayHours, decimal saturdayHours)
        {
            this.sundayHours = sundayHours;
            this.mondayHours = mondayHours;
            this.tuesdayHours = tuesdayHours;
            this.wednesdayHours = wednesdayHours;
            this.thursdayHours = thursdayHours;
            this.fridayHours = fridayHours;
            this.saturdayHours = saturdayHours;
            totalHours = sundayHours + mondayHours + tuesdayHours + wednesdayHours + thursdayHours + fridayHours + saturdayHours;
            if (totalHours > standardOvertime)
            {
                overtimeHours = totalHours - standardOvertime;
            }
            else
            {
                overtimeHours = 0;
            }
        }

        //constructor that takes a seven decimal array
        public TimeSheetData(decimal[] hoursEachDay)
        {
            for (int day = 0; day < 7; day++)
            {
                if (!(hoursEachDay[day] == -1))
                {
                    hourEachDayOfWeek[day] = hoursEachDay[day];
                    totalHours += hoursEachDay[day];
                }
                else
                {
                    hourEachDayOfWeek[day] = -1;
                    hoursEachDay[day] = 0;
                }
            }
            sundayHours = hoursEachDay[0];
            mondayHours = hoursEachDay[1];
            tuesdayHours = hoursEachDay[2];
            wednesdayHours = hoursEachDay[3];
            thursdayHours = hoursEachDay[4]; 
            fridayHours = hoursEachDay[5];
            saturdayHours = hoursEachDay[6];
            if (totalHours > standardOvertime)
            {
                overtimeHours = totalHours - standardOvertime;
            }
            else
            {
                overtimeHours = 0;
            }
        }

        //tostring class that nicely has all the hours each day work, will be used in the employee class
        public override string ToString()
        {
            string toString = null;
 
            for (int day = 0; day < 7; day++)
            {
               if(!(hourEachDayOfWeek[day] == -1))
                {
                    toString += $"{hourEachDayOfWeek[day],7}";
                }
                else
                {
                    toString += $" {0,7}";
                }
            }            
            return toString;
        }

    }
}
