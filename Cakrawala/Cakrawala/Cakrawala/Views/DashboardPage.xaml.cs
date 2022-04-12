using Cakrawala.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Cakrawala.Views
{
    public partial class DashboardPage : ContentPage
    {
        public DashboardPage()
        {
            InitializeComponent();
            // userNameLabel.Text = Application.Current.Properties["username"].ToString();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            RetrieveUserData();
        }

        private async void RetrieveUserData()
        {
            string userId = Application.Current.Properties["userId"].ToString();

            User userData = await App.dashboardService.DashboardAsync(userId);

            if (userData == null)
            {
                await DisplayAlert(
                    "Connection Error",
                    "Cannot connect to the system, please check your connection or contact our administrator.",
                    "Logout");

                App.authService.Logout();
                await Shell.Current.GoToAsync("//login");
                return;
            }

            userNameLabel.Text = userData.displayName != "" ? userData.displayName : "{User Name}";
            lvlLabel.Text = "LVL " + userData.level.ToString();
            expLabel.Text = "EXP " + userData.exp.ToString();
            nominalLabel.Text = "Rp " + userData.balance.ToString();
        }

        public void OnChangeEmail(string value) 
        {
            if (value != null) userNameLabel.Text = value.ToString();
            else userNameLabel.Text = "User";
            BindingContext = value;
        }

        private async void LogoutButton_Clicked(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync("//login");
        }

        private async void ProfileButton_Clicked(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync("//profile");
        }

        private async void HistoryButton_Clicked(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync("//history");
        }

        private async void TransferButton_Clicked(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync("//transfer");
        }

        private async void TopupButton_Clicked(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync("//topupvoucher");
        }
    }
}