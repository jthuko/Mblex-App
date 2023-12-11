
using MblexApp.Models;
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
    
}

