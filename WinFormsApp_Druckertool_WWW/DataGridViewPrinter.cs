using System;
using System.Collections.Generic;
using System.Drawing.Printing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WinFormsApp_Druckertool_WWW
{
    //internal class DataGridViewPrinter
    //{
    //}
    public class DataGridViewPrinter
    {
        private DataGridView dataGridView;
        private PrintPageEventArgs printPageEventArgs;
        private bool useDefaultHeader;
        private bool useDefaultFooter;
        private string headerText;
        private Font headerFont;
        private Color headerColor;
        private bool printFooter;
        private int currentPage;
        private int totalPage;

        public DataGridViewPrinter(
            DataGridView dataGridView,
            PrintPageEventArgs e,
            bool useDefaultHeader,
            bool useDefaultFooter,
            string headerText,
            Font headerFont,
            Color headerColor,
            bool printFooter)
        {
            this.dataGridView = dataGridView;
            this.printPageEventArgs = e;
            this.useDefaultHeader = useDefaultHeader;
            this.useDefaultFooter = useDefaultFooter;
            this.headerText = headerText;
            this.headerFont = headerFont;
            this.headerColor = headerColor;
            this.printFooter = printFooter;
            this.currentPage = 1;

            // Calculate the total number of pages based on the DataGridView's rows
            int rowCount = dataGridView.Rows.Count;
            this.totalPage = (rowCount / dataGridView.Rows[0].Height) + 1;
        }

        public bool DrawDataGridView(Graphics g)
        {
            try
            {
                // Define the print area
                int marginLeft = printPageEventArgs.MarginBounds.Left;
                int marginTop = printPageEventArgs.MarginBounds.Top;
                int marginRight = printPageEventArgs.MarginBounds.Right;
                int marginBottom = printPageEventArgs.MarginBounds.Bottom;

                int pageWidth = printPageEventArgs.PageBounds.Width;
                int pageHeight = printPageEventArgs.PageBounds.Height;

                // Check if there is enough space to print the header and footer
                int headerHeight = CalculateHeaderHeight(g);
                int footerHeight = CalculateFooterHeight(g);
                int availableHeight = pageHeight - headerHeight - footerHeight;

                if (availableHeight < 0)
                    return false; // Not enough space to print anything on this page

                // Calculate the number of rows that fit in the available space
                //int rowsPerPage = availableHeight / dataGridView.RowHeight;===>this brings error
                int rowsPerPage = availableHeight / dataGridView.Rows[0].Height; // Assuming the rows have the same height


                // Calculate the starting row index for this page
                int startIndex = (currentPage - 1) * rowsPerPage;
                int endIndex = startIndex + rowsPerPage;

                // Draw the header
                if (useDefaultHeader)
                    DrawDefaultHeader(g, marginLeft, marginTop, pageWidth);

                // Draw the DataGridView's content
                if (!DrawDataGridViewContent(g, marginLeft, marginTop + headerHeight, pageWidth, footerHeight, startIndex, endIndex))
                    return false;

                // Draw the footer
                if (printFooter)
                    DrawFooter(g, marginLeft, marginTop + headerHeight + availableHeight, pageWidth);

                // Increment the current page
                currentPage++;

                return currentPage <= totalPage;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred while printing: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }

        private int CalculateHeaderHeight(Graphics g)
        {
            if (useDefaultHeader)
            {
                // Calculate the height needed for the default header text
                SizeF textSize = g.MeasureString(headerText, headerFont);
                return (int)textSize.Height + 10; // Add some padding
            }
            return 0;
        }

        private int CalculateFooterHeight(Graphics g)
        {
            if (printFooter)
            {
                // Calculate the height needed for the footer
                return 40; // You can adjust this value as needed
            }
            return 0;
        }

        private void DrawDefaultHeader(Graphics g, int x, int y, int pageWidth)
        {
            // Draw the default header text centered at the top of the page
            g.DrawString(headerText, headerFont, new SolidBrush(headerColor), new Point(x + (pageWidth / 2) - (int)(g.MeasureString(headerText, headerFont).Width / 2), y));
        }

        private bool DrawDataGridViewContent(Graphics g, int x, int y, int pageWidth, int footerHeight, int startIndex, int endIndex)
        {
            try
            {
                int yPosition = y;
                for (int i = startIndex; i < endIndex; i++)
                {
                    if (i >= dataGridView.RowCount)
                        break;

                    DataGridViewRow row = dataGridView.Rows[i];

                    foreach (DataGridViewCell cell in row.Cells)
                    {
                        if (cell.Visible)
                        {
                            // Draw the cell content
                            g.DrawString(cell.FormattedValue.ToString(), dataGridView.Font, Brushes.Black, new RectangleF(x, yPosition, cell.Size.Width, cell.Size.Height));

                            // Draw cell borders (optional)
                            // g.DrawRectangle(Pens.Black, x, yPosition, cell.Size.Width, cell.Size.Height);

                            x += cell.Size.Width;
                        }
                    }

                    yPosition += row.Height;

                    // Check if there's enough space left for the footer
                    if (yPosition + footerHeight > printPageEventArgs.PageBounds.Bottom)
                        return false; // Not enough space for the footer, stop drawing content
                }

                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred while drawing the DataGridView content: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }

        private void DrawFooter(Graphics g, int x, int y, int pageWidth)
        {
            // Draw the footer text centered at the bottom of the page
            string footerText = $"Page {currentPage} of {totalPage}";
            g.DrawString(footerText, dataGridView.Font, Brushes.Black, new Point(x + (pageWidth / 2) - (int)(g.MeasureString(footerText, dataGridView.Font).Width / 2), y));
        }
    }
}


//using System;
//using System.Drawing;
//using System.Drawing.Printing;
//using System.Windows.Forms;

//public class DataGridViewPrinter
//{
//    private DataGridView dataGridView;
//    private PrintPageEventArgs printPageEventArgs;
//    private bool useDefaultHeader;
//    private bool useDefaultFooter;
//    private string headerText;
//    private Font headerFont;
//    private Color headerColor;
//    private bool printFooter;
//    private int currentPage;
//    private int totalPage;

//    public DataGridViewPrinter(
//        DataGridView dataGridView,
//        PrintPageEventArgs e,
//        bool useDefaultHeader,
//        bool useDefaultFooter,
//        string headerText,
//        Font headerFont,
//        Color headerColor,
//        bool printFooter)
//    {
//        this.dataGridView = dataGridView;
//        this.printPageEventArgs = e;
//        this.useDefaultHeader = useDefaultHeader;
//        this.useDefaultFooter = useDefaultFooter;
//        this.headerText = headerText;
//        this.headerFont = headerFont;
//        this.headerColor = headerColor;
//        this.printFooter = printFooter;
//        this.currentPage = 1;

//        // Calculate the total number of pages based on the DataGridView's rows
//        int rowCount = dataGridView.Rows.Count;
//        this.totalPage = (rowCount / dataGridView.RowsPerPage) + 1;
//    }

//    public bool DrawDataGridView(Graphics g)
//    {
//        try
//        {
//            // Define the print area
//            int marginLeft = printPageEventArgs.MarginBounds.Left;
//            int marginTop = printPageEventArgs.MarginBounds.Top;
//            int marginRight = printPageEventArgs.MarginBounds.Right;
//            int marginBottom = printPageEventArgs.MarginBounds.Bottom;

//            int pageWidth = printPageEventArgs.PageBounds.Width;
//            int pageHeight = printPageEventArgs.PageBounds.Height;

//            // Check if there is enough space to print the header and footer
//            int headerHeight = CalculateHeaderHeight(g);
//            int footerHeight = CalculateFooterHeight(g);
//            int availableHeight = pageHeight - headerHeight - footerHeight;

//            if (availableHeight < 0)
//                return false; // Not enough space to print anything on this page

//            // Calculate the number of rows that fit in the available space
//            int rowsPerPage = availableHeight / dataGridView.RowHeight;

//            // Calculate the starting row index for this page
//            int startIndex = (currentPage - 1) * rowsPerPage;
//            int endIndex = startIndex + rowsPerPage;

//            // Draw the header
//            if (useDefaultHeader)
//                DrawDefaultHeader(g, marginLeft, marginTop, pageWidth);

//            // Draw the DataGridView's content
//            if (!DrawDataGridViewContent(g, marginLeft, marginTop + headerHeight, pageWidth, footerHeight, startIndex, endIndex))
//                return false;

//            // Draw the footer
//            if (printFooter)
//                DrawFooter(g, marginLeft, marginTop + headerHeight + availableHeight, pageWidth);

//            // Increment the current page
//            currentPage++;

//            return currentPage <= totalPage;
//        }
//        catch (Exception ex)
//        {
//            MessageBox.Show($"An error occurred while printing: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
//            return false;
//        }
//    }

//    private int CalculateHeaderHeight(Graphics g)
//    {
//        if (useDefaultHeader)
//        {
//            // Calculate the height needed for the default header text
//            SizeF textSize = g.MeasureString(headerText, headerFont);
//            return (int)textSize.Height + 10; // Add some padding
//        }
//        return 0;
//    }

//    private int CalculateFooterHeight(Graphics g)
//    {
//        if (printFooter)
//        {
//            // Calculate the height needed for the footer
//            return 40; // You can adjust this value as needed
//        }
//        return 0;
//    }

//    private void DrawDefaultHeader(Graphics g, int x, int y, int pageWidth)
//    {
//        // Draw the default header text centered at the top of the page
//        g.DrawString(headerText, headerFont, new SolidBrush(headerColor), new Point(x + (pageWidth / 2) - (int)(g.MeasureString(headerText, headerFont).Width / 2), y));
//    }

//    private bool DrawDataGridViewContent(Graphics g, int x, int y, int pageWidth, int footerHeight, int startIndex, int endIndex)
//    {
//        try
//        {
//            int yPosition = y;
//            for (int i = startIndex; i < endIndex; i++)
//            {
//                if (i >= dataGridView.RowCount)
//                    break;

//                DataGridViewRow row = dataGridView.Rows[i];

//                foreach (DataGridViewCell cell in row.Cells)
//                {
//                    if (cell.Visible)
//                    {
//                        // Draw the cell content
//                        g.DrawString(cell.FormattedValue.ToString(), dataGridView.Font, Brushes.Black, new RectangleF(x, yPosition, cell.Size.Width, cell.Size.Height));

//                        // Draw cell borders (optional)
//                        // g.DrawRectangle(Pens.Black, x, yPosition, cell.Size.Width, cell.Size.Height);

//                        x += cell.Size.Width;
//                    }
//                }

//                yPosition += row.Height;

//                // Check if there's enough space left for the footer
//                if (yPosition + footerHeight > printPageEventArgs.PageBounds.Bottom)
//                    return false; // Not enough space for the footer, stop drawing content
//            }

//            return true;
//        }
//        catch (Exception ex)
//        {
//            MessageBox.Show($"An error occurred while drawing the DataGridView content: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
//            return false;
//        }
//    }

//    private void DrawFooter(Graphics g, int x, int y, int pageWidth)
//    {
//        // Draw the footer text centered at the bottom of the page
//        string footerText = $"Page {currentPage} of {totalPage}";
//        g.DrawString(footerText, dataGridView.Font, Brushes.Black, new Point(x + (pageWidth / 2) - (int)(g.MeasureString(footerText, dataGridView.Font).Width / 2), y));
//    }
//}
