﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Cakrawala.Views
{
    public partial class LoginPage : ContentPage
    {
        public LoginPage()
        {
            InitializeComponent();
        }

        private async void LoginButton_Clicked(object sender, EventArgs e)
        {
            Application.Current.Properties["userEmail"] = email.Text.ToString();
            Application.Current.Properties["token"] = "abcd";
            await Shell.Current.GoToAsync($"//dashboard");
        }

        private async void DaftarButton_Clicked(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync("//register");
        }
    }
}