namespace WinFormsApp_Test
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            btn_Anwender = new Button();
            btn_Administrator = new Button();
            panel2 = new Panel();
            Zurückkehren_btn = new Button();
            Abfahrt_btn = new Button();
            Anfahrt_btn = new Button();
            btn_save = new Button();
            btn_UploadExcelFiles = new Button();
            dataGridView_Admin = new DataGridView();
            label2_Administrator = new Label();
            panel1 = new Panel();
            label1_Anwender = new Label();
            panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dataGridView_Admin).BeginInit();
            panel1.SuspendLayout();
            SuspendLayout();
            // 
            // btn_Anwender
            // 
            btn_Anwender.Location = new Point(177, 12);
            btn_Anwender.Name = "btn_Anwender";
            btn_Anwender.Size = new Size(75, 23);
            btn_Anwender.TabIndex = 1;
            btn_Anwender.Text = "Anwender";
            btn_Anwender.UseVisualStyleBackColor = true;
            btn_Anwender.Click += btn_Anwender_Click;
            // 
            // btn_Administrator
            // 
            btn_Administrator.Location = new Point(454, 16);
            btn_Administrator.Name = "btn_Administrator";
            btn_Administrator.Size = new Size(93, 23);
            btn_Administrator.TabIndex = 2;
            btn_Administrator.Text = "Administrator";
            btn_Administrator.UseVisualStyleBackColor = true;
            btn_Administrator.Click += btn_Administrator_Click;
            // 
            // panel2
            // 
            panel2.Controls.Add(Zurückkehren_btn);
            panel2.Controls.Add(Abfahrt_btn);
            panel2.Controls.Add(Anfahrt_btn);
            panel2.Controls.Add(btn_save);
            panel2.Controls.Add(btn_UploadExcelFiles);
            panel2.Controls.Add(dataGridView_Admin);
            panel2.Controls.Add(label2_Administrator);
            panel2.Location = new Point(174, 56);
            panel2.Name = "panel2";
            panel2.Size = new Size(583, 382);
            panel2.TabIndex = 1;
            // 
            // Zurückkehren_btn
            // 
            Zurückkehren_btn.Location = new Point(280, 70);
            Zurückkehren_btn.Name = "Zurückkehren_btn";
            Zurückkehren_btn.Size = new Size(93, 23);
            Zurückkehren_btn.TabIndex = 6;
            Zurückkehren_btn.Text = "Zurückkehren";
            Zurückkehren_btn.UseVisualStyleBackColor = true;
            Zurückkehren_btn.Click += Zurückkehren_btn_Click;
            // 
            // Abfahrt_btn
            // 
            Abfahrt_btn.Location = new Point(195, 343);
            Abfahrt_btn.Name = "Abfahrt_btn";
            Abfahrt_btn.Size = new Size(100, 23);
            Abfahrt_btn.TabIndex = 5;
            Abfahrt_btn.Text = "Abfahrt Button";
            Abfahrt_btn.UseVisualStyleBackColor = true;
            Abfahrt_btn.Click += Abfahrt_btn_Click;
            // 
            // Anfahrt_btn
            // 
            Anfahrt_btn.Location = new Point(70, 345);
            Anfahrt_btn.Name = "Anfahrt_btn";
            Anfahrt_btn.Size = new Size(100, 23);
            Anfahrt_btn.TabIndex = 4;
            Anfahrt_btn.Text = "Anfahrt Button";
            Anfahrt_btn.UseVisualStyleBackColor = true;
            Anfahrt_btn.Click += Anfahrt_btn_Click;
            // 
            // btn_save
            // 
            btn_save.Location = new Point(404, 70);
            btn_save.Name = "btn_save";
            btn_save.Size = new Size(75, 23);
            btn_save.TabIndex = 3;
            btn_save.Text = "Save";
            btn_save.UseVisualStyleBackColor = true;
            btn_save.Click += btn_save_Click;
            // 
            // btn_UploadExcelFiles
            // 
            btn_UploadExcelFiles.Location = new Point(121, 64);
            btn_UploadExcelFiles.Name = "btn_UploadExcelFiles";
            btn_UploadExcelFiles.Size = new Size(116, 23);
            btn_UploadExcelFiles.TabIndex = 2;
            btn_UploadExcelFiles.Text = "Upload Excel Files";
            btn_UploadExcelFiles.UseVisualStyleBackColor = true;
            btn_UploadExcelFiles.Click += btn_UploadExcelFiles_Click;
            // 
            // dataGridView_Admin
            // 
            dataGridView_Admin.AllowUserToAddRows = false;
            dataGridView_Admin.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView_Admin.Location = new Point(67, 101);
            dataGridView_Admin.Name = "dataGridView_Admin";
            dataGridView_Admin.RowTemplate.Height = 25;
            dataGridView_Admin.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridView_Admin.Size = new Size(458, 233);
            dataGridView_Admin.TabIndex = 1;
            dataGridView_Admin.CellContentClick += dataGridView_Admin_CellContentClick;
            // 
            // label2_Administrator
            // 
            label2_Administrator.AutoSize = true;
            label2_Administrator.Location = new Point(3, 24);
            label2_Administrator.Name = "label2_Administrator";
            label2_Administrator.Size = new Size(80, 15);
            label2_Administrator.TabIndex = 0;
            label2_Administrator.Text = "Administrator";
            // 
            // panel1
            // 
            panel1.Controls.Add(label1_Anwender);
            panel1.Location = new Point(36, 80);
            panel1.Name = "panel1";
            panel1.Size = new Size(132, 310);
            panel1.TabIndex = 3;
            // 
            // label1_Anwender
            // 
            label1_Anwender.AutoSize = true;
            label1_Anwender.BackColor = SystemColors.ActiveCaption;
            label1_Anwender.Location = new Point(40, 31);
            label1_Anwender.Name = "label1_Anwender";
            label1_Anwender.Size = new Size(61, 15);
            label1_Anwender.TabIndex = 0;
            label1_Anwender.Text = "Anwender";
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(panel1);
            Controls.Add(panel2);
            Controls.Add(btn_Administrator);
            Controls.Add(btn_Anwender);
            Name = "Form1";
            Text = "Form1";
            panel2.ResumeLayout(false);
            panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)dataGridView_Admin).EndInit();
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            ResumeLayout(false);
        }

        #endregion
        private Panel panel2;
        private Button btn_Anwender;
        private Button btn_Administrator;
        private Label label2_Administrator;
        private DataGridView dataGridView_Admin;
        private Button btn_UploadExcelFiles;
        private Button btn_save;
        private Panel panel1;
        private Label label1_Anwender;
        private Button Anfahrt_btn;
        private Button Abfahrt_btn;
        private Button Zurückkehren_btn;
    }
}