
using MblexApp;
using MblexApp.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Maui.Controls;
using Plugin.InAppBilling;

namespace MblexApp
{
    public partial class MainPage : ContentPage
    {
        int count = 0;
        private readonly IInAppBillingService inAppBillingService;
        public MainPage()
        {
            InitializeComponent();
            // Initialize in-app billing
           
            // Get the InAppBillingService instance using DependencyService
            inAppBillingService = DependencyService.Get<IInAppBillingService>();
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
          var  connected = await CrossInAppBilling.Current.ConnectAsync();

            // Check if there are pending orders, if so then subscribe
            var purchasesInfo = await CrossInAppBilling.Current.GetProductInfoAsync(ItemType.Subscription, productIds);
            var purchases = await CrossInAppBilling.Current.GetPurchasesAsync(ItemType.Subscription);
            CrossInAppBilling.Current.DisconnectAsync();
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