
using MblexApp;
using MblexApp.Services;
using MblexApp.StaticMethods;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Maui.Controls;
using Plugin.InAppBilling;

namespace MblexApp
{
    public partial class MainPage : ContentPage
    {
        int count = 0;
       
        public MainPage()
        {
            InitializeComponent();
            // Initialize in-app billing
                     
        }
        protected override async void OnAppearing()

        {
            base.OnAppearing();
            await GetPurchase();

        }

        private async Task GetPurchase()
        {
            var productIds = new string[] { "mblexpremium" };
            // Connect to the service here
          var  product = await InAppBilling.GetProductInfoAsync(productIds);                 
        }

        private void OnSignupClicked(object sender, EventArgs e)
        {       

                App.Current.MainPage = new NavigationPage(new SignupPage());
        }

        private void OnLoginClicked(object sender, EventArgs e)
        {
            string usernameOrEmail = UsernameEntry.Text;
            string password = PasswordEntry.Text;

            // Add your authentication logic here
            if (LoginUser(usernameOrEmail, password))
            {
                DisplayAlert("Login Successful", "Welcome, " + usernameOrEmail + "!", "OK");
            }
            else
            {
                DisplayAlert("Login Failed", "Invalid username or password", "OK");
            }
        }
        private bool LoginUser(string usernameOrEmail, string password)
        {
            // Replace this with your actual authentication logic using the LoginUser method
            return UserManager.LoginUser(usernameOrEmail, password);
        }


    }
}