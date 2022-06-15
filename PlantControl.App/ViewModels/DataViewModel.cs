using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using System.Windows.Input;
using CommunityToolkit.Mvvm.ComponentModel;
using PlantControl.Models;
using PlantControlApp.Services;
using Syncfusion.SfChart.XForms;
using Xamarin.CommunityToolkit.ObjectModel;
using Xamarin.Forms;
using ObservableObject = CommunityToolkit.Mvvm.ComponentModel.ObservableObject;

namespace PlantControlApp.ViewModels;

[QueryProperty("PlantId", "plantId")]
public partial class DataViewModel : ObservableObject
{
    private readonly HttpClient _http;
    private readonly SignalRService _signalRService;
    [ObservableProperty] private bool _showLabelAndMarker = false;

    public Config LoggerConfig { get; set; } = new() {Air = new Air {MinTemp = 20, MaxTemp = 30}};
    public List<Log> Logs { get; set; }

    public LogChartData TemperatureChartData { get; set; } = new();

    public LogChartData HumidityChartData { get; set; } = new()
        {LabelFormat = "###", MinValue = 40, MaxValue = 60, MaxChartValue = 100};

    public LogChartData MoistureChartData { get; set; } = new()
        {LabelFormat = "###", MinValue = 20, MaxValue = 40, MaxChartValue = 100};

    public ICommand AppearingCommand { get; }

    public string PlantId { get; set; }

    public DataViewModel(HttpClient http, SignalRService signalRService)
    {
        _http = http;
        _signalRService = signalRService;
        AppearingCommand = new AsyncCommand(Refresh);
    }

    /// <summary>
    /// Gets logs from the API
    /// </summary>
    private async Task GetLogs()
    {
        var response = await _http.GetAsync($"logs/plants/{PlantId}");
        var logs = await response.Content.ReadFromJsonAsync<Log[]>();
        Logs = logs.ToList();
    }

    /// <summary>
    /// Initializes the charts by adding data from the logs
    /// </summary>
    private async Task InitCharts()
    {
        await GetLogs();

        foreach (var chartDataPoint in Logs.Select(log =>
                     new ChartDataPoint(log.Time.ToUnixTimeMilliseconds(), log.Temperature)))
        {
            TemperatureChartData.Data.Add(chartDataPoint);
        }

        foreach (var chartDataPoint in Logs.Select(log =>
                     new ChartDataPoint(log.Time.ToUnixTimeMilliseconds(), log.Humidity)))
        {
            HumidityChartData.Data.Add(chartDataPoint);
        }

        foreach (var chartDataPoint in Logs.Select(log =>
                     new ChartDataPoint(log.Time.ToUnixTimeMilliseconds(), log.Moisture)))
        {
            MoistureChartData.Data.Add(chartDataPoint);
        }
    }

    private async Task Refresh()
    {
        await InitCharts();
    }
}