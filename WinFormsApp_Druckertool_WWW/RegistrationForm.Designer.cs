namespace WinFormsApp_Druckertool_WWW
{
    partial class administratorForm
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
            DataGridViewCellStyle dataGridViewCellStyle1 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle2 = new DataGridViewCellStyle();
            close_admin_btn = new Button();
            upload_exl_files = new Button();
            save_btn = new Button();
            dataGridView1 = new DataGridView();
            button1 = new Button();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).BeginInit();
            SuspendLayout();
            // 
            // close_admin_btn
            // 
            close_admin_btn.Anchor = AnchorStyles.None;
            close_admin_btn.Font = new Font("Segoe UI Black", 9.75F, FontStyle.Bold, GraphicsUnit.Point);
            close_admin_btn.Location = new Point(599, 725);
            close_admin_btn.Margin = new Padding(3, 4, 3, 4);
            close_admin_btn.Name = "close_admin_btn";
            close_admin_btn.Size = new Size(93, 31);
            close_admin_btn.TabIndex = 1;
            close_admin_btn.Text = "Schließen";
            close_admin_btn.UseVisualStyleBackColor = true;
            close_admin_btn.Click += save_admin_btn_Click;
            // 
            // upload_exl_files
            // 
            upload_exl_files.Anchor = AnchorStyles.None;
            upload_exl_files.BackColor = SystemColors.ControlLightLight;
            upload_exl_files.FlatStyle = FlatStyle.Flat;
            upload_exl_files.Font = new Font("Segoe UI Black", 9.75F, FontStyle.Bold, GraphicsUnit.Point);
            upload_exl_files.Location = new Point(438, 773);
            upload_exl_files.Margin = new Padding(3, 4, 3, 4);
            upload_exl_files.Name = "upload_exl_files";
            upload_exl_files.Size = new Size(171, 45);
            upload_exl_files.TabIndex = 2;
            upload_exl_files.Text = "Upload Excel Files";
            upload_exl_files.UseVisualStyleBackColor = false;
            upload_exl_files.Click += upload_exl_files_Click;
            // 
            // save_btn
            // 
            save_btn.Anchor = AnchorStyles.None;
            save_btn.BackColor = SystemColors.Window;
            save_btn.FlatAppearance.BorderColor = SystemColors.WindowText;
            save_btn.FlatAppearance.MouseOverBackColor = Color.MediumSeaGreen;
            save_btn.FlatStyle = FlatStyle.Flat;
            save_btn.Font = new Font("Segoe UI Semibold", 14.25F, FontStyle.Bold, GraphicsUnit.Point);
            save_btn.ForeColor = SystemColors.WindowText;
            save_btn.Location = new Point(1005, 846);
            save_btn.Margin = new Padding(3, 4, 3, 4);
            save_btn.Name = "save_btn";
            save_btn.Size = new Size(208, 83);
            save_btn.TabIndex = 3;
            save_btn.Text = "Speichern";
            save_btn.UseVisualStyleBackColor = false;
            save_btn.Click += save_btn_Click;
            // 
            // dataGridView1
            // 
            dataGridView1.Anchor = AnchorStyles.None;
            dataGridView1.BackgroundColor = SystemColors.Window;
            dataGridViewCellStyle1.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = SystemColors.Control;
            dataGridViewCellStyle1.Font = new Font("Segoe UI", 14.25F, FontStyle.Bold, GraphicsUnit.Point);
            dataGridViewCellStyle1.ForeColor = SystemColors.WindowText;
            dataGridViewCellStyle1.Padding = new Padding(0, 0, 1, 1);
            dataGridViewCellStyle1.SelectionBackColor = SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = DataGridViewTriState.True;
            dataGridView1.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            dataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle2.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = SystemColors.Window;
            dataGridViewCellStyle2.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point);
            dataGridViewCellStyle2.ForeColor = SystemColors.ControlText;
            dataGridViewCellStyle2.SelectionBackColor = SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = DataGridViewTriState.False;
            dataGridView1.DefaultCellStyle = dataGridViewCellStyle2;
            dataGridView1.Location = new Point(14, 101);
            dataGridView1.Margin = new Padding(3, 4, 3, 4);
            dataGridView1.Name = "dataGridView1";
            dataGridView1.RowHeadersWidth = 51;
            dataGridView1.RowTemplate.Height = 25;
            dataGridView1.Size = new Size(2040, 717);
            dataGridView1.TabIndex = 4;
            dataGridView1.CellContentClick += dataGridView1_CellContentClick;
            // 
            // button1
            // 
            button1.Anchor = AnchorStyles.None;
            button1.BackColor = Color.SteelBlue;
            button1.FlatAppearance.BorderColor = Color.SteelBlue;
            button1.FlatAppearance.BorderSize = 6;
            button1.FlatAppearance.MouseDownBackColor = Color.SteelBlue;
            button1.FlatAppearance.MouseOverBackColor = Color.SteelBlue;
            button1.FlatStyle = FlatStyle.Flat;
            button1.Font = new Font("Segoe UI", 15.75F, FontStyle.Bold, GraphicsUnit.Point);
            button1.ForeColor = SystemColors.Window;
            button1.Location = new Point(0, 5);
            button1.Margin = new Padding(3, 4, 3, 4);
            button1.Name = "button1";
            button1.Size = new Size(2069, 83);
            button1.TabIndex = 5;
            button1.Text = "← Administrator";
            button1.TextAlign = ContentAlignment.MiddleLeft;
            button1.UseVisualStyleBackColor = false;
            button1.Click += button1_Click;
            // 
            // administratorForm
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            AutoValidate = AutoValidate.EnableAllowFocusChange;
            BackColor = SystemColors.GradientInactiveCaption;
            ClientSize = new Size(1924, 965);
            Controls.Add(button1);
            Controls.Add(dataGridView1);
            Controls.Add(save_btn);
            Controls.Add(upload_exl_files);
            Controls.Add(close_admin_btn);
            Margin = new Padding(3, 4, 3, 4);
            Name = "administratorForm";
            Text = "AdministratorForm";
            ((System.ComponentModel.ISupportInitialize)dataGridView1).EndInit();
            ResumeLayout(false);
        }

        #endregion
        private Button close_admin_btn;
        private Button upload_exl_files;
        private Button save_btn;
        private DataGridView dataGridView1;
        private Button button1;
    }
}