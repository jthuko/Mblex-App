
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
        private ObservableCollection<Question> publicQuestions;
        public ObservableCollection<Question> PublicQuestions
        {
            get { return publicQuestions; }
            set
            {
                if (publicQuestions != value)
                {
                    publicQuestions = value;
                    OnPropertyChanged();
                }
            }
        }

        private List<string> choices;
        public List<string> Choices
        {
            get { return choices; }
            set
            {
                if (choices != value)
                {
                    choices = value;
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

        public ICommand SelectAnswerCommand { get; private set; }

        private readonly QuestionService questionService;

        public QuestionViewModel(QuestionService questionService)
        {
            this.questionService = questionService;
            PublicQuestions = new ObservableCollection<Question>();
            Choices = new List<string>(); // Initialize choices
            LoadPublicQuestions();
            InitializeCommands();
        }

        private void InitializeCommands()
        {
            SelectAnswerCommand = new Command<int>(selectedChoiceIndex =>
            {
                // Implement your logic to obtain the current question here
                // For example, you can use the index of the selected question
                Question selectedQuestion = GetCurrentQuestion(selectedChoiceIndex);

                if (selectedQuestion != null)
                {
                    CheckAnswer(selectedQuestion, selectedChoiceIndex);
                }
            });
        }

        private void LoadPublicQuestions()
        {
            // Retrieve public questions from the QuestionService
            PublicQuestions.Clear();
            List<Question> publicQuestions = questionService.GetPublicQuestions();
            foreach (var question in publicQuestions)
            {
                PublicQuestions.Add(question);
            }
        }

        private Question GetCurrentQuestion(int selectedChoiceIndex)
        {
            // Implement your logic to get the current question here
            // For example, you can use the selectedChoiceIndex to find the current question
            if (selectedChoiceIndex >= 0 && selectedChoiceIndex < PublicQuestions.Count)
            {
                return PublicQuestions[selectedChoiceIndex];
            }
            return null;
        }

        private void CheckAnswer(Question question, int selectedChoiceIndex)
        {
            if (question.CorrectChoiceIndex == selectedChoiceIndex)
            {
                // The answer is correct
                question.IsCorrectAnswer = true;
            }
            else
            {
                // The answer is incorrect
                question.IsCorrectAnswer = false;
            }

            // Mark the question as answered
            question.IsAnswered = true;
        }

        // Implement INotifyPropertyChanged interface for property change notification
        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }   
}
