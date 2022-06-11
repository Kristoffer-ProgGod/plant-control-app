using System.Collections.ObjectModel;
using PlantControl.Models;
using PlantControlApp.Services;

namespace PlantControlApp.ViewModels;

public class DataViewModel : Bindable
{
    private readonly SignalRService _signalRService;

    public DataViewModel(SignalRService signalRService)
    {
        Loggers = new ObservableCollection<Logger>();
        _signalRService = signalRService;
        Loggers.Add(new Logger {_Id = "InitialLogger"});
    }

    public ObservableCollection<Logger> Loggers { get; }
}