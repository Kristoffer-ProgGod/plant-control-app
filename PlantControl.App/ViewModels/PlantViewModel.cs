using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using System.Windows.Input;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using PlantControl.Models;
using PlantControlApp.Views;
using Xamarin.CommunityToolkit.ObjectModel;
using Xamarin.Forms;
using ObservableObject = CommunityToolkit.Mvvm.ComponentModel.ObservableObject;

namespace PlantControlApp.ViewModels;

internal partial class PlantViewModel : ObservableObject
{
    private readonly HttpClient _http;

    private bool _isRefreshing;

    public bool IsRefreshing
    {
        get => _isRefreshing;
        set => SetProperty(ref _isRefreshing, value);
    }
    public ObservableCollection<Plant> PlantList { get; }

    public ICommand GetPlantListCMD { get; set; }

    public ICommand SwitchViewCommand { get; set; }
    
    [ObservableProperty]
    [NotifyCanExecuteChangedFor(nameof(NavigatePlantLogsCommand))]
    private Plant _selectedPlant;
    
    private bool IsPlantSelected => SelectedPlant != null;

    public PlantViewModel(HttpClient http)
    {
        _http = http;
        SwitchViewCommand = new AsyncCommand(async () => await Shell.Current.GoToAsync("//PlantView/CreatePlantView"));
        GetPlantListCMD = new AsyncCommand(GetPlantList);
        PlantList = new ObservableCollection<Plant>();

    }
    private async Task GetPlantList()
    {
        IsRefreshing = true;
        try
        {
            var plants = await _http.GetFromJsonAsync<Plant[]>("plants");

            if (plants == null) return;

            PlantList.Clear();

            foreach (var plant in plants)
            {
                PlantList.Add(plant);
            }
        }
        catch (Exception e)
        {
            Debug.WriteLine(e.Message);
        }

        IsRefreshing = false;
    }
    
    [RelayCommand(CanExecute = nameof(IsPlantSelected))]
    private async Task NavigatePlantLogs()
    {
        await Shell.Current.GoToAsync($"{nameof(DataView)}?plantId={SelectedPlant.Id}");
    }
}