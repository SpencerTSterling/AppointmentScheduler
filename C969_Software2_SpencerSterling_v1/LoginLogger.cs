using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace C969_Software2_SpencerSterling_v1
{
    class LoginLogger
    {
        private const string logFileName = "Login_History.txt";

        public static void Log_loginAttempt (string username, bool isSuccess)
        {
            try
            {
                //Create or open the log file for appending the log of attempt
                using (StreamWriter writer = File.AppendText(logFileName))
                {
                    string logMessage = isSuccess ?
                     $"USER {username} has logged in at {DateTime.Now}" :
                     $"Failed Login Attempt with USER {username} at {DateTime.Now}";

                    // Write the message to the file 
                    writer.WriteLine(logMessage);
                }


                // Show success or failure message
                string message = isSuccess ? "Login successful!" : "Login failed!";
                MessageBox.Show(message);

            }
            catch(Exception ex)
            {
                // Error message
                Console.WriteLine($"Error logging login attemt: {ex.Message}");
            }
        }
    }
}
