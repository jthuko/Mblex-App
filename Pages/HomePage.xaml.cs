
using MblexApp.Models;
using MblexApp.ViewModel;
namespace MblexApp;

public partial class HomePage : ContentPage
{
    private readonly string connectionString = "Server=tcp:jtappserver.database.windows.net,1433;Initial Catalog=MblexDB;Persist Security Info=False;User ID=jthuko;Password=Jnzusyo77!;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";
    private readonly QuestionViewModel viewModel;
    public HomePage()
    {
        InitializeComponent();

        // Resolve dependencies using DependencyService
        var questionService = new QuestionService(connectionString);
        viewModel = new QuestionViewModel(questionService);
        BindingContext = viewModel;

    }
    private void RadioButton_CheckedChanged(object sender, CheckedChangedEventArgs e)
    {
        if (sender is RadioButton radioButton && e.Value)
        {
            string selectedValue = (string)radioButton.Value;
            PublicQuestion selectedQuestion = radioButton.BindingContext as PublicQuestion;

            if (selectedQuestion != null)
            {
                // Find the choice that corresponds to the selected answer
                Choice selectedChoice = selectedQuestion.Choices.FirstOrDefault(choice => choice.ChoiceText == selectedValue);

                if (selectedChoice != null)
                {
                    // Check if the selected choice is marked as correct
                    bool isCorrect = selectedChoice.IsCorrect;

                }
            }
        }
    }


}

