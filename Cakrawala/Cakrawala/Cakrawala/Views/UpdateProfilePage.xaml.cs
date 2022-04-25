using Cakrawala.Models;
using System;
using System.Linq;
using System.Diagnostics;

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

            if (!checkUsernameEntry())
            {
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

            if (!checkPasswordEntry())
            {
                return;
            }

            int status = await App.profileService.UpdatePasswordAsync(userId, oldPassword, newPassword);

            if (status == 0)
            {
                updatePasswordErrMsg.Text = "Gagal mengubah password, tolong hubungi administrasi";
                return;
            } else if(status == 2)
            {
                updatePasswordErrMsg.Text = "Password lama yang dimasukkan tidak sesuai";
                return;
            }
            await Shell.Current.GoToAsync("//profile");
        }

        private bool checkUsernameEntry()
        {
            string newUsername = entryUpdateUsername.Text;

            if (newUsername.Length > 25)
            {
                updateUsernameErrMsg.Text = "Panjang username tidak boleh melebihi 25 karakter";
            }
            else if (!newUsername.All(char.IsLetterOrDigit) ||
                  newUsername.All(char.IsWhiteSpace))
            {
                updateUsernameErrMsg.Text = "Hanya huruf dan angka yang diperbolehkan dan tanpa spasi.";
            }
            else if (newUsername == "")
            {
                updateUsernameErrMsg.Text = "Username tidak boleh kosong.";
            }
            else
            {
                updateUsernameErrMsg.Text = "";
                return true;
            }
            return false;
        }

        private bool checkPasswordEntry()
        {
            string oldPassword = entryUpdateOldPassword.Text;
            string newPassword = entryUpdateNewPassword.Text;
            string confirmPassword = entryUpdateConfirmPassword.Text;

            Debug.WriteLine("[TEST CHECK PASSWORD]");
            Debug.WriteLine(oldPassword + " " + newPassword + " " + confirmPassword);

            if(newPassword == "" || confirmPassword == "" || oldPassword == "")
            {
                updatePasswordErrMsg.Text = "Password tidak dapat kosong.";
            } else if(newPassword != confirmPassword)
            {
                updatePasswordErrMsg.Text = "Password Baru dan Konfirmasi tidak sama.";
            } else if(newPassword.Length < 8 || confirmPassword.Length < 8)
            {
                updatePasswordErrMsg.Text = "Panjang Password Baru dan Konfirmasi harus lebih dari 8 karakter.";
            } else
            {
                updatePasswordErrMsg.Text = "";
                return true;
            }

            return false;
        }

        private void entryUpdateUsername_TextChanged(object sender, TextChangedEventArgs e)
        {
            bool usernameValid = checkUsernameEntry();
        }

        private void entryUpdatePassword_TextChanged(object sender, TextChangedEventArgs e)
        {
            bool passwordValid = checkPasswordEntry();
        }
    }
}