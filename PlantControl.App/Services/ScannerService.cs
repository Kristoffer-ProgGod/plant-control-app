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

    public async Task<string> Scan(string topText = "", string bottomText = "")
    {
        await Permissions.RequestAsync<Permissions.Camera>();

        _scanner.TopText = topText;
        _scanner.BottomText = bottomText;

        return (await _scanner.Scan()).Text;
    }
}