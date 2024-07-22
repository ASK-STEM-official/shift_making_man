using System;
using System.Windows.Forms;
using Microsoft.Extensions.DependencyInjection;
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
            var serviceProvider = ConfigureServices();
            Application.Run(serviceProvider.GetRequiredService<LoginForm>());
        }

        private static IServiceProvider ConfigureServices()
        {
            var services = new ServiceCollection();

            // データアクセスオブジェクトの登録
            services.AddSingleton<IAdminDataAccess, AdminDataAccess>()
                    .AddSingleton<IShiftDataAccess, ShiftDataAccess>()
                    .AddSingleton<IStaffDataAccess, StaffDataAccess>()
                    .AddSingleton<IStoreDataAccess, StoreDataAccess>()
                    .AddSingleton<IShiftRequestDataAccess, ShiftRequestDataAccess>();

            // DataAccessFacadeの登録
            services.AddSingleton<DataAccessFacade>();

            // フォームの登録
            services.AddTransient<LoginForm>()
                    .AddTransient<MainForm>(provider =>
                        new MainForm(provider.GetRequiredService<DataAccessFacade>()))
                    .AddTransient<DashboardForm>(provider =>
                        new DashboardForm(provider.GetRequiredService<DataAccessFacade>()));

            return services.BuildServiceProvider();
        }
    }
}
