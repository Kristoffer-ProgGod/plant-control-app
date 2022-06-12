using PlantControlApp.ViewModels;
using Xamarin.Forms.Xaml;

namespace PlantControlApp.Views;

[XamlCompilation(XamlCompilationOptions.Compile)]
public partial class PairingInfoView
{
    public PairingInfoView()
    {
        InitializeComponent();
        BindingContext = App.Current.Services.GetService<PairingInfoViewModel>();
    }
}