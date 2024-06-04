
namespace C969_Software2_SpencerSterling_v1
{
    partial class Reports
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
            this.label1 = new System.Windows.Forms.Label();
            this.comboBox_Months = new System.Windows.Forms.ComboBox();
            this.dataGridView_AppointmentTypes = new System.Windows.Forms.DataGridView();
            this.label2 = new System.Windows.Forms.Label();
            this.comboBox_Users = new System.Windows.Forms.ComboBox();
            this.dataGridView_UserSchedules = new System.Windows.Forms.DataGridView();
            this.label3 = new System.Windows.Forms.Label();
            this.dataGridView_CustomerCountCity = new System.Windows.Forms.DataGridView();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView_AppointmentTypes)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView_UserSchedules)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView_CustomerCountCity)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(14, 34);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(205, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "REPORT 1: Appointment Types by Month";
            // 
            // comboBox_Months
            // 
            this.comboBox_Months.FormattingEnabled = true;
            this.comboBox_Months.Location = new System.Drawing.Point(17, 73);
            this.comboBox_Months.Name = "comboBox_Months";
            this.comboBox_Months.Size = new System.Drawing.Size(185, 21);
            this.comboBox_Months.TabIndex = 2;
            this.comboBox_Months.SelectedIndexChanged += new System.EventHandler(this.comboBox_Months_SelectedIndexChanged);
            // 
            // dataGridView_AppointmentTypes
            // 
            this.dataGridView_AppointmentTypes.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView_AppointmentTypes.Location = new System.Drawing.Point(208, 73);
            this.dataGridView_AppointmentTypes.Name = "dataGridView_AppointmentTypes";
            this.dataGridView_AppointmentTypes.Size = new System.Drawing.Size(425, 125);
            this.dataGridView_AppointmentTypes.TabIndex = 3;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(14, 242);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(170, 13);
            this.label2.TabIndex = 4;
            this.label2.Text = "REPORT 2: Consultant Schedules";
            // 
            // comboBox_Users
            // 
            this.comboBox_Users.FormattingEnabled = true;
            this.comboBox_Users.Location = new System.Drawing.Point(17, 270);
            this.comboBox_Users.Name = "comboBox_Users";
            this.comboBox_Users.Size = new System.Drawing.Size(185, 21);
            this.comboBox_Users.TabIndex = 5;
            this.comboBox_Users.SelectedIndexChanged += new System.EventHandler(this.comboBox_Users_SelectedIndexChanged);
            // 
            // dataGridView_UserSchedules
            // 
            this.dataGridView_UserSchedules.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView_UserSchedules.Location = new System.Drawing.Point(208, 270);
            this.dataGridView_UserSchedules.Name = "dataGridView_UserSchedules";
            this.dataGridView_UserSchedules.Size = new System.Drawing.Size(425, 125);
            this.dataGridView_UserSchedules.TabIndex = 6;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(14, 422);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(176, 13);
            this.label3.TabIndex = 7;
            this.label3.Text = "REPORT 3: Customer Count by City";
            // 
            // dataGridView_CustomerCountCity
            // 
            this.dataGridView_CustomerCountCity.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridView_CustomerCountCity.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView_CustomerCountCity.Location = new System.Drawing.Point(208, 467);
            this.dataGridView_CustomerCountCity.Name = "dataGridView_CustomerCountCity";
            this.dataGridView_CustomerCountCity.Size = new System.Drawing.Size(425, 125);
            this.dataGridView_CustomerCountCity.TabIndex = 8;
            // 
            // Reports
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(711, 636);
            this.Controls.Add(this.dataGridView_CustomerCountCity);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.dataGridView_UserSchedules);
            this.Controls.Add(this.comboBox_Users);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.dataGridView_AppointmentTypes);
            this.Controls.Add(this.comboBox_Months);
            this.Controls.Add(this.label1);
            this.Name = "Reports";
            this.Text = "Reports";
            this.Load += new System.EventHandler(this.Reports_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView_AppointmentTypes)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView_UserSchedules)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView_CustomerCountCity)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox comboBox_Months;
        private System.Windows.Forms.DataGridView dataGridView_AppointmentTypes;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox comboBox_Users;
        private System.Windows.Forms.DataGridView dataGridView_UserSchedules;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.DataGridView dataGridView_CustomerCountCity;
    }
}