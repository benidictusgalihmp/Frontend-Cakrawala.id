using Xamarin.Forms;

namespace Cakrawala
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();
        }

        private async void LogoutItem_Clicked(object sender, System.EventArgs e)
        {
            // Handle Logout
            await Shell.Current.GoToAsync("//login");
        }
    }
}