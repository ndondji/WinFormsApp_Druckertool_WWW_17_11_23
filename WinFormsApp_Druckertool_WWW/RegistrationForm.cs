using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

//using OfficeOpenXml;
using ClosedXML.Excel;

namespace WinFormsApp_Druckertool_WWW
{
    public partial class administratorForm : System.Windows.Forms.Form
    {
        private ObservableCollection<ExcelFile> excelFiles = new ObservableCollection<ExcelFile>();
        //private bool anfKapaz;
        //private bool abfRep;
        //private bool abfKapaz;
        //private bool anfRepIsNull;
        //private readonly bool anfRep;

        public administratorForm()
        {
            InitializeComponent();

            // Load settings
            Properties.Settings.Default.Reload();

            // Load checkbox states when the application starts
            LoadCheckboxState();

            // Fetch data from Oracle and populate excelFiles collection
            FetchFilesFromOracle();
            dataGridView1.DataSource = excelFiles;

            // To hide visibility of some Columns in dataGridView
            dataGridView1.Columns["FilePath"].Visible = false;
            dataGridView1.Columns["IsChecked"].Visible = false;

            int commonWidth = 155; // Common width for most columns
            int increasedWidth = 200; // Adjust the increased width as needed
            int decreasedWidth = 100; // Adjust the decreased width as needed

            foreach (DataGridViewColumn column in dataGridView1.Columns)
            {
                // Set a common width for most columns
                column.Width = commonWidth;

                // Adjust sizes of specific columns
                if (column.DataPropertyName == "Anf_Rep_Druc" ||
                    column.DataPropertyName == "Anf_Kapaz_Druc" ||
                    column.DataPropertyName == "Abf_Kapaz_Druc" ||
                    column.DataPropertyName == "Abf_Rep_Druc")
                {
                    // Increase the size of selected columns
                    column.Width = increasedWidth;
                }
                else if (column.DataPropertyName == "Anf_Rep" ||
                         column.DataPropertyName == "Anf_Kapaz" ||
                         column.DataPropertyName == "Abf_Rep" ||
                         column.DataPropertyName == "Abf_Kapaz")
                {
                    // Decrease the size of selected columns
                    column.Width = decreasedWidth;
                }

                // Adjust column widths and Padding for "Dateinamen"
                if (column.DataPropertyName == "Dateinamen")
                {
                    column.Width = 435; // Adjust the width for ColumnName1
                    column.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleLeft;
                }
                //else
                //{
                //    // Allow the remaining columns to adjust automatically based on content
                //    column.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCellsExceptHeader;
                //}

                // Adjust the padding for each column to increase the table width
                column.DefaultCellStyle.Padding = new Padding(6, 0, 6, 0); // Adjust padding as needed

                // Break the header text into multiple lines using line breaks ("\n")
                column.HeaderText = column.HeaderText.Replace("_", "_\n");
            }

            // Increase the width of the entire DataGridView
            //dataGridView1.Width = 2200; // Adjust the width as needed


            //InitializeComponent();

            //// Load settings
            //Properties.Settings.Default.Reload();

            //// Load checkbox states when the application starts
            //LoadCheckboxState();

            //// Fetch data from Oracle and populate excelFiles collection
            //FetchFilesFromOracle();
            //dataGridView1.DataSource = excelFiles;
            ////To hidde visibility of some Columns in dataGridView
            //dataGridView1.Columns["FilePath"].Visible = false;
            //dataGridView1.Columns["IsChecked"].Visible = false;

            //dataGridView1.DataSource = excelFiles;

            //int desiredWidth = 135; // Adjust the width as needed
            //foreach (DataGridViewColumn column in dataGridView1.Columns)
            //{
            //    // Set a common width for all columns
            //    column.Width = desiredWidth;

            //    // Adjust column widths and Padding for "Dateinamen"
            //    dataGridView1.Columns["Dateinamen"].Width = 450; // Adjust the width for ColumnName1
            //    dataGridView1.Columns["Dateinamen"].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleLeft;

            //}

        }
        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void save_admin_btn_Click(object sender, EventArgs e)
        {
            //this.Close();
        }


        private void upload_exl_files_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Multiselect = true;
            openFileDialog.Filter = "Excel Files (*.xls;*.xlsx)|*.xls;*.xlsx|All Files (*.*)|*.*";

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                foreach (string filePath in openFileDialog.FileNames)
                {
                    string fileName = Path.GetFileName(filePath);
                    excelFiles.Add(new ExcelFile { Dateinamen = fileName, FilePath = filePath });
                    UploadToOracle(fileName, filePath);
                }
            }
        }

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
                    string checkQuery = "SELECT COUNT(*) FROM ZUORDNUNG_DROOK WHERE DATEINAMEN = :fileName";
                    using (OracleCommand checkCommand = new OracleCommand(checkQuery, connection))
                    {
                        checkCommand.Parameters.Add(new OracleParameter("fileName", fileName));
                        int existingCount = Convert.ToInt32(checkCommand.ExecuteScalar());

                        if (existingCount > 0)
                        {
                            MessageBox.Show("Die Datei ist bereits in der Tabelle vorhanden.");
                            return; // Exit the method if the file exists
                        }
                    }

                    // If the file doesn't exist, insert it into the table
                    string insertQuery = "INSERT INTO ZUORDNUNG_DROOK (DATEINAMEN, URL_EXCL) VALUES (:fileName, :filePath)";
                    using (OracleCommand command = new OracleCommand(insertQuery, connection))
                    {
                        command.Parameters.Add(new OracleParameter("fileName", fileName));
                        command.Parameters.Add(new OracleParameter("filePath", filePath));
                        command.ExecuteNonQuery();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Fehler beim Hochladen nach Oracle: " + ex.Message);
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
                    //string query = "SELECT DATEINAME, URL_EXCEL, NVL(ANFAHRT,0) As ANFAHRT, NVL(ABFAHRT,0) AS ABFAHRT, NVL(REPARATUR,0) As REPARATUR FROM ZUORDNUNG_DROOK";
                    //string query = "SELECT DATEINAMEN, URL_EXCL, ANF_REP, ANF_KAPAZ, ABF_REP, ABF_KAPAZ, UEBERGABE FROM ZUORDNUN_DROOK";
                    //string query = "SELECT DATEINAMEN, URL_EXCL, NVL(ANF_REP,0) AS ANF_REP, NVL(ANF_KAPAZ,0) AS ANF_KAPAZ, NVL(ABF_REP,0) AS ABF_REP," +
                    //    " NVL(ABF_KAPAZ,0) AS ABF_KAPAZ, NVL(UEBERGABE,0) AS UEBERGABE FROM USR_TEST.ZUORDNUNG_DROOK";
                    string query = "SELECT DATEINAMEN, URL_EXCL, NVL(ANF_REP,0) AS ANF_REP, NVL(ANF_KAPAZ,0) AS ANF_KAPAZ, " +
                        "NVL(ABF_REP,0) AS ABF_REP, NVL(ABF_KAPAZ,0) AS ABF_KAPAZ, NVL(UEBERGABE,0) AS UEBERGABE, " +
                        "NVL(ANF_REP_DRUC,0) AS ANF_REP_DRUC, NVL(ANF_KAPAZ_DRUC,0) AS ANF_KAPAZ_DRUC, NVL(ABF_REP_DRUC,0) AS ABF_REP_DRUC, " +
                        "NVL(ABF_KAPAZ_DRUC,0) AS ABF_KAPAZ_DRUC  FROM USR_TEST.ZUORDNUNG_DROOK";
                    using (OracleCommand command = new OracleCommand(query, connection))
                    {
                        using (OracleDataReader reader = command.ExecuteReader())
                        {
                            //Code to be used with the other Table
                            //while (reader.Read())
                            //{
                            //    string fileName = reader["DATEINAME"].ToString();
                            //    string filePath = reader["URL_EXCEL"].ToString();
                            //    bool anfahrt = reader["ANFAHRT"].ToString() == "1";
                            //    bool abfahrt = reader["ABFAHRT"].ToString() == "1";
                            //    bool reparatur = reader["REPARATUR"].ToString() == "1";

                            //    excelFiles.Add(new ExcelFile
                            //    {
                            //        Dateinamen = fileName,
                            //        FilePath = filePath,
                            //        Anfahrt = anfahrt,
                            //        Abfahrt = abfahrt,
                            //        Reparatur = reparatur,
                            //        IsChecked = false // Initialize IsChecked to false initially
                            //    });
                            //}
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

                                //This helps to reset in the DB to null
                                //bool anfRep = (reader["ANF_REP"] != DBNull.Value) && (reader["ANF_REP"].ToString() == "1");

                                excelFiles.Add(new ExcelFile
                                {
                                    Dateinamen = fileName,
                                    FilePath = filePath,
                                    Anfahrt_Reparatur = anfRep,
                                    Anfahrt_Kapazität = anfKapaz,
                                    Abfahrt_Reparatur = abfRep,
                                    Abfahrt_Kapazität = abfKapaz,
                                    Uebergabe = uebergabe,
                                    Anfahrt_Reparatur_Druck = anfRepDruc,
                                    Anfahrt_Kapazität_Druck = anfKapazDruc,
                                    Abfahrt_Reparatur_Druck = abfRepDruc,
                                    Abfahrt_Kapazität_Druck = abfKapazDruc,
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

        private void save_btn_Click(object sender, EventArgs e)
        {
            // Save checkbox states and associated files for "Save" folders
            // You'll need to implement this method similar to the WPF version
            string AnfRepFolder = @"C:\temp\AnfRep\"; // Replace with your desired folder path
            string AnfKapazFolder = @"C:\temp\AnfKapaz\"; // Replace with your desired folder path
            string AbfRepFolder = @"C:\temp\AbfRep\"; // Replace with your desired folder path
            string AbfKapazFolder = @"C:\temp\AbfKapaz\"; // Replace with your desired folder path
            string UebergabeFolder = @"C:\temp\Uebergabe\"; // Replace with your desired folder path

            // Save checkbox states and associated files for "Druc" folders
            string AnfRepDrucFolder = @"C:\temp\AnfRepDruc"; // Replace with your desired folder path
            string AnfKapazDrucFolder = @"C:\temp\AnfKapazDruc\"; // Replace with your desired folder path
            string AbfRepDrucFolder = @"C:\temp\AbfRepDruc\"; // Replace with your desired folder path
            string AbfKapazDrucFolder = @"C:\temp\AbfKapazDruc\"; // Replace with your desired folder path

            HashSet<string> downloadedFiles = new HashSet<string>();

            foreach (var excelFile in excelFiles)
            {
                if (excelFile.Anfahrt_Reparatur)
                {
                    SaveFileToLocalFolder(excelFile.Dateinamen, excelFile.FilePath, AnfRepFolder);
                    UpdateCheckboxInDatabase(excelFile.Dateinamen, "ANF_REP", 1);
                }
                else
                {
                    UpdateCheckboxInDatabase(excelFile.Dateinamen, "ANF_REP", null);
                }

                if (excelFile.Anfahrt_Kapazität)
                {
                    SaveFileToLocalFolder(excelFile.Dateinamen, excelFile.FilePath, AnfKapazFolder);
                    UpdateCheckboxInDatabase(excelFile.Dateinamen, "ANF_KAPAZ", 1);
                }
                else
                {
                    UpdateCheckboxInDatabase(excelFile.Dateinamen, "ANF_KAPAZ", null);
                }

                if (excelFile.Abfahrt_Reparatur)
                {
                    SaveFileToLocalFolder(excelFile.Dateinamen, excelFile.FilePath, AbfRepFolder);
                    UpdateCheckboxInDatabase(excelFile.Dateinamen, "ABF_Rep", 1);
                }
                else
                {
                    UpdateCheckboxInDatabase(excelFile.Dateinamen, "ABF_Rep", null);
                }
                if (excelFile.Abfahrt_Kapazität)
                {
                    SaveFileToLocalFolder(excelFile.Dateinamen, excelFile.FilePath, AbfKapazFolder);
                    UpdateCheckboxInDatabase(excelFile.Dateinamen, "ABF_KAPAZ", 1);
                }
                else
                {
                    UpdateCheckboxInDatabase(excelFile.Dateinamen, "ABF_KAPAZ", null);
                }
                if (excelFile.Uebergabe)
                {
                    SaveFileToLocalFolder(excelFile.Dateinamen, excelFile.FilePath, UebergabeFolder);
                    UpdateCheckboxInDatabase(excelFile.Dateinamen, "UEBERGABE", 1);
                }
                else
                {
                    UpdateCheckboxInDatabase(excelFile.Dateinamen, "UEBERGABE", null);
                }
                if (excelFile.Anfahrt_Reparatur_Druck)
                {
                    SaveFileToLocalFolder(excelFile.Dateinamen, excelFile.FilePath, AnfRepDrucFolder);
                    UpdateCheckboxInDatabase(excelFile.Dateinamen, "ANF_REP_DRUC", 1);
                }
                else
                {
                    UpdateCheckboxInDatabase(excelFile.Dateinamen, "ANF_REP_DRUC", null);
                }

                if (excelFile.Anfahrt_Kapazität_Druck)
                {
                    SaveFileToLocalFolder(excelFile.Dateinamen, excelFile.FilePath, AnfKapazDrucFolder);
                    UpdateCheckboxInDatabase(excelFile.Dateinamen, "ANF_KAPAZ_DRUC", 1);
                }
                else
                {
                    UpdateCheckboxInDatabase(excelFile.Dateinamen, "ANF_KAPAZ_DRUC", null);
                }

                if (excelFile.Abfahrt_Reparatur_Druck)
                {
                    SaveFileToLocalFolder(excelFile.Dateinamen, excelFile.FilePath, AbfRepDrucFolder);
                    UpdateCheckboxInDatabase(excelFile.Dateinamen, "ABF_Rep_DRUC", 1);
                }
                else
                {
                    UpdateCheckboxInDatabase(excelFile.Dateinamen, "ABF_Rep_DRUC", null);
                }
                if (excelFile.Abfahrt_Kapazität_Druck)
                {
                    SaveFileToLocalFolder(excelFile.Dateinamen, excelFile.FilePath, AbfKapazDrucFolder);
                    UpdateCheckboxInDatabase(excelFile.Dateinamen, "ABF_KAPAZ_DRUC", 1);
                }
                else
                {
                    UpdateCheckboxInDatabase(excelFile.Dateinamen, "ABF_KAPAZ_DRUC", null);
                }

            }

            MessageBox.Show("Ausgewählte Dateien werden in lokalen Ordnern gespeichert und die Status der Kontrollkästchen werden in der Datenbank aktualisiert.");
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

                    string updateQuery = $"UPDATE USR_TEST.ZUORDNUNG_DROOK SET {column} = :value WHERE DATEINAMEN = :fileName";
                    using (OracleCommand command = new OracleCommand(updateQuery, connection))
                    {
                        command.Parameters.Add(new OracleParameter("value", value));
                        command.Parameters.Add(new OracleParameter("fileName", fileName));
                        command.ExecuteNonQuery();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Fehler beim Aktualisieren des Kontrollkästchens in der Datenbank: {ex.Message}");
                }
            }
        }

        ////With debugging codes (for SaveFileToLocalFolder)-->it used to work, not anymore(14-11-23)
        //private void SaveFileToLocalFolder(string fileName, string sourceFilePath, string destinationFolder)
        //{
        //    try
        //    {
        //        if (string.IsNullOrEmpty(sourceFilePath))
        //        {
        //            MessageBox.Show($"Error saving file {fileName}: Source file path is null or empty.");
        //            return;
        //        }

        //        string destinationPath = Path.Combine(destinationFolder, fileName);
        //        File.Copy(sourceFilePath, destinationPath, true);
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show($"Error saving file {fileName}: {ex.Message}");
        //    }
        //}
        private void SaveFileToLocalFolder(string fileName, string sourceFilePath, string destinationFolder)
        {
            try
            {
                if (string.IsNullOrEmpty(sourceFilePath))
                {
                    MessageBox.Show($"Error saving file {fileName}: Source file path is null or empty.");
                    return;
                }

                if (!File.Exists(sourceFilePath))
                {
                    MessageBox.Show($"Error saving file {fileName}: Source file does not exist.");
                    return;
                }

                if (!Directory.Exists(destinationFolder))
                {
                    MessageBox.Show($"Error saving file {fileName}: Destination folder does not exist.");
                    return;
                }

                string destinationPath = Path.Combine(destinationFolder, fileName);

                // Check if the file already exists in the destination folder
                if (File.Exists(destinationPath))
                {
                    DialogResult result = MessageBox.Show($"The file {fileName} already exists. Do you want to replace it?", "File Exists", MessageBoxButtons.YesNoCancel);

                    if (result == DialogResult.No || result == DialogResult.Cancel)
                    {
                        // User chose not to replace the file or canceled the operation
                        return;
                    }
                    // If result is Yes, continue to replace the file
                }

                File.Copy(sourceFilePath, destinationPath, true);
            }
            catch (FileNotFoundException ex)
            {
                MessageBox.Show($"Error saving file {fileName}: {ex.Message}");
            }
            catch (DirectoryNotFoundException ex)
            {
                MessageBox.Show($"Error saving file {fileName}: {ex.Message}");
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


        public class ExcelFile
        {
            public string Dateinamen { get; set; }
            public string FilePath { get; set; }
            public bool Anfahrt_Reparatur { get; set; }
            public bool Anfahrt_Reparatur_Druck { get; set; }
            public bool Anfahrt_Kapazität { get; set; }
            public bool Anfahrt_Kapazität_Druck { get; set; }
            public bool Abfahrt_Reparatur { get; set; }
            public bool Abfahrt_Reparatur_Druck { get; set; }
            public bool Abfahrt_Kapazität { get; set; }
            public bool Abfahrt_Kapazität_Druck { get; set; }
            public bool Uebergabe { get; set; }
            public bool IsChecked { get; set; }

        }

    }
}
