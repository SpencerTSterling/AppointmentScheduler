using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static C969_Software2_SpencerSterling_v1.DBConnection;

namespace C969_Software2_SpencerSterling_v1
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
            // Populate the DataGridView with appointments
            DBHelper.GetAllAppointments(dataGridView_Appointments, conn);

            // Change the TimeZone label

            // Get the user's current time zone
            TimeZoneInfo userTimeZone = TimeZoneInfo.Local;
            label_TimeZoneInput_main.Text = userTimeZone.DisplayName;
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            // Open the connection
            DBConnection.startConnection();

            // Populate the DataGridView with the existing appointment data
            DBHelper.GetAllAppointments(dataGridView_Appointments, conn);
            Helper.formatDGV(dataGridView_Appointments);

            // Check "All Appointments" by default
            rad_AllAppointments_main.Checked = true;

        }

        private void PopulateDataGridView(DataTable appointments)
        {
            dataGridView_Appointments.DataSource = appointments;
        }

        private void button_Appointments_main_Click(object sender, EventArgs e)
        {
            new Appointments().Show();
        }

        private void button_Customers_main_Click(object sender, EventArgs e)
        {
            new Customers().Show();
        }

        private void button_Reports_main_Click(object sender, EventArgs e)
        {
            new Reports().Show();
        }

        private void rad_AllAppointments_main_CheckedChanged(object sender, EventArgs e)
        {
            DBConnection.startConnection();
            DBHelper.GetAllAppointments(dataGridView_Appointments, conn);
        }

        private void rad_CurrentWeek_main_CheckedChanged(object sender, EventArgs e)
        {
            //Get current date
            DateTime today = DateTime.Today;

            // Determine start and end dates of the current week
            DateTime startDate = today.AddDays(-(int)today.DayOfWeek + (int)DayOfWeek.Monday);
            DateTime endDate = startDate.AddDays(6); // Sunday is the end of the week

            // Retrieve appointments for the current week
            DataTable appointments = DBHelper.GetAppointmentsByDateRange(startDate, endDate);

            // Populate the DataGridView with the retrieved appointments
            PopulateDataGridView(appointments);
        }

        private void rad_CurrentMonth_main_CheckedChanged(object sender, EventArgs e)
        {
            // Determine start and end dates of the current month
            DateTime startDate = new DateTime(DateTime.Today.Year, DateTime.Today.Month, 1);
            DateTime endDate = startDate.AddMonths(1).AddTicks(-1);

            // Retrieve appointments for the current month
            DataTable appointments = DBHelper.GetAppointmentsByDateRange(startDate, endDate);

            // Populate the DataGridView with the retrieved appointments
            PopulateDataGridView(appointments);
        }

        private void RefreshView_Click(object sender, EventArgs e)
        {
            rad_AllAppointments_main.Checked = true;
            DBConnection.startConnection();
            DBHelper.GetAllAppointments(dataGridView_Appointments, conn);
        }

        private void datetimePicker_searchAppointment_main_ValueChanged(object sender, EventArgs e)
        {
            // Get the selected date from the datetime picker
            DateTime selectedDate = datetimePicker_searchAppointment_main.Value.Date;

            // Determine start and end 
            DateTime startDate = selectedDate.Date;
            DateTime endDate = selectedDate.Date.AddDays(1).AddSeconds(-1); // End of the selected day

            // Retrieve appointments for the current month
            DataTable appointments = DBHelper.GetAppointmentsByDateRange(startDate, endDate);

            // Populate the DataGridView with the retrieved appointments
            PopulateDataGridView(appointments);
        }

        private void button_Logout_main_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
