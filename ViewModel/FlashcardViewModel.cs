using MblexApp.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MblexPrep
{
    public class FlashcardViewModel : INotifyPropertyChanged
    {
        private bool isFlipped;
        private string questionText;
        private string answerText;

        private ObservableCollection<FlashCards> flashcardsViews;
        public ObservableCollection<FlashCards> FlashcardsViews
        {
            get => flashcardsViews;
            set
            {
                if (flashcardsViews != value)
                {
                    flashcardsViews = value;
                    OnPropertyChanged(nameof(FlashcardsViews));
                }
            }
        }


        public bool IsFlipped
        {
            get => isFlipped;
            set
            {
                if (isFlipped != value)
                {
                    isFlipped = value;
                    OnPropertyChanged(nameof(IsFlipped));
                }
            }
        }

        public string AnswerText
        {
            get => answerText;
            set
            {
                if (answerText != value)
                {
                    answerText = value;
                    OnPropertyChanged(nameof(AnswerText));
                }
            }
        }

        public string QuestionText
        {
            get => questionText;
            set
            {
                if (questionText != value)
                {
                    questionText = value;
                    OnPropertyChanged(nameof(QuestionText));
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public static ObservableCollection<FlashCards> InitializeFlashcards()
        {
            return new ObservableCollection<FlashCards>
            {
                new FlashCards { QuestionText = "Front 1", AnswerText = "Back 1" },
                new FlashCards  { QuestionText = "Front 2",  AnswerText = "Back 2" },
                new FlashCards { QuestionText = "Front 3",  AnswerText= "Back 3" }
            };
        }

        public FlashcardViewModel()
        {
            // Handle the tap gesture to flip the card
            Device.StartTimer(TimeSpan.FromSeconds(3), () => { IsFlipped = !IsFlipped; return true; });
        }
    }
}
