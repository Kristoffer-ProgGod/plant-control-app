using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR.Client;
using Microsoft.Extensions.Logging;
using PlantControl.Models;

namespace PlantControlApp.Services;

public class SignalRService
{
    public SignalRService()
    {
        Connection = new HubConnectionBuilder()
            // .WithUrl("https://plant-control-backend.herokuapp.com/hubs/logger")
            // .WithUrl("http://10.0.2.2:5140/hubs/logger")
            .WithUrl("http://40.87.132.220:9093/hubs/logger")
            .ConfigureLogging(builder => builder.AddDebug())
            .Build();
        Connection.On<Config>("ReceiveConfig", config => OnReceiveConfig?.Invoke(config));
        Connection.On<Logger>("NewLogger", logger => OnNewLogger?.Invoke(logger));
        Connection.On<Log>("ReceiveLog", log => OnReceiveLog?.Invoke(log));
        Connection.On<string>("RemoveLogger", loggerId => OnRemoveLogger?.Invoke(loggerId));
    }

    public Action<Config> OnReceiveConfig { get; set; }
    public Action<Logger> OnNewLogger { get; set; }
    public Action<Log> OnReceiveLog { get; set; }
    public Action<string> OnRemoveLogger { get; set; }
    public HubConnection Connection { get; set; }

    public async Task StartConnection()
    {
        if (Connection.State == HubConnectionState.Connected) return;
        await Connection.StartAsync();
        await Connection.InvokeAsync("Subscribe");
    }

    public async Task<IEnumerable<Logger>> GetOnlineLoggers()
    {
        return await Connection.InvokeAsync<IEnumerable<Logger>>("GetOnlineLoggers");
    }

    public async Task SetConfig(Config config)
    {
        await Connection.InvokeAsync("SetConfig", config);
    }

    public async Task GetConfig(string id)
    {
        await Connection.InvokeAsync("GetConfig", id);
    }
}