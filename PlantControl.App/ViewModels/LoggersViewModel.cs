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
    private readonly HttpClient _httpClient;
    private readonly SignalRService _signalRService;

    [ObservableProperty] [NotifyCanExecuteChangedFor(nameof(NavigateLoggerConfigCommand))]
    private Logger _selectedLogger;

    public ObservableCollection<Logger> OnlineLoggers { get; } = new();
    public ObservableCollection<Logger> AllLoggers { get; } = new();

    public LoggersViewModel(SignalRService signalRService, HttpClient httpClient)
    {
        _signalRService = signalRService;
        _httpClient = httpClient;
        InitSignalR();
        InitAllLoggers();
    }

    public bool CanNavigate => SelectedLogger != null;

    [RelayCommand(CanExecute = nameof(CanNavigate))]
    private async void NavigateLoggerConfig()
    {
        await Shell.Current.GoToAsync($"{nameof(LoggerConfigView)}?loggerId={SelectedLogger.Id}");
        // SelectedLogger = null;
    }

    private async void InitAllLoggers()
    {
        AllLoggers.Clear();
        var loggers = await _httpClient.GetFromJsonAsync<Logger[]>("loggers");
        loggers?.ForEach(logger => AllLoggers.Add(logger));
    }

    private async Task InitSignalR()
    {
        OnlineLoggers.Clear();
        await _signalRService.StartConnection();

        (await _signalRService.GetOnlineLoggers()).ForEach(logger => OnlineLoggers.Add(logger));

        _signalRService.OnNewLogger = logger => OnlineLoggers.Add(logger);
        _signalRService.OnRemoveLogger =
            loggerId => OnlineLoggers.Remove(OnlineLoggers.FirstOrDefault(logger => logger.Id == loggerId));
    }
}