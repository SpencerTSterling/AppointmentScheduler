
namespace C969_Software2_SpencerSterling_v1
{
    partial class MainForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.rad_AllAppointments_main = new System.Windows.Forms.RadioButton();
            this.rad_CurrentWeek_main = new System.Windows.Forms.RadioButton();
            this.rad_CurrentMonth_main = new System.Windows.Forms.RadioButton();
            this.datetimePicker_searchAppointment_main = new System.Windows.Forms.DateTimePicker();
            this.label_searchAppointment_main = new System.Windows.Forms.Label();
            this.button_Reports_main = new System.Windows.Forms.Button();
            this.button_Appointments_main = new System.Windows.Forms.Button();
            this.button_Customers_main = new System.Windows.Forms.Button();
            this.label_TimeZone_main = new System.Windows.Forms.Label();
            this.label_TimeZoneInput_main = new System.Windows.Forms.Label();
            this.button_Logout_main = new System.Windows.Forms.Button();
            this.dataGridView_Appointments = new System.Windows.Forms.DataGridView();
            this.button_RefreshView = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView_Appointments)).BeginInit();
            this.SuspendLayout();
            // 
            // rad_AllAppointments_main
            // 
            this.rad_AllAppointments_main.AutoSize = true;
            this.rad_AllAppointments_main.Location = new System.Drawing.Point(888, 123);
            this.rad_AllAppointments_main.Name = "rad_AllAppointments_main";
            this.rad_AllAppointments_main.Size = new System.Drawing.Size(129, 17);
            this.rad_AllAppointments_main.TabIndex = 2;
            this.rad_AllAppointments_main.TabStop = true;
            this.rad_AllAppointments_main.Text = "View All Appointments";
            this.rad_AllAppointments_main.UseVisualStyleBackColor = true;
            this.rad_AllAppointments_main.CheckedChanged += new System.EventHandler(this.rad_AllAppointments_main_CheckedChanged);
            // 
            // rad_CurrentWeek_main
            // 
            this.rad_CurrentWeek_main.AutoSize = true;
            this.rad_CurrentWeek_main.Location = new System.Drawing.Point(888, 159);
            this.rad_CurrentWeek_main.Name = "rad_CurrentWeek_main";
            this.rad_CurrentWeek_main.Size = new System.Drawing.Size(131, 17);
            this.rad_CurrentWeek_main.TabIndex = 3;
            this.rad_CurrentWeek_main.TabStop = true;
            this.rad_CurrentWeek_main.Text = "View by Current Week";
            this.rad_CurrentWeek_main.UseVisualStyleBackColor = true;
            this.rad_CurrentWeek_main.CheckedChanged += new System.EventHandler(this.rad_CurrentWeek_main_CheckedChanged);
            // 
            // rad_CurrentMonth_main
            // 
            this.rad_CurrentMonth_main.AutoSize = true;
            this.rad_CurrentMonth_main.Location = new System.Drawing.Point(888, 196);
            this.rad_CurrentMonth_main.Name = "rad_CurrentMonth_main";
            this.rad_CurrentMonth_main.Size = new System.Drawing.Size(132, 17);
            this.rad_CurrentMonth_main.TabIndex = 4;
            this.rad_CurrentMonth_main.TabStop = true;
            this.rad_CurrentMonth_main.Text = "View by Current Month";
            this.rad_CurrentMonth_main.UseVisualStyleBackColor = true;
            this.rad_CurrentMonth_main.CheckedChanged += new System.EventHandler(this.rad_CurrentMonth_main_CheckedChanged);
            // 
            // datetimePicker_searchAppointment_main
            // 
            this.datetimePicker_searchAppointment_main.Location = new System.Drawing.Point(1019, 229);
            this.datetimePicker_searchAppointment_main.Name = "datetimePicker_searchAppointment_main";
            this.datetimePicker_searchAppointment_main.Size = new System.Drawing.Size(200, 20);
            this.datetimePicker_searchAppointment_main.TabIndex = 5;
            this.datetimePicker_searchAppointment_main.ValueChanged += new System.EventHandler(this.datetimePicker_searchAppointment_main_ValueChanged);
            // 
            // label_searchAppointment_main
            // 
            this.label_searchAppointment_main.AutoSize = true;
            this.label_searchAppointment_main.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label_searchAppointment_main.Location = new System.Drawing.Point(888, 235);
            this.label_searchAppointment_main.Name = "label_searchAppointment_main";
            this.label_searchAppointment_main.Size = new System.Drawing.Size(125, 13);
            this.label_searchAppointment_main.TabIndex = 6;
            this.label_searchAppointment_main.Text = "Search Appointment:";
            // 
            // button_Reports_main
            // 
            this.button_Reports_main.Location = new System.Drawing.Point(90, 12);
            this.button_Reports_main.Name = "button_Reports_main";
            this.button_Reports_main.Size = new System.Drawing.Size(229, 41);
            this.button_Reports_main.TabIndex = 8;
            this.button_Reports_main.Text = "Reports";
            this.button_Reports_main.UseVisualStyleBackColor = true;
            this.button_Reports_main.Click += new System.EventHandler(this.button_Reports_main_Click);
            // 
            // button_Appointments_main
            // 
            this.button_Appointments_main.Location = new System.Drawing.Point(368, 12);
            this.button_Appointments_main.Name = "button_Appointments_main";
            this.button_Appointments_main.Size = new System.Drawing.Size(229, 41);
            this.button_Appointments_main.TabIndex = 9;
            this.button_Appointments_main.Text = "Appointments";
            this.button_Appointments_main.UseVisualStyleBackColor = true;
            this.button_Appointments_main.Click += new System.EventHandler(this.button_Appointments_main_Click);
            // 
            // button_Customers_main
            // 
            this.button_Customers_main.Location = new System.Drawing.Point(653, 12);
            this.button_Customers_main.Name = "button_Customers_main";
            this.button_Customers_main.Size = new System.Drawing.Size(229, 41);
            this.button_Customers_main.TabIndex = 10;
            this.button_Customers_main.Text = "Customers";
            this.button_Customers_main.UseVisualStyleBackColor = true;
            this.button_Customers_main.Click += new System.EventHandler(this.button_Customers_main_Click);
            // 
            // label_TimeZone_main
            // 
            this.label_TimeZone_main.AutoSize = true;
            this.label_TimeZone_main.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label_TimeZone_main.Location = new System.Drawing.Point(90, 88);
            this.label_TimeZone_main.Name = "label_TimeZone_main";
            this.label_TimeZone_main.Size = new System.Drawing.Size(105, 13);
            this.label_TimeZone_main.TabIndex = 12;
            this.label_TimeZone_main.Text = "Your Time Zone: ";
            // 
            // label_TimeZoneInput_main
            // 
            this.label_TimeZoneInput_main.AutoSize = true;
            this.label_TimeZoneInput_main.Location = new System.Drawing.Point(202, 88);
            this.label_TimeZoneInput_main.Name = "label_TimeZoneInput_main";
            this.label_TimeZoneInput_main.Size = new System.Drawing.Size(66, 13);
            this.label_TimeZoneInput_main.TabIndex = 13;
            this.label_TimeZoneInput_main.Text = "*Time Zone*";
            // 
            // button_Logout_main
            // 
            this.button_Logout_main.Location = new System.Drawing.Point(1126, 573);
            this.button_Logout_main.Name = "button_Logout_main";
            this.button_Logout_main.Size = new System.Drawing.Size(126, 45);
            this.button_Logout_main.TabIndex = 14;
            this.button_Logout_main.Text = "Logout";
            this.button_Logout_main.UseVisualStyleBackColor = true;
            this.button_Logout_main.Click += new System.EventHandler(this.button_Logout_main_Click);
            // 
            // dataGridView_Appointments
            // 
            this.dataGridView_Appointments.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView_Appointments.Location = new System.Drawing.Point(93, 105);
            this.dataGridView_Appointments.Name = "dataGridView_Appointments";
            this.dataGridView_Appointments.Size = new System.Drawing.Size(789, 453);
            this.dataGridView_Appointments.TabIndex = 15;
            // 
            // button_RefreshView
            // 
            this.button_RefreshView.Location = new System.Drawing.Point(903, 94);
            this.button_RefreshView.Name = "button_RefreshView";
            this.button_RefreshView.Size = new System.Drawing.Size(75, 23);
            this.button_RefreshView.TabIndex = 16;
            this.button_RefreshView.Text = "Refresh";
            this.button_RefreshView.UseVisualStyleBackColor = true;
            this.button_RefreshView.Click += new System.EventHandler(this.RefreshView_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1264, 630);
            this.Controls.Add(this.button_RefreshView);
            this.Controls.Add(this.dataGridView_Appointments);
            this.Controls.Add(this.button_Logout_main);
            this.Controls.Add(this.label_TimeZoneInput_main);
            this.Controls.Add(this.label_TimeZone_main);
            this.Controls.Add(this.button_Customers_main);
            this.Controls.Add(this.button_Appointments_main);
            this.Controls.Add(this.button_Reports_main);
            this.Controls.Add(this.label_searchAppointment_main);
            this.Controls.Add(this.datetimePicker_searchAppointment_main);
            this.Controls.Add(this.rad_CurrentMonth_main);
            this.Controls.Add(this.rad_CurrentWeek_main);
            this.Controls.Add(this.rad_AllAppointments_main);
            this.Name = "MainForm";
            this.Text = "Main";
            this.Load += new System.EventHandler(this.MainForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView_Appointments)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.RadioButton rad_AllAppointments_main;
        private System.Windows.Forms.RadioButton rad_CurrentWeek_main;
        private System.Windows.Forms.RadioButton rad_CurrentMonth_main;
        private System.Windows.Forms.DateTimePicker datetimePicker_searchAppointment_main;
        private System.Windows.Forms.Label label_searchAppointment_main;
        private System.Windows.Forms.Button button_Reports_main;
        private System.Windows.Forms.Button button_Appointments_main;
        private System.Windows.Forms.Button button_Customers_main;
        private System.Windows.Forms.Label label_TimeZone_main;
        private System.Windows.Forms.Label label_TimeZoneInput_main;
        private System.Windows.Forms.Button button_Logout_main;
        private System.Windows.Forms.DataGridView dataGridView_Appointments;
        private System.Windows.Forms.Button button_RefreshView;
    }
}