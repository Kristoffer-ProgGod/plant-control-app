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

    public Config LoggerConfig { get; set; } = new() {Air = new Air {MinTemp = 20, MaxTemp = 30}};

    [ObservableProperty] private bool _showLabelAndMarker = false;

    public List<Log> Logs { get; set; }

    public LogChartData TemperatureChartData { get; set; } = new();
    public LogChartData HumidityChartData { get; set; } = new() {LabelFormat = "###", MinValue = 40, MaxValue = 60, MaxChartValue = 100};
    public LogChartData MoistureChartData { get; set; } = new() {LabelFormat = "###", MinValue = 20, MaxValue = 40, MaxChartValue = 100};

    public ICommand AppearingCommand { get; }

    public string PlantId { get; set; }

    public DataViewModel(HttpClient http, SignalRService signalRService)
    {
        _http = http;
        _signalRService = signalRService;
        AppearingCommand = new AsyncCommand(Refresh);
    }

    private async Task GetLogs()
    {
        var response = await _http.GetAsync($"logs/plants/{PlantId}");
        var logs = await response.Content.ReadFromJsonAsync<Log[]>();
        Logs = logs.ToList();
        // Logs = new List<Log>();
        // Logs.Add(new Log {Time = DateTimeOffset.Now, Temperature = 16.3});
        // Logs.Add(new Log {Time = DateTimeOffset.Now.AddDays(1), Temperature = 16.6, Humidity = 100});
        // Logs.Add(new Log {Time = DateTimeOffset.Now.AddDays(2), Temperature = 15, Humidity = 50});
        // Logs.Add(new Log {Time = DateTimeOffset.Now.AddDays(3), Temperature = 20, Humidity = 10});
        // Logs.Add(new Log {Time = DateTimeOffset.Now.AddDays(4), Temperature = 17, Humidity = 20});
        // Logs.Add(new Log {Time = DateTimeOffset.Now.AddDays(5), Temperature = 20, Humidity = 30});
        // Logs.Add(new Log {Time = DateTimeOffset.Now.AddDays(6), Temperature = 20, Humidity = 50});
        // Logs.Add(new Log {Time = DateTimeOffset.Now.AddDays(7), Temperature = 23, Humidity = 60});
        // Logs.Add(new Log {Time = DateTimeOffset.Now.AddDays(8), Temperature = 20, Humidity = 70});
        // Logs.Add(new Log {Time = DateTimeOffset.Now.AddDays(9), Temperature = 20, Humidity = 100});
        // Logs.Add(new Log {Time = DateTimeOffset.Now.AddDays(10), Temperature = 20});
    }
    
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