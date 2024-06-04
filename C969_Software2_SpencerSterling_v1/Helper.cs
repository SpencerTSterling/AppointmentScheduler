using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace C969_Software2_SpencerSterling_v1
{
    class Helper
    {
        /// <summary>
        /// Formats the DataGridViews
        /// </summary>
        /// <param name="d">The passed in DataGridView</param>
        public static void formatDGV(DataGridView dgv)
        {
            // Selects the full row
            dgv.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            //Highlights selection
            dgv.DefaultCellStyle.SelectionBackColor = System.Drawing.Color.Yellow;
            dgv.DefaultCellStyle.SelectionForeColor = System.Drawing.Color.Black;
            // Formats grid to be read-only and disables resizing by users
            dgv.RowHeadersVisible = false;
            dgv.ReadOnly = true;
            dgv.AllowUserToAddRows = false;
            dgv.AllowUserToResizeColumns = false;
            dgv.AllowUserToResizeRows = false;
            // Clears selections on load
            dgv.TabStop = false;
            dgv.ClearSelection();
            dgv.CurrentCell = null;
        }

        public static void formatDGVColumns(DataGridView dvg)
        {
            foreach (DataGridViewColumn column in dvg.Columns)
            {
                column.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            }
        }

        public static void formatDTPforTime(DateTimePicker dtp)
        {
            // Changing format to Time
            dtp.Format = DateTimePickerFormat.Time;
            dtp.ShowUpDown = true;
            // Custom format to show hour and minutes
            dtp.CustomFormat = "h:mm tt";
            dtp.Format = DateTimePickerFormat.Custom;
        }

        public static void formatDTPforDate(DateTimePicker dtp)
        {
            // Set the CustomFormat string.
            dtp.CustomFormat = "MMMM dd, yyyy - dddd";
            dtp.Format = DateTimePickerFormat.Custom;
        }

        public static void ConvertUtcToLocalTime(DataTable dataTable, string columnName)
        {
            // Check if the column exists in the DataTable
            if (dataTable.Columns.Contains(columnName))
            {
                foreach (DataRow row in dataTable.Rows)
                {
                    // Convert each DateTime value in the specified column from UTC to local time
                    row[columnName] = ((DateTime)row[columnName]).ToLocalTime();
                }
            }
        }

        public static DateTime ConvertUtcToLocalTime(DateTime utcTime)
        {
            // Get the user's current time zone
            TimeZoneInfo userTimeZone = TimeZoneInfo.Local;

            // Convert UTC time to local time
            DateTime localTime = TimeZoneInfo.ConvertTimeFromUtc(utcTime, userTimeZone);
            return localTime;
        }

        // LAMBDA EXPRESSION: Method to show confirmation dialog
        public static bool ShowConfirmationDialog(string message) => MessageBox.Show(message, "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes;
    }
}
