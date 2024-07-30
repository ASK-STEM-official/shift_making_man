//
using System;
using System.Windows.Forms;
using Microsoft.Extensions.DependencyInjection;
using shift_making_man.Controllers;
using shift_making_man.Controllers.ShiftServices;
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
            var serviceProvider = ConfigureServices();
            Application.Run(serviceProvider.GetRequiredService<LoginForm>());
        }

        private static IServiceProvider ConfigureServices()
        {
            var services = new ServiceCollection();

            // データアクセスオブジェクトの登録
            services.AddSingleton<IAdminDataAccess, AdminDataAccess>()
                    .AddSingleton<IShiftDataAccess>(provider =>
                        new ShiftDataAccess("server=localhost;database=19demo;user=root;password=")) // ここで connectionString を渡す
                    .AddSingleton<IStaffDataAccess, StaffDataAccess>()
                    .AddSingleton<IStoreDataAccess, StoreDataAccess>()
                    .AddSingleton<IShiftRequestDataAccess, ShiftRequestDataAccess>();

            // サービスの登録
            services.AddSingleton<ShiftValidationService>(provider =>
                new ShiftValidationService(
                    provider.GetRequiredService<IStoreDataAccess>(),
                    provider.GetRequiredService<IShiftDataAccess>(),
                    provider.GetRequiredService<IStaffDataAccess>()
                ))
                .AddSingleton<ShiftCreationService>(provider =>
                    new ShiftCreationService(
                        provider.GetRequiredService<IStoreDataAccess>(),
                        provider.GetRequiredService<IStaffDataAccess>(),
                        provider.GetRequiredService<IShiftDataAccess>(),
                        provider.GetRequiredService<IShiftRequestDataAccess>(),
                        provider.GetRequiredService<ShiftValidationService>(),
                        provider.GetRequiredService<ShiftOptimizationService>()
                    ))
                .AddSingleton<ShiftOptimizationService>()
                .AddSingleton<ShiftModificationService>();

            // DataAccessFacadeの登録
            services.AddSingleton<DataAccessFacade>(provider =>
                new DataAccessFacade(
                    provider.GetRequiredService<IAdminDataAccess>(),
                    provider.GetRequiredService<IShiftDataAccess>(),
                    provider.GetRequiredService<IStaffDataAccess>(),
                    provider.GetRequiredService<IStoreDataAccess>(),
                    provider.GetRequiredService<IShiftRequestDataAccess>(),
                    provider.GetRequiredService<ShiftCreationService>(),
                    provider.GetRequiredService<ShiftValidationService>(),
                    provider.GetRequiredService<ShiftOptimizationService>(),
                    provider.GetRequiredService<ShiftModificationService>()
                ));

            // コントローラの登録
            services.AddSingleton<ShiftSchedulerController>(provider =>
                new ShiftSchedulerController(
                    provider.GetRequiredService<ShiftCreationService>(),
                    provider.GetRequiredService<ShiftModificationService>(),
                    provider.GetRequiredService<ShiftValidationService>(),
                    provider.GetRequiredService<ShiftOptimizationService>(),
                    provider.GetRequiredService<IStoreDataAccess>(),
                    provider.GetRequiredService<IShiftDataAccess>()
                ));

            // フォームの登録
            services.AddTransient<LoginForm>()
                    .AddTransient<MainForm>(provider =>
                        new MainForm(provider.GetRequiredService<DataAccessFacade>()))
                    .AddTransient<DashboardForm>(provider =>
                        new DashboardForm(provider.GetRequiredService<DataAccessFacade>()))
                    .AddTransient<ShiftSchedulerForm>(provider =>
                        new ShiftSchedulerForm(
                            provider.GetRequiredService<ShiftSchedulerController>()
                        ));

            return services.BuildServiceProvider();
        }
    }
}
