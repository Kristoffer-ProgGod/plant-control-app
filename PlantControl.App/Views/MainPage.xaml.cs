using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PlantControlApp.Views;

[XamlCompilation(XamlCompilationOptions.Compile)]
public partial class MainPage : Shell
{
    public MainPage()
    {
        InitializeComponent();
        Routing.RegisterRoute(nameof(DataView), typeof(DataView));
        Routing.RegisterRoute(nameof(PlantView), typeof(PlantView));
        Routing.RegisterRoute(nameof(PairingView), typeof(PairingView));
        Routing.RegisterRoute(nameof(PairingInfoView), typeof(PairingInfoView));
        Routing.RegisterRoute(nameof(CreatePlantView), typeof(CreatePlantView));
        Routing.RegisterRoute(nameof(LoggerConfigView), typeof(LoggerConfigView));
    }
}