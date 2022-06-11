using System.Collections.ObjectModel;
using PlantControl.Models;
using PlantControlApp.Services;

namespace PlantControlApp.ViewModels;

public class DataViewModel
{
    private readonly SignalRService _signalRService;

    public ObservableCollection<Logger> Loggers { get; }

    public DataViewModel(SignalRService signalRService)
    {
        _signalRService = signalRService;
        
        Loggers = new();
        Loggers.Add(new Logger { Id = "InitialLogger" });
    }
}