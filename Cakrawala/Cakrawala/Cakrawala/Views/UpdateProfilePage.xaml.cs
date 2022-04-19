using Cakrawala.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Cakrawala.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class UpdateProfilePage : ContentPage
    {
        public UpdateProfilePage()
        {
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            RetrieveUserData();
            resetState();
        }

        public async void RetrieveUserData()
        {
            string userId = Application.Current.Properties["userId"].ToString();

            User userData = await App.profileService.ViewProfileAsync(userId);
            entryUpdateUsername.Placeholder = userData.displayName != "" ? userData.displayName : "{User Name}";
        }
        private void resetState()
        {
            entryUpdateUsername.Text = String.Empty;
            updateUsernameErrMsg.Text = String.Empty;
            entryUpdateOldPassword.Text = String.Empty;
            entryUpdateNewPassword.Text = String.Empty;
            entryUpdateConfirmPassword.Text = String.Empty;
            updatePasswordErrMsg.Text = String.Empty;
        }

        private void resetError()
        {
            updateUsernameErrMsg.Text = String.Empty;
            updatePasswordErrMsg.Text = String.Empty;
        }

        private async void ProfileButton_Clicked(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync("//profile");
        }

        private async void SaveUsernameButton_Clicked(object sender, EventArgs e)
        {
            string userId = Application.Current.Properties["userId"].ToString();
            string newUsername = entryUpdateUsername.Text;
            resetError();

            if (newUsername.Length > 25)
            {
                updateUsernameErrMsg.Text = "Panjang username tidak boleh melebihi 25 karakter";
            } else if (!newUsername.All(char.IsLetterOrDigit) ||
                    newUsername.All(char.IsWhiteSpace))
            {
                updateUsernameErrMsg.Text = "Hanya huruf dan angka yang diperbolehkan dan tanpa spasi.";
                return;
            } else if(newUsername == "")
            {
                updateUsernameErrMsg.Text = "Username tidak boleh kosong.";
                return;
            }

            bool status = await App.profileService.UpdateUsernameAsync(userId, newUsername);

            if (!status)
            {
                updateUsernameErrMsg.Text = "Gagal mengubah username, tolong hubungi administrasi.";
                return;
            }
            await Shell.Current.GoToAsync("//profile");
        } 

        private async void SavePasswordButton_Clicked(object sender, EventArgs e)
        {
            string userId = Application.Current.Properties["userId"].ToString();
            string oldPassword = entryUpdateOldPassword.Text;
            string newPassword = entryUpdateNewPassword.Text;
            string confirmPassword = entryUpdateConfirmPassword.Text;

            if (newPassword == "")
            {
                updatePasswordErrMsg.Text = "Password tidak dapat kosong.";
                return;
            } else if (newPassword != confirmPassword)
            {
                updatePasswordErrMsg.Text = "Password dan Konfirmasi Password tidak sama.";
                return;
            } else if (newPassword.Length < 8 ||
                        confirmPassword.Length < 8)
            {
                updatePasswordErrMsg.Text = "Panjang Password Baru dan Konfirmasi Password harus lebih dari 8 karakter.";
                return;
            }

            bool status = await App.profileService.UpdatePasswordAsync(userId, oldPassword, newPassword);

            if (!status)
            {
                updatePasswordErrMsg.Text = "Gagal mengubah password. Password lama yang Anda masukkan salah.";
                return;
            }
            await Shell.Current.GoToAsync("//profile");
        }
    }
}