using System;
using System.IO;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using Excel = Microsoft.Office.Interop.Excel;

using Oracle.ManagedDataAccess.Client;
using System.Collections.ObjectModel;
using System.Configuration;
using ClosedXML.Excel;

using OfficeOpenXml.Core.ExcelPackage;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace WinFormsApp_Druckertool_WWW
{
    public partial class AnwenderForm_2 : Form
    {
        private string filePath;
        private int userPrefix;
        // Define a field to store the original file name
        //private string originalFileName = string.Empty; // Field to store the original file name
        private string originalFileName = null; // Field to store the original file name
        private object selectedRow;

        // Define the DataGridView
        //private DataGridView dataGridView1;

        public AnwenderForm_2()
        {
            InitializeComponent();

            // Call a method to create and configure the DataGridView
            InitializeDataGridView();

            // Set the DataGridView size
            dataGridView1.Width = 660;  // Adjust the width as needed
            dataGridView1.Height = 340; // Adjust the height as needed

            // Increase the height of the row header for the header row
            dataGridView1.ColumnHeadersHeight = 50;
            // Set the row header height
            dataGridView1.RowTemplate.Height = 30; // Adjust the height as needed


            // Customize the column header font
            DataGridViewCellStyle columnHeaderStyle = new DataGridViewCellStyle
            {
                Font = new Font(dataGridView1.Font.FontFamily, 14, FontStyle.Bold), // Make the header text bold
                ForeColor = Color.Black, // You can change the font color if needed
                Alignment = DataGridViewContentAlignment.MiddleCenter // Center the header text
            };

            // Apply the header style to each column
            foreach (DataGridViewColumn column in dataGridView1.Columns)
            {
                column.HeaderCell.Style = columnHeaderStyle;
            }

            // Set the font for DataGridView cells
            //dataGridView1.DefaultCellStyle.Font = new Font(dataGridView1.Font, FontStyle.Bold);

        }



        private void InitializeDataGridView()
        {
            // Create the DataGridView
            dataGridView1 = new DataGridView
            {
                Name = "dataGridView1",
                Dock = DockStyle.None,
                //Width = 600,  // Adjust the width as needed
                //Height = 300, // Adjust the width as needed
                //Dock = DockStyle.Fill, // Fill the parent container
                AutoGenerateColumns = false // Disable auto-generated columns
            };

            // Create and add columns
            DataGridViewTextBoxColumn indexColumn = new DataGridViewTextBoxColumn
            {
                Name = "Index",
                HeaderText = "Index",
                DataPropertyName = "Index"
            };
            dataGridView1.Columns.Add(indexColumn);

            DataGridViewTextBoxColumn dateinamenColumn = new DataGridViewTextBoxColumn
            {
                Name = "Dateinamen",
                HeaderText = "Dateinamen",
                DataPropertyName = "Dateinamen"
            };
            dataGridView1.Columns.Add(dateinamenColumn);

            //1st column of checkboxes "ReparaturAnfahrt"
            //DataGridViewCheckBoxColumn reparaturAnfahrtColumn = new DataGridViewCheckBoxColumn
            //{
            //    Name = "ReparaturAnfahrt",
            //    HeaderText = "Speich ",
            //    DataPropertyName = "ReparaturAnfahrt"
            //};
            //dataGridView1.Columns.Add(reparaturAnfahrtColumn);

            //Second column of checkboxes "DruckDoku"
            DataGridViewCheckBoxColumn speichernColumn = new DataGridViewCheckBoxColumn
            {
                Name = "Speichern",
                HeaderText = "Speichern ",
                DataPropertyName = "Speichern"
            };
            dataGridView1.Columns.Add(speichernColumn);

            //Second column of checkboxes "DruckDoku"
            DataGridViewCheckBoxColumn druckDokuColumn = new DataGridViewCheckBoxColumn
            {
                Name = "DruckDoku",
                HeaderText = "Druck ",
                DataPropertyName = "DruckDoku"
            };
            dataGridView1.Columns.Add(druckDokuColumn);

            // Add the DataGridView to the form's controls
            Controls.Add(dataGridView1);

            // Set the location of the DataGridView in the middle of the form
            int x = (this.ClientSize.Width - dataGridView1.Width) / 2 - 120; // 120 pixels (5 cm) to the left
            int y = (this.ClientSize.Height - dataGridView1.Height) / 2 - 10; //10 pixels(1 cm) up
            dataGridView1.Location = new Point(x, y);

            //AutoSizeColumnsMode
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            //Font Size
            dataGridView1.DefaultCellStyle.Font = new Font("Arial", 12); // Set the font to Arial with size 12


            // Set the AutoSizeColumnsMode property to Fill
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            // Set the width of the "T-F" column to 50 pixels
            dataGridView1.Columns["Index"].Width = 24;
            // Set the width of the "Dateinamen" column
            dataGridView1.Columns["Dateinamen"].Width = 190;
            // Set the width of the "ReparaturAnfahrt" column
            //dataGridView1.Columns["ReparaturAnfahrt"].Width = 50;
            dataGridView1.Columns["Speichern"].Width = 50;
        }



        //dataGridView
        //private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        //{
        //    // Get the original filename when a cell is clicked
        //    if (e.RowIndex >= 0 && e.RowIndex < dataGridView1.Rows.Count)
        //    {
        //        originalFileName = dataGridView1.Rows[e.RowIndex].Cells["Dateinamen"].Value?.ToString();
        //    }
        //}

        private void Anfahrt_btn_Click(object sender, EventArgs e)
        {

        }

        //Reparatur-Anf
        private void reparatur_anf_btn_Click(object sender, EventArgs e)
        {
            // Define a list to store the data
            List<object> data = new List<object>();

            // Specify the directory path
            string directoryPath = @"C:\temp\AnfRep";

            // Check if the directory exists
            if (Directory.Exists(directoryPath))
            {
                // Get the Excel files in the directory
                string[] excelFiles = Directory.GetFiles(directoryPath, "*.xlsx");

                // Add data to the list
                int index = 1;
                foreach (string excelFile in excelFiles)
                {
                    data.Add(new
                    {
                        Index = index,
                        Dateinamen = Path.GetFileName(excelFile),
                        ReparaturAnfahrt = false // Default value for the checkboxes
                    });
                    index++;
                }
            }

            // Set the list as the data source for the DataGridView
            dataGridView1.DataSource = data;

            // Change the header text of the "Reparatur-Anfahrt" column
            //dataGridView1.Columns["ReparaturAnfahrt"].HeaderText = "Reparatur-Anfahrt";
            dataGridView1.Columns["DruckDoku"].HeaderText = "Reparatur-Anfahrt";
        }



        private void kapazitaet_anf_btn_Click(object sender, EventArgs e)
        {
            // Define a list to store the data
            List<object> data = new List<object>();

            // Specify the directory path
            string directoryPath = @"C:\temp\AnfKapaz";

            // Check if the directory exists
            if (Directory.Exists(directoryPath))
            {
                // Get the Excel files in the directory
                string[] excelFiles = Directory.GetFiles(directoryPath, "*.xlsx");

                // Add data to the list
                int index = 1;
                foreach (string excelFile in excelFiles)
                {
                    data.Add(new
                    {
                        Index = index,
                        Dateinamen = Path.GetFileName(excelFile),
                        ReparaturAnfahrt = false // Default value for the checkboxes
                    });
                    index++;
                }
            }

            // Set the list as the data source for the DataGridView
            dataGridView1.DataSource = data;

            // Change the header text of the "Reparatur-Anfahrt" column to "Kapazität-Anfahrt"
            dataGridView1.Columns["Speichern"].HeaderText = "Kapazität-Anfahrt";
            dataGridView1.Columns["DruckDoku"].HeaderText = "Kapazität-Anfahrt";

           

        }



        private void Abfahrt_btn_Click(object sender, EventArgs e)
        {

        }

        private void reparatur_abfahrt_btn_Click(object sender, EventArgs e)
        {

        }

        private void kapazitaet_abfahrt_btn_Click(object sender, EventArgs e)
        {

        }

        //Placeholder
        //private void textBox1_TextChanged(object sender, EventArgs e)
        //{
        //    // Get the user input from textBox1
        //    string userPrefix = textBox1.Text;

        //    // Check if there is a selected row in the DataGridView
        //    if (dataGridView1.SelectedRows.Count > 0)
        //    {
        //        // Get the selected row
        //        DataGridViewRow selectedRow = dataGridView1.SelectedRows[0];

        //        // Check if the "ReparaturAnfahrt" checkbox in the selected row is checked
        //        if (selectedRow.Cells["Speichern"].Value != null && (bool)selectedRow.Cells["Speichern"].Value)
        //        {
        //            // Get the original filename from the selected row
        //            string originalFileName = selectedRow.Cells["Dateinamen"].Value.ToString();

        //            // Check if user input matches the criteria
        //            if (Regex.IsMatch(userPrefix, "^[A-Za-z]{2}\\d{2}-\\d{2}$"))
        //            {
        //                // Modify the filename by adding user input before it
        //                string newFileName = $"{userPrefix}-{originalFileName}";

        //                // Specify the folder path where the Excel files are located
        //                string folderPath = @"C:\temp\AnfRep";

        //                // Combine the folder path and the new filename
        //                string filePath = Path.Combine(folderPath, newFileName);

        //                // Copy the original file to the new location
        //                File.Copy(Path.Combine(folderPath, originalFileName), filePath);

        //                // Optionally, you can delete the original file if needed
        //                // File.Delete(Path.Combine(folderPath, originalFileName));
        //            }
        //        }
        //    }
        //}

        //This makes the loop infinite
        //private void textBox1_TextChanged(object sender, EventArgs e)
        //{
        //    // Get the user input from textBox1
        //    string userPrefix = textBox1.Text;

        //    // Convert the user input to uppercase
        //    //userPrefix = userPrefix.ToUpper();
        //    //userPrefix = userPrefix.ToUpperInvariant();


        //    // Check if there is a selected row in the DataGridView
        //    if (dataGridView1.SelectedRows.Count > 0)
        //    {
        //        // Get the selected row
        //        DataGridViewRow selectedRow = dataGridView1.SelectedRows[0];

        //        // Check if the "ReparaturAnfahrt" checkbox in the selected row is checked
        //        if (selectedRow.Cells["Speichern"].Value != null && (bool)selectedRow.Cells["Speichern"].Value)
        //        {
        //            // Get the original filename from the selected row
        //            string originalFileName = selectedRow.Cells["Dateinamen"].Value.ToString();

        //            // Combine the folder path and the new filename
        //            string filePath = Path.Combine(@"C:\temp\AnfRep", originalFileName);

        //            // Check if user input matches the criteria
        //            if (Regex.IsMatch(userPrefix, @"^[A-Za-z]{2}\d{2}-\d{2}$"))
        //            {
        //                // Modify the filename by adding user input before it
        //                string newFileName = $"{userPrefix}_{originalFileName}";

        //                // Create a new Excel file or open an existing one
        //                //using (var package = new ExcelPackage(new FileInfo(filePath)))
        //                //{
        //                //    // Get the first worksheet in the Excel file
        //                //    var worksheet = package.Workbook.Worksheets[1];

        //                //    // Write the user input in cell A1 (first row, first column)
        //                //    //worksheet.Cells["A1"].Value = userPrefix; //Always remember, this gives error always!
        //                //    worksheet.Cell(1, 1).Value = userPrefix;

        //                //    // Save the changes to the Excel file
        //                //    package.Save();
        //                //}
        //            }
        //        }
        //    }
        //}
        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            // The event handler should only update the userPrefix variable based on the input.
            string userPrefix = textBox1.Text;

            // Convert the user input to uppercase
            //userPrefix = userPrefix.ToUpper();

            if (Regex.IsMatch(userPrefix, @"^[A-Za-z]{2}\d{2}-\d{2}$"))
            {
                // Display a message if user input doesn't match the pattern
                //MessageBox.Show("Please enter a valid input matching the pattern: XX99-99 (e.g., AB12-34).");

                // Enable Anfahrt, Abfahrt, and Uebergabe buttons
                //Anfahrt_btn.Enabled = true;
                //Abfahrt_btn.Enabled = true;
                //Uebergabe_btn.Enabled = true;
                //Rückwärts_btn.Enabled = true;

                //// Disable the other buttons
                //Reparatur_Anfahrt_btn.Enabled = false;
                //Kapazitaet_Anfahrt_btn.Enabled = false;
                //Reparatur_abfahrt_btn.Enabled = false;
                //Kapazitaet_Abfahrt_btn.Enabled = false;

                //// Show the DataGridView when the condition is met
                //ShowDataGridView(false);

                //// Hide the message label when the condition is satisfied
                //label_message.Visible = false;
            }
            else
            {
                // Display a message if user input doesn't match the pattern
                //MessageBox.Show("Please enter a valid input matching the pattern: XX99-99 (e.g., AB12-34).");

                // Reset all buttons to their initial state
                //Anfahrt_btn.Enabled = false;
                //Abfahrt_btn.Enabled = false;
                //Uebergabe_btn.Enabled = false;
                //Reparatur_Anfahrt_btn.Enabled = false;
                //Kapazitaet_Anfahrt_btn.Enabled = false;
                //Reparatur_abfahrt_btn.Enabled = false;
                //Kapazitaet_Abfahrt_btn.Enabled = false;

                // Hide the DataGridView when the condition is not met
                //ShowDataGridView(false);

                // Show the message label when the condition is not satisfied
                //label_message.Visible = true;
            }
        }



        //Save button
        //private void save_btn_Click(object sender, EventArgs e)
        //{
        //    string folderPath = @"C:\temp\AnfRep"; // Folder path where the Excel files are located
        //    string userPrefix = textBox1.Text; // User input
        //    //userPrefix = userPrefix.ToUpper();
        //    if (string.IsNullOrWhiteSpace(userPrefix))
        //    {
        //        MessageBox.Show("Bitte geben Sie vor dem Speichern der Dokumente einen gültigen SP/K-Wert ein. Beispiel: SP11-22.");
        //        return;
        //    }


        //    foreach (DataGridViewRow row in dataGridView1.Rows)
        //    {
        //        if (row.Cells["Speichern"].Value != null && (bool)row.Cells["Speichern"].Value)
        //        {
        //            string originalFileName = row.Cells["Dateinamen"].Value.ToString();
        //            string newFileName = originalFileName;

        //            // Check if user input matches the criteria
        //            if (Regex.IsMatch(userPrefix, "^[A-Za-z]{2}\\d{2}-\\d{2}$"))
        //            {
        //                // Modify the filename by adding user input before it
        //                newFileName = $"{userPrefix}-{newFileName}";
        //            }

        //            // Combine the folder path and the new filename
        //            string filePath = Path.Combine(folderPath, newFileName);

        //            // Copy the original file to the new location
        //            File.Copy(Path.Combine(folderPath, originalFileName), filePath);

        //            // Optionally, you can delete the original file if needed
        //            // File.Delete(Path.Combine(folderPath, originalFileName));
        //        }
        //    }
        //}
        //===========2:Works, but A1 remains blank
        //private void save_btn_Click(object sender, EventArgs e)
        //{
        //    string folderPath = @"C:\temp\AnfRep"; // Folder path where the Excel files are located
        //    string userPrefix = textBox1.Text; // User input

        //    userPrefix = userPrefix.ToUpper();
        //    if (string.IsNullOrWhiteSpace(userPrefix))
        //    {
        //        MessageBox.Show("Bitte geben Sie vor dem Speichern der Dokumente einen gültigen SP/K-Wert ein. Beispiel: SP11-22.");
        //        return;
        //    }
        //    foreach (DataGridViewRow row in dataGridView1.Rows)
        //    {
        //        if (row.Cells["Speichern"].Value != null && (bool)row.Cells["Speichern"].Value)
        //        {
        //            string originalFileName = row.Cells["Dateinamen"].Value.ToString();

        //            if (Regex.IsMatch(userPrefix, "^[A-Z]{2}\\d{2}-\\d{2}$"))
        //            {
        //                string newFileName = $"{userPrefix}_{originalFileName}";
        //                string originalFilePath = Path.Combine(folderPath, originalFileName);
        //                string newFilePath = Path.Combine(folderPath, newFileName);

        //                try
        //                {
        //                    // Copy the original file to a new file
        //                    File.Copy(originalFilePath, newFilePath);

        //                    // Open the new Excel file
        //                    using (var package = new ExcelPackage(new FileInfo(newFilePath)))
        //                    {
        //                        var worksheet = package.Workbook.Worksheets[1];

        //                        // Write the user input in cell A1 (first row, first column)
        //                        worksheet.Cell(1, 1).Value = userPrefix;

        //                        // Save the changes to the new Excel file
        //                        package.Save();
        //                    }
        //                }
        //                catch (Exception ex)
        //                {
        //                    // Handle the exception, e.g., display an error message
        //                    MessageBox.Show("Error: " + ex.Message);
        //                }
        //            }
        //        }
        //    }
        //}
        //===============================

        //Makes the loop turn indefinately
        //private void save_btn_Click(object sender, EventArgs e)
        //{
        //    string folderPath = @"C:\temp\AnfRep"; // Folder path where the Excel files are located
        //    string userPrefix = textBox1.Text; // User input

        //    // Make sure userPrefix is always in uppercase
        //    userPrefix = userPrefix.ToUpper();

        //    // Check if userPrefix matches the pattern "^[A-Z]{2}\d{2}-\d{2}$"
        //    if (Regex.IsMatch(userPrefix, "^[A-Z]{2}\\d{2}-\\d{2}$"))
        //    {
        //        foreach (DataGridViewRow row in dataGridView1.Rows)
        //        {
        //            if (row.Cells["Speichern"].Value != null && (bool)row.Cells["Speichern"].Value)
        //            {
        //                string originalFileName = row.Cells["Dateinamen"].Value.ToString();
        //                string newFileName = $"{userPrefix}-{originalFileName}";

        //                // Combine the folder path and the new filename
        //                string filePath = Path.Combine(folderPath, newFileName);

        //                try
        //                {
        //                    //using (var package = new ExcelPackage(new FileInfo(filePath)))
        //                    using (var package = new ExcelPackage(new FileInfo(filePath)) )
        //                    {
        //                        var worksheet = package.Workbook.Worksheets.Add("Sheet1");

        //                        // Write the userPrefix to cell A1
        //                        worksheet.Cell(1, 1).Value = userPrefix;

        //                        // Save the Excel package to the specified file path
        //                        package.Save();
        //                    }
        //                }
        //                catch (Exception ex)
        //                {
        //                    // Handle the exception, e.g., display an error message
        //                    MessageBox.Show("Error: " + ex.Message);
        //                }
        //            }
        //        }
        //    }
        //    else
        //    {
        //        // Display a message if userPrefix doesn't match the pattern
        //        MessageBox.Show("Please enter a valid input matching the pattern: XX99-99 (e.g., AB12-34).");
        //    }
        //}
        //private void save_btn_Click(object sender, EventArgs e)
        //{
        //    // Disable the TextChanged event temporarily
        //    textBox1.TextChanged -= textBox1_TextChanged;

        //    string folderPath = @"C:\temp\AnfRep"; // Folder path where the Excel files are located
        //    string userPrefix = textBox1.Text; // User input

        //    // Make sure userPrefix is always in uppercase
        //    userPrefix = userPrefix.ToUpper();

        //    // Check if userPrefix matches the pattern "^[A-Z]{2}\d{2}-\d{2}$"
        //    if (Regex.IsMatch(userPrefix, "^[A-Z]{2}\\d{2}-\\d{2}$"))
        //    {
        //        foreach (DataGridViewRow row in dataGridView1.Rows)
        //        {
        //            if (row.Cells["Speichern"].Value != null && (bool)row.Cells["Speichern"].Value)
        //            {
        //                string originalFileName = row.Cells["Dateinamen"].Value.ToString();
        //                string newFileName = $"{userPrefix}-{originalFileName}";

        //                // Combine the folder path and the new filename
        //                string filePath = Path.Combine(folderPath, newFileName);

        //                try
        //                {
        //                    using (var package = new ExcelPackage(new FileInfo(filePath)) )
        //                    {
        //                        var worksheet = package.Workbook.Worksheets.Add("Sheet1");

        //                        // Write the userPrefix to cell A1
        //                        worksheet.Cell(1, 1).Value = userPrefix;

        //                        // Save the Excel package to the specified file path
        //                        package.Save();
        //                    }
        //                }
        //                catch (Exception ex)
        //                {
        //                    // Handle the exception, e.g., display an error message
        //                    MessageBox.Show("Error: " + ex.Message);
        //                }
        //            }
        //        }
        //    }
        //    else
        //    {
        //        // Display a message if userPrefix doesn't match the pattern
        //        MessageBox.Show("Please enter a valid input matching the pattern: XX99-99 (e.g., AB12-34).");
        //    }

        //    // Re-enable the TextChanged event
        //    textBox1.TextChanged += textBox1_TextChanged;
        //}

        //With "Microsoft.Office.Interop.Excel" 
        //private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        //{
        //    // Get the original filename when a cell is clicked
        //    if (e.RowIndex >= 0 && e.RowIndex < dataGridView1.Rows.Count)
        //    {
        //        originalFileName = dataGridView1.Rows[e.RowIndex].Cells["Dateinamen"].Value?.ToString();
        //    }
        //}

        //==============================================07===========================
        //private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        //{
        //    if (e.RowIndex >= 0 && e.RowIndex < dataGridView1.Rows.Count)
        //    {
        //        originalFileName = dataGridView1.Rows[e.RowIndex].Cells["Dateinamen"].Value?.ToString();
        //    }
        //}

        //private void save_btn_Click(object sender, EventArgs e)
        //{
        //    string folderPath = @"C:\temp\AnfRep"; // Update this to the appropriate folder path
        //    string userPrefix = textBox1.Text.ToUpper();

        //    if (string.IsNullOrWhiteSpace(userPrefix))
        //    {
        //        MessageBox.Show("Bitte geben Sie vor dem Speichern der Dokumente einen gültigen SP/K-Wert ein. Beispiel: SP11-22.");
        //        return;
        //    }


        //    if (string.IsNullOrWhiteSpace(originalFileName))
        //    {
        //        MessageBox.Show("Please select a file from the DataGridView.");
        //        return;
        //    }

        //    string fileName = $"{userPrefix}-{originalFileName}"; // Construct the file name

        //    string filePath = Path.Combine(folderPath, fileName);

        //    if (Path.GetExtension(filePath).Equals(".xlsx", StringComparison.OrdinalIgnoreCase))
        //    {
        //        try
        //        {
        //            using (var package = new ExcelPackage(new FileInfo(filePath)))
        //            {
        //                var worksheet = package.Workbook.Worksheets[1]; // Assuming you want to update the first worksheet

        //                // Update the Excel file with the prefix in "cell A1"
        //                worksheet.Cell(1, 1).Value = userPrefix;

        //                // Save the changes
        //                package.Save();
        //            }
        //        }
        //        catch (Exception ex)
        //        {
        //            MessageBox.Show($"Error updating Excel file {filePath}: {ex.Message}");
        //        }
        //    }
        //}
        //==================================================================

        //Loop ohne ende!
        //private void save_btn_Click(object sender, EventArgs e)
        //{
        //    string folderPath = @"C:\temp\AnfRep"; // Update this to the appropriate folder path
        //    string userPrefix = textBox1.Text.ToUpper();

        //    if (string.IsNullOrWhiteSpace(userPrefix))
        //    {
        //        MessageBox.Show("Bitte geben Sie vor dem Speichern der Dokumente einen gültigen SP/K-Wert ein. Beispiel: SP11-22.");
        //        return;
        //    }

        //    string originalFileName = string.Empty; // Define originalFileName within save_btn_Click

        //    if (dataGridView1.SelectedRows.Count > 0)
        //    {
        //        originalFileName = dataGridView1.SelectedRows[0].Cells["Dateinamen"].Value?.ToString();
        //    }

        //    if (string.IsNullOrWhiteSpace(originalFileName))
        //    {
        //        MessageBox.Show("Please select a file from the DataGridView.");
        //        return;
        //    }

        //    string fileName = $"{userPrefix}-{originalFileName}"; // Construct the file name

        //    string filePath = Path.Combine(folderPath, fileName);

        //    if (Path.GetExtension(filePath).Equals(".xlsx", StringComparison.OrdinalIgnoreCase))
        //    {
        //        try
        //        {
        //            using (var package = new ExcelPackage(new FileInfo(filePath)))
        //            {
        //                var worksheet = package.Workbook.Worksheets[1]; // Assuming you want to update the first worksheet

        //                // Update the Excel file with the prefix in "cell A1"
        //                worksheet.Cell(1, 1).Value = userPrefix;

        //                // Save the changes
        //                package.Save();
        //            }
        //        }
        //        catch (Exception ex)
        //        {
        //            MessageBox.Show($"Error updating Excel file {filePath}: {ex.Message}");
        //        }
        //    }
        //}

        //===================================================================================
        

        private void save_btn_Click(object sender, EventArgs e)
        {
            string userPrefix = textBox1.Text.ToUpper().Trim(); // Ensure userPrefix is trimmed
            string originalFileName = dataGridView1.CurrentRow.Cells["Dateinamen"].Value?.ToString();

            if (string.IsNullOrWhiteSpace(userPrefix) || string.IsNullOrWhiteSpace(originalFileName))
            {
                MessageBox.Show("Please select a valid file from the DataGridView and enter a valid SP/K-Wert.");
                return;
            }

            // Determine the target folder path based on the specific button that was clicked
            string targetFolder = "";

            if (reparatur_anf_btn.Enabled)
            {
                targetFolder = @"C:\temp\AnfRep";
            }
            else if (kapazitaet_anf_btn.Enabled)
            {
                targetFolder = @"C:\temp\Anfkapz";
            }
            // Add similar conditions for other buttons

            if (!string.IsNullOrWhiteSpace(targetFolder))
            {
                // Combine "userPrefix" and "originalFileName" to create the new file name
                string fileName = $"{userPrefix}-{originalFileName}";
                string filePath = Path.Combine(targetFolder, fileName);

                try
                {
                    using (var package = new ExcelPackage(new FileInfo(filePath)))
                    {
                        var worksheet = package.Workbook.Worksheets[1];
                        worksheet.Cell(1, 1).Value = userPrefix;
                        package.Save();
                        MessageBox.Show("File updated and saved successfully.");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error updating Excel file: {ex.Message}");
                }
            }
            else
            {
                MessageBox.Show("Please select a valid operation before saving.");
            }
        }



    }
}






        //Bard
        //private const string FolderPath = @"C:\temp\AnfRep";

        //private void SaveExcelFile(string userPrefix)
        //{
        //    // Make sure userPrefix is always in uppercase
        //    userPrefix = userPrefix.ToUpper();

        //    // Define a regular expression pattern to validate the user input
        //    const string pattern = "^[A-Z]{2}\\d{2}-\\d{2}$";

        //    if (Regex.IsMatch(userPrefix, pattern))
        //    {
        //        foreach (DataGridViewRow row in dataGridView1.Rows)
        //        {
        //            if (row.Cells["Speichern"].Value != null && (bool)row.Cells["Speichern"].Value)
        //            {
        //                string originalFileName = row.Cells["Dateinamen"].Value.ToString();
        //                string newFileName = $"{userPrefix}-{originalFileName}";

        //                // Combine the folder path and the new filename
        //                string filePath = Path.Combine(FolderPath, newFileName);

        //                try
        //                {
        //                    using (var package = new ExcelPackage(new FileInfo(filePath)))
        //                    {
        //                        var worksheet = package.Workbook.Worksheets.Add("Sheet1");

        //                        // Write the userPrefix to cell A1
        //                        worksheet.Cell(1, 1).Value = userPrefix;

        //                        // Save the Excel package to the specified file path
        //                        //package.Save(new FileInfo(filePath));
        //                        package.Save();
        //                    }
        //                }
        //                catch (Exception ex)
        //                {
        //                    // Handle the exception, e.g., log the error
        //                    Console.WriteLine($"Error saving file: {ex.Message}");
        //                }
        //            }
        //        }
        //    }
        //    else
        //    {
        //        // Display a message if userPrefix doesn't match the pattern
        //        MessageBox.Show("Please enter a valid input matching the pattern: XX99-99 (e.g., AB12-34).");
        //    }
        //}



