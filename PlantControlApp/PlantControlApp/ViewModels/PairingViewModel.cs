using System;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;
using ZXing.Mobile;

namespace PlantControlApp.ViewModels
{
    internal class PairingViewModel : Bindable
    {
        private ICommand scanCommand;

        public ICommand ScanCommand
        {
            get { return scanCommand; }
            set { scanCommand = value; }
        }


        private string scanResult;

        public string ScanResult
        {
            get { return scanResult; }
            set { scanResult = value; OnPropertyChanged(); }
        }
        public PairingViewModel()
        {
            scanCommand = new Command(async () =>
            {
                var status = await Permissions.RequestAsync<Permissions.Camera>();

                MobileBarcodeScanner scanner = new MobileBarcodeScanner();
                
                scanner.UseCustomOverlay = false;
                scanner.CameraUnsupportedMessage = "This Device's Camera is not supported.";
                scanner.TopText = "Hold the camera up to the barcode\nAbout 6 inches away";
                scanner.BottomText = "Wait for the barcode to automatically scan!";


                await scanner.Scan().ContinueWith(t =>
                {
                    if(t.Result != null)
                    {
                        Console.WriteLine("Scanned Barcode: " + t.Result.Text);
                    }
                });
            });
        }
    }
}
