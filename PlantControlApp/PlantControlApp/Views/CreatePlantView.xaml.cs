using PlantControlApp.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PlantControlApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CreatePlantView : ContentPage
    {
        public CreatePlantView()
        {
            InitializeComponent();
            BindingContext = new CreatePlantViewModel();
        }
    }
}