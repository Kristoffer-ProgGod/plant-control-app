using PlantControlApp.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PlantControlApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PairingView : ContentPage
    {

        public PairingView()
        {
            InitializeComponent();
            BindingContext = new PairingViewModel();
        }
    }
}