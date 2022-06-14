using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using System.Windows.Input;
using PlantControl.Models;
using Xamarin.CommunityToolkit.ObjectModel;
using Xamarin.Forms;

namespace PlantControlApp.ViewModels;

internal class PlantViewModel : ObservableObject
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
}