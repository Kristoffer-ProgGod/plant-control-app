using PlantControl.Models;
using PlantControl.Xamarin.Models;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;
using ZXing.Mobile;

namespace PlantControlApp.ViewModels
{
    internal class PairingViewModel : Bindable
    {
        private ICommand createPairingCommand;

        public ICommand CreatePairingCommand
        {
            get { return createPairingCommand; }
            set { createPairingCommand = value; }
        }

        private string pairingName;

        public string PairingName
        {
            get { return pairingName; }
            set { pairingName = value; }
        }

        private Pairing pairing;

        public Pairing Pairing
        {
            get { return pairing; }
            set { pairing = value; }
        }

        public PairingViewModel()
        {
            createPairingCommand = new Command(async () =>
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

        public async Task<string> Scan(string barcodeType)
        {
            string result = string.Empty;
            var status = await Permissions.RequestAsync<Permissions.Camera>();

            MobileBarcodeScanner scanner = new MobileBarcodeScanner();

            scanner.UseCustomOverlay = false;
            scanner.CameraUnsupportedMessage = "This Device's Camera is not supported.";
            scanner.TopText = $"Hold the camera up to the {barcodeType} QRCode\nAbout 15 cm away";
            scanner.BottomText = "Wait for the barcode to automatically scan";

            try
            {
                await scanner.Scan().ContinueWith(t =>
                {
                    if (t.Result != null)
                    {
                        result = t.Result.Text;
                    }
                });
            }
            catch
            {
                throw;
            }
            
            return result;
        }
    }
}
