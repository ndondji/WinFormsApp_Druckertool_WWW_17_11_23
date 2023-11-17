namespace WinFormsApp_Druckertool_WWW
{
    partial class Login
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
            label1 = new Label();
            label2 = new Label();
            label3 = new Label();
            text_usename = new TextBox();
            text_password = new TextBox();
            button_login = new Button();
            button_clear = new Button();
            button_exit = new Button();
            button1 = new Button();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Arial Rounded MT Bold", 15.75F, FontStyle.Regular, GraphicsUnit.Point);
            label1.Location = new Point(119, 87);
            label1.Name = "label1";
            label1.Size = new Size(412, 24);
            label1.TabIndex = 0;
            label1.Text = "Willkommen bei der WWW-Anwendung!";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point);
            label2.Location = new Point(103, 166);
            label2.Name = "label2";
            label2.Size = new Size(109, 21);
            label2.TabIndex = 1;
            label2.Text = "Nutzername:";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point);
            label3.Location = new Point(128, 218);
            label3.Name = "label3";
            label3.Size = new Size(82, 21);
            label3.TabIndex = 2;
            label3.Text = "Passwort:";
            // 
            // text_usename
            // 
            text_usename.Font = new Font("Segoe UI", 11F, FontStyle.Regular, GraphicsUnit.Point);
            text_usename.Location = new Point(229, 167);
            text_usename.Name = "text_usename";
            text_usename.Size = new Size(244, 27);
            text_usename.TabIndex = 3;
            text_usename.TextChanged += text_usename_TextChanged;
            // 
            // text_password
            // 
            text_password.Font = new Font("Segoe UI", 11F, FontStyle.Regular, GraphicsUnit.Point);
            text_password.Location = new Point(229, 219);
            text_password.Name = "text_password";
            text_password.PasswordChar = '*';
            text_password.Size = new Size(244, 27);
            text_password.TabIndex = 4;
            text_password.TextChanged += text_password_TextChanged;
            // 
            // button_login
            // 
            button_login.BackColor = SystemColors.Window;
            button_login.FlatAppearance.MouseOverBackColor = Color.MediumSeaGreen;
            button_login.FlatStyle = FlatStyle.Flat;
            button_login.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point);
            button_login.Location = new Point(361, 302);
            button_login.Name = "button_login";
            button_login.Size = new Size(112, 33);
            button_login.TabIndex = 5;
            button_login.Text = "Anmeldung";
            button_login.UseVisualStyleBackColor = false;
            button_login.Click += button_login_Click;
            // 
            // button_clear
            // 
            button_clear.BackColor = SystemColors.Window;
            button_clear.FlatAppearance.MouseOverBackColor = Color.MediumSeaGreen;
            button_clear.FlatStyle = FlatStyle.Flat;
            button_clear.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point);
            button_clear.Location = new Point(231, 302);
            button_clear.Name = "button_clear";
            button_clear.Size = new Size(80, 33);
            button_clear.TabIndex = 6;
            button_clear.Text = "Leeren";
            button_clear.UseVisualStyleBackColor = false;
            button_clear.Click += button_clear_Click;
            // 
            // button_exit
            // 
            button_exit.BackColor = SystemColors.Window;
            button_exit.FlatAppearance.MouseOverBackColor = Color.MediumSeaGreen;
            button_exit.FlatStyle = FlatStyle.Flat;
            button_exit.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point);
            button_exit.Location = new Point(128, 348);
            button_exit.Name = "button_exit";
            button_exit.Size = new Size(80, 33);
            button_exit.TabIndex = 7;
            button_exit.Text = "Beeden";
            button_exit.UseVisualStyleBackColor = false;
            button_exit.Click += button_exit_Click;
            // 
            // button1
            // 
            button1.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            button1.BackColor = Color.SteelBlue;
            button1.BackgroundImageLayout = ImageLayout.Stretch;
            button1.FlatAppearance.BorderColor = Color.SteelBlue;
            button1.FlatAppearance.BorderSize = 6;
            button1.FlatAppearance.MouseDownBackColor = Color.SteelBlue;
            button1.FlatAppearance.MouseOverBackColor = Color.SteelBlue;
            button1.FlatStyle = FlatStyle.Flat;
            button1.Location = new Point(-2, 12);
            button1.Name = "button1";
            button1.Size = new Size(620, 50);
            button1.TabIndex = 8;
            button1.UseVisualStyleBackColor = false;
            // 
            // Login
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = SystemColors.GradientInactiveCaption;
            ClientSize = new Size(618, 445);
            Controls.Add(button1);
            Controls.Add(button_exit);
            Controls.Add(button_clear);
            Controls.Add(button_login);
            Controls.Add(text_password);
            Controls.Add(text_usename);
            Controls.Add(label3);
            Controls.Add(label2);
            Controls.Add(label1);
            Name = "Login";
            Text = "Login";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label1;
        private Label label2;
        private Label label3;
        private TextBox text_usename;
        private TextBox text_password;
        private Button button_login;
        private Button button_clear;
        private Button button_exit;
        private Button button1;
    }
}