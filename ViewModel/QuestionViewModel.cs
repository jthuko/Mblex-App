
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

        
        private string _selectedValue;
        public string SelectedValue
        {
            get { return _selectedValue;}
            set
            {
                if (_selectedValue != value)
                {
                    _selectedValue = value;
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
                // Implement your logic to obtain the current question here
                // For example, you can use the index of the selected question
                                         
        }

        private void LoadPublicQuestions()
        {
            // Retrieve public questions from the QuestionService
            PublicQuestions.Clear();
            ObservableCollection<Question> publicQuestions = questionService.GetPublicQuestions();
            foreach (var question in publicQuestions)
            {
                PublicQuestions.Add(question);
            }
        }

       

        private void CheckAnswer(Question question, string selectedValue)
        {
            if (question.CorrectAnswer == selectedValue)
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
