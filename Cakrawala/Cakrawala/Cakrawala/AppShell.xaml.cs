using Xamarin.Forms;

namespace Cakrawala
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();
            if (Application.Current.Properties["token"] == null || (string)Application.Current.Properties["token"] == "")
            {
                this.CurrentItem = loginPage;
            } else
            {
                this.CurrentItem = dashboardPage;
            }
        }

        private async void LogoutItem_Clicked(object sender, System.EventArgs e)
        {
            // Handle Logout
            Application.Current.Properties["token"] = null;
            await Shell.Current.GoToAsync("//login");
        }
    }
}