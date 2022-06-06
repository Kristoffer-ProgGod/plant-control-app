using System;
using System.Windows.Input;
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
                

                MobileBarcodeScanner scanner = new MobileBarcodeScanner();

                var result = await scanner.Scan();

                if (result != null)
                    Console.WriteLine("Scanned Barcode: " + result.Text);
                else
                    Console.WriteLine("No result");
            });
        }
    }
}
