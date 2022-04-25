using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Cakrawala.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class RegisterPage : ContentPage
    {
        public RegisterPage()
        {
            InitializeComponent();
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();
            resetState();
        }

        private async void RegisterButton_Clicked(object sender, EventArgs e)
        {
            if (username.Text == string.Empty || 
                name.Text == string.Empty || 
                email.Text == string.Empty || 
                password.Text == string.Empty || 
                password.Text != retypePassword.Text)
            {
                errorText.Text = "Tolong isi semua baris yang ada";
                return;
            }

            bool status = await App.authService.RegisterAsync(username.Text, name.Text, email.Text, password.Text);
            if (!status)
            {
                errorText.Text = "Gagal registrasi, tolong hubungi administrasi";
                return;
            }
            await Shell.Current.GoToAsync("//login");
        }

        private async void MasukButton_Clicked(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync("//login");
        }

        private void password_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (password.Text != retypePassword.Text)
            {
                errorText.Text = "Password dan Konfirmasi Password tidak sama";
            }
            else if (password.Text.Length < 8)
            {
                errorText.Text = "Password harus 8 karakter atau lebih";
            }
            else
            {
                errorText.Text = String.Empty;
            }
        }

        private void resetState()
        {
            name.Text = String.Empty;
            email.Text = String.Empty;
            password.Text = String.Empty;
            retypePassword.Text = String.Empty;
            errorText.Text = String.Empty;
        }
    }
}