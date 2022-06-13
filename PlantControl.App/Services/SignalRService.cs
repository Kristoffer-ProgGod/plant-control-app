using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR.Client;
using Microsoft.Extensions.Logging;
using PlantControl.Models;
using PlantControlApp.Enums;

namespace PlantControlApp.Services;

/**
 * Service for communicating with the PlantControl signalr server.
 * Contains delegates for handling events from the server.
 */
public class SignalRService
{
    /// <summary>
    /// Initializes connection and event handlers for the PlantControl signalr server.
    /// </summary>
    public SignalRService()
    {
        Connection = new HubConnectionBuilder()
            // .WithUrl("https://plant-control-backend.herokuapp.com/hubs/logger")
            // .WithUrl("http://10.0.2.2:5140/hubs/logger")
            .WithUrl("http://20.4.59.10:9093/hubs/logger")
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


    /// <summary>
    /// Starts connection if not already started.
    /// </summary>
    public async Task StartConnection()
    {
        if (Connection.State == HubConnectionState.Connected) return;
        await Connection.StartAsync();
        await Connection.InvokeAsync("Subscribe");
    }

    /// <summary>
    /// get all online loggers
    /// </summary>
    /// <returns></returns>
    public async Task<IEnumerable<Logger>> GetOnlineLoggers()
    {
        return await Connection.InvokeAsync<IEnumerable<Logger>>("GetOnlineLoggers");
    }

    /// <summary>
    /// send a new config to the server
    /// </summary>
    /// <param name="config"></param>
    public async Task SetConfig(Config config)
    {
        await Connection.InvokeAsync("SetConfig", config);
    }
    

    /// <summary>
    /// Gets a specific logger's config from the server
    /// </summary>
    /// <param name="id"></param>
    public async Task GetConfig(string id)
    {
        await Connection.InvokeAsync("GetConfig", id);
    }

    /// <summary>
    /// calibrate a specific logger's seonsor 
    /// </summary>
    /// <param name="calibration"></param>
    /// <param name="loggerId"></param>
    public async Task Calibrate(Calibration calibration, string loggerId)
    {
        await Connection.InvokeAsync("Calibrate", calibration.ToString(), loggerId);
    }
}