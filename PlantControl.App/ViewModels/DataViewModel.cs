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

    public Config LoggerConfig { get; set; } = new() {Air = new Air() {MinTemp = 2, MaxTemp = 30}};

    public double AcceptedTemp => LoggerConfig.Air.MaxTemp - LoggerConfig.Air.MinTemp;

    public ICommand AppearingCommand { get; }

    public DataViewModel(HttpClient http, SignalRService signalRService)
    {
        _http = http;
        _signalRService = signalRService;
        Logs.Add(new Log {Time = DateTimeOffset.Now, Temperature = 10.3});
        Logs.Add(new Log {Time = DateTimeOffset.Now.AddDays(1), Temperature = 10.6});
        Logs.Add(new Log {Time = DateTimeOffset.Now.AddDays(2), Temperature = 11});
        Logs.Add(new Log {Time = DateTimeOffset.Now.AddDays(3), Temperature = 15});
        Logs.Add(new Log {Time = DateTimeOffset.Now.AddDays(4), Temperature = 17});
        Logs.Add(new Log {Time = DateTimeOffset.Now.AddDays(5), Temperature = 20});
        Logs.Add(new Log {Time = DateTimeOffset.Now.AddDays(6), Temperature = 40});
        Logs.Add(new Log {Time = DateTimeOffset.Now.AddDays(7), Temperature = 40});
        Logs.Add(new Log {Time = DateTimeOffset.Now.AddDays(8), Temperature = 30});
        Logs.Add(new Log {Time = DateTimeOffset.Now.AddDays(9), Temperature = 15});
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