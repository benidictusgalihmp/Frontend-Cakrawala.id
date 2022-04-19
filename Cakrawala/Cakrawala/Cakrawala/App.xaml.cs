using Cakrawala.Data;
using System;
using System.Net.Http;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Cakrawala
{
    public partial class App : Application
    {
        public static AuthService authService = new AuthService();
        public static TransferService transferService = new TransferService();
        public static TopupService topupService = new TopupService();
        public static DashboardService dashboardService = new DashboardService();
        public static ProfileService profileService = new ProfileService();
        public static BankService bankService = new BankService();

        public App()
        {
            InitializeComponent();
            MainPage = new AppShell();
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
