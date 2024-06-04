using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace C969_Software2_SpencerSterling_v1
{
    public partial class Login : Form
    {
        public Login()
        {
            InitializeComponent();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            string username = textUsername.Text;
            string password = textPassword.Text;

            // Check if the username and password match
            bool isValidCredentials = DBHelper.validateCredentials(username, password);

            // Log the attempt
            LoginLogger.Log_loginAttempt(username, isValidCredentials);

            if (isValidCredentials)
            {
                // Popup notification if there are appointments for user within 15 minutes
                AppointmentAlert(username);

                // Close the login form
                this.Hide();
                // Open the main form
                new MainForm().Show();
            }
            else
            {
                // Show error message
                labelError.Text = "Username and Password do not match";
                labelError.Show();

                // If language is spanish
                if (CultureInfo.CurrentCulture.TwoLetterISOLanguageName == "es")
                {
                    labelError.Text = "Nombre de usuario y contraseña no coinciden";
                }
            }
        }


        private void AppointmentAlert(string username)
        {
            // Get the current local time 
            DateTime currentTime = DateTime.Now;

            // Get the user's id
            int userId = DBHelper.GetUserId(username);

            // Get the schedule of the logged-in user
            DataTable schedule = DBHelper.GetUserSchedule(userId);

            // Iterate through the appointments
            foreach (DataRow row in schedule.Rows)
            {
                DateTime appointmentTime = (DateTime)row["start"];
                TimeSpan timeDifference = appointmentTime - currentTime;

                // Check if there is an appointment within 15 minutes of the current time 
                if (timeDifference.TotalMinutes <= 15 && timeDifference.TotalMinutes > 0)
                {
                    //Display alert
                    MessageBox.Show("You have an appointment within 15 minutes!", "Appointment Alert", MessageBoxButtons.OK);
                    break; // Break out of the loop
                }
            }
        }

        private void Login_Load(object sender, EventArgs e)
        {
            labelError.Hide();
            labelWelcomeMessage.Text = "Hello";

            // Show the user's location
            // Set the user's location based on the current region
            RegionInfo region = new RegionInfo(CultureInfo.CurrentCulture.Name);
            labelLocation.Text = "Location: " + region.DisplayName;

            // Change the language of the labels based on the language
            string language = CultureInfo.CurrentCulture.TwoLetterISOLanguageName;
            // Change the language of the labels based on the language
            changeLanguage(language);
        }

        private void changeLanguage(string language)
        {
            if (language == "en")
            {
                labelWelcomeMessage.Text = "Hello";
                labelUsername.Text = "Username";
                labelPassword.Text = "Password";
            }
            else if (language == "es")
            {
                labelWelcomeMessage.Text = "Hola";
                labelUsername.Text = "Nombre de usuario";
                labelPassword.Text = "Contraseña";
            }
        }


    }
}
