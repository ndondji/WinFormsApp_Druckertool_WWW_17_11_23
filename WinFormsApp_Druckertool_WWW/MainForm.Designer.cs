namespace WinFormsApp_Druckertool_WWW
{
    partial class mainform
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
            components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(mainform));
            reg_std_btn = new Button();
            anwender_btn = new Button();
            pictureBox1 = new PictureBox();
            fileSystemWatcher1 = new FileSystemWatcher();
            imageList1 = new ImageList(components);
            button1 = new Button();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)fileSystemWatcher1).BeginInit();
            SuspendLayout();
            // 
            // reg_std_btn
            // 
            reg_std_btn.Anchor = AnchorStyles.None;
            reg_std_btn.BackColor = SystemColors.Window;
            reg_std_btn.FlatStyle = FlatStyle.Popup;
            reg_std_btn.Font = new Font("Segoe UI Semibold", 14.25F, FontStyle.Bold, GraphicsUnit.Point);
            reg_std_btn.Location = new Point(190, 86);
            reg_std_btn.Margin = new Padding(50);
            reg_std_btn.Name = "reg_std_btn";
            reg_std_btn.Size = new Size(182, 61);
            reg_std_btn.TabIndex = 0;
            reg_std_btn.Text = "Administrator ➞";
            reg_std_btn.UseVisualStyleBackColor = false;
            reg_std_btn.Click += reg_std_btn_Click;
            // 
            // anwender_btn
            // 
            anwender_btn.Anchor = AnchorStyles.None;
            anwender_btn.BackColor = SystemColors.Window;
            anwender_btn.FlatAppearance.BorderColor = Color.FromArgb(0, 0, 0, 2);
            anwender_btn.FlatStyle = FlatStyle.Popup;
            anwender_btn.Font = new Font("Segoe UI Semibold", 14.25F, FontStyle.Bold, GraphicsUnit.Point);
            anwender_btn.Location = new Point(190, 169);
            anwender_btn.Name = "anwender_btn";
            anwender_btn.Size = new Size(182, 61);
            anwender_btn.TabIndex = 1;
            anwender_btn.Text = "Anwender ➞";
            anwender_btn.UseVisualStyleBackColor = false;
            anwender_btn.Click += anwender_btn_Click;
            // 
            // pictureBox1
            // 
            pictureBox1.Anchor = AnchorStyles.None;
            pictureBox1.BackColor = Color.SteelBlue;
            pictureBox1.Image = (Image)resources.GetObject("pictureBox1.Image");
            pictureBox1.Location = new Point(446, -3);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(92, 56);
            pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBox1.TabIndex = 2;
            pictureBox1.TabStop = false;
            // 
            // fileSystemWatcher1
            // 
            fileSystemWatcher1.EnableRaisingEvents = true;
            fileSystemWatcher1.SynchronizingObject = this;
            // 
            // imageList1
            // 
            imageList1.ColorDepth = ColorDepth.Depth8Bit;
            imageList1.ImageSize = new Size(16, 16);
            imageList1.TransparentColor = Color.Transparent;
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
            button1.Location = new Point(-2, -3);
            button1.Name = "button1";
            button1.Size = new Size(555, 56);
            button1.TabIndex = 4;
            button1.Text = "WWW - Druckertool";
            button1.TextAlign = ContentAlignment.MiddleLeft;
            button1.UseVisualStyleBackColor = false;
            // 
            // mainform
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = SystemColors.GradientInactiveCaption;
            ClientSize = new Size(550, 286);
            Controls.Add(pictureBox1);
            Controls.Add(button1);
            Controls.Add(anwender_btn);
            Controls.Add(reg_std_btn);
            Name = "mainform";
            Text = "MainForm";
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            ((System.ComponentModel.ISupportInitialize)fileSystemWatcher1).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private Button reg_std_btn;
        private Button anwender_btn;
        private PictureBox pictureBox1;
        private FileSystemWatcher fileSystemWatcher1;
        private ImageList imageList1;
        private Button button1;
    }
}