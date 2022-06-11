using Xamarin.Forms.Xaml;

namespace PlantControlApp.Popups;

[XamlCompilation(XamlCompilationOptions.Compile)]
public partial class ErrorPopup
{
    public ErrorPopup(string message)
    {
        InitializeComponent();

        Label.Text = message;
    }
}