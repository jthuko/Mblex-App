
using MblexApp.Models;
using MblexApp.ViewModel;
namespace MblexApp;

public partial class AddQuestionsPage : ContentPage
{
    public int SubjectID { get; }
    private List<Entry> additionalQuestions = new List<Entry>();
    public AddQuestionsPage(int subjectID)
    {
        InitializeComponent();  
        SubjectID = subjectID;
    }
    private void OnAddQuestionClicked(object sender, EventArgs e)
    {
        Entry newQuestionEntry = new Entry
        {
            Placeholder = "New Question"
        };

        additionalQuestions.Add(newQuestionEntry);
        QuestionsStackLayout.Children.Insert(QuestionsStackLayout.Children.Count - 2, newQuestionEntry);
    }

    private void OnSaveQuestionsClicked(object sender, EventArgs e)
    {
        List<UserQuestions> userQuestionsList = new List<UserQuestions>();

        // Iterate through the existing and additional questions
        foreach (Entry entry in additionalQuestions)
        {
            UserQuestions userQuestion = new UserQuestions
            {
                Text = entry.Text,
                // Set other properties as needed
                Choices = new List<Choice>
                {
                    new Choice { ChoiceText = ChoiceOne.Text, IsCorrect = CorrectAnswerPicker.SelectedItem.ToString() == "ChoiceOne" },
                    new Choice { ChoiceText = ChoiceTwo.Text, IsCorrect = CorrectAnswerPicker.SelectedItem.ToString() == "ChoiceTwo" },
                    new Choice { ChoiceText = ChoiceThree.Text, IsCorrect = CorrectAnswerPicker.SelectedItem.ToString() == "ChoiceThree" }
                }
            };

            userQuestionsList.Add(userQuestion);
        }

        // Save userQuestionsList to the database
        // Your database logic goes here
    }

}

