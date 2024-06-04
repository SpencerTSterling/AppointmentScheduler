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
    public partial class Appointments : Form
    {
        public Appointments()
        {
            InitializeComponent();
            // Open the connection
            DBConnection.startConnection();

            // Populate the DataGridView with the existing appointment data
            DBHelper.GetAllAppointments(dataGridView_allAppointments, conn);

            Helper.formatDTPforTime(dateTime_AppointmentTime);
            Helper.formatDTPforDate(dateTime_AppointmentDate);
        }

        private void Appointments_Load(object sender, EventArgs e)
        {
            // Set the default RadioButton selection to "15 minutes" or option 1
            radio_Option1.Checked = true;
            // Format the DataGridView
            Helper.formatDGV(dataGridView_allAppointments);
        }


        private void ClearFields()
        {
            text_AppointmentType.Text = string.Empty;
            text_AppointmentCustomer.Text = string.Empty;
            text_AppointmentTitle.Text = string.Empty;
            text_AppointmentDesc.Text = string.Empty;
            text_AppointmentLocation.Text = string.Empty;
            text_AppointmentUser.Text = string.Empty;
            dateTime_AppointmentDate.Value = DateTime.Today;
            dateTime_AppointmentTime.Value = DateTime.Now;
            // Set the default RadioButton selection to "15 minutes" or option 1
            radio_Option1.Checked = true;
        }

        /// <summary>
        /// Determine the length of an appointment
        /// </summary>
        /// <returns></returns>
        private int GetSelectedDuration()
        {
            // Determining the selected appointment duration/length
            if (radio_Option2.Checked)
            {
                return 30; // Option 2 is "30 minutes"
            }
            else if (radio_Option3.Checked)
            {
                return 60; // Option 3 is "60 minutes"
            } 
            else
            {
                return 15; // Option 1 or "15 minutes" is the default duration of an appointment
            }
        }

        private void button_AddAppointment_Click(object sender, EventArgs e)
        {
            // Gather all the information from the textboxes
            int appointmentDuration = GetSelectedDuration(); // The selected duration of the appointment
            string type = text_AppointmentType.Text;
            string customerId = text_AppointmentCustomer.Text;
            string title = text_AppointmentTitle.Text;
            string description = text_AppointmentDesc.Text;
            string location = text_AppointmentLocation.Text;
            string userId = text_AppointmentUser.Text;

            DateTime date = dateTime_AppointmentDate.Value;
            DateTime time = dateTime_AppointmentTime.Value;
            
            DateTime start = new DateTime(date.Year, date.Month, date.Day, time.Hour, time.Minute, 0);
            DateTime end = start.AddMinutes(appointmentDuration);


            // Validate inputs
            if (Validator.ValidateCustomerId(customerId) &&
                Validator.ValidateUserId(userId) &&
                Validator.ValidateAppointmentInputs(title, description, type, location) &&
                Validator.ValidateAppointmentDateTime(userId, start, end))
            {
                // If all validations pass, proceed with adding the appointment
                DBHelper.AddAppointment(Convert.ToInt32(customerId), Convert.ToInt32(userId), title, description, type, location, start, end);
                RefreshDataGridView(dataGridView_allAppointments);
                ClearFields();
            }

        }

        public static void RefreshDataGridView(DataGridView dataGridView)
        {
            // Clear existing data in the DataGridView
            dataGridView.DataSource = null;

            // Start the connection
            DBConnection.startConnection();

            // Re-populate the DataGridView with updated data
            DBHelper.GetAllAppointments(dataGridView, conn);
        }

        private void Appointment_Click(object sender, EventArgs e)
        {
            dataGridView_allAppointments.ClearSelection();
        }

        private void dataGridView_allAppointments_SelectionChanged(object sender, EventArgs e)
        {
            if(dataGridView_allAppointments.SelectedRows.Count == 1)
            {
                DataGridViewRow selectedRow = dataGridView_allAppointments.SelectedRows[0];
                int appointmentId = Convert.ToInt32(selectedRow.Cells["appointmentId"].Value);
                string type = Convert.ToString(selectedRow.Cells["type"].Value);
                int customerId = Convert.ToInt32(selectedRow.Cells["customerId"].Value);
                string title = Convert.ToString(selectedRow.Cells["title"].Value);
                string description = Convert.ToString(selectedRow.Cells["description"].Value);
                string location = Convert.ToString(selectedRow.Cells["location"].Value);
                int userId = Convert.ToInt32(selectedRow.Cells["userId"].Value);
                DateTime start = Convert.ToDateTime(selectedRow.Cells["start"].Value);
                DateTime end = Convert.ToDateTime(selectedRow.Cells["end"].Value);


                // Populate the form fields with the selected appointment's information
                text_AppointmentType.Text = type;
                text_AppointmentCustomer.Text = customerId.ToString();
                text_AppointmentTitle.Text = title;
                text_AppointmentDesc.Text = description;
                text_AppointmentLocation.Text = location;
                text_AppointmentUser.Text = userId.ToString();
                dateTime_AppointmentDate.Value = start.Date;
                dateTime_AppointmentTime.Value = start;

                TimeSpan duration = end - start;
                switch (duration.TotalMinutes)
                {
                    case 15:
                        radio_Option1.Checked = true;
                        break;
                    case 30:
                        radio_Option2.Checked = true;
                        break;
                    case 60:
                        radio_Option3.Checked = true;
                        break;
                    default:
                        // If duration doesn't match any case, default to 15 minutes
                        radio_Option1.Checked = true;
                        break;
                }
            }
        }

        private void button_UpdateAppointment_Click(object sender, EventArgs e)
        {

            if (dataGridView_allAppointments.SelectedRows.Count == 0)
            {
                MessageBox.Show("Please select an appointment to update.", "No Appointment Selected", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Display confirmation message
            if (!Helper.ShowConfirmationDialog("Are you sure you want to update this appointment?"))
                return;

            // Get the appointmentId
            int appointmentId = Convert.ToInt32(dataGridView_allAppointments.SelectedRows[0].Cells["appointmentId"].Value);

            // Gather all the information from the textboxes
            int appointmentDuration = GetSelectedDuration(); // The selected duration of the appointment
            string type = text_AppointmentType.Text;
            string customerId = text_AppointmentCustomer.Text;
            string title = text_AppointmentTitle.Text;
            string description = text_AppointmentDesc.Text;
            string location = text_AppointmentLocation.Text;
            string userId = text_AppointmentUser.Text;
            DateTime date = dateTime_AppointmentDate.Value;
            DateTime time = dateTime_AppointmentTime.Value;

            DateTime start = new DateTime(date.Year, date.Month, date.Day, time.Hour, time.Minute, 0);
            DateTime end = start.AddMinutes(appointmentDuration);

            // Validate inputs
            if (Validator.ValidateCustomerId(customerId) &&
                Validator.ValidateUserId(userId) &&
                Validator.ValidateAppointmentInputs(title, description, type, location) &&
                Validator.ValidateNewAppointmentDateTime(userId, appointmentId, start, end))
            {
                //Update the appointment
                DBHelper.UpdateAppointment(appointmentId, Convert.ToInt32(customerId), Convert.ToInt32(userId), title, description, type, location, start, end);
                RefreshDataGridView(dataGridView_allAppointments);
                ClearFields();
            }

        }

        private void button_DeleteAppointment_Click(object sender, EventArgs e)
        {

            if (dataGridView_allAppointments.SelectedRows.Count == 0)
            {
                MessageBox.Show("Please select an appointment to update.", "No Appointment Selected", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            // Display confirmation message
            if (!Helper.ShowConfirmationDialog("Are you sure you want to delete this appointment?"))
                return;

            // Get the appointmentId
            int appointmentId = Convert.ToInt32(dataGridView_allAppointments.SelectedRows[0].Cells["appointmentId"].Value);

            DBHelper.DeleteAppointment(appointmentId);
            RefreshDataGridView(dataGridView_allAppointments);
            ClearFields();
        }

        private void button_ExitAppointments_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void button_ResetForm_Click(object sender, EventArgs e)
        {
            ClearFields();
        }
    }

}
