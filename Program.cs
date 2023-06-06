using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using chimneys.Data;
namespace chimneys
{

    public static class Program
    {
        public static int cell = 0;
        public static double input_h2 = 0;
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            // To customize application configuration such as set high DPI settings or default font,
            // see https://aka.ms/applicationconfiguration.
            ApplicationConfiguration.Initialize();
            //Application.Run(new Form_variants());
            Application.Run(new Form_Result_temp());
        }
    }
}