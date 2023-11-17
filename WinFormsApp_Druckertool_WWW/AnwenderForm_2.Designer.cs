namespace WinFormsApp_Druckertool_WWW
{
    partial class AnwenderForm_2
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AnwenderForm_2));
            label1 = new Label();
            textBox1 = new TextBox();
            label2 = new Label();
            anwender_btn = new Button();
            Anfahrt_btn = new Button();
            Abfahrt_btn = new Button();
            uebergabe_btn = new Button();
            rueckwaerts_btn = new Button();
            reparatur_anf_btn = new Button();
            kapazitaet_anf_btn = new Button();
            reparatur_abfahrt_btn = new Button();
            kapazitaet_abfahrt_btn = new Button();
            drucken_btn = new Button();
            dataGridView1 = new DataGridView();
            save_btn = new Button();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).BeginInit();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 14.25F, FontStyle.Regular, GraphicsUnit.Point);
            label1.Location = new Point(38, 171);
            label1.Name = "label1";
            label1.Size = new Size(63, 32);
            label1.TabIndex = 0;
            label1.Text = "SP\\K";
            // 
            // textBox1
            // 
            textBox1.Font = new Font("Segoe UI", 14F, FontStyle.Regular, GraphicsUnit.Point);
            textBox1.Location = new Point(94, 172);
            textBox1.Margin = new Padding(3, 4, 3, 4);
            textBox1.Name = "textBox1";
            textBox1.PlaceholderText = "Beispiel: SP23-43";
            textBox1.Size = new Size(177, 39);
            textBox1.TabIndex = 1;
            textBox1.TextChanged += textBox1_TextChanged;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Perpetua", 12.25F, FontStyle.Bold, GraphicsUnit.Point);
            label2.ForeColor = Color.Crimson;
            label2.Location = new Point(14, 253);
            label2.Name = "label2";
            label2.Size = new Size(474, 120);
            label2.TabIndex = 2;
            label2.Text = resources.GetString("label2.Text");
            // 
            // anwender_btn
            // 
            anwender_btn.BackColor = Color.SteelBlue;
            anwender_btn.Font = new Font("Segoe UI", 15.75F, FontStyle.Bold, GraphicsUnit.Point);
            anwender_btn.ForeColor = SystemColors.Window;
            anwender_btn.Location = new Point(-2, 3);
            anwender_btn.Margin = new Padding(3, 4, 3, 4);
            anwender_btn.Name = "anwender_btn";
            anwender_btn.Size = new Size(1454, 83);
            anwender_btn.TabIndex = 3;
            anwender_btn.Text = "← Anwender";
            anwender_btn.TextAlign = ContentAlignment.MiddleLeft;
            anwender_btn.UseVisualStyleBackColor = false;
            // 
            // Anfahrt_btn
            // 
            Anfahrt_btn.BackColor = Color.Bisque;
            Anfahrt_btn.FlatStyle = FlatStyle.Flat;
            Anfahrt_btn.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point);
            Anfahrt_btn.Location = new Point(521, 103);
            Anfahrt_btn.Margin = new Padding(3, 4, 3, 4);
            Anfahrt_btn.Name = "Anfahrt_btn";
            Anfahrt_btn.Size = new Size(176, 75);
            Anfahrt_btn.TabIndex = 4;
            Anfahrt_btn.Text = "Anfahrt";
            Anfahrt_btn.UseVisualStyleBackColor = false;
            Anfahrt_btn.Click += Anfahrt_btn_Click;
            // 
            // Abfahrt_btn
            // 
            Abfahrt_btn.BackColor = Color.Bisque;
            Abfahrt_btn.FlatStyle = FlatStyle.Flat;
            Abfahrt_btn.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point);
            Abfahrt_btn.Location = new Point(725, 104);
            Abfahrt_btn.Margin = new Padding(3, 4, 3, 4);
            Abfahrt_btn.Name = "Abfahrt_btn";
            Abfahrt_btn.Size = new Size(179, 73);
            Abfahrt_btn.TabIndex = 5;
            Abfahrt_btn.Text = "Abfahrt";
            Abfahrt_btn.UseVisualStyleBackColor = false;
            Abfahrt_btn.Click += Abfahrt_btn_Click;
            // 
            // uebergabe_btn
            // 
            uebergabe_btn.BackColor = Color.Bisque;
            uebergabe_btn.FlatStyle = FlatStyle.Flat;
            uebergabe_btn.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point);
            uebergabe_btn.Location = new Point(938, 104);
            uebergabe_btn.Margin = new Padding(3, 4, 3, 4);
            uebergabe_btn.Name = "uebergabe_btn";
            uebergabe_btn.Size = new Size(183, 73);
            uebergabe_btn.TabIndex = 6;
            uebergabe_btn.Text = "Übergabe";
            uebergabe_btn.UseVisualStyleBackColor = false;
            // 
            // rueckwaerts_btn
            // 
            rueckwaerts_btn.BackColor = Color.Bisque;
            rueckwaerts_btn.FlatStyle = FlatStyle.Flat;
            rueckwaerts_btn.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point);
            rueckwaerts_btn.Location = new Point(1253, 104);
            rueckwaerts_btn.Margin = new Padding(3, 4, 3, 4);
            rueckwaerts_btn.Name = "rueckwaerts_btn";
            rueckwaerts_btn.Size = new Size(167, 73);
            rueckwaerts_btn.TabIndex = 7;
            rueckwaerts_btn.Text = "◀ Rückwärts";
            rueckwaerts_btn.UseVisualStyleBackColor = false;
            // 
            // reparatur_anf_btn
            // 
            reparatur_anf_btn.BackColor = Color.Bisque;
            reparatur_anf_btn.FlatStyle = FlatStyle.Flat;
            reparatur_anf_btn.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point);
            reparatur_anf_btn.Location = new Point(431, 187);
            reparatur_anf_btn.Margin = new Padding(3, 4, 3, 4);
            reparatur_anf_btn.Name = "reparatur_anf_btn";
            reparatur_anf_btn.Size = new Size(169, 77);
            reparatur_anf_btn.TabIndex = 8;
            reparatur_anf_btn.Text = "Reparatur-Anf";
            reparatur_anf_btn.UseVisualStyleBackColor = false;
            reparatur_anf_btn.Click += reparatur_anf_btn_Click;
            // 
            // kapazitaet_anf_btn
            // 
            kapazitaet_anf_btn.BackColor = Color.Bisque;
            kapazitaet_anf_btn.FlatStyle = FlatStyle.Flat;
            kapazitaet_anf_btn.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point);
            kapazitaet_anf_btn.Location = new Point(608, 185);
            kapazitaet_anf_btn.Margin = new Padding(3, 4, 3, 4);
            kapazitaet_anf_btn.Name = "kapazitaet_anf_btn";
            kapazitaet_anf_btn.Size = new Size(185, 79);
            kapazitaet_anf_btn.TabIndex = 9;
            kapazitaet_anf_btn.Text = "Kapazität-Anf";
            kapazitaet_anf_btn.UseVisualStyleBackColor = false;
            kapazitaet_anf_btn.Click += kapazitaet_anf_btn_Click;
            // 
            // reparatur_abfahrt_btn
            // 
            reparatur_abfahrt_btn.BackColor = Color.Bisque;
            reparatur_abfahrt_btn.FlatStyle = FlatStyle.Flat;
            reparatur_abfahrt_btn.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point);
            reparatur_abfahrt_btn.Location = new Point(821, 185);
            reparatur_abfahrt_btn.Margin = new Padding(3, 4, 3, 4);
            reparatur_abfahrt_btn.Name = "reparatur_abfahrt_btn";
            reparatur_abfahrt_btn.Size = new Size(182, 79);
            reparatur_abfahrt_btn.TabIndex = 10;
            reparatur_abfahrt_btn.Text = "Reparatur-Abfahrt";
            reparatur_abfahrt_btn.UseVisualStyleBackColor = false;
            reparatur_abfahrt_btn.Click += reparatur_abfahrt_btn_Click;
            // 
            // kapazitaet_abfahrt_btn
            // 
            kapazitaet_abfahrt_btn.BackColor = Color.Bisque;
            kapazitaet_abfahrt_btn.FlatStyle = FlatStyle.Flat;
            kapazitaet_abfahrt_btn.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point);
            kapazitaet_abfahrt_btn.Location = new Point(1013, 187);
            kapazitaet_abfahrt_btn.Margin = new Padding(3, 4, 3, 4);
            kapazitaet_abfahrt_btn.Name = "kapazitaet_abfahrt_btn";
            kapazitaet_abfahrt_btn.Size = new Size(167, 77);
            kapazitaet_abfahrt_btn.TabIndex = 11;
            kapazitaet_abfahrt_btn.Text = "Kapazität-Abf";
            kapazitaet_abfahrt_btn.UseVisualStyleBackColor = false;
            kapazitaet_abfahrt_btn.Click += kapazitaet_abfahrt_btn_Click;
            // 
            // drucken_btn
            // 
            drucken_btn.BackColor = SystemColors.Window;
            drucken_btn.FlatStyle = FlatStyle.Flat;
            drucken_btn.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point);
            drucken_btn.Location = new Point(1064, 920);
            drucken_btn.Margin = new Padding(3, 4, 3, 4);
            drucken_btn.Name = "drucken_btn";
            drucken_btn.Size = new Size(127, 61);
            drucken_btn.TabIndex = 12;
            drucken_btn.Text = "Drucken";
            drucken_btn.UseVisualStyleBackColor = false;
            // 
            // dataGridView1
            // 
            dataGridView1.Anchor = AnchorStyles.None;
            dataGridView1.BackgroundColor = SystemColors.Window;
            dataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView1.GridColor = Color.SlateGray;
            dataGridView1.Location = new Point(475, 781);
            dataGridView1.Margin = new Padding(3, 4, 3, 4);
            dataGridView1.Name = "dataGridView1";
            dataGridView1.RowHeadersWidth = 51;
            dataGridView1.RowTemplate.Height = 25;
            dataGridView1.Size = new Size(775, 91);
            dataGridView1.TabIndex = 13;
            dataGridView1.Visible = false;
            //dataGridView1.CellContentClick += dataGridView1_CellContentClick;
            // 
            // save_btn
            // 
            save_btn.BackColor = SystemColors.Window;
            save_btn.FlatStyle = FlatStyle.Flat;
            save_btn.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point);
            save_btn.Location = new Point(891, 921);
            save_btn.Margin = new Padding(3, 4, 3, 4);
            save_btn.Name = "save_btn";
            save_btn.Size = new Size(127, 61);
            save_btn.TabIndex = 14;
            save_btn.Text = "Save";
            save_btn.UseVisualStyleBackColor = false;
            save_btn.Click += save_btn_Click;
            // 
            // AnwenderForm_2
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = SystemColors.GradientInactiveCaption;
            ClientSize = new Size(1435, 997);
            Controls.Add(save_btn);
            Controls.Add(dataGridView1);
            Controls.Add(drucken_btn);
            Controls.Add(kapazitaet_abfahrt_btn);
            Controls.Add(reparatur_abfahrt_btn);
            Controls.Add(kapazitaet_anf_btn);
            Controls.Add(reparatur_anf_btn);
            Controls.Add(rueckwaerts_btn);
            Controls.Add(uebergabe_btn);
            Controls.Add(Abfahrt_btn);
            Controls.Add(Anfahrt_btn);
            Controls.Add(anwender_btn);
            Controls.Add(label2);
            Controls.Add(textBox1);
            Controls.Add(label1);
            Margin = new Padding(3, 4, 3, 4);
            Name = "AnwenderForm_2";
            Text = "AnwenderForm_2";
            ((System.ComponentModel.ISupportInitialize)dataGridView1).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label1;
        private TextBox textBox1;
        private Label label2;
        private Button anwender_btn;
        private Button Anfahrt_btn;
        private Button Abfahrt_btn;
        private Button uebergabe_btn;
        private Button rueckwaerts_btn;
        private Button reparatur_anf_btn;
        private Button kapazitaet_anf_btn;
        private Button reparatur_abfahrt_btn;
        private Button kapazitaet_abfahrt_btn;
        private Button drucken_btn;
        private DataGridView dataGridView1;
        private Button save_btn;
    }
}