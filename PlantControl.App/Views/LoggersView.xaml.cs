using PlantControlApp.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PlantControlApp.Views;

[XamlCompilation(XamlCompilationOptions.Compile)]
public partial class LoggersView : ContentPage
{
    public LoggersView()
    {
        InitializeComponent();
        BindingContext = App.Current.Services.GetService<LoggersViewModel>();
    }
}