using System;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using shift_making_man.Controllers;
using shift_making_man.Data;
using shift_making_man.Services;
using shift_making_man.Views;

namespace shift_making_man
{
    static class Program
    {
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            string connectionString = "server=localhost;database=19demo;user=root;password=;";
            IDataAccess dataAccess = new MySqlDataAccess(connectionString);
            ShiftSchedulerService schedulerService = new ShiftSchedulerService(dataAccess);
            ShiftController shiftController = new ShiftController(schedulerService);

            Application.Run(new MainForm(shiftController));
        }
    }
}
