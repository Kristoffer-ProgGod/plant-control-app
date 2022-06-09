using PlantControlApp.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PlantControlApp.Views;

[XamlCompilation(XamlCompilationOptions.Compile)]
public partial class PlantView : ContentPage
{
    public PlantView()
    {
        InitializeComponent();
        BindingContext = App.Current.Services.GetService<PlantViewModel>();
    }
}