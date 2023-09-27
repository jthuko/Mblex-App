using MblexApp.Context;
using MblexApp;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;


namespace MblexApp
{
    public partial class MainPage : ContentPage
    {
        int count = 0;

        public MainPage()
        {
            InitializeComponent();
            var services = new ServiceCollection();

            // Register your DbContext and other services here
            services.AddDbContext<MyDbContext>(options =>
            {
                options.UseSqlServer("Server=tcp:jtappserver.database.windows.net,1433;Initial Catalog=MblexDB;Persist Security Info=False;User ID=jthuko;Password=Jnzusyo77!;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;");
            });
            // Build the service provider
            var serviceProvider = services.BuildServiceProvider();
        }
        

        private void OnCounterClicked(object sender, EventArgs e)
        {
            App.Current.MainPage = new NavigationPage(new HomePage());
        }
    }
}