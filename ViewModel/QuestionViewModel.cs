﻿
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
        private readonly QuestionService questionService;
        public ObservableCollection<Question> PublicQuestions { get; set; }

        // Event to notify the UI of property changes
        public event PropertyChangedEventHandler PropertyChanged;           

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

        public ICommand SelectAnswerCommand { get; private set; }


        public QuestionViewModel(QuestionService questionService)
        {
            this.questionService = questionService;
            PublicQuestions = new ObservableCollection<Question>();
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

        // Implement the logic to obtain the current question based on the selectedChoiceIndex
        private Question GetCurrentQuestion(int selectedChoiceIndex)
        {
            // Replace this with your actual logic to obtain the current question.
            // You might use the selectedChoiceIndex or other criteria to determine the current question.
            // Return the appropriate Question object or null if the current question is not found.
            // Example: 
            // return PublicQuestions.FirstOrDefault(q => q.Id == selectedChoiceIndex);

            // For demonstration purposes, returning the first question in the list.
            return PublicQuestions.FirstOrDefault();
        }

        private void CheckAnswer(Question selectedQuestion, int selectedChoiceIndex)
        {
            // Check if the selected choice is correct for the current question.
            IsAnswerCorrect = selectedQuestion.CorrectChoiceIndex == selectedChoiceIndex;
        }

        private void LoadPublicQuestions()
        {
            // Load a list of all public questions.
            PublicQuestions.Clear();
            var publicQuestions = questionService.GetPublicQuestions();
            foreach (var question in publicQuestions)
            {
                PublicQuestions.Add(question);
            }
        }
       
        // Helper method to raise property change events
        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }   
}