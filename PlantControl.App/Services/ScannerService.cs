using System.Threading.Tasks;
using Xamarin.Essentials;
using ZXing.Mobile;

namespace PlantControlApp.Services;

public class ScannerService
{
    private readonly MobileBarcodeScanner _scanner;
    
    public ScannerService()
    {
        _scanner = new MobileBarcodeScanner();
    }

    /// <summary>
    /// Scans a barcode
    /// </summary>
    /// <param name="topText">The prompt above where the image is taken on the screen</param>
    /// <param name="bottomText">The prompt below where the image is taken on the screen</param>
    /// <returns>String value of the scanned barcode</returns>
    public async Task<string> Scan(string topText = "", string bottomText = "")
    {
        await Permissions.RequestAsync<Permissions.Camera>();

        _scanner.TopText = topText;
        _scanner.BottomText = bottomText;

        return (await _scanner.Scan()).Text;
    }
}