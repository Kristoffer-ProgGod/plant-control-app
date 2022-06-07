using System.Collections.Generic;
using PlantControl.Models;
using PlantControl.Xamarin.Models;
using Log = PlantControl.Models.Log;
using Logger = PlantControl.Models.Logger;

namespace PlantControlApp.Services;

using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR.Client;

public class SignalRService
{
    public Action<LoggerConfig> OnReceiveConfig { get; set; }
    public Action<Logger> OnNewLogger { get; set; }
    public Action<Log> OnReceiveLog { get; set; }
    public HubConnection Connection { get; }


    public SignalRService()
    {
        Connection = new HubConnectionBuilder()
            .WithUrl("http://localhost:5140/hubs/logger")
            .Build();

        Connection.On<LoggerConfig>("ReceiveConfig", (config) => OnReceiveConfig?.Invoke(config));
        Connection.On<Logger>("NewLogger", (logger) => OnNewLogger?.Invoke(logger));
        Connection.On<Log>("ReceiveLog", (log) => OnReceiveLog?.Invoke(log));
    }

    public async Task StartConnection()
    {
        await Connection.StartAsync();
        await Connection.InvokeAsync("Subscribe");
    }

    public async Task<IEnumerable<Logger>> GetOnlineLoggers()
    {
        return await Connection.InvokeAsync<IEnumerable<Logger>>("GetOnlineLoggers");
    }

    public async Task SetConfig(LoggerConfig config)
    {
        await Connection.InvokeAsync("SetConfig", config);
    }
}