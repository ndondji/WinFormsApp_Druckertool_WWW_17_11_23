

using System;
using System.Collections.ObjectModel;
using System.Data;
using System.Windows.Forms;
using System.IO;
using Oracle.ManagedDataAccess.Client;
using System.Linq;
using System.Diagnostics;
using System.Configuration;
using System.Data.Common;

namespace WinFormsApp_Test
{
    public partial class Form1 : Form
    {
        private ObservableCollection<ExcelFile> excelFiles = new ObservableCollection<ExcelFile>();
        private DataTable filteredDataTable = new DataTable(); // Declare filteredDataTable at the class level

        public Form1()
        {
            InitializeComponent();

            // Set panel2 to be visible initially
            panel1.Visible = true;
            panel2.Visible = false;
            panel2.BackColor = Color.LightGreen;
            btn_Administrator.BackColor = Color.PaleVioletRed;

            // Load settings
            Properties.Settings.Default.Reload();

            // Load checkbox states when the application starts
            LoadCheckboxState();

            // Fetch data from Oracle and populate excelFiles collection
            FetchFilesFromOracle();
            dataGridView_Admin.DataSource = excelFiles;
            //To hidde visibility of some Columns in dataGridView
            dataGridView_Admin.Columns["FilePath"].Visible = false;
            dataGridView_Admin.Columns["IsChecked"].Visible = false;
        }

        // Button click event to show panel1 (Anwender)
        private void btn_Anwender_Click(object sender, EventArgs e)
        {
            // Show the first panel and hide the second panel.
            panel1.Visible = true;
            panel2.Visible = false;
            btn_Anwender.BackColor = Color.LightGoldenrodYellow;
        }

        // Button click event to show panel2 (Administrator)
        private void btn_Administrator_Click(object sender, EventArgs e)
        {
            // Show the second panel and hide the first panel.
            panel1.Visible = false;
            panel2.Visible = true;
            panel2.BackColor = Color.LightGreen;
            btn_Administrator.BackColor = Color.PaleVioletRed;
        }

        //
        private void btn_UploadExcelFiles_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Multiselect = true;
            openFileDialog.Filter = "Excel Files (*.xls;*.xlsx)|*.xls;*.xlsx|All Files (*.*)|*.*";

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                foreach (string filePath in openFileDialog.FileNames)
                {
                    string fileName = Path.GetFileName(filePath);
                    excelFiles.Add(new ExcelFile { FileName = fileName, FilePath = filePath });
                    UploadToOracle(fileName, filePath);
                }
            }
        }

        //Calling the dataGridView ===================>>>>>>>>>>>>>>>>>19-10-23
        //private void dataGridView_Admin_CellContentClick(object sender, DataGridViewCellEventArgs e)
        //{
        //    //To show all columns
        //    //if (e.RowIndex >= 0 && e.ColumnIndex >= 0)
        //    //{
        //    //    // Get the selected cell's value
        //    //    DataGridViewCell cell = dataGridView_Admin.Rows[e.RowIndex].Cells[e.ColumnIndex];
        //    //    string cellValue = cell.Value.ToString();

        //    //    // You can now perform actions based on the cell value or row data
        //    //    MessageBox.Show($"Cell clicked: {cellValue}");
        //    //}

        //    //To show all columns and selected Anfahrt
        //    if (e.RowIndex >= 0 && e.ColumnIndex >= 0)
        //    {
        //        // Get the selected cell's value from the filtered DataTable
        //        DataGridViewCell cell = dataGridView_Admin.Rows[e.RowIndex].Cells[e.ColumnIndex];
        //        string columnName = filteredDataTable.Columns[e.ColumnIndex].ColumnName;
        //        string cellValue = cell.Value.ToString();

        //        // You can now perform actions based on the cell value, column name, or row data
        //        MessageBox.Show($"Column: {columnName}, Value: {cellValue}");
        //    }
        //}
        private void dataGridView_Admin_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex >= 0)
            {
                // Get the selected cell's value from the filtered DataTable
                DataGridViewCell cell = dataGridView_Admin.Rows[e.RowIndex].Cells[e.ColumnIndex];
                string columnName = filteredDataTable.Columns[e.ColumnIndex].ColumnName;
                string cellValue = cell.Value.ToString();

                // You can now perform actions based on the cell value, column name, or row data
                MessageBox.Show($"Column: {columnName}, Value: {cellValue}");

                // Handle the "JA" checkbox clicks
                if (columnName == "JA")
                {
                    // Toggle the checkbox state in the DataGridView
                    bool currentCheckedState = (bool)cell.Value;
                    cell.Value = !currentCheckedState;

                    // Update the corresponding Oracle database value
                    UpdateCheckboxInDatabase(excelFiles[e.RowIndex].FileName, "JA", currentCheckedState ? 0 : 1);

                    // Remember the checkbox state in your application
                    excelFiles[e.RowIndex].Ja = !currentCheckedState;
                }
            }
        }





        //Save button generated from design 11-09-23
        //private void btn_save_Click(object sender, EventArgs e)
        //{
        //    btn_save.BackColor = Color.LightBlue;
        //}


        private void UploadToOracle(string fileName, string filePath)
        {
            // Replace this with your Oracle database upload logic
            // You'll need to adapt it for Oracle data access in WinForms
            string connectionString = ConfigurationManager.ConnectionStrings["OracleConnection"].ConnectionString;

            using (OracleConnection connection = new OracleConnection(connectionString))
            {
                try
                {
                    connection.Open();

                    // Check if the file already exists in the table
                    string checkQuery = "SELECT COUNT(*) FROM ZUORDNUNG_DOKU WHERE DATEINAME = :fileName";
                    using (OracleCommand checkCommand = new OracleCommand(checkQuery, connection))
                    {
                        checkCommand.Parameters.Add(new OracleParameter("fileName", fileName));
                        int existingCount = Convert.ToInt32(checkCommand.ExecuteScalar());

                        if (existingCount > 0)
                        {
                            MessageBox.Show("File already exists in the table.");
                            return; // Exit the method if the file exists
                        }
                    }

                    // If the file doesn't exist, insert it into the table
                    string insertQuery = "INSERT INTO ZUORDNUNG_DOKU (DATEINAME, URL_EXCEL) VALUES (:fileName, :filePath)";
                    using (OracleCommand command = new OracleCommand(insertQuery, connection))
                    {
                        command.Parameters.Add(new OracleParameter("fileName", fileName));
                        command.Parameters.Add(new OracleParameter("filePath", filePath));
                        command.ExecuteNonQuery();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error uploading to Oracle: " + ex.Message);
                }
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
                    string query = "SELECT DATEINAME, URL_EXCEL, NVL(ANFAHRT,0) As ANFAHRT, NVL(ABFAHRT,0) AS ABFAHRT, NVL(REPARATUR,0) As REPARATUR, NVL(JA,0) AS JA FROM ZUORDNUNG_DOKU";
                    using (OracleCommand command = new OracleCommand(query, connection))
                    {
                        using (OracleDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                string fileName = reader["DATEINAME"].ToString();
                                string filePath = reader["URL_EXCEL"].ToString();
                                bool anfahrt = reader["ANFAHRT"].ToString() == "1";
                                bool abfahrt = reader["ABFAHRT"].ToString() == "1";
                                bool reparatur = reader["REPARATUR"].ToString() == "1";
                                bool ja = reader["JA"].ToString() == "1";

                                excelFiles.Add(new ExcelFile
                                {
                                    FileName = fileName,
                                    FilePath = filePath,
                                    Anfahrt = anfahrt,
                                    Abfahrt = abfahrt,
                                    Reparatur = reparatur,
                                    Ja = ja,
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

        //private void SaveCheckboxState()
        private void btn_save_Click(object sender, EventArgs e)
        {
            // Save checkbox states and associated files
            // You'll need to implement this method similar to the WPF version
            string anfahrtFolder = @"C:\temp\Anfahrt\"; // Replace with your desired folder path
            string abfahrtFolder = @"C:\temp\Abfahrt\"; // Replace with your desired folder path
            string reparaturFolder = @"C:\temp\Reparatur\"; // Replace with your desired folder path
            string JaFolder = @"C:\temp\Ja\"; // Replace with your desired folder path

            foreach (var excelFile in excelFiles)
            {
                if (excelFile.Anfahrt)
                {
                    SaveFileToLocalFolder(excelFile.FileName, excelFile.FilePath, anfahrtFolder);
                    UpdateCheckboxInDatabase(excelFile.FileName, "ANFAHRT", 1);
                }
                else
                {
                    UpdateCheckboxInDatabase(excelFile.FileName, "ANFAHRT", null);
                }

                if (excelFile.Abfahrt)
                {
                    SaveFileToLocalFolder(excelFile.FileName, excelFile.FilePath, abfahrtFolder);
                    UpdateCheckboxInDatabase(excelFile.FileName, "ABFAHRT", 1);
                }
                else
                {
                    UpdateCheckboxInDatabase(excelFile.FileName, "ABFAHRT", null);
                }

                if (excelFile.Reparatur)
                {
                    SaveFileToLocalFolder(excelFile.FileName, excelFile.FilePath, reparaturFolder);
                    UpdateCheckboxInDatabase(excelFile.FileName, "REPARATUR", 1);
                }
                else
                {
                    UpdateCheckboxInDatabase(excelFile.FileName, "REPARATUR", null);
                }
                if (excelFile.Ja)
                {
                    SaveFileToLocalFolder(excelFile.FileName, excelFile.FilePath, JaFolder);
                    UpdateCheckboxInDatabase(excelFile.FileName, "JA", 1);
                }
                else
                {
                    UpdateCheckboxInDatabase(excelFile.FileName, "JA", null);
                }
            }

            MessageBox.Show("Selected files saved to local folders and checkbox states updated in the database.");
        }
        //To UpdateCheckboxInDatabase
        private void UpdateCheckboxInDatabase(string fileName, string column, int? value)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["OracleConnection"].ConnectionString;

            using (OracleConnection connection = new OracleConnection(connectionString))
            {
                try
                {
                    connection.Open();

                    string updateQuery = $"UPDATE ZUORDNUNG_DOKU SET {column} = :value WHERE DATEINAME = :fileName";
                    using (OracleCommand command = new OracleCommand(updateQuery, connection))
                    {
                        command.Parameters.Add(new OracleParameter("value", value));
                        command.Parameters.Add(new OracleParameter("fileName", fileName));
                        command.ExecuteNonQuery();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error updating checkbox in database: {ex.Message}");
                }
            }
        }
        //With debugging codes (for SaveFileToLocalFolder)
        private void SaveFileToLocalFolder(string fileName, string sourceFilePath, string destinationFolder)
        {
            try
            {
                if (string.IsNullOrEmpty(sourceFilePath))
                {
                    MessageBox.Show($"Error saving file {fileName}: Source file path is null or empty.");
                    return;
                }

                string destinationPath = Path.Combine(destinationFolder, fileName);
                File.Copy(sourceFilePath, destinationPath, true);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error saving file {fileName}: {ex.Message}");
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

        //To display only the "FileName," "ANFAHRT," and "JA" columns in the dataGridView_Admin_CellContentClick event
        //handler when the "Anfahrt" button is clicked, you can use the following approach.
        //private DataTable filteredDataTable = new DataTable(); // Create a DataTable to store filtered data

        private bool isAnfahrtView = false; // Track whether the "Anfahrt" view is active
        private bool isAbfahrtView = false; // Track whether the "Abfahrt" view is active

        private void Anfahrt_btn_Click(object sender, EventArgs e)
        {
            // Set the current view state
            isAnfahrtView = true;
            isAbfahrtView = false;

            // Filter the DataTable to show only the "FileName," "ANFAHRT," and "JA" columns
            filteredDataTable = new DataTable();
            filteredDataTable.Columns.Add("FileName", typeof(string));
            filteredDataTable.Columns.Add("ANFAHRT", typeof(bool));
            //filteredDataTable.Columns.Add("JA", typeof(bool)); //Hide not to see this column
            foreach (var row in excelFiles)
            {
                if (row.Anfahrt) // Only add rows where the "ANFAHRT" checkbox is checked (true)
                {
                    //filteredDataTable.Rows.Add(row.FileName, row.Anfahrt, row.Reparatur);
                    filteredDataTable.Rows.Add(row.FileName, row.Anfahrt);
                }
            }

            // Set the filtered data as the DataGridView's DataSource
            dataGridView_Admin.DataSource = filteredDataTable;

            // Hide the "ANFAHRT" column
            dataGridView_Admin.Columns["ANFAHRT"].Visible = true;
        }


        //This returns the whole table
        //private DataTable filteredDataTable = new DataTable(); // Create a DataTable to store filtered data
        //private bool isAnfahrtFiltered = false; // Variable to track the current state

        //private void Anfahrt_btn_Click(object sender, EventArgs e)
        //{
        //    if (isAnfahrtFiltered)
        //    {
        //        // If already filtered, show the whole table
        //        dataGridView_Admin.DataSource = excelFiles;
        //    }
        //    else
        //    {
        //        // If not filtered, filter and show only checked "ANFAHRT" rows
        //        filteredDataTable = new DataTable();
        //        filteredDataTable.Columns.Add("FileName", typeof(string));
        //        filteredDataTable.Columns.Add("ANFAHRT", typeof(bool));
        //        filteredDataTable.Columns.Add("JA", typeof(bool));

        //        foreach (var row in excelFiles)
        //        {
        //            if (row.Anfahrt) // Only add rows where the "ANFAHRT" checkbox is checked (true)
        //            {
        //                filteredDataTable.Rows.Add(row.FileName, row.Anfahrt, row.Reparatur);
        //            }
        //        }

        //        dataGridView_Admin.DataSource = filteredDataTable;
        //    }

        //    // Toggle the state
        //    isAnfahrtFiltered = !isAnfahrtFiltered;
        //}

        ////Hide the "FilePath" column
        //private bool isFilteredView = false; // Track whether the filtered view is active

        //private void Anfahrt_btn_Click(object sender, EventArgs e)
        //{
        //    if (isFilteredView)
        //    {
        //        // If the filtered view is currently displayed, return to the entire table view
        //        dataGridView_Admin.Columns["ANFAHRT"].Visible = true;
        //        filteredDataTable.Dispose(); // Dispose the filteredDataTable
        //        dataGridView_Admin.DataSource = excelFiles;
        //        isFilteredView = false;
        //    }
        //    else
        //    {
        //        // Filter the DataTable to show only the "FileName," "ANFAHRT," and "JA" columns
        //        filteredDataTable = new DataTable();
        //        filteredDataTable.Columns.Add("FileName", typeof(string));
        //        filteredDataTable.Columns.Add("ANFAHRT", typeof(bool));
        //        filteredDataTable.Columns.Add("JA", typeof(bool));

        //        foreach (var row in excelFiles)
        //        {
        //            if (row.Anfahrt) // Only add rows where the "ANFAHRT" checkbox is checked (true)
        //            {
        //                filteredDataTable.Rows.Add(row.FileName, row.Anfahrt, row.Reparatur);
        //            }
        //        }

        //        // Hide the "ANFAHRT" column
        //        dataGridView_Admin.Columns["ANFAHRT"].Visible = false;

        //        // Set the filtered data as the DataGridView's DataSource
        //        dataGridView_Admin.DataSource = filteredDataTable;
        //        isFilteredView = true;
        //    }
        //}






        private void Abfahrt_btn_Click(object sender, EventArgs e)
        {
            // Set the current view state
            isAnfahrtView = false;
            isAbfahrtView = true;

            // Filter the DataTable to show only the "FileName," "ABFAHRT," and "JA" columns
            filteredDataTable = new DataTable();
            filteredDataTable.Columns.Add("FileName", typeof(string));
            filteredDataTable.Columns.Add("ABFAHRT", typeof(bool));
            //filteredDataTable.Columns.Add("JA", typeof(bool)); //Hide not to see this column
            foreach (var row in excelFiles)
            {
                if (row.Abfahrt) // Only add rows where the "ABFAHRT" checkbox is checked (true)
                {
                    //filteredDataTable.Rows.Add(row.FileName, row.Abfahrt, row.Reparatur);
                    filteredDataTable.Rows.Add(row.FileName, row.Abfahrt);
                }
            }

            // Set the filtered data as the DataGridView's DataSource
            dataGridView_Admin.DataSource = filteredDataTable;

            // Hide the "ABFAHRT" column
            dataGridView_Admin.Columns["ABFAHRT"].Visible = true;
        }


        private void Zurückkehren_btn_Click(object sender, EventArgs e)
        {
            if (isAnfahrtView)
            {
                // If the "Anfahrt" view is active, show the entire table again
                dataGridView_Admin.DataSource = excelFiles;
                isAnfahrtView = false;
            }
            else if (isAbfahrtView)
            {
                // If the "Abfahrt" view is active, show the entire table again
                dataGridView_Admin.DataSource = excelFiles;
                isAbfahrtView = false;
            }
            // Add more conditions for other views if needed
        }



        public class ExcelFile
        {
            public string FileName { get; set; }
            public string FilePath { get; set; }
            public bool Anfahrt { get; set; }
            public bool Abfahrt { get; set; }
            public bool Reparatur { get; set; }
            public bool Ja { get; set; }
            public bool IsChecked { get; set; }
        }

        
    }
}


//==========================Works well ===========================================
//using System;
//using System.Windows.Forms;
//using System.Data.OracleClient;
//using System.Collections.ObjectModel;

//using System;
//using System.Collections.ObjectModel;
//using System.Data;
//using System.Windows.Forms;
//using System.IO;
//using Oracle.ManagedDataAccess.Client;
//using System.Linq;
//using System.Diagnostics;
//using System.Configuration;
//using System.Data.Common;

//namespace WinFormsApp_Test
//{
//    public partial class Form1 : Form
//    {
//        private ObservableCollection<ExcelFile> excelFiles = new ObservableCollection<ExcelFile>();

//        public Form1()
//        {
//            InitializeComponent();

//            // Set panel2 to be visible initially
//            panel1.Visible = true;
//            panel2.Visible = false;
//            panel2.BackColor = Color.LightGreen;
//            btn_Administrator.BackColor = Color.PaleVioletRed;

//            // Load settings
//            Properties.Settings.Default.Reload();

//            // Load checkbox states when the application starts
//            LoadCheckboxState();

//            // Fetch data from Oracle and populate excelFiles collection
//            FetchFilesFromOracle();
//            dataGridView_Admin.DataSource = excelFiles;
//            //To hidde visibility of some Columns in dataGridView
//            dataGridView_Admin.Columns["FilePath"].Visible = false;
//            dataGridView_Admin.Columns["IsChecked"].Visible = false;
//        }

//        // Button click event to show panel1 (Anwender)
//        private void btn_Anwender_Click(object sender, EventArgs e)
//        {
//            // Show the first panel and hide the second panel.
//            panel1.Visible = true;
//            panel2.Visible = false;
//            btn_Anwender.BackColor = Color.LightGoldenrodYellow;
//        }

//        // Button click event to show panel2 (Administrator)
//        private void btn_Administrator_Click(object sender, EventArgs e)
//        {
//            // Show the second panel and hide the first panel.
//            panel1.Visible = false;
//            panel2.Visible = true;
//            panel2.BackColor = Color.LightGreen;
//            btn_Administrator.BackColor = Color.PaleVioletRed;
//        }

//        //
//        private void btn_UploadExcelFiles_Click(object sender, EventArgs e)
//        {
//            OpenFileDialog openFileDialog = new OpenFileDialog();
//            openFileDialog.Multiselect = true;
//            openFileDialog.Filter = "Excel Files (*.xls;*.xlsx)|*.xls;*.xlsx|All Files (*.*)|*.*";

//            if (openFileDialog.ShowDialog() == DialogResult.OK)
//            {
//                foreach (string filePath in openFileDialog.FileNames)
//                {
//                    string fileName = Path.GetFileName(filePath);
//                    excelFiles.Add(new ExcelFile { FileName = fileName, FilePath = filePath });
//                    UploadToOracle(fileName, filePath);
//                }
//            }
//        }

//        //Calling the dataGridView
//        private void dataGridView_Admin_CellContentClick(object sender, DataGridViewCellEventArgs e)
//        {
//            if (e.RowIndex >= 0 && e.ColumnIndex >= 0)
//            {
//                // Get the selected cell's value
//                DataGridViewCell cell = dataGridView_Admin.Rows[e.RowIndex].Cells[e.ColumnIndex];
//                string cellValue = cell.Value.ToString();

//                // You can now perform actions based on the cell value or row data
//                MessageBox.Show($"Cell clicked: {cellValue}");
//            }
//        }

//        //Save button generated from design 11-09-23
//        //private void btn_save_Click(object sender, EventArgs e)
//        //{
//        //    btn_save.BackColor = Color.LightBlue;
//        //}


//        private void UploadToOracle(string fileName, string filePath)
//        {
//            // Replace this with your Oracle database upload logic
//            // You'll need to adapt it for Oracle data access in WinForms
//            string connectionString = ConfigurationManager.ConnectionStrings["OracleConnection"].ConnectionString;

//            using (OracleConnection connection = new OracleConnection(connectionString))
//            {
//                try
//                {
//                    connection.Open();

//                    // Check if the file already exists in the table
//                    string checkQuery = "SELECT COUNT(*) FROM ZUORDNUNG_DOKU WHERE DATEINAME = :fileName";
//                    using (OracleCommand checkCommand = new OracleCommand(checkQuery, connection))
//                    {
//                        checkCommand.Parameters.Add(new OracleParameter("fileName", fileName));
//                        int existingCount = Convert.ToInt32(checkCommand.ExecuteScalar());

//                        if (existingCount > 0)
//                        {
//                            MessageBox.Show("File already exists in the table.");
//                            return; // Exit the method if the file exists
//                        }
//                    }

//                    // If the file doesn't exist, insert it into the table
//                    string insertQuery = "INSERT INTO ZUORDNUNG_DOKU (DATEINAME, URL_EXCEL) VALUES (:fileName, :filePath)";
//                    using (OracleCommand command = new OracleCommand(insertQuery, connection))
//                    {
//                        command.Parameters.Add(new OracleParameter("fileName", fileName));
//                        command.Parameters.Add(new OracleParameter("filePath", filePath));
//                        command.ExecuteNonQuery();
//                    }
//                }
//                catch (Exception ex)
//                {
//                    MessageBox.Show("Error uploading to Oracle: " + ex.Message);
//                }
//            }
//        }

//        private void FetchFilesFromOracle()
//        {
//            // Replace this with your Oracle database fetch logic
//            // Populate excelFiles collection with data from Oracle
//            excelFiles.Clear(); // Clear existing items from the ObservableCollection

//            string connectionString = ConfigurationManager.ConnectionStrings["OracleConnection"].ConnectionString;

//            using (OracleConnection connection = new OracleConnection(connectionString))
//            {
//                try
//                {
//                    connection.Open();
//                    //string query = "SELECT DATEINAME, URL_EXCEL, NVL(ANFAHRT,0) As ANFAHRT, NVL(ABFAHRT,0) AS ABFAHRT, NVL(REPARATUR,0) As REPARATUR FROM ZUORDNUNG_DOKU";
//                    string query = "SELECT DATEINAME, URL_EXCEL, NVL(ANFAHRT,0) As ANFAHRT, NVL(ABFAHRT,0) AS ABFAHRT, NVL(REPARATUR,0) As REPARATUR FROM ZUORDNUNG_DOKU";
//                    using (OracleCommand command = new OracleCommand(query, connection))
//                    {
//                        using (OracleDataReader reader = command.ExecuteReader())
//                        {
//                            while (reader.Read())
//                            {
//                                string fileName = reader["DATEINAME"].ToString();
//                                string filePath = reader["URL_EXCEL"].ToString();
//                                bool anfahrt = reader["ANFAHRT"].ToString() == "1";
//                                bool abfahrt = reader["ABFAHRT"].ToString() == "1";
//                                bool reparatur = reader["REPARATUR"].ToString() == "1";

//                                excelFiles.Add(new ExcelFile
//                                {
//                                    FileName = fileName,
//                                    FilePath = filePath,
//                                    Anfahrt = anfahrt,
//                                    Abfahrt = abfahrt,
//                                    Reparatur = reparatur,
//                                    IsChecked = false // Initialize IsChecked to false initially
//                                });
//                            }
//                        }
//                    }
//                }
//                catch (Exception ex)
//                {
//                    MessageBox.Show("Error fetching data from Oracle: " + ex.Message);
//                }
//            }
//        }

//        //private void SaveCheckboxState()
//        private void btn_save_Click(object sender, EventArgs e)
//        {
//            // Save checkbox states and associated files
//            // You'll need to implement this method similar to the WPF version
//            string anfahrtFolder = @"C:\temp\Anfahrt\"; // Replace with your desired folder path
//            string abfahrtFolder = @"C:\temp\Abfahrt\"; // Replace with your desired folder path
//            string reparaturFolder = @"C:\temp\Reparatur\"; // Replace with your desired folder path

//            foreach (var excelFile in excelFiles)
//            {
//                if (excelFile.Anfahrt)
//                {
//                    SaveFileToLocalFolder(excelFile.FileName, excelFile.FilePath, anfahrtFolder);
//                    UpdateCheckboxInDatabase(excelFile.FileName, "ANFAHRT", 1);
//                }
//                else
//                {
//                    UpdateCheckboxInDatabase(excelFile.FileName, "ANFAHRT", null);
//                }

//                if (excelFile.Abfahrt)
//                {
//                    SaveFileToLocalFolder(excelFile.FileName, excelFile.FilePath, abfahrtFolder);
//                    UpdateCheckboxInDatabase(excelFile.FileName, "ABFAHRT", 1);
//                }
//                else
//                {
//                    UpdateCheckboxInDatabase(excelFile.FileName, "ABFAHRT", null);
//                }

//                if (excelFile.Reparatur)
//                {
//                    SaveFileToLocalFolder(excelFile.FileName, excelFile.FilePath, reparaturFolder);
//                    UpdateCheckboxInDatabase(excelFile.FileName, "REPARATUR", 1);
//                }
//                else
//                {
//                    UpdateCheckboxInDatabase(excelFile.FileName, "REPARATUR", null);
//                }
//            }

//            MessageBox.Show("Selected files saved to local folders and checkbox states updated in the database.");
//        }
//        //To UpdateCheckboxInDatabase
//        private void UpdateCheckboxInDatabase(string fileName, string column, int? value)
//        {
//            string connectionString = ConfigurationManager.ConnectionStrings["OracleConnection"].ConnectionString;

//            using (OracleConnection connection = new OracleConnection(connectionString))
//            {
//                try
//                {
//                    connection.Open();

//                    string updateQuery = $"UPDATE ZUORDNUNG_DOKU SET {column} = :value WHERE DATEINAME = :fileName";
//                    using (OracleCommand command = new OracleCommand(updateQuery, connection))
//                    {
//                        command.Parameters.Add(new OracleParameter("value", value));
//                        command.Parameters.Add(new OracleParameter("fileName", fileName));
//                        command.ExecuteNonQuery();
//                    }
//                }
//                catch (Exception ex)
//                {
//                    MessageBox.Show($"Error updating checkbox in database: {ex.Message}");
//                }
//            }
//        }
//        //With debugging codes (for SaveFileToLocalFolder)
//        private void SaveFileToLocalFolder(string fileName, string sourceFilePath, string destinationFolder)
//        {
//            try
//            {
//                if (string.IsNullOrEmpty(sourceFilePath))
//                {
//                    MessageBox.Show($"Error saving file {fileName}: Source file path is null or empty.");
//                    return;
//                }

//                string destinationPath = Path.Combine(destinationFolder, fileName);
//                File.Copy(sourceFilePath, destinationPath, true);
//            }
//            catch (Exception ex)
//            {
//                MessageBox.Show($"Error saving file {fileName}: {ex.Message}");
//            }
//        }


//        private void LoadCheckboxState()
//        {
//            // Load checkbox states from settings
//            // You'll need to implement this method similar to the WPF version
//            //MessageBox.Show("Loading checkbox states"); // Add this line for debugging ==============================>

//            string checkboxStateString = Properties.Settings.Default.CheckboxStates;

//            if (checkboxStateString != null)
//            {
//                string[] checkboxStateArray = checkboxStateString.Split(',');

//                for (int i = 0; i < checkboxStateArray.Length && i < excelFiles.Count; i++)
//                {
//                    if (bool.TryParse(checkboxStateArray[i], out bool isChecked))
//                    {
//                        excelFiles[i].IsChecked = isChecked;
//                    }
//                }
//            }
//        }

//        private void Anfahrt_btn_Click(object sender, EventArgs e)
//        {

//        }

//        private void Abfahrt_btn_Click(object sender, EventArgs e)
//        {

//        }


//        public class ExcelFile
//        {
//            public string FileName { get; set; }
//            public string FilePath { get; set; }
//            public bool Anfahrt { get; set; }
//            public bool Abfahrt { get; set; }
//            public bool Reparatur { get; set; }
//            public bool IsChecked { get; set; }
//        }


//    }
//}
