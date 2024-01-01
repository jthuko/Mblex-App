
using MblexApp.Models;
using MblexApp.ViewModel;
using MblexPrep;
using Microsoft.Maui.Controls;
using System.Collections.ObjectModel;


namespace MblexApp;

public partial class FlashCardsPage : ContentPage
{
    public ObservableCollection<FlashCards> Flashcards { get; set; }
    private int currentPosition = 0;

    public FlashCardsPage()
    {
        InitializeComponent();
        Flashcards = FlashcardViewModel.InitializeFlashcards();
        BindingContext = this;
    }

    private void OnCardTapped(object sender, EventArgs e)
    {
        if (sender is ContentView contentView && contentView.BindingContext is FlashcardViewModel viewModel)
        {
            viewModel.IsFlipped = !viewModel.IsFlipped;
        }
    }

    private void OnBackButtonClicked(object sender, EventArgs e)
    {
        if (currentPosition > 0)
        {
            currentPosition--;
           // (collectionView.ItemsSource as ObservableCollection<FlashCards>)?.ElementAt(currentPosition).IsFlipped = false;
            collectionView.ScrollTo(currentPosition, position: ScrollToPosition.Start, animate: true);
        }
    }

    private void OnForwardButtonClicked(object sender, EventArgs e)
    {
        if (currentPosition < Flashcards.Count - 1)
        {
            currentPosition++;
           // (collectionView.ItemsSource as ObservableCollection<FlashCards>)?.ElementAt(currentPosition).IsFlipped = false;
            collectionView.ScrollTo(currentPosition, position: ScrollToPosition.Start, animate: true);
        }
    }
}



