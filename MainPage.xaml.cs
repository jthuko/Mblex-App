
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
        protected override void OnAppearing()
        {
            base.OnAppearing();
            var billing = CrossInAppBilling.Current;
            // Replace "your_subscription_product_id_here" with the actual subscription product ID
            var connected = billing.ConnectAsync().Result;
            if (connected)
            {
                var subscriptionProductId = "mblexpremium";
                var subscriptionInfo = inAppBillingService.GetProductInfoAsync(ItemType.Subscription, subscriptionProductId).Result;
            }
             
           

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