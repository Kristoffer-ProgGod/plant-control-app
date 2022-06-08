using System.Threading.Tasks;
using System.Windows.Input;
using PlantControl.Models;
using Xamarin.Essentials;
using Xamarin.Forms;
using ZXing.Mobile;

namespace PlantControlApp.ViewModels;

internal class PairingViewModel : Bindable
{
    public PairingViewModel()
    {
        CreatePairingCommand = new Command(async () =>
        {
            var plant = new Plant();
            plant.Id = await Scan("plant");
            var logger = new Logger();
            logger.Id = await Scan("logger");
            Pairing = new Pairing
            {
                Logger = logger,
                Plant = plant
            };
        });
    }

    public ICommand CreatePairingCommand { get; set; }

    public string PairingName { get; set; }

    public Pairing Pairing { get; set; }

    public async Task<string> Scan(string barcodeType)
    {
        var result = string.Empty;
        var status = await Permissions.RequestAsync<Permissions.Camera>();

        var scanner = new MobileBarcodeScanner();

        scanner.UseCustomOverlay = false;
        scanner.CameraUnsupportedMessage = "This Device's Camera is not supported.";
        scanner.TopText = $"Hold the camera up to the {barcodeType} QRCode\nAbout 15 cm away";
        scanner.BottomText = "Wait for the barcode to automatically scan";

        await scanner.Scan().ContinueWith(t =>
        {
            if (t.Result != null) result = t.Result.Text;
        });

        return result;
    }
}