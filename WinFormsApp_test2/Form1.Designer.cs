namespace WinFormsApp_test2
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
            btn_Anfahrt = new Button();
            btn_Abfahrt = new Button();
            btn_Reparatur = new Button();
            btn_Drucken = new Button();
            dataGridView1 = new DataGridView();
            label1 = new Label();
            textBox_Anwender = new TextBox();
            button1 = new Button();
            button2 = new Button();
            button3 = new Button();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).BeginInit();
            SuspendLayout();
            // 
            // btn_Anfahrt
            // 
            btn_Anfahrt.Location = new Point(38, 50);
            btn_Anfahrt.Name = "btn_Anfahrt";
            btn_Anfahrt.Size = new Size(75, 23);
            btn_Anfahrt.TabIndex = 0;
            btn_Anfahrt.Text = "Anfahrt";
            btn_Anfahrt.UseVisualStyleBackColor = true;
            btn_Anfahrt.Click += btn_Anfahrt_Click;
            // 
            // btn_Abfahrt
            // 
            btn_Abfahrt.Location = new Point(169, 50);
            btn_Abfahrt.Name = "btn_Abfahrt";
            btn_Abfahrt.Size = new Size(75, 23);
            btn_Abfahrt.TabIndex = 1;
            btn_Abfahrt.Text = "Abfahrt";
            btn_Abfahrt.UseVisualStyleBackColor = true;
            // 
            // btn_Reparatur
            // 
            btn_Reparatur.Location = new Point(303, 50);
            btn_Reparatur.Name = "btn_Reparatur";
            btn_Reparatur.Size = new Size(75, 23);
            btn_Reparatur.TabIndex = 2;
            btn_Reparatur.Text = "Reparatur";
            btn_Reparatur.UseVisualStyleBackColor = true;
            // 
            // btn_Drucken
            // 
            btn_Drucken.Location = new Point(157, 168);
            btn_Drucken.Name = "btn_Drucken";
            btn_Drucken.Size = new Size(75, 23);
            btn_Drucken.TabIndex = 3;
            btn_Drucken.Text = "Drucken";
            btn_Drucken.UseVisualStyleBackColor = true;
            // 
            // dataGridView1
            // 
            dataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView1.Location = new Point(12, 206);
            dataGridView1.Name = "dataGridView1";
            dataGridView1.RowTemplate.Height = 25;
            dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridView1.Size = new Size(776, 357);
            dataGridView1.TabIndex = 4;
            dataGridView1.CellContentClick += dataGridView1_CellContentClick;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(39, 138);
            label1.Name = "label1";
            label1.Size = new Size(97, 15);
            label1.TabIndex = 5;
            label1.Text = "Number of Files: ";
            // 
            // textBox_Anwender
            // 
            textBox_Anwender.Location = new Point(136, 132);
            textBox_Anwender.Name = "textBox_Anwender";
            textBox_Anwender.Size = new Size(100, 23);
            textBox_Anwender.TabIndex = 6;
            textBox_Anwender.TextChanged += textBox_Anwender_TextChanged;
            // 
            // button1
            // 
            button1.Location = new Point(242, 86);
            button1.Name = "button1";
            button1.Size = new Size(75, 23);
            button1.TabIndex = 8;
            button1.Text = "Reparatur";
            button1.UseVisualStyleBackColor = true;
            // 
            // button2
            // 
            button2.Location = new Point(108, 86);
            button2.Name = "button2";
            button2.Size = new Size(75, 23);
            button2.TabIndex = 7;
            button2.Text = "Abfahrt";
            button2.UseVisualStyleBackColor = true;
            // 
            // button3
            // 
            button3.Location = new Point(363, 170);
            button3.Name = "button3";
            button3.Size = new Size(75, 23);
            button3.TabIndex = 9;
            button3.Text = "Drucken";
            button3.UseVisualStyleBackColor = true;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 586);
            Controls.Add(button3);
            Controls.Add(button1);
            Controls.Add(button2);
            Controls.Add(textBox_Anwender);
            Controls.Add(label1);
            Controls.Add(dataGridView1);
            Controls.Add(btn_Drucken);
            Controls.Add(btn_Reparatur);
            Controls.Add(btn_Abfahrt);
            Controls.Add(btn_Anfahrt);
            Name = "Form1";
            Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)dataGridView1).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button btn_Anfahrt;
        private Button btn_Abfahrt;
        private Button btn_Reparatur;
        private Button btn_Drucken;
        private DataGridView dataGridView1;
        private Label label1;
        private TextBox textBox_Anwender;
        private Button button1;
        private Button button2;
        private Button button3;
    }
}