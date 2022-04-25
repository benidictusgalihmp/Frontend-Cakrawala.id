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
    public partial class ProfilePage : ContentPage
    {
        public ProfilePage()
        {
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            RetrieveUserData();
        }

        public async void RetrieveUserData()
        {
            string userId = Application.Current.Properties["userId"].ToString();

            User userData = await App.profileService.ViewProfileAsync(userId);
            photoProfile.Source = "profile-pic-dummy.jpg";
            namaProfile.Text = userData.displayName != "" ? userData.displayName : "{Name}";
            usernameProfile.Text = userData.userName != "" ? "#" + userData.userName : "{User Name}";
            emailProfile.Text = userData.email != "" ? userData.email : "{Email}";
        }

        private async void UpdateButton_Clicked(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync("//update");

        }
    }
}