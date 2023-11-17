namespace WinFormsApp_Druckertool_WWW
{
    partial class anwenderForm
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
            DataGridViewCellStyle dataGridViewCellStyle3 = new DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(anwenderForm));
            Dokumente_label = new Label();
            close_anw_btn = new Button();
            Dok_numb_txt = new TextBox();
            Anfahrt_btn = new Button();
            Abfahrt_btn = new Button();
            Uebergabe_btn = new Button();
            Drucken_btn = new Button();
            dataGridView1 = new DataGridView();
            printDocument1 = new System.Drawing.Printing.PrintDocument();
            Anwender_btn = new Button();
            Kapazitaet_Anfahrt_btn = new Button();
            SP_K_textBox = new TextBox();
            SP_K_label = new Label();
            save_btn = new Button();
            Kapazitaet_Abfahrt_btn = new Button();
            Reparatur_abfahrt_btn = new Button();
            Reparatur_Anfahrt_btn = new Button();
            Rückwärts_btn = new Button();
            label_message = new Label();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).BeginInit();
            SuspendLayout();
            // 
            // Dokumente_label
            // 
            Dokumente_label.Anchor = AnchorStyles.None;
            Dokumente_label.AutoSize = true;
            Dokumente_label.Font = new Font("Segoe UI", 14.25F, FontStyle.Regular, GraphicsUnit.Point);
            Dokumente_label.Location = new Point(438, 662);
            Dokumente_label.Name = "Dokumente_label";
            Dokumente_label.Size = new Size(109, 25);
            Dokumente_label.TabIndex = 0;
            Dokumente_label.Text = "Dokumente";
            // 
            // close_anw_btn
            // 
            close_anw_btn.Anchor = AnchorStyles.None;
            close_anw_btn.Font = new Font("Segoe UI Black", 9F, FontStyle.Bold, GraphicsUnit.Point);
            close_anw_btn.Location = new Point(527, 692);
            close_anw_btn.Name = "close_anw_btn";
            close_anw_btn.Size = new Size(75, 23);
            close_anw_btn.TabIndex = 1;
            close_anw_btn.Text = "Schließen";
            close_anw_btn.UseVisualStyleBackColor = true;
            close_anw_btn.Visible = false;
            close_anw_btn.Click += save_anw_btn_Click;
            // 
            // Dok_numb_txt
            // 
            Dok_numb_txt.Anchor = AnchorStyles.None;
            Dok_numb_txt.Font = new Font("Segoe UI", 14F, FontStyle.Regular, GraphicsUnit.Point);
            Dok_numb_txt.Location = new Point(360, 655);
            Dok_numb_txt.Name = "Dok_numb_txt";
            Dok_numb_txt.Size = new Size(82, 32);
            Dok_numb_txt.TabIndex = 2;
            Dok_numb_txt.TextChanged += Dok_numb_txt_TextChanged;
            // 
            // Anfahrt_btn
            // 
            Anfahrt_btn.Anchor = AnchorStyles.None;
            Anfahrt_btn.BackColor = Color.Bisque;
            Anfahrt_btn.FlatAppearance.MouseDownBackColor = Color.Bisque;
            Anfahrt_btn.FlatAppearance.MouseOverBackColor = Color.Bisque;
            Anfahrt_btn.FlatStyle = FlatStyle.Flat;
            Anfahrt_btn.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point);
            Anfahrt_btn.Location = new Point(410, 48);
            Anfahrt_btn.Name = "Anfahrt_btn";
            Anfahrt_btn.Size = new Size(182, 62);
            Anfahrt_btn.TabIndex = 3;
            Anfahrt_btn.Text = "Anfahrt";
            Anfahrt_btn.UseVisualStyleBackColor = false;
            Anfahrt_btn.Click += Anfahrt_btn_Click;
            // 
            // Abfahrt_btn
            // 
            Abfahrt_btn.Anchor = AnchorStyles.None;
            Abfahrt_btn.BackColor = Color.Bisque;
            Abfahrt_btn.FlatAppearance.MouseOverBackColor = Color.Bisque;
            Abfahrt_btn.FlatStyle = FlatStyle.Flat;
            Abfahrt_btn.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point);
            Abfahrt_btn.Location = new Point(616, 48);
            Abfahrt_btn.Name = "Abfahrt_btn";
            Abfahrt_btn.Size = new Size(182, 62);
            Abfahrt_btn.TabIndex = 4;
            Abfahrt_btn.Text = "Abfahrt";
            Abfahrt_btn.UseVisualStyleBackColor = false;
            Abfahrt_btn.Click += Abfahrt_btn_Click;
            // 
            // Uebergabe_btn
            // 
            Uebergabe_btn.Anchor = AnchorStyles.None;
            Uebergabe_btn.BackColor = Color.Bisque;
            Uebergabe_btn.FlatAppearance.MouseOverBackColor = Color.Bisque;
            Uebergabe_btn.FlatStyle = FlatStyle.Flat;
            Uebergabe_btn.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point);
            Uebergabe_btn.Location = new Point(816, 48);
            Uebergabe_btn.Name = "Uebergabe_btn";
            Uebergabe_btn.Size = new Size(182, 62);
            Uebergabe_btn.TabIndex = 5;
            Uebergabe_btn.Text = "Uebergabe";
            Uebergabe_btn.UseVisualStyleBackColor = false;
            Uebergabe_btn.Click += Uebergabe_btn_Click;
            // 
            // Drucken_btn
            // 
            Drucken_btn.Anchor = AnchorStyles.None;
            Drucken_btn.BackColor = SystemColors.Window;
            Drucken_btn.FlatAppearance.BorderColor = Color.Black;
            Drucken_btn.FlatAppearance.MouseOverBackColor = Color.MediumSeaGreen;
            Drucken_btn.FlatStyle = FlatStyle.Flat;
            Drucken_btn.Font = new Font("Segoe UI Semibold", 14.25F, FontStyle.Bold, GraphicsUnit.Point);
            Drucken_btn.ForeColor = SystemColors.WindowText;
            Drucken_btn.Location = new Point(1004, 635);
            Drucken_btn.Name = "Drucken_btn";
            Drucken_btn.Size = new Size(156, 62);
            Drucken_btn.TabIndex = 6;
            Drucken_btn.Text = "Drucken";
            Drucken_btn.UseVisualStyleBackColor = false;
            Drucken_btn.Click += Drucken_btn_Click;
            // 
            // dataGridView1
            // 
            dataGridView1.Anchor = AnchorStyles.None;
            dataGridView1.BackgroundColor = SystemColors.Window;
            dataGridViewCellStyle1.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = SystemColors.Control;
            dataGridViewCellStyle1.Font = new Font("Segoe UI", 14.25F, FontStyle.Bold, GraphicsUnit.Point);
            dataGridViewCellStyle1.ForeColor = SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = DataGridViewTriState.True;
            dataGridView1.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            dataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle2.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = SystemColors.Window;
            dataGridViewCellStyle2.Font = new Font("Segoe UI", 14.25F, FontStyle.Regular, GraphicsUnit.Point);
            dataGridViewCellStyle2.ForeColor = SystemColors.ControlText;
            dataGridViewCellStyle2.SelectionBackColor = SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = DataGridViewTriState.False;
            dataGridView1.DefaultCellStyle = dataGridViewCellStyle2;
            dataGridView1.Location = new Point(360, 206);
            dataGridView1.Name = "dataGridView1";
            dataGridViewCellStyle3.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = SystemColors.Control;
            dataGridViewCellStyle3.Font = new Font("Segoe UI", 14.25F, FontStyle.Bold, GraphicsUnit.Point);
            dataGridViewCellStyle3.ForeColor = SystemColors.WindowText;
            dataGridViewCellStyle3.SelectionBackColor = SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = DataGridViewTriState.True;
            dataGridView1.RowHeadersDefaultCellStyle = dataGridViewCellStyle3;
            dataGridView1.RowHeadersWidth = 51;
            dataGridView1.RowTemplate.Height = 25;
            dataGridView1.Size = new Size(835, 420);
            dataGridView1.TabIndex = 7;
            dataGridView1.CellContentClick += dataGridView1_CellContentClick;
            // 
            // printDocument1
            // 
            printDocument1.PrintPage += printDocument1_PrintPage;
            // 
            // Anwender_btn
            // 
            Anwender_btn.Anchor = AnchorStyles.None;
            Anwender_btn.BackColor = Color.SteelBlue;
            Anwender_btn.FlatAppearance.BorderColor = Color.SteelBlue;
            Anwender_btn.FlatAppearance.BorderSize = 6;
            Anwender_btn.FlatAppearance.MouseDownBackColor = Color.SteelBlue;
            Anwender_btn.FlatAppearance.MouseOverBackColor = Color.SteelBlue;
            Anwender_btn.FlatStyle = FlatStyle.Flat;
            Anwender_btn.Font = new Font("Segoe UI", 15.75F, FontStyle.Bold, GraphicsUnit.Point);
            Anwender_btn.ForeColor = SystemColors.Window;
            Anwender_btn.Location = new Point(0, -19);
            Anwender_btn.Name = "Anwender_btn";
            Anwender_btn.Size = new Size(1272, 62);
            Anwender_btn.TabIndex = 8;
            Anwender_btn.Text = "← Anwender";
            Anwender_btn.TextAlign = ContentAlignment.MiddleLeft;
            Anwender_btn.UseVisualStyleBackColor = false;
            Anwender_btn.Click += Anwender_btn_Click;
            // 
            // Kapazitaet_Anfahrt_btn
            // 
            Kapazitaet_Anfahrt_btn.Anchor = AnchorStyles.None;
            Kapazitaet_Anfahrt_btn.BackColor = Color.Bisque;
            Kapazitaet_Anfahrt_btn.FlatAppearance.MouseOverBackColor = Color.Bisque;
            Kapazitaet_Anfahrt_btn.FlatStyle = FlatStyle.Flat;
            Kapazitaet_Anfahrt_btn.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point);
            Kapazitaet_Anfahrt_btn.Location = new Point(707, 121);
            Kapazitaet_Anfahrt_btn.Name = "Kapazitaet_Anfahrt_btn";
            Kapazitaet_Anfahrt_btn.Size = new Size(182, 62);
            Kapazitaet_Anfahrt_btn.TabIndex = 10;
            Kapazitaet_Anfahrt_btn.Text = "Kapazität-Anfar";
            Kapazitaet_Anfahrt_btn.UseVisualStyleBackColor = false;
            Kapazitaet_Anfahrt_btn.Visible = false;
            Kapazitaet_Anfahrt_btn.Click += Kapazitaet_btn_Click;
            // 
            // SP_K_textBox
            // 
            SP_K_textBox.Anchor = AnchorStyles.None;
            SP_K_textBox.Font = new Font("Segoe UI", 14F, FontStyle.Regular, GraphicsUnit.Point);
            SP_K_textBox.Location = new Point(96, 60);
            SP_K_textBox.MaxLength = 7;
            SP_K_textBox.Name = "SP_K_textBox";
            SP_K_textBox.PlaceholderText = "Beispiel: SP23-43";
            SP_K_textBox.Size = new Size(166, 32);
            SP_K_textBox.TabIndex = 12;
            SP_K_textBox.TextChanged += SP_K_textBox_TextChanged;
            // 
            // SP_K_label
            // 
            SP_K_label.Anchor = AnchorStyles.None;
            SP_K_label.AutoSize = true;
            SP_K_label.Font = new Font("Segoe UI", 14.25F, FontStyle.Regular, GraphicsUnit.Point);
            SP_K_label.Location = new Point(41, 65);
            SP_K_label.Name = "SP_K_label";
            SP_K_label.Size = new Size(51, 25);
            SP_K_label.TabIndex = 11;
            SP_K_label.Text = "SP/K";
            // 
            // save_btn
            // 
            save_btn.Anchor = AnchorStyles.None;
            save_btn.BackColor = SystemColors.Window;
            save_btn.FlatAppearance.BorderColor = Color.Black;
            save_btn.FlatAppearance.MouseOverBackColor = Color.MediumSeaGreen;
            save_btn.FlatStyle = FlatStyle.Flat;
            save_btn.Font = new Font("Segoe UI Semibold", 14.25F, FontStyle.Bold, GraphicsUnit.Point);
            save_btn.ForeColor = SystemColors.WindowText;
            save_btn.Location = new Point(842, 635);
            save_btn.Name = "save_btn";
            save_btn.Size = new Size(156, 62);
            save_btn.TabIndex = 13;
            save_btn.Text = "Speichern";
            save_btn.UseVisualStyleBackColor = false;
            save_btn.Click += save_btn_Click_1;
            // 
            // Kapazitaet_Abfahrt_btn
            // 
            Kapazitaet_Abfahrt_btn.Anchor = AnchorStyles.None;
            Kapazitaet_Abfahrt_btn.BackColor = Color.Bisque;
            Kapazitaet_Abfahrt_btn.FlatAppearance.MouseOverBackColor = Color.Bisque;
            Kapazitaet_Abfahrt_btn.FlatStyle = FlatStyle.Flat;
            Kapazitaet_Abfahrt_btn.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point);
            Kapazitaet_Abfahrt_btn.Location = new Point(708, 122);
            Kapazitaet_Abfahrt_btn.Name = "Kapazitaet_Abfahrt_btn";
            Kapazitaet_Abfahrt_btn.Size = new Size(182, 62);
            Kapazitaet_Abfahrt_btn.TabIndex = 15;
            Kapazitaet_Abfahrt_btn.Text = "Kapazität-Abf";
            Kapazitaet_Abfahrt_btn.UseVisualStyleBackColor = false;
            Kapazitaet_Abfahrt_btn.Visible = false;
            Kapazitaet_Abfahrt_btn.Click += Kapazitaet_Abfahrt_btn_Click;
            // 
            // Reparatur_abfahrt_btn
            // 
            Reparatur_abfahrt_btn.Anchor = AnchorStyles.None;
            Reparatur_abfahrt_btn.BackColor = Color.Bisque;
            Reparatur_abfahrt_btn.FlatAppearance.MouseOverBackColor = Color.Bisque;
            Reparatur_abfahrt_btn.FlatStyle = FlatStyle.Flat;
            Reparatur_abfahrt_btn.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point);
            Reparatur_abfahrt_btn.Location = new Point(506, 124);
            Reparatur_abfahrt_btn.Name = "Reparatur_abfahrt_btn";
            Reparatur_abfahrt_btn.Size = new Size(182, 62);
            Reparatur_abfahrt_btn.TabIndex = 14;
            Reparatur_abfahrt_btn.Text = "Reparatur-Abf";
            Reparatur_abfahrt_btn.UseVisualStyleBackColor = false;
            Reparatur_abfahrt_btn.Visible = false;
            Reparatur_abfahrt_btn.Click += Reparatur_abfahrt_btn_Click;
            // 
            // Reparatur_Anfahrt_btn
            // 
            Reparatur_Anfahrt_btn.Anchor = AnchorStyles.None;
            Reparatur_Anfahrt_btn.BackColor = Color.Bisque;
            Reparatur_Anfahrt_btn.FlatAppearance.MouseOverBackColor = Color.Bisque;
            Reparatur_Anfahrt_btn.FlatStyle = FlatStyle.Flat;
            Reparatur_Anfahrt_btn.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point);
            Reparatur_Anfahrt_btn.Location = new Point(505, 123);
            Reparatur_Anfahrt_btn.Name = "Reparatur_Anfahrt_btn";
            Reparatur_Anfahrt_btn.Size = new Size(182, 62);
            Reparatur_Anfahrt_btn.TabIndex = 16;
            Reparatur_Anfahrt_btn.Text = "Reparatur-Anf";
            Reparatur_Anfahrt_btn.UseVisualStyleBackColor = false;
            Reparatur_Anfahrt_btn.Visible = false;
            Reparatur_Anfahrt_btn.Click += Reparatur_Anfahrt_btn_Click;
            // 
            // Rückwärts_btn
            // 
            Rückwärts_btn.Anchor = AnchorStyles.None;
            Rückwärts_btn.BackColor = Color.Bisque;
            Rückwärts_btn.FlatAppearance.MouseOverBackColor = Color.Bisque;
            Rückwärts_btn.FlatStyle = FlatStyle.Flat;
            Rückwärts_btn.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point);
            Rückwärts_btn.Location = new Point(1136, 58);
            Rückwärts_btn.Name = "Rückwärts_btn";
            Rückwärts_btn.Size = new Size(98, 52);
            Rückwärts_btn.TabIndex = 17;
            Rückwärts_btn.Text = "◀ Rückwärts";
            Rückwärts_btn.UseVisualStyleBackColor = false;
            Rückwärts_btn.Click += Rückwärts_btn_Click;
            // 
            // label_message
            // 
            label_message.Anchor = AnchorStyles.None;
            label_message.AutoSize = true;
            label_message.Font = new Font("Perpetua", 12.25F, FontStyle.Bold, GraphicsUnit.Point);
            label_message.ForeColor = Color.Crimson;
            label_message.Location = new Point(22, 103);
            label_message.Name = "label_message";
            label_message.Size = new Size(386, 100);
            label_message.TabIndex = 18;
            label_message.Text = resources.GetString("label_message.Text");
            label_message.Click += label_message_Click;
            // 
            // anwenderForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = SystemColors.GradientInactiveCaption;
            ClientSize = new Size(1275, 717);
            Controls.Add(label_message);
            Controls.Add(Rückwärts_btn);
            Controls.Add(Reparatur_Anfahrt_btn);
            Controls.Add(Kapazitaet_Abfahrt_btn);
            Controls.Add(Reparatur_abfahrt_btn);
            Controls.Add(save_btn);
            Controls.Add(SP_K_textBox);
            Controls.Add(SP_K_label);
            Controls.Add(Kapazitaet_Anfahrt_btn);
            Controls.Add(Anwender_btn);
            Controls.Add(dataGridView1);
            Controls.Add(Drucken_btn);
            Controls.Add(Uebergabe_btn);
            Controls.Add(Abfahrt_btn);
            Controls.Add(Anfahrt_btn);
            Controls.Add(Dok_numb_txt);
            Controls.Add(close_anw_btn);
            Controls.Add(Dokumente_label);
            Name = "anwenderForm";
            Text = "AnwenderForm";
            ((System.ComponentModel.ISupportInitialize)dataGridView1).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label Dokumente_label;
        private Button close_anw_btn;
        private TextBox Dok_numb_txt;
        private Button Anfahrt_btn;
        private Button Abfahrt_btn;
        private Button Uebergabe_btn;
        private Button Drucken_btn;
        private DataGridView dataGridView1;
        private System.Drawing.Printing.PrintDocument printDocument1;
        private Button Anwender_btn;
        private Button Kapazitaet_Anfahrt_btn;
        private TextBox SP_K_textBox;
        private Label SP_K_label;
        private Button save_btn;
        private Button Kapazitaet_Abfahrt_btn;
        private Button Reparatur_abfahrt_btn;
        private Button button1;
        private Button Reparatur_Anfahrt_btn;
        private Button Rückwärts_btn;
        private Label label_message;
    }
}