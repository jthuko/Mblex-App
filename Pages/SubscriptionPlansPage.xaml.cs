
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
       bool purchaseSuccessful = await InAppBilling.PurchaseItem("mblexpremium");
        if(purchaseSuccessful)
        {
            await DisplayAlert("Success", "Your subscription has started. Enjoy", "Ok");
        }
        else
        {
            await DisplayAlert("Failed", "Transaction failed. Please try again", "Ok");
        }
    }
}

