using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Net.Http;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using System.Windows.Input;
using PlantControl.Models;
using PlantControlApp.Services;
using Xamarin.Forms;

namespace PlantControlApp.ViewModels;

internal class PairingViewModel : Bindable
{
    private readonly HttpClient _http;
    private readonly ScannerService _scannerService;

    public ObservableCollection<Pairing> Pairings { get; }
    public ICommand AppearingCommand { get; }
    public ICommand CreatePairingCommand { get; }


    public PairingViewModel(HttpClient http, ScannerService scannerService)
    {
        _http = http;
        _scannerService = scannerService;
        Pairings = new ObservableCollection<Pairing>();

        AppearingCommand = new Command(async () => await OnAppearing());
        CreatePairingCommand = new Command(async () => await OnCreatePairing());
    }

    private async Task OnAppearing()
    {
        var response = await _http.GetStringAsync("pairings");

        if (response == null) return;
        
        var pairings = JsonSerializer.Deserialize<Pairing[]>(response);

        foreach (var pairing in pairings)
        {
            Pairings.Add(pairing);
        }
    }

    private async Task OnCreatePairing()
    {
        var plantId = await _scannerService.Scan(bottomText: "Scan the QR code on the plant");
        var loggerId = await _scannerService.Scan(bottomText: "Scan the QR code on the logger");
    }
}