﻿
using MblexApp.Models;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;


namespace MblexApp.ViewModel
{

    public class TechniquesViewModel : INotifyPropertyChanged
    {
        private ObservableCollection<PublicQuestion> publicQuestions;
        public ObservableCollection<PublicQuestion> PublicQuestions
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

        private string selectedValue;
        public string SelectedValue
        {
            get { return selectedValue; }
            set
            {
                if (selectedValue != value)
                {
                    selectedValue = value;
                    OnPropertyChanged();
                }
            }
        }
       

        private readonly AppService appService;

        public TechniquesViewModel(AppService appService)
        {
            this.appService = appService;
            PublicQuestions = new ObservableCollection<PublicQuestion>();
            LoadPublicQuestions();           
        }       

        private async void LoadPublicQuestions()
        {
            // Retrieve public questions from the QuestionService
            PublicQuestions.Clear();
            var publicQuestions = await appService.GetPublicQuestionsAsync(5);
            foreach (var question in publicQuestions)
            {
                PublicQuestions.Add(question);
            }
        }


        // Implement INotifyPropertyChanged interface for property change notification
        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }   
}