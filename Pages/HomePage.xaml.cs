
using MblexApp.Models;
using MblexApp.ViewModel;
using Microsoft.Maui.Controls;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;
namespace MblexApp;

public partial class HomePage : ContentPage
{
    private AppService appService;
    private readonly string connectionString = "Server=tcp:jtappserver.database.windows.net,1433;Initial Catalog=MblexDB;Persist Security Info=False;User ID=jthuko;Password=Jnzusyo77!;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";
    public int SubjectID { get; }
    public HomePage()
    {
        InitializeComponent();
        //SubjectID = subjectID;
       
        appService = new AppService(connectionString);
    }
    protected override void OnAppearing()
    {
        base.OnAppearing();

        // Perform initialization or load data here
       SubjectList.ItemsSource = LoadData();
    }

    private List<SubjectModel> LoadData()
    {
        // Load data to be displayed on the page
        // For example, you might fetch data from a service or a database
        // and update the UI accordingly
        return appService.GetSubjects();
    }

}

