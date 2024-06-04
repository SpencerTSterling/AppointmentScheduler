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
    public partial class Customers : Form
    {
        public Customers()
        {
            InitializeComponent();
            // Open the connection
            DBConnection.startConnection();

            //Populate the DataGridView with the existing customer data
            DBHelper.GetAllCustomers(dataGridView_allCustomers, conn);

            // Populate the Combobox with city names
            List<string> cityNames = DBHelper.GetCityNames();
            comboBox_customerCity.DataSource = cityNames;
        }

        private void Customers_Load(object sender, EventArgs e)
        {
            // Format the data view
            Helper.formatDGV(dataGridView_allCustomers);
        }

        /// <summary>
        /// Clears all the feilds on the form
        /// </summary>
        private void ClearFeilds()
        {
            text_customerId.Text = string.Empty;
            text_customerName.Text = string.Empty;
            text_customerAddress.Text = string.Empty;
            text_customerAddress2.Text = string.Empty;
            //text_customerCountry.Text = string.Empty;
            text_customerZipCode.Text = string.Empty;
            text_customerPhone.Text = string.Empty;
        }

        /// <summary>
        /// Reset button that clears all the feilds on the form
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button_ResetForm_Click(object sender, EventArgs e)
        {
            ClearFeilds();
        }

        private void comboBox_customerCity_SelectionChanged(object sender, EventArgs e)
        {
            UpdateCountryFeildByCity(comboBox_customerCity.SelectedItem.ToString());
        }

        /// <summary>
        /// Updates the Country text field based on the selected City in the combobox
        /// </summary>
        /// <param name="selectedCity"></param>
        private void UpdateCountryFeildByCity(string selectedCity)
        {
            // Retrieve the selected city ID
            int selectedCityId = DBHelper.GetCityId(selectedCity);

            // Retrieve the country ID based on the selected city ID
            int countryId = DBHelper.GetCountryId(selectedCityId);

            // Retrieve country name based on country Id
            string countryName = DBHelper.GetCountryName(countryId);

            //Update the Country field with the retrieved country Name
            text_customerCountry.Text = countryName;

        }

        private void dataGridView_allCustomers_SelectionChanged(object sender, EventArgs e)
        {
            // If a selection is made
            if (dataGridView_allCustomers.SelectedRows.Count == 1)
            {
                DataGridViewRow selectedRow = dataGridView_allCustomers.SelectedRows[0];
                int customerId = Convert.ToInt32(selectedRow.Cells["customerId"].Value);
                string customerName = Convert.ToString(selectedRow.Cells["Name"].Value);
                string address = Convert.ToString(selectedRow.Cells["Address Line #1"].Value);
                string address2 = Convert.ToString(selectedRow.Cells["Address Line #2"].Value);
                string city = Convert.ToString(selectedRow.Cells["City"].Value);
                string country = Convert.ToString(selectedRow.Cells["Country"].Value);
                string zipcode = Convert.ToString(selectedRow.Cells["ZipCode"].Value);
                string phone = Convert.ToString(selectedRow.Cells["Phone"].Value);

                // Populate the form fields with the selected customers's information
                text_customerId.Text = Convert.ToString(customerId);
                text_customerName.Text = customerName;
                text_customerAddress.Text = address;
                text_customerAddress2.Text = address2;
                comboBox_customerCity.SelectedItem = city;
                text_customerCountry.Text = country;
                text_customerZipCode.Text = Convert.ToString(zipcode);
                text_customerPhone.Text = phone;

            }
        }

        private void button_ExitAppointments_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void Customers_Click(object sender, EventArgs e)
        {
            //Clear all selections of the datagridview
            dataGridView_allCustomers.ClearSelection();
        }

        private void button_AddCustomer_Click(object sender, EventArgs e)
        {
            // Get all the information from the textboxes
            string customerName = Validator.TrimWhitespace(text_customerName.Text);
            string customerAddress = Validator.TrimWhitespace(text_customerAddress.Text);
            string customerAddress2 = Validator.TrimWhitespace(text_customerAddress2.Text);
            string customerCity = comboBox_customerCity.SelectedItem.ToString();
            string customerCountry = text_customerCountry.Text; // is a "read-only" feild
            string customerZipCode = Validator.TrimWhitespace(text_customerZipCode.Text);
            string customerPhone = Validator.TrimWhitespace(text_customerPhone.Text);

            // Validate inputs 
            // NOTE: Address Line 2 (address2) is not validated the same way as address, it is not required - but is still trimmed
            if (Validator.ValidateCustomerInputs(customerName, customerAddress, customerZipCode, customerPhone)) 
            {
                // If all validations pass, proceed with adding the customer
                DBHelper.AddCustomer(customerName, customerAddress, customerAddress2, customerCity, customerCountry, customerZipCode, customerPhone);
                RefreshDataGridView(dataGridView_allCustomers);
                ClearFeilds();
            }
        }

        public static void RefreshDataGridView(DataGridView dataGridView)
        {
            // Clear existing data in the DataGridView
            dataGridView.DataSource = null;

            // Start the connection
            DBConnection.startConnection();

            // Re-populate the DataGridView with updated data
            DBHelper.GetAllCustomers(dataGridView, conn);
        }


        private void button_UpdateCustomer_Click(object sender, EventArgs e)
        {
            if (dataGridView_allCustomers.SelectedRows.Count == 0)
            {
                MessageBox.Show("Please select an customer to update.", "No Customer Selected", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            // Display confirmation message
            if (!Helper.ShowConfirmationDialog("Are you sure you want to update this customer?"))
                return;

            // Get customerId
            int customerId = Convert.ToInt32(dataGridView_allCustomers.SelectedRows[0].Cells["customerId"].Value);

            // Gather all the information from the textboxes
            string customerName = text_customerName.Text;
            string customerAddress = text_customerAddress.Text;
            string customerAddress2 = text_customerAddress2.Text;
            string customerCity = comboBox_customerCity.SelectedItem.ToString();
            string customerCountry = text_customerCountry.Text;
            string customerZipCode = text_customerZipCode.Text;
            string customerPhone = text_customerPhone.Text;

            //Validate inputs
            bool isValid = true;
            if (isValid)
            {
                //Update the customer
                DBHelper.UpdateCustomer(customerId, customerName, customerAddress, customerAddress2, customerCity, customerCountry, customerZipCode, customerPhone);
                RefreshDataGridView(dataGridView_allCustomers);
                ClearFeilds();
            }
        }

        private void button_DeleteCustomer_Click(object sender, EventArgs e)
        {
            if (dataGridView_allCustomers.SelectedRows.Count == 0)
            {
                MessageBox.Show("Please select an customer to delete.", "No Customer Selected", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            // Display confirmation message
            if (!Helper.ShowConfirmationDialog("Are you sure you want to delete this customer?"))
                return;

            // Get customerId
            int customerId = Convert.ToInt32(dataGridView_allCustomers.SelectedRows[0].Cells["customerId"].Value);

            DBHelper.DeleteCustomer(customerId);
            RefreshDataGridView(dataGridView_allCustomers);
            ClearFeilds();

        }
    }
}
