﻿using System;
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

        protected override void OnDisappearing()
        {
            base.OnDisappearing();
            Debug.WriteLine("HELOOOOOO");
            resetState();
        }

        private async void LoginButton_Clicked(object sender, EventArgs e)
        {
            this.errorText.Text = String.Empty;

            if (email.Text == string.Empty || password.Text == string.Empty)
            {
                this.errorText.Text = "Tolong isi email dan password dengan nilai yang sesuai.";
                return;
            }

            LoginResponse response = await App.authService.LoginAsync(email.Text, password.Text);
            if (response.jwtToken == null) 
            {
                this.errorText.Text = "Email dan password yang dimasukkan tidak sesuai";
                return;
            } else if (response.jwtToken == "error")
            {
                this.errorText.Text = "Terjadi kesalahan dalam mengakses server, mohon coba lagi nanti";
                return;
            }

            Application.Current.Properties["username"] = response.username;
            Application.Current.Properties["token"] = response.jwtToken;
            await Shell.Current.GoToAsync($"//dashboard");
        }

        private async void DaftarButton_Clicked(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync("//register");
        }

        private void resetState()
        {
            email.Text = string.Empty;
            password.Text = string.Empty;
            errorText.Text = string.Empty;
        }
    }
}