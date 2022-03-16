using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using static Cakrawala.Data.AuthService;

namespace Cakrawala.Views
{
    public partial class LoginPage : ContentPage
    {
        public LoginPage()
        {
            InitializeComponent();
            email.Text = "skaldraf@gmail.com";
            password.Text = "secret123";
        }

        private async void LoginButton_Clicked(object sender, EventArgs e)
        {
            LoginResponse response = await App.authService.LoginAsync(email.Text, password.Text);
            if (response.jwt_token == null) 
            {
                return;
            }

            Application.Current.Properties["username"] = response.username;
            Application.Current.Properties["token"] = response.jwt_token;
            await Shell.Current.GoToAsync($"//dashboard");
        }

        private async void DaftarButton_Clicked(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync("//register");
        }
    }
}