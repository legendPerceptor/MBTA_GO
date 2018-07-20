using System;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using MyMBTAApp.DataHandler;
namespace MyMBTAApp.Views
{
	public partial class MainPage : ContentPage
    {
		public HomePage myHomePage;
        public MainPage()
        {
            InitializeComponent();
			myHomePage = new HomePage();
        }
		private void LoginButton_Clicked(object sender, EventArgs args)
        {
            bool isEmailEmpty = string.IsNullOrEmpty(emailEntry.Text);
            bool isPasswordEmpty = string.IsNullOrEmpty(passwordEntry.Text);
            if (isEmailEmpty || isPasswordEmpty)
            {
				DisplayAlert("Error", "Password and Email cannot be empty", "OK");
            }
            else
            {
				
				Navigation.PushAsync(myHomePage);
            }
        }
		public void RefereshSchedulePage(DirectionEnum dir){
			myHomePage.RefreshSchedules(dir);
		}
    }
}