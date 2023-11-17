namespace WinFormsApp_folderList
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
            display_btn = new Button();
            dataGridView1 = new DataGridView();
            print_btn = new Button();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).BeginInit();
            SuspendLayout();
            // 
            // display_btn
            // 
            display_btn.Location = new Point(216, 27);
            display_btn.Name = "display_btn";
            display_btn.Size = new Size(75, 23);
            display_btn.TabIndex = 0;
            display_btn.Text = "Display";
            display_btn.UseVisualStyleBackColor = true;
            display_btn.Click += display_btn_Click;
            // 
            // dataGridView1
            // 
            dataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView1.Location = new Point(130, 76);
            dataGridView1.Name = "dataGridView1";
            dataGridView1.RowTemplate.Height = 25;
            dataGridView1.Size = new Size(518, 341);
            dataGridView1.TabIndex = 1;
            dataGridView1.CellContentClick += dataGridView1_CellContentClick;
            // 
            // print_btn
            // 
            print_btn.Location = new Point(391, 27);
            print_btn.Name = "print_btn";
            print_btn.Size = new Size(75, 23);
            print_btn.TabIndex = 2;
            print_btn.Text = "Print";
            print_btn.UseVisualStyleBackColor = true;
            print_btn.Click += print_btn_Click;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(print_btn);
            Controls.Add(dataGridView1);
            Controls.Add(display_btn);
            Name = "Form1";
            Text = "CopyFolderExcFiles";
            ((System.ComponentModel.ISupportInitialize)dataGridView1).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private Button display_btn;
        private DataGridView dataGridView1;
        private Button print_btn;
    }
}