using System;
using System.ComponentModel.DataAnnotations;
using System.Net.Http;
using System.Net.Http.Json;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using PlantControl.Models;
using PlantControlApp.Services;
using Xamarin.Forms;

namespace PlantControlApp.ViewModels;

[QueryProperty("LoggerId", "loggerId")]
public partial class LoggerConfigViewModel : ObservableValidator
{
    private readonly HttpClient _httpClient;
    private readonly SignalRService _signalRService;

    [ObservableProperty] private Config _loggerConfig;

    [ObservableProperty] private Logger _logger;

    [ObservableProperty] [Url] private string _socketUrl;

    [ObservableProperty] [Url] private string _restUrl;

    [ObservableProperty] private string _loggerId;

    [ObservableProperty] private bool _isActive;

    [ObservableProperty] private double _minHumidity;

    [ObservableProperty] private double _maxHumidity;

    [ObservableProperty] private double _minTemperature;

    [ObservableProperty] private double _maxTemperature;

    [ObservableProperty] private double _soilMoist;

    [ObservableProperty] private double _soilDry;



    public LoggerConfigViewModel(HttpClient httpClient, SignalRService signalRService)
    {
        _httpClient = httpClient;
        _signalRService = signalRService;
        _signalRService.OnReceiveConfig += config =>
        {
            ReceiveConfig(config);
            InitializeConfigFields();
        };
    }    
    partial void OnLoggerIdChanged(string value) => GetLogger();

    private void InitializeConfigFields()
    {
        if (_loggerConfig is null) return;
        IsActive = _loggerConfig.Logging.Active;
        RestUrl = _loggerConfig.Logging.RestUrl;
        SocketUrl = _loggerConfig.Logging.SocketUrl;
        SoilDry = _loggerConfig.Soil.Dry;
        SoilMoist = _loggerConfig.Soil.Moist;
        MinTemperature = _loggerConfig.Air.MinTemp;
        MaxTemperature = _loggerConfig.Air.MaxTemp;
        MinHumidity = _loggerConfig.Air.MinHumid;
        MaxHumidity = _loggerConfig.Air.MaxHumid;
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

    private void ReceiveConfig(Config loggerConfig)
    {
        if (loggerConfig.Logging.LoggerId == LoggerId)
        {
            LoggerConfig = loggerConfig;
        }
    }

    private async void GetLoggerConfig() => await _signalRService.GetConfig(LoggerId);

    [RelayCommand]
    private async void SaveConfig()
    {
        Console.Out.WriteLine(_minHumidity);
        // Config newConfig = new()
        // {
        //     Air = new Air()
        //     {
        //         MinHumid = MinHumidity,
        //         MaxHumid = MaxHumidity,
        //         MinTemp = MinTemperature,
        //         MaxTemp = MaxTemperature
        //     },
        // }
        await _signalRService.SetConfig(LoggerConfig);
    }
}