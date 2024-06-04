using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static C969_Software2_SpencerSterling_v1.DBConnection;

namespace C969_Software2_SpencerSterling_v1
{
    public static class DBHelper
    {
        /// <summary>
        /// Executes a SQL query against the MySQL database and returns the result as a Data Table.
        /// </summary>
        /// <param name="query">The SQL query to be executed</param>
        /// <param name="connection">The MySqlConnection object that connects to the MySql Db</param>
        /// <returns>A DataTable containing the result set of the excuted query.</returns>
        public static DataTable ExecuteQuery (string query, MySqlConnection connection)
        {
            DataTable dataTable = new DataTable();

            try
            {
                DBConnection.startConnection();

                MySqlCommand command = new MySqlCommand(query, connection);
                MySqlDataAdapter adapter = new MySqlDataAdapter(command);
                adapter.Fill(dataTable);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error executing query: " + ex.Message);
            }
            finally
            {
                DBConnection.closeConnection();
            }
            return dataTable;
        }


        /* //////////////////////////////////
        *   GETTERS 
        * /////////////////////////////////// */
        public static bool GetAllAppointments(DataGridView dataGridView, MySqlConnection connection)
        {
            // Populate the DataGridView with appointments
            try
            {
                //Start the database connection
                DBConnection.startConnection();

                //Execute query to retrieve all the appointment attributes 
                string query = "SELECT appointmentId, customerId, userId, title, description, type, location, start, end FROM appointment";

                DataTable dataTable = DBHelper.ExecuteQuery(query, connection);


                // Convert the start and end times from UTC to local time
                Helper.ConvertUtcToLocalTime(dataTable, "start");
                Helper.ConvertUtcToLocalTime(dataTable, "end");

                // Bind the DataTable to the DataGridView
                dataGridView.DataSource = dataTable;
                // Indicates success
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
                return false; // Indicates failure
            }
            finally
            {
                // Close the database connection
                DBConnection.closeConnection();
            }
            return true;
        }

        public static DataTable GetAppointmentsByDateRange(DateTime startDate, DateTime endDate)
        {
            DataTable appointments = new DataTable();
            try
            {
                // Start the database connection
                DBConnection.startConnection();

                string query = @"
                    SELECT appointmentId, customerId, userId, title, description, type, location, start, end
                    FROM appointment
                    WHERE start BETWEEN @startDate AND @endDate";

                using (MySqlCommand command = new MySqlCommand(query, conn))
                {
                    // Add parameters to the command
                    command.Parameters.AddWithValue("@startDate", startDate);
                    command.Parameters.AddWithValue("@endDate", endDate);

                    // Execute the query and retrieve the result in a DataTable
                    using (MySqlDataAdapter adapter = new MySqlDataAdapter(command))
                    {
                        adapter.Fill(appointments);
                    }
                }

                // Convert the start and end times from UTC to local time
                Helper.ConvertUtcToLocalTime(appointments, "start");
                Helper.ConvertUtcToLocalTime(appointments, "end");

            }
            catch (Exception ex)
            {
                Console.WriteLine("Error retrieving appointments by date range: " + ex.Message);
            }
            finally
            {
                // Close the database connection
                DBConnection.closeConnection();
            }

            return appointments;
        }


        public static bool GetAllCustomers(DataGridView dataGridView, MySqlConnection connection)
        {
            // Populate the DataGridView with Customers
            try
            {
                //Start the database connection
                DBConnection.startConnection();

                //Execute query to retrieve all the customer attributes 
                string query = @"
                    SELECT c.customerId, c.customerName AS 'Name', a.address AS 'Address Line #1', a.address2 AS 'Address Line #2',
                           ci.city AS City, co.country as Country, a.postalCode AS Zipcode, a.phone AS Phone
                    FROM customer c
                    JOIN address a ON c.addressId = a.addressId
                    JOIN city ci ON a.cityId = ci.cityId
                    JOIN country co ON ci.countryId = co.countryId";

                DataTable dataTable = DBHelper.ExecuteQuery(query, connection);
                // Bind the DataTable to the DataGridView
                dataGridView.DataSource = dataTable;
                // Indicates success
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
                return false; // Indicates failure
            }
            finally
            {
                // Close the database connection
                DBConnection.closeConnection();
            }
            return true;
        }

        public static List<string> GetAllUsers(MySqlConnection connection)
        {
            List<string> usernames = new List<string>();

            try
            {
                //Start the database connection
                DBConnection.startConnection();

                //Execute query to retrieve all the customer attributes 
                string query = @"SELECT userName FROM User";

                using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    DBConnection.startConnection();
                    // Execute the command and obtain a data reader
                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        // Read each row from the data reader
                        while (reader.Read())
                        {
                            // Add the username to the combo box
                            usernames.Add(reader["userName"].ToString());
                        }
                    }
                }

                // Indicates success
                return usernames;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
                return null; // indiates failure
            }
            finally
            {
                // Close the database connection
                DBConnection.closeConnection();
            }
        }

        /// <summary>
        /// Retrives the city names from the database in a list of strings. 
        /// </summary>
        /// <param name="conn"></param>
        /// <returns></returns>
        public static List<string> GetCityNames()
        {
            List<string> cityNames = new List<string>();

            try
            {
                // Start the database connection
                DBConnection.startConnection();

                string query = "SELECT city FROM city";

                // Execute the query
                using (MySqlCommand command = new MySqlCommand(query, DBConnection.conn))
                {
                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        // Read city names from the reader and add them to the list
                        while (reader.Read())
                        {
                            string cityName = reader["city"].ToString();
                            cityNames.Add(cityName);
                        }
                    }
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
            finally
            {
                // Close the database connection
                DBConnection.closeConnection();
            }

            return cityNames;
        }

        /// <summary>
        /// Retrieves the city ID based on the cityName
        /// </summary>
        /// <param name="cityName"></param>
        /// <returns></returns>
        public static int GetCityId(string cityName)
        {
            int cityId = 0;

            try
            {
                // Start the database connection
                DBConnection.startConnection();

                string query = "SELECT cityId FROM city WHERE city = @CityName";

                // Execute the query
                using (MySqlCommand command = new MySqlCommand(query, DBConnection.conn))
                {
                    command.Parameters.AddWithValue("@CityName", cityName);
                    object result = command.ExecuteScalar();
                    if (result != null)
                    {
                        cityId = Convert.ToInt32(result);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
            finally
            {
                // Close the database connection
                DBConnection.closeConnection();
            }

            return cityId;
        }

        /// <summary>
        /// Retrieves the country ID based on the city ID
        /// </summary>
        /// <param name="cityId"></param>
        /// <returns></returns>
        public static int GetCountryId(int cityId)
        {
            int countryId = 0;

            try
            {
                // Start the database connection
                DBConnection.startConnection();

                string query = "SELECT countryId FROM city WHERE cityId = @CityId";

                // Execute the query
                using (MySqlCommand command = new MySqlCommand(query, conn))
                {
                    command.Parameters.AddWithValue("@CityId", cityId);
                    object result = command.ExecuteScalar();
                    if (result != null)
                    {
                        countryId = Convert.ToInt32(result);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
            finally
            {
                // Close the database connection
                DBConnection.closeConnection();
            }
            return countryId;
        }

        /// <summary>
        /// Retrives the country name based on the countryId
        /// </summary>
        /// <param name="countryId"></param>
        /// <returns></returns>
        public static string GetCountryName(int countryId)
        {
            string countryName = "";

            try
            {
                // Start the database connection
                DBConnection.startConnection();

                string query = "SELECT country FROM country WHERE countryId = @CountryId";

                // Execute the query
                using (MySqlCommand command = new MySqlCommand(query, conn))
                {
                    command.Parameters.AddWithValue("@CountryId", countryId);
                    object result = command.ExecuteScalar();
                    if (result != null)
                    {
                        countryName = result.ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
            finally
            {
                // Close the database connection
                DBConnection.closeConnection();
            }
            return countryName;
        }

        /// <summary>
        /// Retrieves the userId based on the userName
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        public static int GetUserId (string userName)
        {
            int userId = 0;
            try
            {
                //Start the database connection
                DBConnection.startConnection();

                string query = "SELECT userId FROM user WHERE userName = @userName";
                
                // Execute the query
                using (MySqlCommand command = new MySqlCommand(query, conn))
                {
                    command.Parameters.AddWithValue("@userName", userName);
                    object result = command.ExecuteScalar();
                    if (result != null)
                    {
                        userId = Convert.ToInt32(result);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
            finally
            {
                // Close the database connection
                DBConnection.closeConnection();
            }
            return userId;
        }

        public static DataTable GetUserSchedule(int userId)
        {
            try
            {
                // Start DB connection
                DBConnection.startConnection();

                // Query to retrieve appointments for the specified user
                string query = "SELECT appointmentId, title, type, start, end FROM appointment WHERE userId = @userId";

                // Create a new MySqlCommand object with the query and connection
                MySqlCommand command = new MySqlCommand(query, conn);

                // Add parameters to the command
                command.Parameters.AddWithValue("@userId", userId);

                // Execute the query and retrieve the result in a DataTable
                DataTable schedule = new DataTable();
                using (MySqlDataAdapter adapter = new MySqlDataAdapter(command))
                {
                    adapter.Fill(schedule);
                }

                // Convert the start and end times from UTC to local time
                Helper.ConvertUtcToLocalTime(schedule, "start");
                Helper.ConvertUtcToLocalTime(schedule, "end");

                return schedule;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error retrieving user schedule: " + ex.Message);
                return null; // Return null in case of any exception
            }
            finally
            {
                // Close the database connection
                DBConnection.closeConnection();
            }
        }

        public static DataTable GetUserScheduleForDay(int userId, DateTime date)
        {
            DataTable schedule = new DataTable();

            try
            {
                // Start DB connection
                DBConnection.startConnection();

                // Query to retrieve appointments for the specified user on the given day
                string query = @"SELECT start, end FROM appointment 
                         WHERE userId = @userId 
                         AND DATE(start) = @date";

                MySqlCommand command = new MySqlCommand(query, conn);
                command.Parameters.AddWithValue("@userId", userId);
                command.Parameters.AddWithValue("@date", date.Date);

                // Execute the query and fill the DataTable with the results
                MySqlDataAdapter adapter = new MySqlDataAdapter(command);
                adapter.Fill(schedule);

                // Convert the start and end times from UTC to local time
                Helper.ConvertUtcToLocalTime(schedule, "start");
                Helper.ConvertUtcToLocalTime(schedule, "end");

            }
            catch (Exception ex)
            {
                Console.WriteLine("Error retrieving user schedule: " + ex.Message);
            }
            finally
            {
                // Close the database connection
                DBConnection.closeConnection();
            }

            return schedule;
        }

        public static int GetLatestAddressId()
        {
            int addressId = 0;

            try
            {
                // Start the database connection
                DBConnection.startConnection();

                // Query to get the maximum addressId from the address table
                string query = "SELECT MAX(addressId) FROM address";

                // Execute the query
                using (MySqlCommand command = new MySqlCommand(query, conn))
                {
                    object result = command.ExecuteScalar();
                    if (result != null && result != DBNull.Value)
                    {
                        addressId = Convert.ToInt32(result);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
            finally
            {
                // Close the database connection
                DBConnection.closeConnection();
            }

            return addressId;
        }

        /* //////////////////////////////////
        *   DATA VALIDATION 
        * /////////////////////////////////// */
        /// <summary>
        /// Checks if passed in Customer ID exists in the database. 
        /// </summary>
        /// <param name="customerId"></param>
        /// <returns></returns>
        public static bool doesCustomerIdExist(int customerId)
        {
            try
            {
                //Start DB connection
                DBConnection.startConnection();

                //Check if the customerID exists in the database
                string query = "SELECT COUNT(*) FROM customer WHERE customerId = @customerId";
                using (MySqlCommand command = new MySqlCommand(query, conn))
                {
                    command.Parameters.AddWithValue("@customerId", customerId);

                    // Open the database connection if not already open
                    if (conn.State != ConnectionState.Open)
                    {
                        conn.Open();
                    }

                    // Execute the query and get the result
                    int count = Convert.ToInt32(command.ExecuteScalar());

                    // Return true if count is greater than 0
                    return count > 0;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error checking customer ID validity: " + ex.Message);
                return false; // Return false in case of any exception
            }
            finally
            {
                DBConnection.closeConnection();
            }
        }

        /// <summary>
        /// Checks to see if passed in userId exists in the database
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public static bool doesUserIdExist(int userId)
        {
            try
            {
                //Start DB connection
                DBConnection.startConnection();

                //Check if the customerID exists in the database
                string query = "SELECT COUNT(*) FROM user WHERE userId = @userId";
                using (MySqlCommand command = new MySqlCommand(query, conn))
                {
                    command.Parameters.AddWithValue("@userId", userId);

                    // Open the database connection if not already open
                    if (conn.State != ConnectionState.Open)
                    {
                        conn.Open();
                    }

                    // Execute the query and get the result
                    int count = Convert.ToInt32(command.ExecuteScalar());

                    // Return true if count is greater than 0
                    return count > 0;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error checking customer ID validity: " + ex.Message);
                return false; // Return false in case of any exception
            }
            finally
            {
                DBConnection.closeConnection();
            }
        }

        public static bool doesAppointmentOverlap(int userId, DateTime start, DateTime end)
        {
            try
            {
                // Everytime a date is added to the database, change it from local time to UTC
                start = start.ToUniversalTime();
                end = end.ToUniversalTime();

                // Start DB connection
                DBConnection.startConnection();

                // Check for overlapping appointments for the given user
                string query = @"SELECT COUNT(*) 
                             FROM appointment 
                             WHERE userId = @userId 
                             AND ((@start BETWEEN start AND end) OR (@end BETWEEN start AND end))";

                MySqlCommand command = new MySqlCommand(query, conn);
                command.Parameters.AddWithValue("@userId", userId);
                command.Parameters.AddWithValue("@start", start);
                command.Parameters.AddWithValue("@end", end);

                int overlappingAppointmentsCount = Convert.ToInt32(command.ExecuteScalar());

                return overlappingAppointmentsCount == 0;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error validating appointment overlap: " + ex.Message);
                return false; // Return false in case of any exception
            }
            finally
            {
                // Close the database connection
                DBConnection.closeConnection();
            }
        }
        /// <summary>
        /// Checks if there aer overlapping appointments for a given user, EXCLUDING a specfied appointment
        /// </summary>
        /// <param name="userId">The ID of the user for whom the appointments are being checked</param>
        /// <param name="appointmentId">The ID of the appointment to be EXCLUDED from the overlap check</param>
        /// <param name="start"></param>
        /// <param name="end"></param>
        /// <returns></returns>
        public static bool doesAppointmentOverlapExcluding(int userId, int appointmentId, DateTime start, DateTime end)
        {
            try
            {

                // Everytime a date is added to the database, change it from local time to UTC
                start = start.ToUniversalTime();
                end = end.ToUniversalTime();

                // Start DB connection
                DBConnection.startConnection();

                // Check for overlapping appointments for the given user, excluding the current appointment
                string query = @"SELECT COUNT(*) 
                        FROM appointment 
                        WHERE userId = @userId 
                        AND appointmentId != @appointmentId
                        AND ((start BETWEEN @start AND @end) OR (end BETWEEN @start AND @end))";

                MySqlCommand command = new MySqlCommand(query, conn);
                command.Parameters.AddWithValue("@userId", userId);
                command.Parameters.AddWithValue("@appointmentId", appointmentId);
                command.Parameters.AddWithValue("@start", start);
                command.Parameters.AddWithValue("@end", end);

                int overlappingAppointmentsCount = Convert.ToInt32(command.ExecuteScalar());

                return overlappingAppointmentsCount > 0;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error validating appointment overlap except: " + ex.Message);
                return false; // Return false in case of any exception
            }
            finally
            {
                // Close the database connection
                DBConnection.closeConnection();
            }
        }

        /* //////////////////////////////////
         * APPOINTMENT DATABASE CRUD FUNCTIONS
         * ///////////////////////////////////
         * */

        public static bool AddAppointment(int customerId, int userId, string title, string description, string type, string location, DateTime start, DateTime end)
        {
            try
            {
                // Everytime a date is added to the database, change it from local time to UTC
                start = start.ToUniversalTime();
                end = end.ToUniversalTime();

                //Start the database connection
                DBConnection.startConnection();


                // INSERT INTO (appointment) VALUES (appointmentId, customerId, userId, title, descriptio, type, location, start, end)
                string nonquery = @"INSERT INTO appointment (customerId, userId, title, description, type, location, url, 
                                    contact, start, end, createDate, createdBy, lastUpdate, lastUpdateBy)
                         VALUES (@customerId, @userId, @title, @description, @type, @location, @url, @contact, 
                                    @start, @end, @createDate, @createdBy, @lastUpdate, @lastUpdateBy)";

                using (MySqlCommand command = new MySqlCommand(nonquery, conn))
                {
                    // Adding parameters to the command
                    command.Parameters.AddWithValue("@customerId", customerId);
                    command.Parameters.AddWithValue("@userId", userId);
                    command.Parameters.AddWithValue("@title", title);
                    command.Parameters.AddWithValue("@description", description);
                    command.Parameters.AddWithValue("@location", location);
                    command.Parameters.AddWithValue("@contact", "not needed"); // not needed
                    command.Parameters.AddWithValue("@type", type);
                    command.Parameters.AddWithValue("@url", "not needed"); // not needed
                    command.Parameters.AddWithValue("@start", start);
                    command.Parameters.AddWithValue("@end", end);
                    command.Parameters.AddWithValue("@createDate", DateTime.Today); // not needed
                    command.Parameters.AddWithValue("@createdBy", "not needed"); // not needed
                    command.Parameters.AddWithValue("@lastUpdate", DateTime.Today); // not needed
                    command.Parameters.AddWithValue("@lastUpdateBy", "not needed"); // not needed


                    // Execute the command
                    int rowsAffected = command.ExecuteNonQuery();

                    // Check if the command executed successfully
                    if (rowsAffected > 0)
                    {
                        Console.WriteLine("Appointment added successfully!");
                    }
                    else
                    {
                        Console.WriteLine("Failed to add appointment.");
                    }

                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
                return false; // Indicates failure
            }
            finally
            {
                // Close the database connection
                DBConnection.closeConnection();
            }

            return true;
        }


        public static bool UpdateAppointment(int appointmentId, int customerId, int userId, string title, string description, string type, string location, DateTime start, DateTime end)
        {

            try
            {
                // Everytime a date is added to the database, change it from local time to UTC
                start = start.ToUniversalTime();
                end = end.ToUniversalTime();

                //Start the database connection
                DBConnection.startConnection();


                // UPDATE (appointment) VALUES (appointmentId, customerId, userId, title, descriptio, type, location, start, end)
                string nonquery = @"UPDATE appointment
                                    SET customerId = @customerId, userId = @userId, title = @title, description = @description, type = @type, location = @location, start = @start, end = @end
                                    WHERE appointmentId = @appointmentId";

                using (MySqlCommand command = new MySqlCommand(nonquery, conn))
                {
                    // Adding parameters to the command
                    command.Parameters.AddWithValue("@appointmentId", appointmentId);
                    command.Parameters.AddWithValue("@customerId", customerId);
                    command.Parameters.AddWithValue("@userId", userId);
                    command.Parameters.AddWithValue("@title", title);
                    command.Parameters.AddWithValue("@description", description);
                    command.Parameters.AddWithValue("@location", location);
                    //command.Parameters.AddWithValue("@contact", "not needed"); // not needed
                    command.Parameters.AddWithValue("@type", type);
                    command.Parameters.AddWithValue("@url", "not needed"); // not needed
                    command.Parameters.AddWithValue("@start", start);
                    command.Parameters.AddWithValue("@end", end);
                    //command.Parameters.AddWithValue("@createDate", DateTime.Today); // not needed
                    //command.Parameters.AddWithValue("@createdBy", "not needed"); // not needed
                    command.Parameters.AddWithValue("@lastUpdate", DateTime.Today); // not needed
                    //command.Parameters.AddWithValue("@lastUpdateBy", "not needed"); // not needed


                    // Execute the command
                    int rowsAffected = command.ExecuteNonQuery();

                    // Check if the command executed successfully
                    if (rowsAffected > 0)
                    {
                       Console.WriteLine("Appointment updated successfully!");
                    }
                    else
                    {
                        Console.WriteLine("Failed to updated appointment.");
                    }

                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
                return false; // Indicates failure
            }
            finally
            {
                // Close the database connection
                DBConnection.closeConnection();
            }

            return true;
        }


        public static bool DeleteAppointment(int appointmentId)
        {
            try
            {
                // Start the database connection
                DBConnection.startConnection();

                // DELETE FROM appointment WHERE appointmentId = @appointmentId
                string nonquery = @"DELETE FROM appointment WHERE appointmentId = @appointmentId";

                using (MySqlCommand command = new MySqlCommand(nonquery, conn))
                {
                    // Adding parameters to the command
                    command.Parameters.AddWithValue("@appointmentId", appointmentId);

                    // Execute the command
                    int rowsAffected = command.ExecuteNonQuery();

                    // Check if the command executed successfully
                    if (rowsAffected > 0)
                    {
                        Console.WriteLine("Appointment deleted successfully!");
                    }
                    else
                    {
                        Console.WriteLine("Failed to delete appointment.");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
                return false; // Indicates failure
            }
            finally
            {
                // Close the database connection
                DBConnection.closeConnection();
            }

            return true;
        }


        /* ////////////////////////////////
         * CUSTOMER DATABASE CRUD FUNCTIONS
         * ////////////////////////////////
         * */

        /// <summary>
        /// Adds an address to the Address table with a unique ID
        /// </summary>
        /// <param name="address"></param>
        /// <param name="address2"></param>
        /// <param name="cityId"></param>
        /// <param name="postalCode"></param>
        /// <param name="phone"></param>
        /// <returns></returns>
        public static bool AddAddress (string address, string address2, int cityId, string postalCode, string phone)
        {
            try
            {
                // Start the database connection
                DBConnection.startConnection();

                // INSERT INTO (address) VALUES (address, address2, cityid, postalCode, phone)
                string nonquery = @"
                    INSERT INTO address (address, address2, cityId, postalCode, phone, createDate, createdBy, lastUpdate, lastUpdateBy)
                        VALUES (@address, @address2, @cityId, @postalCode, @phone, @createDate, @createdBy, @lastUpdate, @lastUpdateBy)";

                using (MySqlCommand command = new MySqlCommand(nonquery, conn))
                {
                    command.Parameters.AddWithValue("@address", address);
                    command.Parameters.AddWithValue("@address2", address2);
                    command.Parameters.AddWithValue("@cityId", cityId);
                    command.Parameters.AddWithValue("@postalCode", postalCode);
                    command.Parameters.AddWithValue("@phone", phone);
                    command.Parameters.AddWithValue("@createDate", DateTime.Today); // not needed
                    command.Parameters.AddWithValue("@createdBy", "not needed"); // not needed
                    command.Parameters.AddWithValue("@lastUpdate", DateTime.Today); // not needed
                    command.Parameters.AddWithValue("@lastUpdateBy", "not needed"); // not needed

                    // Execute the command
                    int rowsAffected = command.ExecuteNonQuery();

                    // Check if the command executed successfully
                    if (rowsAffected > 0)
                    {
                        Console.WriteLine("Address added successfully!");
                    }
                    else
                    {
                        Console.WriteLine("Failed to add address.");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
                return false; // Indicates failure
            }
            finally
            {
                // Close the database connection
                DBConnection.closeConnection();
            }

            return true;
        }

        public static bool AddCustomer(string customerName, string customerAddress, string customerAddress2, string customerCity, string customerCountry,
            string customerZipCode, string customerPhone)
        {

            try
            {
                //Start the database connection
                DBConnection.startConnection();

                //Create unique ID for address
                int cityId = GetCityId(customerCity);
                AddAddress(customerAddress, customerAddress2, cityId, customerZipCode, customerPhone);
                int addressId = GetLatestAddressId();

                // INSERT INTO (customer) VALUES (customerName, customerAddress, customerAddress2, customerCity, customerCountry, customerZipCode, customerPhone)
                string nonquery = @"INSERT INTO customer (customerName, addressId, active, createDate, createdBy, lastUpdate, lastUpdateBy)
                    VALUES(@customerName, @addressId, @active, @createDate, @createdBy, @lastUpdate, @lastUpdateBy)";


                // Restart the database connection
                DBConnection.startConnection();

                using (MySqlCommand command = new MySqlCommand(nonquery, conn))
                {
                    command.Parameters.AddWithValue("@customerName", customerName);
                    command.Parameters.AddWithValue("@addressId", addressId);
                    command.Parameters.AddWithValue("@active", 1); // Assuming customer is active
                    command.Parameters.AddWithValue("@createDate", DateTime.Today); // not needed
                    command.Parameters.AddWithValue("@createdBy", "not needed"); // not needed
                    command.Parameters.AddWithValue("@lastUpdate", DateTime.Today);
                    command.Parameters.AddWithValue("@lastUpdateBy", "not needed"); // not needed

                    // Execute the command
                    int rowsAffected = command.ExecuteNonQuery();

                    // Check if the command executed successfully
                    if (rowsAffected > 0)
                    {
                        Console.WriteLine("Customer added successfully!");
                    }
                    else
                    {
                        Console.WriteLine("Failed to add customer.");
                    }

                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
                return false; // Indicates failure
            }
            finally
            {
                // Close the database connection
                DBConnection.closeConnection();
            }

            return true;
        }


        public static bool UpdateCustomer(int customerId, string customerName, string customerAddress, string customerAddress2, string customerCity, string customerCountry,
            string customerZipCode, string customerPhone)
        {
            try
            {
                // Start the database connection
                DBConnection.startConnection();

                // Update the customer's address
                string addressNonquery = @"
                    UPDATE address a
                    JOIN city ci ON a.cityId = ci.cityId
                    JOIN country co ON ci.countryId = co.countryId
                    SET 
                        a.address = @customerAddress,
                        a.address2 = @customerAddress2,
                        ci.city = @customerCity,
                        co.country = @customerCountry,
                        a.postalCode = @customerZipCode,
                        a.phone = @customerPhone
                    WHERE a.addressId = (SELECT addressId FROM customer WHERE customerId = @customerId)";
                
                using (MySqlCommand addressCommand = new MySqlCommand(addressNonquery, conn))
                {
                    addressCommand.Parameters.AddWithValue("@customerId", customerId);
                    addressCommand.Parameters.AddWithValue("@customerAddress", customerAddress);
                    addressCommand.Parameters.AddWithValue("@customerAddress2", customerAddress2);
                    addressCommand.Parameters.AddWithValue("@customerCity", customerCity);
                    addressCommand.Parameters.AddWithValue("@customerCountry", customerCountry);
                    addressCommand.Parameters.AddWithValue("@customerZipCode", customerZipCode);
                    addressCommand.Parameters.AddWithValue("@customerPhone", customerPhone);
                    addressCommand.Parameters.AddWithValue("@lastUpdate", DateTime.Today);

                    // Execute the address update command
                    int addressRowsAffected = addressCommand.ExecuteNonQuery();

                    // Check if the address update executed successfully
                    if (addressRowsAffected <= 0)
                    {
                        Console.WriteLine("Failed to update customer address.");
                        return false;
                    }
                }

                // Update Customer info
                string customerNonquery = @"
                    UPDATE customer
                    SET customerName = @customerName
                    WHERE customerId = @customerId";

                using (MySqlCommand customerCommand = new MySqlCommand(customerNonquery, conn))
                {
                    customerCommand.Parameters.AddWithValue("@customerId", customerId);
                    customerCommand.Parameters.AddWithValue("@customerName", customerName);

                    // Execute the customer update command
                    int customerRowsAffected = customerCommand.ExecuteNonQuery();

                    // Check if the customer update executed successfully
                    if (customerRowsAffected <= 0)
                    {
                        Console.WriteLine("Failed to update customer name.");
                        return false;
                    }
                }

                Console.WriteLine("Customer information updated successfully!");
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
                return false; // Indicates failure
            }
            finally
            {
                // Close the database connection
                DBConnection.closeConnection();
            }
        }

        public static bool DeleteCustomer(int customerId)
        {
            try
            {
                // Start the database connection
                DBConnection.startConnection();

                // DELETE FROM customer WHERE customerId = @customerId
                string nonquery = @"DELETE FROM customer WHERE customerId = @customerId";

                using (MySqlCommand command = new MySqlCommand(nonquery, conn))
                {
                    // Adding parameters to the command
                    command.Parameters.AddWithValue("@customerId", customerId);

                    // Execute the command
                    int rowsAffected = command.ExecuteNonQuery();

                    // Check if the command executed successfully
                    if (rowsAffected > 0)
                    {
                        Console.WriteLine("Appointment deleted customer!");
                    }
                    else
                    {
                        Console.WriteLine("Failed to delete customer.");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
                return false; // Indicates failure
            }
            finally
            {
                // Close the database connection
                DBConnection.closeConnection();
            }

            return true;
        }



        /* //////////////////////////////////
         * REPORT DATABASE FUNCTIONS
         * ///////////////////////////////////
         * */
        /// <summary>
        /// Gets appointment types and counts for the passed in month
        /// </summary>
        /// <param name="month"></param>
        /// <returns></returns>
        public static Dictionary<string, int> GetAppointmentTypesByMonth(string month)
        {
            Dictionary<string, int> appointmentTypesCount = new Dictionary<string, int>();

            try
            {
                // Start the database connection
                DBConnection.startConnection();

                //query to revieve appointment types + their count for the passed in month
                string query = @"
                    SELECT type, COUNT(*) as Count
                    FROM appointment
                    WHERE monthname(start) = @month
                    GROUP BY type";

                // Execute the query
                using (MySqlCommand command = new MySqlCommand(query, conn))
                {
                    command.Parameters.AddWithValue("@month", month);

                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        // Loop through the result set
                        while (reader.Read())
                        {
                            string appointmentType = reader["type"].ToString();
                            int count = Convert.ToInt32(reader["count"]);

                            // Add the appointment type and its count to the dictionary
                            appointmentTypesCount.Add(appointmentType, count);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
                // Handle the exception as per your application's requirements
            }
            finally
            {
                // Close the database connection
                DBConnection.closeConnection();
            }

            return appointmentTypesCount;
        }

        public static List<(string city, int count)> GetCustomerCountByCity()
        {
            List<(string city, int count)> customerCountByCity = new List<(string city, int count)>();

            try
            {
                // Start DB connection
                DBConnection.startConnection();

                // Query to retrieve customers and their associated city information
                string query = @"
                       SELECT ci.city, COUNT(*) as Count
                       FROM customer c
                       JOIN address a ON c.addressId = a.addressId
                       JOIN city ci ON a.cityId = ci.cityId
                       GROUP BY ci.city";

                // Execute the query and retrieve the result
                using (MySqlCommand command = new MySqlCommand(query, conn))
                {
                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            // Extract the city name and customer count
                            string city = reader.GetString("city");
                            int count = reader.GetInt32("count");

                            // Add the city and its corresponding count to the dictionary
                            customerCountByCity.Add((city, count));
                        }
                    }
                }

                return customerCountByCity;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error retrieving customer count by city: " + ex.Message);
                return null; // Return null in case of any exception
            }
            finally
            {
                // Close the database connection
                DBConnection.closeConnection();
            }
        }



        /* //////////////////////////////////
         * LOG IN FUNCTIONS
         * ///////////////////////////////////
         * */

        public static bool validateCredentials (string username, string password)
        {
            try
            {
                // Start the database connection
                DBConnection.startConnection();

                string query = "SELECT COUNT(*) FROM user WHERE username = @username AND password = @password";

                using (MySqlCommand command = new MySqlCommand(query, DBConnection.conn))
                {
                    // Add parameters to the command
                    command.Parameters.AddWithValue("@username", username);
                    command.Parameters.AddWithValue("@password", password);

                    // Execute the query and retrieve the result
                    int count = Convert.ToInt32(command.ExecuteScalar());

                    // If count is greater than 0, username/password match exists
                    return count > 0;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error validating credentials: " + ex.Message);
                return false; // Indicates failure
            }
            finally
            {
                // Close the database connection
                DBConnection.closeConnection();
            }
        }
    }
}

