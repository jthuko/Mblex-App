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
    private void OnQuestionSelected(object sender, SelectedItemChangedEventArgs e)
    {
        if (e.SelectedItem is Question selectedQuestion)
        {
            selectedQuestion.IsAnswered = !selectedQuestion.IsAnswered;
            QuestionsListView.SelectedItem = null; // Deselect the item
        }
    } 
}

