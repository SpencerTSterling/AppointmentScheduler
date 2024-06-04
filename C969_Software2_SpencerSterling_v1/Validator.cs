using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace C969_Software2_SpencerSterling_v1
{
    class Validator
    {

        public static bool isTextValid(string text) => !string.IsNullOrEmpty(text);
        public static bool isTextValidInt(string text)
        {
            return !string.IsNullOrEmpty(text) && int.TryParse(text, out int result) && result >= 0;
        }
        public static bool isValidPhoneNumber(string phoneNumber)
        {
            // Phone number should contain only digits and dashes
            return Regex.IsMatch(phoneNumber, @"^[0-9\-]*$");
        }

        public static bool ValidateCustomerInputs(string name, string address, string zipcode, string phone)
        {
            if (!isTextValid(name))
            {
                MessageBox.Show("Name must be a valid string.", "Invalid Input", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            if (!isTextValid(address))
            {
                MessageBox.Show("Address must be a valid string.", "Invalid Input", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            if (!isTextValidInt(zipcode))
            {
                MessageBox.Show("Zipcode must only contain digits and cannot be empty.", "Invalid Input", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            if (!isValidPhoneNumber(phone) || !isTextValid(phone))
            {
                MessageBox.Show("Phone numbers must only contain digits and dashes.", "Invalid Input", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            return true;
        }

        public static bool ValidateAppointmentInputs(string title, string description, string type, string location)
        {

            if (!isTextValid(title))
            {
                MessageBox.Show("Title must be a valid string.", "Invalid Input", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            if (!isTextValid(description))
            {
                MessageBox.Show("Description must be a valid string.", "Invalid Input", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            if (!isTextValid(type))
            {
                MessageBox.Show("Type must be a valid string.", "Invalid Input", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            if (!isTextValid(location))
            {
                MessageBox.Show("Location must be a valid string.", "Invalid Input", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            return true;

        }

        /// <summary>
        /// Checks if customerId is a valid int, and checks if the ID exists in the database
        /// </summary>
        /// <param name="customerId"></param>
        /// <returns></returns>
        public static bool ValidateCustomerId(string customerId)
        {
            if (!isTextValidInt(customerId))
            {
                MessageBox.Show("CustomerID must be a valid non-negative interger value.", "Invalid CustomerID", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            if (!DBHelper.doesCustomerIdExist(Convert.ToInt32(customerId)))
            {
                MessageBox.Show("This customerID does not exist.", "Invald CustomerID", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            else
            {
                return true;
            }
        }

        /// <summary>
        /// Checks if userID is a valid int, and checks if the ID exists in the database
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public static bool ValidateUserId(string userId)
        {
            if (!isTextValidInt(userId))
            {
                MessageBox.Show("UserID must be a valid non-negative interger value.", "Invalid UserID", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            if (!DBHelper.doesUserIdExist(Convert.ToInt32(userId)))
            {
                MessageBox.Show("This userID does not exist.", "Invald UserID", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            else
            {
                return true;
            }
        }

        public static bool ValidateAppointmentDateTime(string userId, DateTime start, DateTime end)
        {
            DateTime appointmentDay = start.Date;

            // Check if the appointment is within business hours
            if (!IsWithinBusinessHours(start, end))
            {
                MessageBox.Show("Appointments can only be scheduled between 9AM and 5PM Monday to Friday.", 
                    "Business Hours", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            //Check if the appointment is overlapping

            // get the user's schedule for that day
            DataTable schedule = DBHelper.GetUserScheduleForDay(Convert.ToInt32(userId), appointmentDay);

            if (!DBHelper.doesAppointmentOverlap(Convert.ToInt32(userId), start, end))
            {
                // Construct message with existing appointment times
                StringBuilder messageBuilder = new StringBuilder();
                messageBuilder.AppendLine("The user already has appointments during the following times on that day:");

                foreach (DataRow row in schedule.Rows)
                {
                    DateTime appointmentStart = Helper.ConvertUtcToLocalTime(Convert.ToDateTime(row["start"]));
                    DateTime appointmentEnd = Helper.ConvertUtcToLocalTime(Convert.ToDateTime(row["end"]));
                    messageBuilder.AppendLine($"{appointmentStart.ToShortTimeString()} - {appointmentEnd.ToShortTimeString()}");
                }

                MessageBox.Show(messageBuilder.ToString(), "Appointment Overlap", MessageBoxButtons.OK);
                return false;

            }
            
            return true;
        }

        public static bool ValidateNewAppointmentDateTime(string userId, int appointmentId, DateTime newStart, DateTime newEnd)
        {
            DateTime appointmentDay = newStart.Date;

            // Check if the appointment is within business hours
            if (!IsWithinBusinessHours(newStart, newEnd))
            {
                MessageBox.Show("Appointments can only be scheduled between 9AM and 5PM Monday to Friday.",
                    "Business Hours", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            // get the user's schedule for the day
            DataTable schedule = DBHelper.GetUserScheduleForDay(Convert.ToInt32(userId), appointmentDay);

            // Check if there an overlap, EXCLUDING the original appointment start/end dates
            if (DBHelper.doesAppointmentOverlapExcluding(Convert.ToInt32(userId), appointmentId, newStart, newEnd))
            {
                // Construct message with existing appointment times
                StringBuilder messageBuilder = new StringBuilder();
                messageBuilder.AppendLine("The user already has appointments during the following times on that day:");

                foreach (DataRow row in schedule.Rows)
                {
                    DateTime appointmentStart = Helper.ConvertUtcToLocalTime(Convert.ToDateTime(row["start"]));
                    DateTime appointmentEnd = Helper.ConvertUtcToLocalTime(Convert.ToDateTime(row["end"]));
                    messageBuilder.AppendLine($"{appointmentStart.ToShortTimeString()} - {appointmentEnd.ToShortTimeString()}");
                }

                MessageBox.Show(messageBuilder.ToString(), "Appointment Overlap", MessageBoxButtons.OK);
                return false;
            }

            return true;

        }

        /// <summary>
        /// Checks if the specfied appointment time is within the business hours.
        /// Business hours are Monday-Friday, from 9:00AM to 5:00PM
        /// </summary>
        /// <param name="start"></param>
        /// <param name="end"></param>
        /// <returns></returns>
        private static bool IsWithinBusinessHours(DateTime start, DateTime end)
        {
           // Check if the appointment is on Sunday
           if (start.DayOfWeek == DayOfWeek.Saturday || start.DayOfWeek == DayOfWeek.Sunday)
            {
                return false;
            }

            // Check if the appointment start time is between 9 AM and 5 PM
            TimeSpan startTime = start.TimeOfDay;
            TimeSpan endTime = end.TimeOfDay;
            TimeSpan businessStartTime = TimeSpan.FromHours(9); // 9 AM
            TimeSpan businessEndTime = TimeSpan.FromHours(17); // 5 PM
            if (startTime < businessStartTime || endTime > businessEndTime)
            {
                return false;
            }

            return true;
        }

        public static string TrimWhitespace(string input)
        {
            // Trim leading and trailing whitespace
            return input.Trim();
        }
    }
}
