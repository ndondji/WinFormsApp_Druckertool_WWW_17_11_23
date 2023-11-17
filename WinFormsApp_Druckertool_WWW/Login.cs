using Oracle.ManagedDataAccess.Client;

namespace WinFormsApp_Druckertool_WWW
{
    public partial class Login : Form
    {
        public Login()
        {
            InitializeComponent();
        }


        private void button_login_Click(object sender, EventArgs e)
        {
            string username = text_usename.Text;
            string enteredPassword = text_password.Text;

            // Retrieve the connection string from App.config
            string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["OracleConnection"].ConnectionString;

            using (OracleConnection connection = new OracleConnection(connectionString))
            {
                connection.Open();

                // Prepare a SQL query to retrieve the hashed password for the entered username
                string query = "SELECT PASSWORDHASH FROM USERCREDENTIALS WHERE USERNAME = :username";

                using (OracleCommand command = new OracleCommand(query, connection))
                {
                    command.Parameters.Add(new OracleParameter("username", OracleDbType.Varchar2) { Value = username });

                    using (OracleDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            string storedPasswordHash = reader.GetString(0);

                            // Verify the entered password against the stored hash
                            if (VerifyPassword(enteredPassword, storedPasswordHash))
                            {
                                // Password is correct, set the DialogResult to OK
                                this.DialogResult = DialogResult.OK;
                                this.Close();
                            }
                            else
                            {
                                // Password is incorrect, show an error message or handle it as you prefer
                                MessageBox.Show("Falsches Passwort. Bitte versuche es erneut.");
                                text_password.Clear();
                            }
                        }
                        else
                        {
                            // Username not found, show an error message or handle it as you prefer
                            MessageBox.Show("Benutzername nicht gefunden.");
                        }
                    }
                }
            }

        }

        // Function to verify the entered password against the stored hash (implement your hashing logic here)
        private bool VerifyPassword(string enteredPassword, string storedPasswordHash)
        {
            // Implement your password hashing and verification logic here
            // Compare the enteredPassword with the storedPasswordHash

            // Example (for demonstration purposes only, not secure):
            return enteredPassword == storedPasswordHash;
        }

        private void text_usename_TextChanged(object sender, EventArgs e)
        {
            //There is nothing here.
        }

        private void text_password_TextChanged(object sender, EventArgs e)
        {
            //There is nothing here.
        }

        private void button_clear_Click(object sender, EventArgs e)
        {
            text_usename.Clear();
            text_password.Clear();

            text_usename.Focus();
        }

        private void button_exit_Click(object sender, EventArgs e)
        {
            DialogResult res;
            res = MessageBox.Show("Möchtest Du aussteigen?", "Aussteigen", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (res == DialogResult.Yes)
            {
                Application.Exit();
            }
            else
            {
                this.Show();
            }
        }
    }
}
