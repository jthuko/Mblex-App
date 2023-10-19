using MblexApp.Context;
using MblexApp.Models;
using MblexApp.ViewModel;
namespace MblexApp;

public partial class HomePage : ContentPage
{
    private readonly MyDbContext dbContext;
    private readonly QuestionViewModel viewModel;
    public HomePage()
    {
        InitializeComponent();

        // Resolve dependencies using DependencyService
        dbContext = DependencyService.Get<MyDbContext>();
        viewModel = new QuestionViewModel(new QuestionService(dbContext));
        BindingContext = viewModel;
    }

    private void RadioButton_CheckedChanged(object sender, CheckedChangedEventArgs e)
    {
        if (sender is RadioButton radioButton && e.Value)
        {//
            string selectedValue = (string)radioButton.Value;
            // Now, 'selectedValue' contains the text of the checked radio button
            Question selectedDataObject = radioButton.BindingContext as Question;

            if (selectedDataObject != null)
            {
                // 'selectedDataObject' contains the data object bound to the checked radio button
            }
        }

    }
}

