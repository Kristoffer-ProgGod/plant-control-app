using System;
using System.Collections.ObjectModel;
using System.Net.Http;
using System.Threading.Tasks;
using System.Windows.Input;
using PlantControl.Models;
using PlantControlApp.Services;
using Syncfusion.SfChart.XForms;
using Xamarin.CommunityToolkit.ObjectModel;

namespace PlantControlApp.ViewModels;

public class DataViewModel
{

    private readonly HttpClient _http;
    private readonly SignalRService _signalRService;
    public ObservableCollection<Log> Logs { get; set; } = new();
    public ObservableCollection<ChartDataPoint> Data { get; set; } = new();

    public Config LoggerConfig { get; set; } = new() {Air = new Air() {MinTemp = 10, MaxTemp = 30}};

    public double MinValue { get; set; } = 15;

    public double MaxValue { get; set; } = 30;
    
    const int StripLineWidth = 25;
    public double MinValueStart => MinValue - StripLineWidth;
    public double MinValueWidth => StripLineWidth;
    public double MaxValueStart => MaxValue;
    public double MaxValueWidth => StripLineWidth;
    public double AcceptedValueStart => MinValue;
    public double AcceptedValueWidth => MaxValue - MinValue;

    public ICommand AppearingCommand { get; }

    public DataViewModel(HttpClient http, SignalRService signalRService)
    {
        _http = http;
        _signalRService = signalRService;
        Logs.Add(new Log {Time = DateTimeOffset.Now, Temperature = 16.3});
        Logs.Add(new Log {Time = DateTimeOffset.Now.AddDays(1), Temperature = 16.6});
        Logs.Add(new Log {Time = DateTimeOffset.Now.AddDays(2), Temperature = 15});
        Logs.Add(new Log {Time = DateTimeOffset.Now.AddDays(3), Temperature = 20});
        Logs.Add(new Log {Time = DateTimeOffset.Now.AddDays(4), Temperature = 17});
        Logs.Add(new Log {Time = DateTimeOffset.Now.AddDays(5), Temperature = 20});
        Logs.Add(new Log {Time = DateTimeOffset.Now.AddDays(6), Temperature = 20});
        Logs.Add(new Log {Time = DateTimeOffset.Now.AddDays(7), Temperature = 23});
        Logs.Add(new Log {Time = DateTimeOffset.Now.AddDays(8), Temperature = 20});
        Logs.Add(new Log {Time = DateTimeOffset.Now.AddDays(9), Temperature = 20});
        Logs.Add(new Log {Time = DateTimeOffset.Now.AddDays(10), Temperature = 20});
        
        foreach (var log in Logs)
        {
            Data.Add(new ChartDataPoint(log.Time.ToUnixTimeMilliseconds(), log.Temperature));
        }

        AppearingCommand = new AsyncCommand(Refresh);
    }

    private async Task Refresh()
    {
        // Todo
    }
}