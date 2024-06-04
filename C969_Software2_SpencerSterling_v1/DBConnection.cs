using System;
using MySql.Data.MySqlClient;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using System.Windows.Forms;

namespace C969_Software2_SpencerSterling_v1
{
    class DBConnection
    {

        public static MySqlConnection conn { get; set; }

        /// <summary>
        /// Establishes a connection to the mySQL database using the connection string in the application configuration file. 
        /// </summary>
        public static void startConnection()
        {
            try
            {
                string constr = ConfigurationManager.ConnectionStrings["MyConnectionString"].ConnectionString;
                conn = new MySqlConnection(constr);
                //open the connection
                conn.Open();
                Console.WriteLine("Connection is open");
            }
            catch (MySqlException ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        /// <summary>
        /// Closes the connection to the MySQL database. 
        /// </summary>
        public static void closeConnection()
        {
            try
            {
                //close connection
                if (conn != null)
                {
                    conn.Close();
                }
                conn = null;
            }
            catch (MySqlException ex)
            {
                MessageBox.Show(ex.Message);
            }
        }


    }
}
