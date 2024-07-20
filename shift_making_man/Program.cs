using System;
using System.Windows.Forms;
using shift_making_man.Data;
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

            // 各データアクセスオブジェクトを作成
            IAdminDataAccess adminDataAccess = new AdminDataAccess();
            IShiftDataAccess shiftDataAccess = new ShiftDataAccess();
            IStaffDataAccess staffDataAccess = new StaffDataAccess();
            IStoreDataAccess storeDataAccess = new StoreDataAccess();

            // LoginFormにすべてのデータアクセスオブジェクトを渡す
            Application.Run(new LoginForm(adminDataAccess, shiftDataAccess, staffDataAccess, storeDataAccess));
        }
    }
}
