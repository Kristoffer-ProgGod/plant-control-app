using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using System.Windows.Input;
using PlantControl.Models;
using PlantControlApp.Popups;
using PlantControlApp.Services;
using PlantControlApp.Views;
using Xamarin.CommunityToolkit.Extensions;
using Xamarin.CommunityToolkit.ObjectModel;
using Xamarin.Forms;
using ObservableObject = CommunityToolkit.Mvvm.ComponentModel.ObservableObject;

namespace PlantControlApp.ViewModels;

internal class PairingViewModel : ObservableObject
{
    private readonly HttpClient _http;
    private readonly ScannerService _scannerService;
    private readonly SignalRService _signalRService;
    private bool _isRefreshing;

    public bool IsRefreshing
    {
        get => _isRefreshing;
        set => Device.BeginInvokeOnMainThread(() => SetProperty(ref _isRefreshing, value));
    }

    public ObservableCollection<Pairing> Pairings { get; }
    public ICommand SelectCommand { get; }
    public ICommand RefreshCommand { get; }
    public ICommand CreatePairingCommand { get; }


    public PairingViewModel(HttpClient http, ScannerService scannerService, SignalRService signalRService)
    {
        _http = http;
        _scannerService = scannerService;
        _signalRService = signalRService;

        Pairings = new ObservableCollection<Pairing>();
        SelectCommand = new AsyncCommand<Pairing>(Select);
        RefreshCommand = new AsyncCommand(Refresh);
        CreatePairingCommand = new AsyncCommand(CreatePairing);
    }

    private async Task Select(Pairing pairing)
    {
        await Shell.Current.GoToAsync($"//{nameof(PairingView)}/{nameof(PairingInfoView)}?pairingId={pairing.Id}");
    }

    private async Task Refresh()
    {
        IsRefreshing = true;

        try
        {
            var pairings = await _http.GetFromJsonAsync<Pairing[]>("pairings");

            if (pairings == null) return;

            Pairings.Clear();

            foreach (var pairing in pairings)
            {
                Pairings.Add(pairing);
            }
        }
        catch (Exception e)
        {
            Debug.WriteLine(e.Message);
        }

        IsRefreshing = false;
    }

    private async Task CreatePairing()
    {
        var name = await App.Current.MainPage.Navigation.ShowPopupAsync(new CreatePairingPopup());
        if (string.IsNullOrWhiteSpace(name)) return;

        var plantId = await _scannerService.Scan(bottomText: "Scan the QR code on the plant");
        if (string.IsNullOrWhiteSpace(name)) return;

        var loggerId = await _scannerService.Scan(bottomText: "Scan the QR code on the logger");
        if (string.IsNullOrWhiteSpace(loggerId)) return;

        var plantExists = (await _http.GetAsync($"plants/{plantId}")).IsSuccessStatusCode;

        if (!plantExists)
        {
            App.Current.MainPage.Navigation.ShowPopup(new ErrorPopup("The plant does not exist in the database."));

            return;
        }

        var loggerExists = (await _http.GetAsync($"loggers/{loggerId}")).IsSuccessStatusCode;

        if (!loggerExists)
        {
            App.Current.MainPage.Navigation.ShowPopup(new ErrorPopup("The logger does not exist in the database."));

            return;
        }

        var result = await _http.PostAsJsonAsync("pairings", new
        {
            name,
            plant = plantId,
            logger = loggerId
        });

        await _signalRService.StartConnection();
        await _signalRService.SetPairingId(loggerId, await result.Content.ReadFromJsonAsync<string>());
        await Refresh();
    }
}