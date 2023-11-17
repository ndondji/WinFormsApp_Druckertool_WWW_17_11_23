//using Microsoft.Office.Interop.Excel;
//using System.Windows.Forms; // Add this line to specify System.Windows.Forms namespace

//namespace WinFormsApp_folderList
//{
//    internal static class Program
//    {
//        /// <summary>
//        /// The main entry point for the application.
//        /// </summary>
//        [STAThread]
//        static void Main()
//        {
//            // To customize application configuration such as set high DPI settings or default font,
//            // see https://aka.ms/applicationconfiguration.
//            ApplicationConfiguration.Initialize();
//            System.Windows.Forms.Application.Run(new Form1()); // Specify the full namespace for System.Windows.Forms.Application
//        }
//    }
//}

//The original from the system (Put it back once it works)
using Microsoft.Office.Interop;


namespace WinFormsApp_folderList
{
    internal static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            // To customize application configuration such as set high DPI settings or default font,
            // see https://aka.ms/applicationconfiguration.
            ApplicationConfiguration.Initialize();
            Application.Run(new Form1());
        }
    }
}