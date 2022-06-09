using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using PlantControl.Models;
using PlantControlApp.Services;
using PlantControlApp.Views;
using Xamarin.Forms;
using Xamarin.Forms.Internals;

namespace PlantControlApp.ViewModels;

[ObservableObject]
public partial class LoggersViewModel
{
    private readonly SignalRService _signalRService;
    private readonly HttpClient _httpClient;


    [ObservableProperty]
    private Logger _selectedLogger;

    public LoggersViewModel(SignalRService signalRService, HttpClient httpClient)
    {
        _signalRService = signalRService;
        _httpClient = httpClient;
        InitSignalR();
        InitAllLoggers();
    }

    public ObservableCollection<Logger> OnlineLoggers { get; } = new();
    public ObservableCollection<Logger> AllLoggers { get; } = new();

    [RelayCommand]
    public async void NavigateLoggerConfig()
    {
        await Shell.Current.GoToAsync($"{nameof(LoggerConfigView)}?loggerId={SelectedLogger._Id}");
        // SelectedLogger = null;

    }
    private async void InitAllLoggers()
    {
        AllLoggers.Clear();
        var loggers = await _httpClient.GetFromJsonAsync<Logger[]>("loggers");
        foreach (var logger in loggers)
        {
            AllLoggers.Add(logger);
        }
    }

    private async Task InitSignalR()
    {
        OnlineLoggers.Clear();
        await _signalRService.StartConnection();

        (await _signalRService.GetOnlineLoggers()).ForEach(logger => OnlineLoggers.Add(logger));

        _signalRService.OnNewLogger = logger => OnlineLoggers.Add(logger);
        _signalRService.OnRemoveLogger =
            loggerId => OnlineLoggers.Remove(OnlineLoggers.FirstOrDefault(logger => logger._Id == loggerId));
    }
}