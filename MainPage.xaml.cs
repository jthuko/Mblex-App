namespace MblexApp
{
    public partial class MainPage : ContentPage
    {
        int count = 0;

        public MainPage()
        {
            InitializeComponent();
        }

        private void OnCounterClicked(object sender, EventArgs e)
        {
            count++;
            
            CounterBtn.Text = count >= 2 ? $"Clicked {count} times": $"Clicked {count} time";
           
            SemanticScreenReader.Announce(CounterBtn.Text);
        }
    }
}