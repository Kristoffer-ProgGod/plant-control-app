using System;
using System.ComponentModel.DataAnnotations;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using PlantControl.Models;
using PlantControlApp.Enums;
using PlantControlApp.Services;
using Xamarin.Forms;

namespace PlantControlApp.ViewModels;

[QueryProperty("LoggerId", "loggerId")]
public partial class LoggerConfigViewModel : ObservableValidator
{
    private readonly HttpClient _httpClient;
    private readonly SignalRService _signalRService;

    [ObservableProperty] private bool _isActive;

    [ObservableProperty] private Logger _logger;

    [ObservableProperty] private Config _loggerConfig;

    [ObservableProperty] private string _loggerId;

    [ObservableProperty] private double _maxHumidity;

    [ObservableProperty] private double _maxTemperature;

    [ObservableProperty] private double _minHumidity;

    [ObservableProperty] private double _minTemperature;

    [ObservableProperty] [Url] private string _restUrl;

    [ObservableProperty] [Url] private string _socketUrl;

    [ObservableProperty] private double _soilDry;

    [ObservableProperty] private double _soilMoist;


    public LoggerConfigViewModel(HttpClient httpClient, SignalRService signalRService)
    {
        _httpClient = httpClient;
        _signalRService = signalRService;
        _signalRService.OnReceiveConfig += ReceiveConfig;
    }

    partial void OnLoggerIdChanged(string value) => GetLogger();

    /// <summary>
    /// Initializes the view models properties from the remote logger config.
    /// </summary>
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

    /// <summary>
    /// Gets the selected logger from the server
    /// </summary>
    private async Task GetLogger()
    {
        var response = await _httpClient.GetFromJsonAsync<Logger>($"loggers/{LoggerId}");
        if (response is not null)
        {
            Logger = response;
            GetLoggerConfig();
        }
    }

    /// <summary>
    /// Callback for when the logger config is received from the server.
    /// </summary>
    /// <param name="loggerConfig"></param>
    private void ReceiveConfig(Config loggerConfig)
    {
        if (loggerConfig.Logging.LoggerId == LoggerId)
        {
            LoggerConfig = loggerConfig;
            InitializeConfigFields();
        }
    }

    private async Task GetLoggerConfig() => await _signalRService.GetConfig(LoggerId);

    /// <summary>
    /// Sends the updated config to the server.
    /// </summary>
    [RelayCommand]
    private async Task SaveConfig()
    {
        Config newConfig = new()
        {
            Air = new Air
            {
                MinHumid = MinHumidity,
                MaxHumid = MaxHumidity,
                MinTemp = MinTemperature,
                MaxTemp = MaxTemperature
            },
            Logging = new Logging
            {
                Active = IsActive,
                RestUrl = RestUrl,
                SocketUrl = SocketUrl,
                LoggerId = LoggerId
            },
            Soil = new Soil
            {
                Moist = SoilMoist,
                Dry = SoilDry
            }
        };
        await _signalRService.SetConfig(newConfig);
    }

    /// <summary>
    /// Calibrates a specific logger's sensor
    /// </summary>
    /// <param name="param"></param>
    [RelayCommand]
    private async Task Calibrate(string param)
    {
        if (Enum.TryParse<Calibration>(param, out var calibrationType))
        {
            await _signalRService.Calibrate(calibrationType, LoggerId);
        }
    }
}