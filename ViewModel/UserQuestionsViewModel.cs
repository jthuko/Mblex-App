
using MblexApp.Models;
using MblexApp.Services;
using MblexApp.StaticMethods;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;


namespace MblexApp.ViewModel
{

    public class UserQuestionsViewModel : INotifyPropertyChanged
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
        private bool isBusy;

        public bool IsBusy
        {
            get { return isBusy; }
            set
            {
                if (isBusy != value)
                {
                    isBusy = value;
                    OnPropertyChanged(nameof(IsBusy));
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
        private readonly int subjectID;

        public UserQuestionsViewModel(AppService appService, int SubjectID)
        {
            this.appService = appService;
            this.subjectID = SubjectID;

            PublicQuestions = new ObservableCollection<PublicQuestion>();
            LoadPublicQuestions(SubjectID);           
        }
        public void ReloadQuestions()
        {
            // Implement logic to reload questions or reset properties
            // For example:
            LoadPublicQuestions(subjectID);
        }
        private async void LoadPublicQuestions(int subjectID)
        {
            // Check if user settings already exist
            var existingUserSettings = AuthenticationService.GetUserSettings();
            // Retrieve public questions from the QuestionService
            PublicQuestions.Clear();
            IsBusy = true;
            var publicQuestions = await appService.GetPublicQuestionsAsync(subjectID);
            if (existingUserSettings != null && existingUserSettings.IsPremium && existingUserSettings.IsLoggedIn)
            {
                // Assuming PublicQuestion is the type of objects in the ObservableCollection
                var filteredQuestions = new ObservableCollection<PublicQuestion>(
                    publicQuestions.Where(question => question.Username == existingUserSettings.Username)
                );
                if (filteredQuestions.Count > 0)
                {
                    foreach (var question in filteredQuestions)
                    {
                        PublicQuestions.Add(question);
                    }
                    CollectionShuffler.Shuffle(PublicQuestions);
                    KeepElements.KeepAtMostNElements(PublicQuestions, 10);

                }
                else
                {
                    Application.Current.MainPage.DisplayAlert("Create", "You have no questions created. Please Create new questions", "OK");
                }
            }
            else if (existingUserSettings != null && existingUserSettings.IsPremium && !existingUserSettings.IsLoggedIn)
            {
                // The following code is a generic example and might not work directly in your project
                Application.Current.MainPage.DisplayAlert("Login", "Please login", "OK");
            }
            else if (existingUserSettings != null && !existingUserSettings.IsPremium) 
            {
                // The following code is a generic example and might not work directly in your project
                Application.Current.MainPage.DisplayAlert("Subscribe","Please subscribe to create questions", "OK");
            }
            
            IsBusy = false;
        }


        // Implement INotifyPropertyChanged interface for property change notification
        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }   
}
