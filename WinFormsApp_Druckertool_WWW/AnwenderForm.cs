
using Oracle.ManagedDataAccess.Client;
using System.Collections.ObjectModel;
using System.Configuration;
using System.Text.RegularExpressions;
using ClosedXML.Excel;
using System.Diagnostics;
//using OfficeOpenXml.Style;
//using Microsoft.Office.Interop.Excel;


namespace WinFormsApp_Druckertool_WWW
{
    public partial class anwenderForm : System.Windows.Forms.Form
    {
        private ObservableCollection<ExcelFile> excelFiles = new ObservableCollection<ExcelFile>();
        private Label labelDateinamen;
        

        public object ExcelWriterFactory { get; private set; }

        // Declare printedFiles at the class level.=============11.11.2023(PDF Print)
        private HashSet<string> printedFiles = new HashSet<string>();

        // Define a method to show or hide the DataGridView ============================>24-10-23
        private void ShowDataGridView(bool show)
        {
            dataGridView1.Visible = show;
        }

        // Add a stack to store the application states ===================================>23-10-23
        //private Stack<ApplicationState> stateStack = new Stack<ApplicationState>();

        private WorkflowStep previousStep; // Add a variable to track the previous step ========>24-10-23

        public anwenderForm()
        {
            InitializeComponent();
            // Initialize the previous step to "Initial" since there's no previous step initially =====24-10-23
            previousStep = WorkflowStep.Initial;

            // Initially, disable Anfahrt_btn, Abfahrt_btn, and Uebergabe_btn
            Anfahrt_btn.Enabled = false;
            Abfahrt_btn.Enabled = false;
            Uebergabe_btn.Enabled = false;

            Reparatur_abfahrt_btn.Enabled = false;
            Kapazitaet_Abfahrt_btn.Enabled = false;
            Rückwärts_btn.Enabled = false;

            // Disable all buttons except Anfahrt_btn, Abfahrt_btn, and Uebergabe_btn
            Reparatur_Anfahrt_btn.Enabled = false;
            Kapazitaet_Anfahrt_btn.Enabled = false;

            //16-10-23 ==============================================================>
            // Create and configure the label
            labelDateinamen = new Label();
            labelDateinamen.Text = "Dateinamen: "; // Initial text
            labelDateinamen.Location = new Point(10, 10); // Adjust the position
            this.Controls.Add(labelDateinamen); // Add the label to the form



            //From Admin Page!!
            // Load checkbox states when the application starts
            LoadCheckboxState();

            // Fetch data from Oracle and populate excelFiles collection
            FetchFilesFromOracle();
            dataGridView1.DataSource = excelFiles;
            //To hidde visibility of some Columns in dataGridView
            dataGridView1.Columns["FilePath"].Visible = false;
            dataGridView1.Columns["IsChecked"].Visible = false;

            dataGridView1.DataSource = excelFiles;

            int desiredWidth = 135; // Adjust the width as needed
            foreach (DataGridViewColumn column in dataGridView1.Columns)
            {
                // Set a common width for all columns
                column.Width = desiredWidth;

                // Adjust column widths and Padding for "Dateinamen"
                dataGridView1.Columns["Dateinamen"].Width = 450; // Adjust the width for ColumnName1
                dataGridView1.Columns["Dateinamen"].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleLeft;

            }

            // Subscribe to the TextChanged event of the Dok_numb_txt TextBox
            Dok_numb_txt.TextChanged += Dok_numb_txt_TextChanged;
            Dok_numb_txt.Text = "Placeholder"; // Set an initial value for design view
            Dok_numb_txt.TextChanged += Dok_numb_txt_TextChanged;

            // Initially, disable all buttons except Anfahrt, Abfahrt, and Uebergabe =======20-10-2023
            DisableAllButtons();

            //// Initialize the stack with the initial state =====================>23---------------->25(brings error. To be checked!!!!!!!!)
            //stateStack.Push(new ApplicationState
            //{
            //    Anfahrt_btnEnabled = Anfahrt_btn.Enabled,
            //    Abfahrt_btnEnabled = Abfahrt_btn.Enabled,
            //    Uebergabe_btnEnabled = Uebergabe_btn.Enabled,
            //    // Add more properties to capture the state of other elements as needed
            //});


            // Initially, configure the DataGridView to display only "Dateinamen" and "Anf_Rep" =========================================24-10-23
            //ShowSelectedColumns("Dateinamen", "Anf_Rep");
            // Initially, configure the DataGridView to be hidden  =========================================24-10-23
            ShowDataGridView(false);

            // Subscribe to the TextChanged event of the SP_K_textBox  =========================================24-10-23
            //SP_K_textBox.TextChanged += SP_K_textBox_TextChanged;
            //SP_K_textBox.Text = "Placeholder"; // Set an initial value for design view
            //SP_K_textBox.TextChanged += SP_K_textBox_TextChanged;
        }

        // Helper method to show/hide specific DataGridView columns =========================================24-10-23
        private void ShowSelectedColumns(params string[] columnNames)
        {
            foreach (DataGridViewColumn column in dataGridView1.Columns)
            {
                // Hide all columns except the specified ones
                column.Visible = columnNames.Contains(column.Name);
            }
        }
        //To disable all buttons 
        private void DisableAllButtons()
        {
            // Disable all buttons except Anfahrt, Abfahrt, and Uebergabe
            Anfahrt_btn.Enabled = false;
            Abfahrt_btn.Enabled = false;
            Uebergabe_btn.Enabled = false;
            Reparatur_Anfahrt_btn.Enabled = false;
            Kapazitaet_Anfahrt_btn.Enabled = false;
            // Add any other buttons you want to disable here
        }

        //To enable specific buttons ======>20-10-2023
        private void EnableSpecificButtons(bool enableAnfahrt, bool enableReparaturAnfahrt, bool enableKapazitaetAnfahrt)
        {
            // Enable or disable buttons as required
            Anfahrt_btn.Enabled = enableAnfahrt;
            Reparatur_Anfahrt_btn.Enabled = enableReparaturAnfahrt;
            Kapazitaet_Anfahrt_btn.Enabled = enableKapazitaetAnfahrt;
            // Add other buttons as needed
        }


        private void save_anw_btn_Click(object sender, EventArgs e)
        {
            //this.Close();
        }



        private enum WorkflowStep
        {
            Initial,
            Anfahrt,
            ReparaturAnfahrt,
            Abfahrt,
            ReparaturAbfahrt,
            // Add more workflow steps as needed
        }

        // Add a variable to track the current step
        private WorkflowStep currentStep = WorkflowStep.Initial;

        // Update the stateStack to store WorkflowStep values
        private Stack<WorkflowStep> stateStack = new Stack<WorkflowStep>();
        private ProcessStartInfo filePath;

        private void Rückwärts_btn_Click(object sender, EventArgs e)
        {
            // Handle the navigation logic
            if (stateStack.Count > 0)
            {
                currentStep = stateStack.Pop(); // Pop the previous state
            }
            else
            {
                // Handle the case when the stack is empty (already at the initial state)
                currentStep = WorkflowStep.Initial;
            }

            switch (currentStep)
            {
                case WorkflowStep.Initial:
                    EnableOrDisableButtons(true, false, false); // Enable Anfahrt, Abfahrt, Uebergabe
                    break;
                case WorkflowStep.Anfahrt:
                    EnableOrDisableButtons(false, false, true); // Enable Abfahrt
                    break;
                case WorkflowStep.ReparaturAnfahrt:
                    EnableOrDisableButtons(false, false, true); // Enable Abfahrt
                                                                // Handle any additional logic for this state
                    break;
                    // Add more cases for other workflow steps as needed
            }

            // Hide the DataGridView when "Rückwärts_btn_Clic" is clicked
            ShowDataGridView(false);

            //Set visibility to false for Buttons when clicking on Rückwärts
            Reparatur_Anfahrt_btn.Visible = false; // Set visibility to false
            Kapazitaet_Anfahrt_btn.Visible = false; // Set visibility to false
            Reparatur_abfahrt_btn.Visible = false; // Set visibility to false
            Kapazitaet_Abfahrt_btn.Visible = false; // Set visibility to false

            //To enable these buttons
            Abfahrt_btn.Enabled = true;
            Uebergabe_btn.Enabled = true;
            Anfahrt_btn.Enabled = true;
        }


        private void Anwender_btn_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        //
        //private void EnableOrDisableButtons(bool v1, bool v2, bool v3)
        //{
        //    throw new NotImplementedException();
        //}
        // Helper method to enable/disable buttons based on the current step
        private void EnableOrDisableButtons(bool enableAnfahrt, bool enableReparaturAnfahrt, bool enableKapazitaetAnfahrt)
        {
            Anfahrt_btn.Enabled = enableAnfahrt;
            Reparatur_Anfahrt_btn.Enabled = enableReparaturAnfahrt;
            Kapazitaet_Anfahrt_btn.Enabled = enableKapazitaetAnfahrt;
            // Add other buttons as needed
        }

        //This works and was established on the 18-10-2023
        //private void SP_K_textBox_TextChanged(object sender, EventArgs e)
        //{
        //    //check if the length of the text is equal to 7. If so, enable
        //    //the "Anfahrt_btn", "Abfahrt_btn", and "Uebergabe_btn" buttons.
        //    string text = SP_K_textBox.Text;

        //    if (Regex.IsMatch(text, @"^[A-Za-z]{2}\d{2}-\d{2}$"))
        //    {
        //        Anfahrt_btn.Enabled = true;
        //        Abfahrt_btn.Enabled = true;
        //        Uebergabe_btn.Enabled = true;
        //    }
        //    else
        //    {
        //        Anfahrt_btn.Enabled = false;
        //        Abfahrt_btn.Enabled = false;
        //        Uebergabe_btn.Enabled = false;
        //    }


        //    //New 16-10-23 ==========================================================================
        //    // Fetch "Dateinamen" from the Oracle table based on the value in SP_K_textBox.Text
        //    string spkValue = SP_K_textBox.Text; // Ensure this contains your valid input
        //    string dateinamenValue = FetchDateinamenFromOracle(spkValue);

        //    // Use the fetched "Dateinamen" value
        //    if (!string.IsNullOrEmpty(dateinamenValue))
        //    {
        //        // Do something with the fetched "Dateinamen," e.g., display it in a label
        //        labelDateinamen.Text = dateinamenValue;
        //    }
        //    else
        //    {
        //        // Handle the case where "Dateinamen" is not found
        //        labelDateinamen.Text = "Dateinamen not found";
        //    }

        //}
        //======================================>20-10-2023


        private void SP_K_textBox_TextChanged(object sender, EventArgs e)
        {
            string text = SP_K_textBox.Text;

            if (Regex.IsMatch(text, @"^[A-Za-z]{2}\d{2}-\d{2}$"))
            {
                // Enable Anfahrt, Abfahrt, and Uebergabe buttons
                Anfahrt_btn.Enabled = true;
                Abfahrt_btn.Enabled = true;
                Uebergabe_btn.Enabled = true;
                Rückwärts_btn.Enabled = true;

                // Disable the other buttons
                Reparatur_Anfahrt_btn.Enabled = false;
                Kapazitaet_Anfahrt_btn.Enabled = false;
                Reparatur_abfahrt_btn.Enabled = false;
                Kapazitaet_Abfahrt_btn.Enabled = false;

                // Show the DataGridView when the condition is met
                ShowDataGridView(false);

                // Hide the message label when the condition is satisfied
                label_message.Visible = false;
            }
            else
            {
                // Reset all buttons to their initial state
                Anfahrt_btn.Enabled = false;
                Abfahrt_btn.Enabled = false;
                Uebergabe_btn.Enabled = false;
                Reparatur_Anfahrt_btn.Enabled = false;
                Kapazitaet_Anfahrt_btn.Enabled = false;
                Reparatur_abfahrt_btn.Enabled = false;
                Kapazitaet_Abfahrt_btn.Enabled = false;

                // Hide the DataGridView when the condition is not met
                ShowDataGridView(false);

                // Show the message label when the condition is not satisfied
                label_message.Visible = true;
            }

        }



        ////New 16-10-23 ==================================================================>
        //private string FetchDateinamenFromOracle(string spkValue)
        //{
        //    // Replace this with your Oracle database query to fetch "Dateinamen" based on "spkValue"
        //    string dateinamenValue = ""; // Initialize with an empty string

        //    // Replace with your Oracle database connection and query logic
        //    // Example:
        //    // using (OracleConnection connection = new OracleConnection(connectionString))
        //    // {
        //    //     // Create and execute the Oracle query to fetch "Dateinamen"
        //    //     // Set the value of "dateinamenValue" based on the result
        //    // }

        //    return dateinamenValue;
        //}


        //any letter entered into the "SP_K_textBox" will be automatically converted to uppercase, and lowercase letters will not be allowed.
        private void SP_K_textBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Convert the entered character to uppercase
            e.KeyChar = char.ToUpper(e.KeyChar);

            // Set Handled to true to prevent the lowercase letter from being entered
            e.Handled = true;
        }

        //The Red message Logic
        private void label_message_Click(object sender, EventArgs e)
        {

        }



        //==============================01-11-23=============================
        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex >= 0)
            {
                // Get the selected cell's value
                DataGridViewCell cell = dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex];
                string cellValue = cell.Value.ToString();

                // You can now perform actions based on the cell value or row data (Annoying message!)
                //MessageBox.Show($"Zelle hat geklickt: {cellValue}");
            }
        }





        private void FetchFilesFromOracle()
        {
            // Replace this with your Oracle database fetch logic
            // Populate excelFiles collection with data from Oracle
            excelFiles.Clear(); // Clear existing items from the ObservableCollection

            string connectionString = ConfigurationManager.ConnectionStrings["OracleConnection"].ConnectionString;

            using (OracleConnection connection = new OracleConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    //string query = "SELECT DATEINAME, URL_EXCEL, NVL(ANFAHRT,0) As ANFAHRT, NVL(ABFAHRT,0) AS ABFAHRT, NVL(REPARATUR,0) As REPARATUR FROM ZUORDNUNG_DOKU";
                    //string query = "SELECT DATEINAME, URL_EXCEL, NVL(ANFAHRT,0) As ANFAHRT, NVL(ABFAHRT,0) AS ABFAHRT, NVL(REPARATUR,0) As REPARATUR FROM ZUORDNUNG_DROOK";
                    //string query = "SELECT DATEINAMEN, URL_EXCL, ANF_REP, ANF_KAPAZ, ABF_REP, ABF_KAPAZ, UEBERGABE FROM ZUORDNUN_DROOK";
                    string query = "SELECT DATEINAMEN, URL_EXCL, NVL(ANF_REP,0) AS ANF_REP, NVL(ANF_KAPAZ,0) AS ANF_KAPAZ, " +
                        "NVL(ABF_REP,0) AS ABF_REP, NVL(ABF_KAPAZ,0) AS ABF_KAPAZ, NVL(UEBERGABE,0) AS UEBERGABE, " +
                        "NVL(ANF_REP_DRUC,0) AS ANF_REP_DRUC, NVL(ANF_KAPAZ_DRUC,0) AS ANF_KAPAZ_DRUC, NVL(ABF_REP_DRUC,0) AS ABF_REP_DRUC, " +
                        "NVL(ABF_KAPAZ_DRUC,0) AS ABF_KAPAZ_DRUC  FROM USR_TEST.ZUORDNUNG_DROOK";
                    using (OracleCommand command = new OracleCommand(query, connection))
                    {
                        using (OracleDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                string fileName = reader["DATEINAMEN"].ToString();
                                string filePath = reader["URL_EXCL"].ToString();
                                bool anfRep = reader["ANF_REP"].ToString() == "1";
                                bool anfKapaz = reader["ANF_KAPAZ"].ToString() == "1";
                                bool abfRep = reader["ABF_REP"].ToString() == "1";
                                bool abfKapaz = reader["ABF_KAPAZ"].ToString() == "1";
                                bool uebergabe = reader["UEBERGABE"].ToString() == "1";
                                bool anfRepDruc = reader["ANF_REP_DRUC"].ToString() == "1";
                                bool anfKapazDruc = reader["ANF_KAPAZ_DRUC"].ToString() == "1";
                                bool abfRepDruc = reader["ABF_REP_DRUC"].ToString() == "1";
                                bool abfKapazDruc = reader["ABF_KAPAZ_DRUC"].ToString() == "1";


                                excelFiles.Add(new ExcelFile
                                {
                                    Dateinamen = fileName,
                                    FilePath = filePath,
                                    Anf_Rep = anfRep,
                                    Anf_Kapaz = anfKapaz,
                                    Abf_Rep = abfRep,
                                    Abf_Kapaz = abfKapaz,
                                    Uebergabe = uebergabe,
                                    Anf_Rep_Druc = anfRepDruc,
                                    Anf_Kapaz_Druc = anfKapazDruc,
                                    Abf_Rep_Druc = abfRepDruc,
                                    Abf_Kapaz_Druc = abfKapazDruc,
                                    IsChecked = false // Initialize IsChecked to false initially
                                });
                            }

                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error fetching data from Oracle: " + ex.Message);
                }
            }
        }

        ////Save for Anwender
        ////This sends message when no "SP/P" was given
        //private void save_btn_Click_1(object sender, EventArgs e)
        //{
        //    //16-10-23
        //    string prefix = SP_K_textBox.Text;

        //    if (string.IsNullOrWhiteSpace(prefix))
        //    {
        //        MessageBox.Show("Bitte geben Sie vor dem Speichern der Dokumente einen gültigen SP/K-Wert ein. Beispiel: SP23-43. Es sind nur 7 Zeichen zulässig");
        //        return;
        //    }

        //    // Save checkbox states and associated files(Update DB as well)
        //    // You'll need to implement this method similar to the WPF version
        //    string AnfRepFolder = @"C:\temp\AnfRep\"; // Replace with your desired folder path
        //    string AnfKapazFolder = @"C:\temp\AnfKapaz\"; // Replace with your desired folder path
        //    string AbfRepFolder = @"C:\temp\AbfRep\"; // Replace with your desired folder path
        //    string AbfKapazFolder = @"C:\temp\AbfKapaz\"; // Replace with your desired folder path
        //    string UebergabeFolder = @"C:\temp\Uebergabe\"; // Replace with your desired folder path

        //    foreach (var excelFile in excelFiles)
        //    {
        //        if (excelFile.Anf_Rep)
        //        {
        //            SaveFileToLocalFolder(excelFile.Dateinamen, excelFile.FilePath, AnfRepFolder);
        //            UpdateCheckboxInDatabase(excelFile.Dateinamen, "ANF_REP", 1);
        //        }
        //        else
        //        {
        //            UpdateCheckboxInDatabase(excelFile.Dateinamen, "ANF_REP", null);
        //        }

        //        if (excelFile.Anf_Kapaz)
        //        {
        //            SaveFileToLocalFolder(excelFile.Dateinamen, excelFile.FilePath, AnfKapazFolder);
        //            UpdateCheckboxInDatabase(excelFile.Dateinamen, "ANF_KAPAZ", 1);
        //        }
        //        else
        //        {
        //            UpdateCheckboxInDatabase(excelFile.Dateinamen, "ANF_KAPAZ", null);
        //        }

        //        if (excelFile.Abf_Rep)
        //        {
        //            SaveFileToLocalFolder(excelFile.Dateinamen, excelFile.FilePath, AbfRepFolder);
        //            UpdateCheckboxInDatabase(excelFile.Dateinamen, "ABF_Rep", 1);
        //        }
        //        else
        //        {
        //            UpdateCheckboxInDatabase(excelFile.Dateinamen, "ABF_Rep", null);
        //        }
        //        if (excelFile.Abf_Kapaz)
        //        {
        //            SaveFileToLocalFolder(excelFile.Dateinamen, excelFile.FilePath, AbfKapazFolder);
        //            UpdateCheckboxInDatabase(excelFile.Dateinamen, "ABF_KAPAZ", 1);
        //        }
        //        else
        //        {
        //            UpdateCheckboxInDatabase(excelFile.Dateinamen, "ABF_KAPAZ", null);
        //        }
        //        if (excelFile.Uebergabe)
        //        {
        //            SaveFileToLocalFolder(excelFile.Dateinamen, excelFile.FilePath, UebergabeFolder);
        //            UpdateCheckboxInDatabase(excelFile.Dateinamen, "UEBERGABE", 1);
        //        }
        //        else
        //        {
        //            UpdateCheckboxInDatabase(excelFile.Dateinamen, "UEBERGABE", null);
        //        }
        //    }

        //    MessageBox.Show("Ausgewählte Dateien werden in lokalen Ordnern gespeichert und die Status der Kontrollkästchen werden in der Datenbank aktualisiert.");
        //}

        //// Save checkbox states and associated files (This code do not update in the DB)
        private void save_btn_Click_1(object sender, EventArgs e)
        {
            // 16-10-23
            string prefix = SP_K_textBox.Text;

            if (string.IsNullOrWhiteSpace(prefix))
            {
                MessageBox.Show("Bitte geben Sie vor dem Speichern der Dokumente einen gültigen SP/K-Wert ein. Beispiel: SP23-43. Es sind nur 7 Zeichen zulässig");
                return;
            }

            // Save checkbox states and associated files
            // You'll need to implement this method similar to the WPF version
            string AnfRepFolder = @"C:\temp\AnfRep\"; // Replace with your desired folder path
            string AnfKapazFolder = @"C:\temp\AnfKapaz\"; // Replace with your desired folder path
            string AbfRepFolder = @"C:\temp\AbfRep\"; // Replace with your desired folder path
            string AbfKapazFolder = @"C:\temp\AbfKapaz\"; // Replace with your desired folder path
            string UebergabeFolder = @"C:\temp\Uebergabe\"; // Replace with your desired folder path

            // Save checkbox states and associated files for "Druc" folders
            string AnfRepDrucFolder = @"C:\temp\AnfRepDruc\"; // Replace with your desired folder path
            string AnfKapazDrucFolder = @"C:\temp\AnfKapazDruc\"; // Replace with your desired folder path
            string AbfRepDrucFolder = @"C:\temp\AbfRepDruc\"; // Replace with your desired folder path
            string AbfKapazDrucFolder = @"C:\temp\AbfKapazDruc\"; // Replace with your desired folder path

            HashSet<string> downloadedFiles = new HashSet<string>();

            foreach (var excelFile in excelFiles)
            {
                if (!downloadedFiles.Contains(excelFile.Dateinamen))
                {
                    if (excelFile.Anf_Rep)
                    {
                        SaveFileToLocalFolder(excelFile.Dateinamen, excelFile.FilePath, AnfRepFolder);
                    }
                    if (excelFile.Anf_Kapaz)
                    {
                        SaveFileToLocalFolder(excelFile.Dateinamen, excelFile.FilePath, AnfKapazFolder);
                    }
                    if (excelFile.Abf_Rep)
                    {
                        SaveFileToLocalFolder(excelFile.Dateinamen, excelFile.FilePath, AbfRepFolder);
                    }
                    if (excelFile.Abf_Kapaz)
                    {
                        SaveFileToLocalFolder(excelFile.Dateinamen, excelFile.FilePath, AbfKapazFolder);
                    }
                    if (excelFile.Uebergabe)
                    {
                        SaveFileToLocalFolder(excelFile.Dateinamen, excelFile.FilePath, UebergabeFolder);
                    }
                    if (excelFile.Anf_Rep_Druc)
                    {
                        SaveFileToLocalFolder(excelFile.Dateinamen, excelFile.FilePath, AnfRepDrucFolder);
                    }
                    if (excelFile.Anf_Kapaz_Druc)
                    {
                        SaveFileToLocalFolder(excelFile.Dateinamen, excelFile.FilePath, AnfKapazDrucFolder);
                    }
                    if (excelFile.Abf_Rep_Druc)
                    {
                        SaveFileToLocalFolder(excelFile.Dateinamen, excelFile.FilePath, AbfRepDrucFolder);
                    }
                    if (excelFile.Abf_Kapaz_Druc)
                    {
                        SaveFileToLocalFolder(excelFile.Dateinamen, excelFile.FilePath, AbfKapazDrucFolder);
                    }

                }
            }

            MessageBox.Show("Ausgewählte Dateien werden in lokalen Ordnern gespeichert.");
        }



        ////To UpdateCheckboxInDatabase (DO NOT DELETE THIS!)
        //private void UpdateCheckboxInDatabase(string fileName, string column, int? value)
        //{
        //    string connectionString = ConfigurationManager.ConnectionStrings["OracleConnection"].ConnectionString;

        //    using (OracleConnection connection = new OracleConnection(connectionString))
        //    {
        //        try
        //        {
        //            connection.Open();

        //            string updateQuery = $"UPDATE USR_TEST.ZUORDNUNG_DROOK SET {column} = :value WHERE DATEINAMEN = :fileName";
        //            using (OracleCommand command = new OracleCommand(updateQuery, connection))
        //            {
        //                command.Parameters.Add(new OracleParameter("value", value));
        //                command.Parameters.Add(new OracleParameter("fileName", fileName));
        //                command.ExecuteNonQuery();
        //            }
        //        }
        //        catch (Exception ex)
        //        {
        //            MessageBox.Show($"Fehler beim Aktualisieren des Kontrollkästchens in der Datenbank: {ex.Message}");
        //        }
        //    }
        //}



        //==============================Hidden Buttons===============================================================


        private void Anfahrt_btn_Click(object sender, EventArgs e)
        {
            currentStep = WorkflowStep.Anfahrt;
            stateStack.Push(currentStep);

            // Your Anfahrt_btn logic here: Visible or Invisible (don't delete)
            Kapazitaet_Anfahrt_btn.Visible = true; // Set visibility to false
            Reparatur_Anfahrt_btn.Visible = true; // Set visibility to true

            //// Your Anfahrt_btn logic here: Enabled or Disabled (don't delete)
            Reparatur_Anfahrt_btn.Enabled = true;
            Kapazitaet_Anfahrt_btn.Enabled = true;
            Abfahrt_btn.Enabled = false;
            Uebergabe_btn.Enabled = false;
            Anfahrt_btn.Enabled = false;

        }

        private void Abfahrt_btn_Click(object sender, EventArgs e)
        {
            currentStep = WorkflowStep.ReparaturAnfahrt;
            stateStack.Push(currentStep);

            // Your Anfahrt_btn logic here: Visible or Invisible (don't delete)
            Reparatur_abfahrt_btn.Visible = true; // Set visibility to false
            Kapazitaet_Abfahrt_btn.Visible = true; // Set visibility to false

            // Your Abfahrt_btn logic here: Enabled or Disabled
            Anfahrt_btn.Enabled = false;
            Reparatur_Anfahrt_btn.Enabled = false;
            Kapazitaet_Anfahrt_btn.Enabled = false;
            Uebergabe_btn.Enabled = false;
            Reparatur_abfahrt_btn.Enabled = true;
            Kapazitaet_Abfahrt_btn.Enabled = true;
        }

        private void Uebergabe_btn_Click(object sender, EventArgs e)
        {
            currentStep = WorkflowStep.ReparaturAnfahrt;
            stateStack.Push(currentStep);

            // Enable/Disable buttons as needed
            Anfahrt_btn.Enabled = false;
            Abfahrt_btn.Enabled = false;
            Reparatur_Anfahrt_btn.Enabled = false;
            Kapazitaet_Anfahrt_btn.Enabled = false;
            Uebergabe_btn.Enabled = true;
            Reparatur_abfahrt_btn.Enabled = false;
            Kapazitaet_Abfahrt_btn.Enabled = false;
        }

        //Reparatur_Anfahrt_btn_Click
        private void Reparatur_Anfahrt_btn_Click(object sender, EventArgs e)
        {
            currentStep = WorkflowStep.ReparaturAnfahrt;
            stateStack.Push(currentStep);

            // Enable/Disable buttons as needed --------------------------------->Don't delete this part!!!!(Model)
            Anfahrt_btn.Enabled = false;
            Abfahrt_btn.Enabled = false;
            Kapazitaet_Anfahrt_btn.Enabled = true;
            Uebergabe_btn.Enabled = false;
            Reparatur_abfahrt_btn.Enabled = false;
            Kapazitaet_Abfahrt_btn.Enabled = false;
            Reparatur_Anfahrt_btn.Enabled = true;

            // Show the DataGridView when "Reparatur_Anfahrt_btn" is clicked
            ShowDataGridView(true);

            //Show only the required DataGridView columns ("Dateinamen" and "Anf_Rep") in the DataGridView =========================================>24-10-23
            ShowSelectedColumns("Dateinamen", "Anf_Rep", "Anf_Rep_Druc");
        }


        //Kapazitaet_Anfahrt_btn_Click
        private void Kapazitaet_btn_Click(object sender, EventArgs e)
        {
            currentStep = WorkflowStep.ReparaturAnfahrt;
            stateStack.Push(currentStep);

            // Enable/Disable buttons as needed
            Anfahrt_btn.Enabled = false;
            Abfahrt_btn.Enabled = false;
            Reparatur_Anfahrt_btn.Enabled = true;
            Uebergabe_btn.Enabled = false;
            Reparatur_abfahrt_btn.Enabled = false;
            Kapazitaet_Abfahrt_btn.Enabled = false;
            Kapazitaet_Anfahrt_btn.Enabled = true;

            // Show the DataGridView when "Reparatur_Anfahrt_btn" is clicked
            ShowDataGridView(true);

            //Show only the required DataGridView columns ("Dateinamen" and "Anf_Kapa") in the DataGridView
            ShowSelectedColumns("Dateinamen", "Anf_Kapaz", "Anf_Kapaz_Druc");
        }

        private void Reparatur_abfahrt_btn_Click(object sender, EventArgs e)
        {
            currentStep = WorkflowStep.ReparaturAnfahrt;
            stateStack.Push(currentStep);

            // // Enable/Disable buttons as needed
            Anfahrt_btn.Enabled = false;
            Abfahrt_btn.Enabled = false;
            Reparatur_Anfahrt_btn.Enabled = false;
            Kapazitaet_Anfahrt_btn.Enabled = false;
            Uebergabe_btn.Enabled = false;
            Kapazitaet_Abfahrt_btn.Enabled = true;

            // Show the DataGridView when "Reparatur_Anfahrt_btn" is clicked
            ShowDataGridView(true);

            //Show only the required DataGridView columns ("Dateinamen" and "Abf_Rep") in the DataGridView
            ShowSelectedColumns("Dateinamen", "Abf_Rep", "Abf_Rep_Druc") ;
        }

        private void Kapazitaet_Abfahrt_btn_Click(object sender, EventArgs e)
        {


            // Your Uebergabe_btn logic here
            Anfahrt_btn.Enabled = false;
            Abfahrt_btn.Enabled = false;
            Reparatur_Anfahrt_btn.Enabled = false;
            Kapazitaet_Anfahrt_btn.Enabled = false;
            Uebergabe_btn.Enabled = false;
            Reparatur_abfahrt_btn.Enabled = true;
            //Kapazitaet_Abfahrt_btn.Enabled = false;

            // Show the DataGridView when "Reparatur_Anfahrt_btn" is clicked
            ShowDataGridView(true);

            //Show only the required DataGridView columns ("Dateinamen" and "Abf_Kapaz") in the DataGridView
            ShowSelectedColumns("Dateinamen", "Abf_Kapaz", "Abf_Kapaz_Druc") ;
        }

        //===========================End hidden Buttons====================================================================


        //===================Printing Part====================================
        //================For PDF Part========================

        //=====================================================
        private void Drucken_btn_Click(object sender, EventArgs e)
        {

            // Save checkbox states and associated files
            // You'll need to implement this method similar to the WPF version
            //string AnfRepFolder = @"C:\temp\AnfRep\"; // Replace with your desired folder path
            //string AnfKapazFolder = @"C:\temp\AnfKapaz\"; // Replace with your desired folder path
            //string AbfRepFolder = @"C:\temp\AbfRep\"; // Replace with your desired folder path
            //string AbfKapazFolder = @"C:\temp\AbfKapaz\"; // Replace with your desired folder path
            //string UebergabeFolder = @"C:\temp\Uebergabe\"; // Replace with your desired folder path

            // Specify the folder for Anf_Rep_Druc files
            //string AnfRepDrucFolder = @"C:\temp\AnfRepDruc\";

            //HashSet<string> printedFiles = new HashSet<string>();

            foreach (var excelFile in excelFiles)
            {
                if (excelFile.Anf_Rep_Druc && !printedFiles.Contains(excelFile.Dateinamen))
                {
                    string excelFilePath = GetExcelFilePath(excelFile);

                    if (excelFilePath != null)
                    {
                        PrintExcel(excelFilePath);
                        printedFiles.Add(excelFile.Dateinamen); // Add the file name to the set after printing.
                    }
                }
            }

            MessageBox.Show("Selected Excel files from Anf_Rep_Druc column have been sent to the printer.");

            //MessageBox.Show("Selected Excel files have been sent to the printer.");

            //To be used directly when want to print Excel directly(must incorporate/create excelholders"PrintFiles")
            //foreach (var excelFile in excelFiles)
            //{
            //    if (!printedFiles.Contains(excelFile.Dateinamen))
            //    {
            //        if (excelFile.Anf_Rep)
            //        {
            //            PrintFile(excelFile.Dateinamen, AnfRepDruc);
            //            printedFiles.Add(excelFile.Dateinamen); // Add the file name to the set after printing.
            //        }

            //        if (excelFile.Anf_Kapaz)
            //        {
            //            PrintFile(excelFile.Dateinamen, AnfKapazDruc);
            //            printedFiles.Add(excelFile.Dateinamen); // Add the file name to the set after printing.
            //        }

            //        if (excelFile.Abf_Rep)
            //        {
            //            PrintFile(excelFile.Dateinamen, AbfRepDruc);
            //            printedFiles.Add(excelFile.Dateinamen); // Add the file name to the set after printing.
            //        }

            //        if (excelFile.Abf_Kapaz)
            //        {
            //            PrintFile(excelFile.Dateinamen, AbfKapazDruc);
            //            printedFiles.Add(excelFile.Dateinamen); // Add the file name to the set after printing.
            //        }
            //    }
            //}
            //MessageBox.Show("Selected documents have been printed.");

            //=====================================Old printing method=========
            //string folderPath = @"C:\Temp\Anfahrt";
            //string[] excelFiles = Directory.GetFiles(folderPath, "*.xlsx");

            //if (excelFiles.Length == 0)
            //{
            //    MessageBox.Show("No Excel files found in the folder.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
            //    return;
            //}

            //// Create an instance of Excel Application
            //dynamic excelApp = Activator.CreateInstance(Type.GetTypeFromProgID("Excel.Application"));

            //foreach (string excelFile in excelFiles)
            //{
            //    try
            //    {
            //        // Open the Excel file using late binding
            //        dynamic workbook = excelApp.Workbooks.Open(excelFile);

            //        foreach (dynamic worksheet in workbook.Sheets)
            //        {
            //            // Print each worksheet
            //            worksheet.PrintOut();

            //            // Release the worksheet object
            //            Marshal.ReleaseComObject(worksheet);
            //        }

            //        // Close and release the workbook object
            //        workbook.Close(false);
            //        Marshal.ReleaseComObject(workbook);
            //    }
            //    catch (Exception ex)
            //    {
            //        MessageBox.Show($"Error printing file {excelFile}: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //    }
            //}

            //// Quit and release the Excel Application
            //excelApp.Quit();
            //Marshal.ReleaseComObject(excelApp);
        }
        //PrintFile Logic
        //private void PrintFile(string dateinamen, string anfRepDruc)
        private void PrintFile(string filePath)
        {
            //throw new NotImplementedException();
            try
            {
                // You can use the Process.Start method to open and print files.
                System.Diagnostics.Process.Start(filePath);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error while printing the file: {ex.Message}");
            }
        }

        private void PrintExcel(string excelFilePath)
        {
            try
            {
                Microsoft.Office.Interop.Excel.Application excelApp = new Microsoft.Office.Interop.Excel.Application();
                Microsoft.Office.Interop.Excel.Workbook workbook = excelApp.Workbooks.Open(excelFilePath);

                // Iterate through all sheets and print each one
                foreach (Microsoft.Office.Interop.Excel.Worksheet worksheet in workbook.Sheets)
                {
                    worksheet.PrintOut();
                }

                // Close the workbook without saving changes
                workbook.Close(false);

                // Quit Excel
                excelApp.Quit();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error while printing Excel: {ex.Message}");
            }
        }



        //==13-11(this is to filter between columns) ====================================
        private string GetExcelFilePath(ExcelFile excelFile)
        {
            // Save checkbox states and associated files
            // You'll need to implement this method similar to the WPF version
            string AnfRepDruc = @"C:\temp\AnfRepDruc\";
            string AnfKapazDruc = @"C:\temp\AnfKapaz\";
            string AbfRepDruc = @"C:\temp\AbfRep\";
            string AbfKapazDruc = @"C:\temp\AbfKapaz\";

            string AnfRepFolder = @"C:\temp\AnfRep\"; // Replace with your desired folder path
            string AnfKapazFolder = @"C:\temp\AnfKapaz\"; // Replace with your desired folder path
            string AbfRepFolder = @"C:\temp\AbfRep\"; // Replace with your desired folder path
            string AbfKapazFolder = @"C:\temp\AbfKapaz\"; // Replace with your desired folder path
            string UebergabeFolder = @"C:\temp\Uebergabe\"; // Replace with your desired folder path

            string folderPath;

            if (excelFile.Anf_Rep_Druc)
            {
                folderPath = AnfRepDruc;
            }
            else
            {
                folderPath = AnfRepFolder;
            }

            string excelFilePath = Path.Combine(folderPath, excelFile.Dateinamen);

            try
            {
                // Check if the file exists in the primary folder
                string primaryFilePath = Path.Combine(folderPath, excelFile.Dateinamen);
                if (File.Exists(primaryFilePath))
                {
                    return primaryFilePath;
                }

                // If the file is not found in the primary folder, check the secondary folder
                string secondaryFilePath = Path.Combine(AnfRepDruc, excelFile.Dateinamen);
                if (File.Exists(secondaryFilePath))
                {
                    // File found in secondary folder, copy it to the primary folder
                    File.Copy(secondaryFilePath, primaryFilePath);

                    return primaryFilePath;
                }
                else
                {
                    // Original file not found in either folder. Handle accordingly.
                    MessageBox.Show($"Original file  found: {excelFilePath}");

                    // Print the file using the original path (if available).
                    PrintFile(excelFile.FilePath);

                    // Move the file to the "not found" folder if it exists
                    string notFoundFolderPath = @"C:\temp\NotFoundFiles\";
                    string notFoundFilePath = Path.Combine(notFoundFolderPath, excelFile.Dateinamen);

                    if (File.Exists(excelFile.FilePath))
                    {
                        try
                        {
                            File.Move(excelFile.FilePath, notFoundFilePath);
                            MessageBox.Show($"File moved to {notFoundFolderPath}");
                        }
                        catch (Exception ex)
                        {
                            // Handle file move exception
                            MessageBox.Show($"Error moving file: {ex.Message}");
                        }

                        // Delete the moved file after printing and moving
                        File.Delete(notFoundFilePath);
                    }

                    return null;
                }
            }
            catch (Exception ex)
            {
                // Capture and display exception details.
                MessageBox.Show($"Exception: {ex.Message}\nStackTrace: {ex.StackTrace}");

                return null;
            }
        }








        private void printDocument1_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            //// Create a DataGridViewPrinter instance
            //DataGridViewPrinter printer = new DataGridViewPrinter(dataGridView1, e, true, true, "Title", new Font("Arial", 12, FontStyle.Bold, GraphicsUnit.Point), Color.Black, true);

            //// Print the DataGridView content
            //if (printer.DrawDataGridView(e.Graphics))
            //{
            //    e.HasMorePages = false; // No more pages
            //}
        }

        //=============================Can sst document number===================================

        //To count the number of occurrences of a specific "Dateinamen" in your dataGridView1
        private void Dok_numb_txt_TextChanged(object sender, EventArgs e)
        {
            // Display the number of rows in the DataGridView in the Dok_numb_txt TextBox
            Dok_numb_txt.Text = dataGridView1.RowCount.ToString();
        }
        //=========================================================




        //With debugging codes (for SaveFileToLocalFolder)
        //Here is the prefix "SP_K_textBox.Text" also saved
        //With ExcelDataReader mixed with OfficeOpenXml

        private void SaveFileToLocalFolder(string fileName, string sourceFilePath, string destinationFolder)
        {
            try
            {
                if (string.IsNullOrEmpty(sourceFilePath))
                {
                    MessageBox.Show($"Error saving file {fileName}: Source file path is null or empty.");
                    return;
                }

                // Get the prefix from the SP_K_textBox and convert it to uppercase
                string prefix = SP_K_textBox.Text.ToUpper();

                if (string.IsNullOrWhiteSpace(prefix))
                {
                    MessageBox.Show("Bitte geben Sie vor dem Speichern der Dokumente einen gültigen SP/K-Wert ein. Beispiel: SP11-22.");
                    return;
                }

                // Construct the destination path with the uppercase prefix
                string destinationPath = Path.Combine(destinationFolder, $"{prefix}_{fileName}");

                // Copy the file to the destination folder
                File.Copy(sourceFilePath, destinationPath, true);

                // Update the Excel file with the prefix in "column A" and "row 1"
                UpdateExcelFile(destinationPath, prefix); // Call the method to update the Excel file

                // You can also include additional logic here if needed
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error saving file {fileName}: {ex.Message}");
            }
        }

        //Must be associated to "Uebergabe" to be saved
        //private void SaveFileToLocalFolder(string fileName, string sourceFilePath, string destinationFolder)
        //{
        //    try
        //    {
        //        if (string.IsNullOrEmpty(sourceFilePath))
        //        {
        //            // Skip saving if source file path is null or empty
        //            return;
        //        }

        //        // Check if the "UEBERGABE" checkbox is checked
        //        bool isUebergabeChecked = excelFiles.FirstOrDefault(file => file.Dateinamen == fileName)?.Uebergabe ?? false;

        //        if (!isUebergabeChecked)
        //        {
        //            // Skip saving if 'UEBERGABE' checkbox is not checked
        //            return;
        //        }

        //        // Get the prefix from the SP_K_textBox and convert it to uppercase
        //        string prefix = SP_K_textBox.Text.ToUpper();

        //        if (string.IsNullOrWhiteSpace(prefix))
        //        {
        //            // Skip saving if SP_K value is not valid
        //            return;
        //        }

        //        // Construct the destination path with the uppercase prefix
        //        string destinationPath = Path.Combine(destinationFolder, $"{prefix}_{fileName}");

        //        // Copy the file to the destination folder
        //        File.Copy(sourceFilePath, destinationPath, true);

        //        // Update the Excel file with the prefix in "column A" and "row 1"
        //        UpdateExcelFile(destinationPath, prefix); // Call the method to update the Excel file

        //        // You can also include additional logic here if needed
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show($"Error saving file {fileName}: {ex.Message}");
        //    }
        //}


        private void UpdateExcelFile(string filePath, string prefix)
        {
            if (Path.GetExtension(filePath).Equals(".xlsx", StringComparison.OrdinalIgnoreCase))
            {
                try
                {
                    using (var workbook = new XLWorkbook(filePath))
                    {
                        var worksheet = workbook.Worksheet(1); // Assuming you want to update the first worksheet

                        // Get the prefix from SP_K_textBox
                        prefix = SP_K_textBox.Text.ToUpper(); // Make sure the text is in the desired format

                        if (string.IsNullOrWhiteSpace(prefix))
                        {
                            MessageBox.Show("Bitte geben Sie vor dem Speichern der Dokumente einen gültigen SP/K-Wert ein. Beispiel: SP11-22.");
                            return;
                        }

                        // Update the Excel file with the prefix in "cell A1"
                        worksheet.Cell(1, 1).Value = prefix;

                        // Save the changes
                        workbook.Save();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error updating Excel file {filePath}: {ex.Message}");
                }
            }
        }


        private void LoadCheckboxState()
        {
            // Load checkbox states from settings
            // You'll need to implement this method similar to the WPF version
            //MessageBox.Show("Loading checkbox states"); // Add this line for debugging ==============================>

            string checkboxStateString = Properties.Settings.Default.CheckboxStates;

            if (checkboxStateString != null)
            {
                string[] checkboxStateArray = checkboxStateString.Split(',');

                for (int i = 0; i < checkboxStateArray.Length && i < excelFiles.Count; i++)
                {
                    if (bool.TryParse(checkboxStateArray[i], out bool isChecked))
                    {
                        excelFiles[i].IsChecked = isChecked;
                    }
                }
            }
        }

        public class ExcelFile
        {
            public string Dateinamen { get; set; }
            public string FilePath { get; set; }
            public bool Anf_Rep { get; set; }
            public bool Anf_Rep_Druc { get; set; }
            public bool Anf_Kapaz { get; set; }
            public bool Anf_Kapaz_Druc { get; set; }
            public bool Abf_Rep { get; set; }
            public bool Abf_Rep_Druc { get; set; }
            public bool Abf_Kapaz { get; set; }
            public bool Abf_Kapaz_Druc { get; set; }
            public bool Uebergabe { get; set; }
            public bool IsChecked { get; set; }
        }

        // Define a class to represent the application state ==========================>23-10-23
        private class ApplicationState
        {
            public bool Anfahrt_btnEnabled { get; set; }
            public bool Abfahrt_btnEnabled { get; set; }
            public bool Uebergabe_btnEnabled { get; set; }
            // Add more properties to capture the state of other elements as needed
        }

    }
}


//========================================================================

//using System.Data;

////using OfficeOpenXml; // This namespace contains the ExcelPackage class
//using System.Runtime.InteropServices;
//using Excel = Microsoft.Office.Interop.Excel;


////using System.Windows.Controls;

//namespace WinFormsApp_Druckertool_WWW
//{
//    public partial class anwenderForm : System.Windows.Forms.Form
//    {
//        public anwenderForm()
//        {
//            InitializeComponent();
//        }

//        private void save_anw_btn_Click(object sender, EventArgs e)
//        {
//            this.Close();
//        }

//        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
//        {

//        }

//        private void Anfahrt_btn_Click(object sender, EventArgs e)
//        {
//            string folderPath = @"C:\Temp\Anfahrt"; // Replace with the actual path to the "Anfahrt" folder
//            string[] excelFiles = Directory.GetFiles(folderPath, "*.xlsx"); // Filter for Excel files

//            if (Directory.Exists(folderPath))
//            {
//                // Get the list of files in the folder
//                string[] files = Directory.GetFiles(folderPath);

//                // Display the total number of documents in textBox1
//                textBox1.Text = $" {files.Length}"; //Number of Files:

//                // Create a DataTable to store the file names
//                DataTable dataTable = new DataTable();
//                dataTable.Columns.Add("Name des Excel-Dokuments");
//                //DataGridView.Columns["Name des Excel-Dokuments"].DefaultCellStyle.Font = new Font("Segoe UI", 14, FontStyle.Bold);

//                // Populate the DataTable with file names
//                foreach (string filePath in files)
//                {
//                    dataTable.Rows.Add(Path.GetFileName(filePath));
//                }

//                // Set the DataGridView's DataSource to the DataTable
//                dataGridView1.DataSource = dataTable;

//                // Adjust column widths
//                //dataGridView1.Columns["ColumnName1"].Width = 150; // Adjust the width for ColumnName1

//                // Set a common width for all columns
//                int desiredWidth = 525; // Adjust the width as needed

//                foreach (DataGridViewColumn column in dataGridView1.Columns)
//                {
//                    column.Width = desiredWidth;
//                }

//            }
//            else
//            {
//                MessageBox.Show("The 'Anfahrt' folder does not exist.");
//            }
//        }

//        private void Abfahrt_btn_Click(object sender, EventArgs e)
//        {

//        }

//        private void Reparatur_btn_Click(object sender, EventArgs e)
//        {

//        }

//        //With EPPlus
//        //private void Drucken_btn_Click(object sender, EventArgs e)
//        //{
//        //    //printDocument1.Print(); // Start printing
//        //    string folderPath = @"C:\Temp\Anfahrt";
//        //    string[] excelFiles = Directory.GetFiles(folderPath, "*.xlsx");

//        //    if (excelFiles.Length == 0)
//        //    {
//        //        MessageBox.Show("No Excel files found in the folder.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
//        //        return;
//        //    }

//        //    foreach (string excelFile in excelFiles)
//        //    {
//        //        try
//        //        {
//        //            // Load the Excel file using EPPlus
//        //            using (var package = new ExcelPackage(new FileInfo(excelFile)))
//        //            {
//        //                // Assuming there's a worksheet named "Sheet1" - adjust as needed
//        //                var worksheet = package.Workbook.Worksheets["Sheet1"];

//        //                // Print the worksheet
//        //                Process.Start(new ProcessStartInfo(excelFile)
//        //                {
//        //                    Verb = "Print"
//        //                });
//        //            }
//        //        }
//        //        catch (Exception ex)
//        //        {
//        //            MessageBox.Show($"Error printing file {excelFile}: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
//        //        }
//        //    }
//        //}
//        private void Drucken_btn_Click(object sender, EventArgs e)
//        {
//            string folderPath = @"C:\Temp\Anfahrt";
//            string[] excelFiles = Directory.GetFiles(folderPath, "*.xlsx");

//            if (excelFiles.Length == 0)
//            {
//                MessageBox.Show("No Excel files found in the folder.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
//                return;
//            }

//            // Create an instance of Excel Application
//            dynamic excelApp = Activator.CreateInstance(Type.GetTypeFromProgID("Excel.Application"));

//            foreach (string excelFile in excelFiles)
//            {
//                try
//                {
//                    // Open the Excel file using late binding
//                    dynamic workbook = excelApp.Workbooks.Open(excelFile);

//                    foreach (dynamic worksheet in workbook.Sheets)
//                    {
//                        // Print each worksheet
//                        worksheet.PrintOut();

//                        // Release the worksheet object
//                        Marshal.ReleaseComObject(worksheet);
//                    }

//                    // Close and release the workbook object
//                    workbook.Close(false);
//                    Marshal.ReleaseComObject(workbook);
//                }
//                catch (Exception ex)
//                {
//                    MessageBox.Show($"Error printing file {excelFile}: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
//                }
//            }

//            // Quit and release the Excel Application
//            excelApp.Quit();
//            Marshal.ReleaseComObject(excelApp);
//        }









//        private void textBox1_TextChanged(object sender, EventArgs e)
//        {

//        }

//        private void printDocument1_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
//        {
//            // Create a DataGridViewPrinter instance
//            DataGridViewPrinter printer = new DataGridViewPrinter(dataGridView1, e, true, true, "Title", new Font("Arial", 12, FontStyle.Bold, GraphicsUnit.Point), Color.Black, true);

//            // Print the DataGridView content
//            if (printer.DrawDataGridView(e.Graphics))
//            {
//                e.HasMorePages = false; // No more pages
//            }
//        }

//        private void label1_Click(object sender, EventArgs e)
//        {

//        }
//    }
//}
