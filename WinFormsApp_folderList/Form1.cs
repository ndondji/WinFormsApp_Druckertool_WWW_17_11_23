using System;
using System.IO;
using System.Windows.Forms;
using System.Drawing.Printing;
using Microsoft.Office.Interop.Excel;

namespace WinFormsApp_folderList
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void display_btn_Click(object sender, EventArgs e)
        {
            // Get a list of all Excel files in the folder.
            string[] files = Directory.GetFiles(@"C:\Temp\Anfahrt", "*.xlsx");

            // Check to see if the "Dateiname" column already exists.
            if (!dataGridView1.Columns.Contains("Dateiname"))
            {
                // Create a new DataGridViewTextBoxColumn object.
                DataGridViewTextBoxColumn column = new DataGridViewTextBoxColumn();
                column.Name = "Dateiname";
                column.HeaderText = "Dateiname";

                // Add the column to the DataGridView.
                dataGridView1.Columns.Add(column);
            }

            // Clear the DataGridView.
            dataGridView1.Rows.Clear();

            // Add each Excel file to the DataGridView.
            foreach (string file in files)
            {
                dataGridView1.Rows.Add(file);
            }
        }

        private void print_btn_Click(object sender, EventArgs e)
        {
            // Get a list of all Excel files in the folder.
            string[] files = Directory.GetFiles(@"C:\Temp\Anfahrt", "*.xlsx");

            // Create a new PrintDocument object.
            PrintDocument document = new PrintDocument();

            // Set the printer name.(This doesn't really play a huge role.Does still print without!!!!!)
            //document.PrinterSettings.PrinterName = "FollowMe";

            // Iterate through each Excel file and print it.
            foreach (string file in files)
            {
                // Set the document name to just the file name (without path).
                document.DocumentName = Path.GetFileName(file);

                // Add the Excel file to the PrintDocument object.
                document.PrintPage += new PrintPageEventHandler(ExcelPrintPage);

                // Print the Excel file.
                document.Print();

                // Remove the event handler to avoid multiple print jobs.
                document.PrintPage -= new PrintPageEventHandler(ExcelPrintPage);
            }
        }

        private void ExcelPrintPage(object sender, PrintPageEventArgs e)
        {
            // Get the Excel file name (without path) from the document name.
            string fileName = Path.GetFileNameWithoutExtension((sender as PrintDocument).DocumentName);

            // Construct the full path to the Excel file.
            string fullPath = Path.Combine(@"C:\Temp\Anfahrt", fileName + ".xlsx");

            // Open the Excel file.
            Microsoft.Office.Interop.Excel.Application application = new Microsoft.Office.Interop.Excel.Application();
            Workbook workbook = application.Workbooks.Open(fullPath);

            // Print the active worksheet.
            Worksheet worksheet = (Worksheet)workbook.ActiveSheet;
            worksheet.PrintOut();

            // Close the Excel file.
            workbook.Close();
            application.Quit();
        }



        //This one sends to printer double documents with path(doesn't print out) and no path(needed ones)
        //private void print_btn_Click(object sender, EventArgs e)
        //{
        //    // Get a list of all Excel files in the folder.
        //    string[] files = Directory.GetFiles(@"C:\Temp\Anfahrt", "*.xlsx");

        //    // Create a new PrintDocument object.
        //    PrintDocument document = new PrintDocument();

        //    // Set the printer name.
        //    document.PrinterSettings.PrinterName = "FollowMe";

        //    // Iterate through each Excel file and print it.
        //    foreach (string file in files)
        //    {
        //        // Set the document name.
        //        document.DocumentName = file;

        //        // Add the Excel file to the PrintDocument object.
        //        document.PrintPage += new PrintPageEventHandler(ExcelPrintPage);

        //        // Print the Excel file.
        //        document.Print();

        //        // Remove the event handler to avoid multiple print jobs.
        //        document.PrintPage -= new PrintPageEventHandler(ExcelPrintPage);
        //    }
        //}

        //This one sends to printer double documents with path(doesn't print out) and no path(needed ones)
        //private void ExcelPrintPage(object sender, PrintPageEventArgs e)
        //{
        //    // Get the Excel file.
        //    string file = (sender as PrintDocument).DocumentName;

        //    // Open the Excel file.
        //    Microsoft.Office.Interop.Excel.Application application = new Microsoft.Office.Interop.Excel.Application();
        //    Microsoft.Office.Interop.Excel.Workbook workbook = application.Workbooks.Open(file);

        //    // Print the active worksheet.
        //    Microsoft.Office.Interop.Excel.Worksheet worksheet = (Microsoft.Office.Interop.Excel.Worksheet)workbook.ActiveSheet;
        //    worksheet.PrintOut();

        //    // Close the Excel file.
        //    workbook.Close();
        //    application.Quit();
        //}


        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            // Get the path to the Excel file.
            string file = dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString();

            // Create a new Excel.Application object.
            Microsoft.Office.Interop.Excel.Application application = new Microsoft.Office.Interop.Excel.Application();

            // Open the Excel file.
            Workbook workbook = application.Workbooks.Open(file);

            // Close the Excel file.
            workbook.Close();
            application.Quit();
        }
    }
}

