using MblexApp.ApiService;
using MblexApp.Models;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;


namespace MblexApp.ViewModel
{
    public class QuestionViewModel:INotifyPropertyChanged
    {
        // Event to notify the UI of property changes
        public event PropertyChangedEventHandler PropertyChanged;

        private readonly QuestionService questionService;

        private ObservableCollection<Question> questions;
        public ObservableCollection<Question> Questions
        {
            get { return questions; }
            set
            {
                if (questions != value)
                {
                    questions = value;
                    OnPropertyChanged();
                }
            }
        }

        private Question currentQuestion;
        public Question CurrentQuestion
        {
            get { return currentQuestion; }
            set
            {
                if (currentQuestion != value)
                {
                    currentQuestion = value;
                    OnPropertyChanged();
                }
            }
        }

        private string selectedAnswer;
        public string SelectedAnswer
        {
            get { return selectedAnswer; }
            set
            {
                if (selectedAnswer != value)
                {
                    selectedAnswer = value;
                    OnPropertyChanged();
                }
            }
        }

        private bool isAnswerCorrect;
        public bool IsAnswerCorrect
        {
            get { return isAnswerCorrect; }
            set
            {
                if (isAnswerCorrect != value)
                {
                    isAnswerCorrect = value;
                    OnPropertyChanged();
                }
            }
        }

        public ICommand CheckAnswerCommand { get; private set; }
        public ICommand UpdateQuestionCommand { get; private set; }
        public ICommand PatchQuestionCommand { get; private set; }
        public ICommand DeleteQuestionCommand { get; private set; }

        public QuestionViewModel(QuestionService questionService)
        {
            this.questionService = questionService;
            // Initialize your ObservableCollection of questions here.
            Questions = new ObservableCollection<Question>();

            // Initialize the commands
            CheckAnswerCommand = new Command(CheckAnswer);
            // Initialize the command to perform PUT operation (update a question).
            UpdateQuestionCommand = new Command(UpdateQuestionAsync);
            // Initialize the command to perform PATCH operation (partially update a question).
            PatchQuestionCommand = new Command(PatchQuestion);
            // Initialize the command to perform DELETE operation (delete a question).
            DeleteQuestionCommand = new Command(DeleteQuestionAsync);
        }
        public async Task LoadQuestionsFromDatabase()
        {
            // Fetch questions from the service (Azure SQL Database).
            var questions = await questionService.GetQuestionsAsync();

            // Populate the Questions ObservableCollection with data from the service.
            Questions.Clear(); // Clear existing data
            foreach (var question in questions)
            {
                Questions.Add(question);
            }

            // Set the CurrentQuestion to the first question if needed.
            CurrentQuestion = Questions.FirstOrDefault();
        }
        private void CheckAnswer()
        {
            if (string.Equals(SelectedAnswer, CurrentQuestion.CorrectAnswer, StringComparison.OrdinalIgnoreCase))
            {
                IsAnswerCorrect = true;
            }
            else
            {
                IsAnswerCorrect = false;
            }
        }

        private async void UpdateQuestionAsync()
        {
            await questionService.UpdateQuestionAsync(CurrentQuestion);
            // Optionally, you can reload the questions from the database after an update.
           await LoadQuestionsFromDatabase();
        }
        private async void PatchQuestion()
        {
            // Construct a dictionary of changes.
            var changes = new Dictionary<string, object>();
            // Add properties and their updated values to the dictionary.
            // For example:
            // changes.Add("Text", "New Text");
            // changes.Add("CorrectAnswer", "New Correct Answer");

           await questionService.PatchQuestionAsync(CurrentQuestion.Id, changes);
            // Optionally, you can reload the questions from the database after a patch.
            await LoadQuestionsFromDatabase();
        }

        private async void DeleteQuestionAsync()
        {
            await questionService.DeleteQuestionAsync(CurrentQuestion.Id);
            // Optionally, you can reload the questions from the database after a delete.
            await LoadQuestionsFromDatabase();
        }

        // Helper method to raise property change events
        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }   
}
