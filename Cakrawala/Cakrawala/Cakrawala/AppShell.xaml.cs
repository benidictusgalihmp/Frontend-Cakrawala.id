using System;
using System.Collections.Generic;
using Xamarin.Forms;

namespace Cakrawala
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();
            try
            {
                if (Application.Current.Properties["token"] == null || (string)Application.Current.Properties["token"] == "")
                {
                    this.CurrentItem = loginPage;
                }
                else
                {
                    this.CurrentItem = dashboardPage;
                }

            } catch (KeyNotFoundException ex)
            {
                this.CurrentItem = loginPage;
            }
            
        }

        private async void LogoutItem_Clicked(object sender, System.EventArgs e)
        {
            // Handle Logout
            App.authService.Logout();
            await Shell.Current.GoToAsync("//login");
        }
    }
}