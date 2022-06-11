using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using PlantControl.Models;
using PlantControlApp.Services;
using Xamarin.Forms.Internals;

namespace PlantControlApp.ViewModels;

public class LoggersViewsModel
{
    private readonly SignalRService _signalRService;
    private readonly HttpClient _httpClient;

    public LoggersViewsModel(SignalRService signalRService, HttpClient httpClient)
    {
        _signalRService = signalRService;
        _httpClient = httpClient;
        InitSignalR();
    }

    public ObservableCollection<Logger> OnlineLoggers { get; } = new();
    public ObservableCollection<Logger> AllLoggers { get; } = new();

    public async void initOnlineLoggers()
    {
        OnlineLoggers.Clear();
        var response = await _httpClient.GetAsync("loggers");
        var loggers = await response.Content.ReadAsAsync<Logger[]>();
        foreach (var logger in loggers)
        {
            AllLoggers.Add(logger);
        }

    }

    private async Task InitSignalR()
    {
        OnlineLoggers.Clear();
        OnlineLoggers.Add(new Logger {_Id = "InitialLogger"});
        OnlineLoggers.Add(new Logger {_Id = "InitialLogger2"});
        OnlineLoggers.Add(new Logger {_Id = "InitialLogger3"});
        await _signalRService.StartConnection();

        (await _signalRService.GetOnlineLoggers()).ForEach(logger => OnlineLoggers.Add(logger));

        _signalRService.OnNewLogger = logger => OnlineLoggers.Add(logger);
        _signalRService.OnRemoveLogger =
            loggerId => OnlineLoggers.Remove(OnlineLoggers.FirstOrDefault(logger => logger._Id == loggerId));
    }
}