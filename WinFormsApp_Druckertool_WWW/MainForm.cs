using System;
using System.Configuration;
using System.Windows.Forms;
using Oracle.ManagedDataAccess.Client;

namespace WinFormsApp_Druckertool_WWW
{
    public partial class mainform : Form
    {
        private string connectionString = ConfigurationManager.ConnectionStrings["OracleConnection"].ConnectionString; // Replace with your actual Oracle connection string

        public mainform()
        {
            InitializeComponent();
        }

        private bool IsUsernameInTable(string username)
        {
            using (OracleConnection connection = new OracleConnection(connectionString))
            {
                connection.Open();
                using (OracleCommand command = new OracleCommand("SELECT COUNT(*) FROM ZUORDNUNG_GENEHMIGUNG WHERE USERNAME = :username", connection))
                {
                    command.Parameters.Add(new OracleParameter("username", username));
                    int count = Convert.ToInt32(command.ExecuteScalar());
                    return count > 0;
                }
            }
        }

        private void reg_std_btn_Click(object sender, EventArgs e)
        {
            // Check if the current user (Environment.UserName) is in the Oracle table
            string currentUsername = Environment.UserName;
            if (IsUsernameInTable(currentUsername))
            {
                // User exists in the table, open the RegistrationForm(administratorForm)
                administratorForm registrationForm = new administratorForm();
                registrationForm.ShowDialog();
            }
            else
            {
                // User does not exist, display an error message
                MessageBox.Show("Zugriff abgelehnt. Ungültiger Benutzername.", "Error");
            }
        }

        private void anwender_btn_Click(object sender, EventArgs e)
        {
            // For "anwenderForm"
            anwenderForm anwenderForm = new anwenderForm();
            anwenderForm.ShowDialog();
        }
    }
}




//=========================User must enter password in Table====================================
//using System;
//using System.Configuration;
//using System.Windows.Forms;
//using Oracle.ManagedDataAccess.Client;

//namespace WinFormsApp_Druckertool_WWW
//{
//    public partial class mainform : Form
//    {
//        private string connectionString = ConfigurationManager.ConnectionStrings["OracleConnection"].ConnectionString; // Replace with your actual Oracle connection string

//        public mainform()
//        {
//            InitializeComponent();
//        }

//        private bool IsUsernameInTable(string username)
//        {
//            using (OracleConnection connection = new OracleConnection(connectionString))
//            {
//                connection.Open();
//                using (OracleCommand command = new OracleCommand("SELECT COUNT(*) FROM ZUORDNUNG_GENEHMIGUNG WHERE USERNAME = :username", connection))
//                {
//                    command.Parameters.Add(new OracleParameter("username", username));
//                    int count = Convert.ToInt32(command.ExecuteScalar());
//                    return count > 0;
//                }
//            }
//        }

//        private void reg_std_btn_Click(object sender, EventArgs e)
//        {
//            // Prompt the user to enter their username
//            string username = PromptForUsername();

//            // Check if the entered username is in the Oracle table
//            if (IsUsernameInTable(username))
//            {
//                // Username exists in the table, open the registrationForm
//                administratorForm registrationForm = new administratorForm();
//                registrationForm.ShowDialog();
//            }
//            else
//            {
//                // Username does not exist, display an error message
//                MessageBox.Show("Zugriff abgelehnt. Ungültiger Benutzername.", "Error");
//            }
//        }

//        private void anwender_btn_Click(object sender, EventArgs e)
//        {
//            // For "anwenderForm"
//            anwenderForm anwenderForm = new anwenderForm();
//            anwenderForm.ShowDialog();
//        }

//        // Prompt the user to enter their username (with simple appearance)
//        //private string PromptForUsername()
//        //{
//        //    string username = "";
//        //    using (var inputForm = new Form())
//        //    using (var usernameTextBox = new TextBox())
//        //    using (var okButton = new Button())
//        //    {
//        //        inputForm.Text = "Enter Username";
//        //        usernameTextBox.Dock = DockStyle.Top;
//        //        okButton.Dock = DockStyle.Bottom;
//        //        okButton.Text = "OK";
//        //        okButton.DialogResult = DialogResult.OK;
//        //        inputForm.Controls.Add(usernameTextBox);
//        //        inputForm.Controls.Add(okButton);

//        //        if (inputForm.ShowDialog() == DialogResult.OK)
//        //        {
//        //            username = usernameTextBox.Text;
//        //        }
//        //    }
//        //    return username;
//        //}

//        //With Bling bling appearance!
//        private string PromptForUsername()
//        {
//            string username = "";
//            using (var inputForm = new Form())
//            using (var usernameTextBox = new TextBox())
//            using (var okButton = new Button())
//            {
//                // Set the form title font
//                Label titleLabel = new Label();
//                titleLabel.Text = "Benutzernamen"; // Set the form title
//                titleLabel.Font = new Font("Arial", 14, FontStyle.Bold); // Set form font and size
//                titleLabel.Dock = DockStyle.Top;
//                titleLabel.TextAlign = ContentAlignment.MiddleCenter;
//                titleLabel.Padding = new Padding(10, 0, 0, 0); // Reduce space below the title
//                inputForm.Controls.Add(titleLabel);

//                // Set the text box style
//                usernameTextBox.Dock = DockStyle.Top;
//                usernameTextBox.Font = new Font("Arial", 14, FontStyle.Bold); // Set text box font and size
//                usernameTextBox.BackColor = Color.White; // Set text box background color

//                // Set the button style
//                okButton.Dock = DockStyle.Bottom;
//                okButton.Text = "OK";
//                okButton.Font = new Font("Arial", 14, FontStyle.Bold); // Set button font and size
//                okButton.ForeColor = Color.DarkGreen; // Set button text color
//                okButton.BackColor = Color.SkyBlue; // Set button background color
//                okButton.DialogResult = DialogResult.OK; // Set button dialog result

//                // Increase the size of the button
//                okButton.Size = new Size(200, 40); // Set the width and height

//                // Handle the Enter key press in the usernameTextBox
//                usernameTextBox.KeyDown += (sender, e) =>
//                {
//                    if (e.KeyCode == Keys.Enter)
//                    {
//                        okButton.PerformClick(); // Trigger the OK button click event
//                    }
//                };

//                inputForm.Controls.Add(usernameTextBox);
//                inputForm.Controls.Add(okButton);

//                if (inputForm.ShowDialog() == DialogResult.OK)
//                {
//                    username = usernameTextBox.Text;
//                }
//            }
//            return username;
//        }



//    }
//}




//=================Asks for UserName (works very well!)
//using System;
//using System.Windows.Forms;

//namespace WinFormsApp_Druckertool_WWW
//{
//    public partial class mainform : Form
//    {
//        public mainform()
//        {
//            InitializeComponent();

//            // Get the current user's name and display it
//            string username = Environment.UserName;
//            MessageBox.Show($"Aktueller Benutzer: {username}", "User Name");
//        }

//        private void reg_std_btn_Click(object sender, EventArgs e)
//        {
//            // Without customized Login requirement
//            administratorForm registrationForm = new administratorForm();
//            registrationForm.ShowDialog();
//        }

//        // For "anwenderForm"
//        private void anwender_btn_Click(object sender, EventArgs e)
//        {
//            anwenderForm anwenderForm = new anwenderForm();
//            anwenderForm.ShowDialog();
//        }
//    }
//}
//=========================================================================

////This works without Windows Authentication! (works fine)
//namespace WinFormsApp_Druckertool_WWW
//{
//    public partial class mainform : System.Windows.Forms.Form
//    {
//        public mainform()
//        {
//            InitializeComponent();
//        }

//        private void reg_std_btn_Click(object sender, EventArgs e)
//        {
//            //Without customized Login requirement
//            administratorForm registrationForm = new administratorForm();
//            registrationForm.ShowDialog();

//            ////With customized Ligin requirements
//            //Login loginForm = new Login();
//            //if (loginForm.ShowDialog() == DialogResult.OK)
//            //{
//            //    // Login was successful, open the registrationForm
//            //    administratorForm registrationForm = new administratorForm();
//            //    registrationForm.ShowDialog();
//            //}
//        }
//        //For "anwenderForm"
//        private void anwender_btn_Click(object sender, EventArgs e)
//        {
//            anwenderForm anwenderForm = new anwenderForm();
//            anwenderForm.ShowDialog();
//        }

//        //For "AnwenderForm_2"
//        //private void anwender_btn_Click(object sender, EventArgs e)
//        //{
//        //    AnwenderForm_2 anwenderForm_2 = new AnwenderForm_2();
//        //    anwenderForm_2.ShowDialog();
//        //}
//    }
//}

//==========================================================================

//This try to use with the authentification, but still not working
//using System.Security.Principal;
//using System.Windows.Forms;

//namespace WinFormsApp_Druckertool_WWW
//{
//    public partial class mainform : Form
//    {
//        public mainform()
//        {
//            InitializeComponent();
//        }

//        private void reg_std_btn_Click(object sender, EventArgs e)
//        {
//            try
//            {
//                // Trigger Windows authentication before proceeding
//                if (TriggerWindowsAuthentication())
//                {
//                    // Windows authentication successful; continue with the logic
//                    administratorForm registrationForm = new administratorForm();
//                    registrationForm.ShowDialog();
//                }
//                else
//                {
//                    // Windows authentication failed
//                    MessageBox.Show("Windows authentication failed.");
//                }
//            }
//            catch (Exception ex)
//            {
//                MessageBox.Show($"Error: {ex.Message}");
//            }


//            //// Trigger Windows authentication before proceeding
//            //if (TriggerWindowsAuthentication())
//            //{
//            //    // Windows authentication successful; continue with the logic
//            //    administratorForm registrationForm = new administratorForm();
//            //    registrationForm.ShowDialog();
//            //}
//            //else
//            //{
//            //    // Windows authentication failed
//            //    MessageBox.Show("Windows authentication failed.");
//            //}
//        }

//        private void anwender_btn_Click(object sender, EventArgs e)
//        {
//            // Trigger Windows authentication before proceeding
//            if (TriggerWindowsAuthentication())
//            {
//                // Windows authentication successful; continue with the logic
//                anwenderForm anwenderForm = new anwenderForm();
//                anwenderForm.ShowDialog();
//            }
//            else
//            {
//                // Windows authentication failed
//                MessageBox.Show("Windows authentication failed.");
//            }
//        }

//        private bool TriggerWindowsAuthentication()
//        {
//            WindowsPrincipal principal = new WindowsPrincipal(WindowsIdentity.GetCurrent());

//            // Check if the user is authenticated with Windows credentials
//            return principal.Identity.IsAuthenticated;
//        }
//    }
//}
//=======================================================================


//Not working
//using System.Security.Principal;
//using System.IdentityModel;
////using System.IdentityModel.Services;

//namespace WinFormsApp_Druckertool_WWW
//{
//    public partial class mainform : System.Windows.Forms.Form
//    {
//        public mainform()
//        {
//            InitializeComponent();
//        }

//        private void reg_std_btn_Click(object sender, EventArgs e)
//        {
//            // Without customized Login requirement
//            administratorForm registrationForm = new administratorForm();
//            registrationForm.ShowDialog();
//        }

//        private void anwender_btn_Click(object sender, EventArgs e)
//        {
//            anwenderForm anwenderForm = new anwenderForm();
//            anwenderForm.ShowDialog();
//        }

//        private void windowsLogin_btn_Click(object sender, EventArgs e)
//        {
//            WindowsIdentity identity = WindowsIdentity.GetCurrent();

//            if (identity != null && identity.IsAuthenticated)
//            {
//                // User is authenticated with Windows credentials
//                // You can perform further actions here or redirect to another form.
//            }
//            else
//            {
//                // User is not authenticated or Windows credentials are not available
//                MessageBox.Show("Windows authentication failed.");
//            }
//        }
//    }
//}


//        ////with message
//        //private void Anwender_Load(object sender, EventArgs e)
//        //{
//        //    // Display a message to the user
//        //    DialogResult result = MessageBox.Show("HHHHHHHH", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Information);

//        //    // Check if the user clicked 'Yes' to proceed
//        //    if (result == DialogResult.Yes)
//        //    {
//        //        // Continue with the form's initialization
//        //        // You can place your initialization code here if needed
//        //        anwenderForm anwenderForm = new anwenderForm();
//        //        anwenderForm.ShowDialog();
//        //    }
//        //    else
//        //    {
//        //        // Close the form or take appropriate action if the user clicked 'No'
//        //        this.Close(); // Closes the form
//        //    }
//        //}

//    }
//}