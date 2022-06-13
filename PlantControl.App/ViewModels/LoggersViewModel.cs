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
        Task.Run(() =>
        {
            InitAllLoggers();
            InitSignalR();
        });
    }

    public bool CanNavigate => SelectedLogger != null;

    [RelayCommand]
    public async Task Refresh()
    {
        // await InitAllLoggers();
        // await InitSignalR();
    }
    /// <summary>
    ///  Navigate to the logger config page, with the parameters of the selected logger
    /// </summary>
    [RelayCommand(CanExecute = nameof(CanNavigate))]
    private async Task NavigateLoggerConfig()
    {
        await Shell.Current.GoToAsync($"{nameof(LoggerConfigView)}?loggerId={SelectedLogger.Id}");
        // SelectedLogger = null;
    }

    /// <summary>
    /// Get all loggers from the server
    /// </summary>
    private async Task InitAllLoggers()
    {
        AllLoggers.Clear();
        var loggers = await _httpClient.GetFromJsonAsync<Logger[]>("loggers");
        loggers?.ForEach(logger => AllLoggers.Add(logger));
    }

    
    /// <summary>
    /// Initialize the SignalR connection and get all online loggers
    /// </summary>
    private async Task InitSignalR()
    {
        OnlineLoggers.Clear();
        await _signalRService.StartConnection();

        var loggers = await _signalRService.GetOnlineLoggers();
        loggers.ForEach(logger => OnlineLoggers.Add(logger));

        _signalRService.OnNewLogger = logger => OnlineLoggers.Add(logger);
        _signalRService.OnRemoveLogger =
            loggerId => OnlineLoggers.Remove(OnlineLoggers.FirstOrDefault(logger => logger.Id == loggerId));
    }
}