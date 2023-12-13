
using MblexApp.Models;
using MblexApp.StaticMethods;
using MblexApp.ViewModel;
namespace MblexApp;

public partial class SubscriptionPlansPage : ContentPage
{
    
    private readonly SubscriptionViewModel viewModel;
    public SubscriptionPlansPage()
    {
        InitializeComponent();
       
        viewModel = new SubscriptionViewModel();
        BindingContext = viewModel;      
    }

    private async void SubscriptionButton_Clicked(object sender, EventArgs e)
    {
        //chack if user is logged in, if not take them to login 
        //if logged in get user info then do the purchase
       bool purchaseSuccessful = await InAppBilling.PurchaseItem("mblexpremium");
        if(purchaseSuccessful)
        {
            await DisplayAlert("Success", "Your subscription has started. Enjoy", "Ok");
            //Save to user on db
            //update settings
        }
        else
        {
            await DisplayAlert("Failed", "Transaction failed. Please try again", "Ok");
        }
    }
}

