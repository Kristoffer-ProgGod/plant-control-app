using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Runtime.CompilerServices;
using System.Web;
using CommunityToolkit.Mvvm.ComponentModel;
using PlantControl.Models;
using PlantControlApp.Services;
using Xamarin.Forms;

namespace PlantControlApp.ViewModels;

[ObservableObject]
[QueryProperty("LoggerId","loggerId")]
public partial class LoggerConfigViewModel
{
    public string Type { get; set; }
    private readonly HttpClient _httpClient;
    private readonly SignalRService _signalRService;

    [ObservableProperty] private LoggerConfig _loggerConfig;

    [ObservableProperty] private Logger _logger;

    [ObservableProperty] private string _loggerId;

    partial void OnLoggerIdChanged(string value)
    {
        GetLogger();
    }


    public LoggerConfigViewModel(HttpClient httpClient, SignalRService signalRService)
    {
        _httpClient = httpClient;
        _signalRService = signalRService;
        _signalRService.OnReceiveConfig += RecieveConfig;

    }

    private async void GetLogger()
    {
        var response = await _httpClient.GetFromJsonAsync<Logger>($"loggers/{LoggerId}");
        if (response is not null)
        {
            Logger = response;
            GetLoggerConfig();
        }
    }

    private void RecieveConfig(LoggerConfig loggerConfig)
    {
        if (loggerConfig.Logging.LoggerId == LoggerId)
        {
            LoggerConfig = loggerConfig;
        }
    } 
    
    private async void GetLoggerConfig()
    {
        await _signalRService.GetConfig(LoggerId);
    }
}