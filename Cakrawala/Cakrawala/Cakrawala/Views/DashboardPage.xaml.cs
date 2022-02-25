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
            userNameLabel.Text = Application.Current.Properties["userEmail"].ToString();  
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
    }
}