using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace C969_Software2_SpencerSterling_v1
{
    public partial class Reports : Form
    {
        // Define an array of month names
        private string[] monthNames = { "January", "February", "March", "April", "May", "June", "July", "August", "September", "October", "November", "December" };

        public Reports()
        {
            InitializeComponent();
        }

        private void Reports_Load(object sender, EventArgs e)
        {
            PopulateMonths(); // contains lambda expression to fill combobox w months
            PopulateUsers(); // contains lambda expression to fill combobox w users

            // Format REPORT 1 - Appointment Types - datagridview
            Helper.formatDGV(dataGridView_AppointmentTypes);
            Helper.formatDGVColumns(dataGridView_AppointmentTypes);

            // Format REPORT 2 - User Schedules 
            Helper.formatDGV(dataGridView_UserSchedules);

            // Format REPORT 3 - Customer count By City
            Helper.formatDGV(dataGridView_CustomerCountCity);
            Helper.formatDGVColumns(dataGridView_CustomerCountCity);

            // REPORT 1
            DBConnection.startConnection();
            string selectedMonth = comboBox_Months.SelectedItem.ToString();
            Dictionary<string, int> appointmentTypeCounts = DBHelper.GetAppointmentTypesByMonth(selectedMonth);
            PopulateDataGridView_AppointmentTypes(appointmentTypeCounts);

            // REPORT 3
            DBConnection.startConnection();
            List<(string city, int count)> customerCountByCity = DBHelper.GetCustomerCountByCity();
            PopulateDataGridView_CustomerCountCity(customerCountByCity); // contains lambda expression to fill datagridview

        }

        private void PopulateUsers()
        {
            DBConnection.startConnection();
            var users = DBHelper.GetAllUsers(DBConnection.conn);

            // LAMBDA EXPRESSION FOR REPORT 2
            users.ForEach(user => comboBox_Users.Items.Add(user));

        }

        private void PopulateMonths()
        {
            // LAMBDA EXPRESSION FOR REPORT 1 
            monthNames.ToList().ForEach(month => comboBox_Months.Items.Add(month));
            //Set the default selection to January
            comboBox_Months.SelectedIndex = 0;
        }


        private void PopulateDataGridView_AppointmentTypes(Dictionary<string, int> appointmentTypesCount)
        {
            // Clear the DataGridView
            dataGridView_AppointmentTypes.Rows.Clear();

            // Check if columns already exist; if not, add them
            if (dataGridView_AppointmentTypes.Columns.Count == 0)
            {
                dataGridView_AppointmentTypes.Columns.Add("ColumnAppointmentType", "Appointment Type");
                dataGridView_AppointmentTypes.Columns.Add("ColumnCount", "Count");
            }

            // Loop through the dictionary and add each appointment type and its count to the DataGridView
            foreach (var kvp in appointmentTypesCount)
            {
                int rowIndex = dataGridView_AppointmentTypes.Rows.Add();
                dataGridView_AppointmentTypes.Rows[rowIndex].Cells["ColumnAppointmentType"].Value = kvp.Key;
                dataGridView_AppointmentTypes.Rows[rowIndex].Cells["ColumnCount"].Value = kvp.Value;
            }
        }

        private void PopulateDataGridView_UserSchedules(int userId)
        {
            // Get the schedule for the selected user
            DataTable schedule = DBHelper.GetUserSchedule(userId);
            // Check if the schedule is not null and has rows
            if (schedule != null && schedule.Rows.Count > 0)
            {
                // Bind the DataTable to the DataGridView
                dataGridView_UserSchedules.DataSource = schedule;
            }
        }

        private void PopulateDataGridView_CustomerCountCity(List<(string city, int count)> customerCountByCity)
        {
            // Clear existing rows in the DataGridView
            dataGridView_CustomerCountCity.Rows.Clear();

            //Check if the column already exists; if not add them
            if (dataGridView_CustomerCountCity.Columns.Count == 0)
            {
                dataGridView_CustomerCountCity.Columns.Add("ColumnCity", "City");
                dataGridView_CustomerCountCity.Columns.Add("ColumnCount", "Count");
            }

            // Populate the DataGridView with the customer count by city data
            // USES LAMBDA EXPRESSION 
            customerCountByCity.ForEach(item =>
            {
                int rowIndex = dataGridView_CustomerCountCity.Rows.Add();
                dataGridView_CustomerCountCity.Rows[rowIndex].Cells["ColumnCity"].Value = item.city;
                dataGridView_CustomerCountCity.Rows[rowIndex].Cells["ColumnCount"].Value = item.count;
            });
        }

        private void comboBox_Months_SelectedIndexChanged(object sender, EventArgs e)
        {
            // retrieve the selected month
            string selectedMonth = comboBox_Months.SelectedItem.ToString();

            // get the appointment types and their counts
            Dictionary<string, int> appointmentTypeCounts = DBHelper.GetAppointmentTypesByMonth(selectedMonth);

            // Populate the datagridview
            PopulateDataGridView_AppointmentTypes(appointmentTypeCounts);
        }

        private void comboBox_Users_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Get the selected user ID from the combo box
            if (comboBox_Users.SelectedItem != null)
            {
                string selectedUserName = comboBox_Users.SelectedItem.ToString();

                // Assuming you have a method to retrieve user ID from user name
                int userId = DBHelper.GetUserId(selectedUserName);

                // Populate the DataGridView with the selected user's schedule
                PopulateDataGridView_UserSchedules(userId);
            }
        }
    }
}
