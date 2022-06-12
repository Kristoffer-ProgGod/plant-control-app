using System.Net.Http;
using System.Threading.Tasks;
using System.Windows.Input;
using Newtonsoft.Json;
using PlantControl.Models;
using Xamarin.CommunityToolkit.ObjectModel;
using Xamarin.Forms;
using ObservableObject = CommunityToolkit.Mvvm.ComponentModel.ObservableObject;

namespace PlantControlApp.ViewModels;

[QueryProperty(nameof(PairingId), "pairingId")]
public class PairingInfoViewModel : ObservableObject
{
    private readonly HttpClient _http;
    private string _pairingId;
    private Pairing _pairing;

    public string PairingId
    {
        get => _pairingId;
        set => SetProperty(ref _pairingId, value);
    }

    public Pairing Pairing
    {
        get => _pairing;
        set => SetProperty(ref _pairing, value);
    }

    public ICommand RefreshCommand { get; }

    public PairingInfoViewModel(HttpClient http)
    {
        _http = http;
        
        RefreshCommand = new AsyncCommand(Refresh);
    }

    private async Task Refresh()
    {
        Pairing = JsonConvert.DeserializeObject<Pairing>(await _http.GetStringAsync($"pairings/{PairingId}"));
    }
}