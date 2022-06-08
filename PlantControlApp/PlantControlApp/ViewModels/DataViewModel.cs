﻿using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using PlantControl.Models;
using PlantControlApp.Services;
using Xamarin.Forms.Internals;

namespace PlantControlApp.ViewModels;

public class DataViewModel: Bindable
{
    public ObservableCollection<Logger> Loggers { get; }
    private readonly SignalRService _signalRService;
    public DataViewModel(SignalRService signalRService)
    {
        Loggers = new ObservableCollection<Logger>();
        _signalRService = signalRService;
        Loggers.Add(new Logger(){Id = "InitialLogger"});
        InitSignalR();
    }
    
    
    private async Task InitSignalR()
    {
        Loggers.Clear();
        await _signalRService.StartConnection();
        
        (await _signalRService.GetOnlineLoggers()).ForEach(logger => Loggers.Add(logger));

        _signalRService.OnNewLogger = logger => Loggers.Add(logger);
        
    }
}