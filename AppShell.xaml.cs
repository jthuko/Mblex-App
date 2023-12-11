using MblexApp.Services;

namespace MblexApp
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();
            // Check if user settings already exist
            var existingUserSettings = AuthenticationService.GetUserSettings();
            if(existingUserSettings != null && existingUserSettings.IsPremium)
            {
                Upgrade.Text = "Unsubscribe";
            }
            else
            {
                Upgrade.Text = "Subscribe";
            }        
        }

        private async void Upgrade_Clicked(object sender, EventArgs e)
        {
            Label label = sender as Label;
            if(label != null &&  label.Text == "Unsubscribe")
            {
                //1. set settings to isPremium = false
                //2. Set user.ispremium = false
                //3. Unscubsribe inAppBilling
            }
            else
            {
                //navigate to Subscription page
                App.Current.MainPage = new NavigationPage(new SubscriptionPlansPage());
            }
        }
    }
}