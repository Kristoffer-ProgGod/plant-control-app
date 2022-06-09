using PlantControl.Models;
using System.Collections.Generic;
using Microsoft.Extensions.Logging;
using Log = PlantControl.Models.Log;
using Logger = PlantControl.Models.Logger;

namespace PlantControlApp.Services;

using Microsoft.AspNetCore.SignalR.Client;
using System;
using System.Threading.Tasks;

public class SignalRService
{
    public Action<LoggerConfig> OnReceiveConfig { get; set; }
    public Action<Logger> OnNewLogger { get; set; }
    public Action<Log> OnReceiveLog { get; set; }
    public Action<string> OnRemoveLogger { get; set; }
    public HubConnection Connection { get; set; }


    public SignalRService()
    {
        Connection = new HubConnectionBuilder()
            //.WithUrl("http://10.0.2.2:5140/hubs/logger")
            .WithUrl("http://40.87.132.220:9093/hubs/logger")
            .ConfigureLogging(builder => builder.AddDebug())
            .Build();
        Connection.On<LoggerConfig>("ReceiveConfig", (config) => OnReceiveConfig?.Invoke(config));
        Connection.On<Logger>("NewLogger", (logger) => OnNewLogger?.Invoke(logger));
        Connection.On<Log>("ReceiveLog", (log) => OnReceiveLog?.Invoke(log));
        Connection.On<string>("RemoveLogger", (loggerId) => OnRemoveLogger?.Invoke(loggerId));
    }

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

    public async Task SetConfig(LoggerConfig config)
    {
        await Connection.InvokeAsync("SetConfig", config);
    }
}