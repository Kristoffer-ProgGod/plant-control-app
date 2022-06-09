using System.Threading.Tasks;
using System.Windows.Input;
using PlantControlApp.Services;
using Xamarin.Forms;

namespace PlantControlApp.ViewModels;

internal class PairingViewModel : Bindable
{
    private readonly ScannerService _scannerService;
    
    public ICommand CreatePairingCommand { get; set; }


    public PairingViewModel(ScannerService scannerService)
    {
        _scannerService = scannerService;
        
        CreatePairingCommand = new Command(async () => await OnCreatePairing());
    }

    private async Task OnCreatePairing()
    {
        var plantId = await _scannerService.Scan();
    }
}