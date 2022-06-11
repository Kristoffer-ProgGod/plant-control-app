using System;
using Xamarin.Forms.Xaml;

namespace PlantControlApp.Popups;

[XamlCompilation(XamlCompilationOptions.Compile)]
public partial class CreatePairingPopup
{
    public CreatePairingPopup()
    {
        InitializeComponent();

        Entry.TextChanged += (_, args) => Button.IsEnabled = !string.IsNullOrWhiteSpace(args.NewTextValue);
    }

    private void Button_OnClicked(object sender, EventArgs e)
    {
        Dismiss(Entry.Text);
    }
}