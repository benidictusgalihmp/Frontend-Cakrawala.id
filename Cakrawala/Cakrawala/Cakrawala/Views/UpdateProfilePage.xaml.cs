using Cakrawala.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            RetrieveUserData();
            resetState();
        }

        public async void RetrieveUserData()
        {
            string userId = Application.Current.Properties["userId"].ToString();

            User userData = await App.profileService.ViewProfileAsync(userId);
            entryUpdateUsername.Placeholder = userData.userName != "" ? userData.userName : "{User Name}";
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

        private async void ProfileButton_Clicked(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync("//profile");
        }

        private async void SaveUsernameButton_Clicked(object sender, EventArgs e)
        {
            string userId = Application.Current.Properties["userId"].ToString();
            string newUsername = entryUpdateUsername.Text;

            if (newUsername.Length > 25)
            {
                updateUsernameErrMsg.Text = "Panjang username tidak boleh melebihi 25 karakter";
            } else if (!newUsername.All(char.IsLetterOrDigit) ||
                    !newUsername.All(char.IsWhiteSpace))
            {
                updateUsernameErrMsg.Text = "Hanya huruf dan angka yang diperbolehkan dan tanpa spasi.";
            } else if(newUsername == "")
            {
                updateUsernameErrMsg.Text = "Username tidak boleh kosong.";
            }

            bool status = await App.profileService.UpdateUsernameAsync(userId, newUsername);

            if (!status)
            {
                updateUsernameErrMsg.Text = "Gagal mengubah username, tolong hubungi administrasi.";
                return;
            }
            await Shell.Current.GoToAsync("//update");
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
            } else if (newPassword != confirmPassword)
            {
                updatePasswordErrMsg.Text = "Password dan Konfirmasi Password tidak sama.";
            } else if (oldPassword != oldPassword) // kurang tahu
            {
                updatePasswordErrMsg.Text = "Password lama salah.";
            }

            bool status = await App.profileService.UpdatePasswordAsync(userId, newPassword);

            if (!status)
            {
                updatePasswordErrMsg.Text = "Gagal mengubah password, tolong hubungi administrasi.";
                return;
            }
            await Shell.Current.GoToAsync("//update");
        }
    }
}