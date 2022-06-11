﻿using System;
using System.Net.Http;
using Microsoft.Extensions.DependencyInjection;
using PlantControlApp.Services;
using PlantControlApp.ViewModels;
using PlantControlApp.Views;
using Xamarin.Forms;

namespace PlantControlApp;

public partial class App : Application
{
    public App()
    {
        InitializeComponent();


        Services = ConfigureServices();
        MainPage = new MainPage();
    }

    /// <summary>
    ///     Gets the current <see cref="App" /> instance in use
    /// </summary>
    public new static App Current => (App) Application.Current;

    /// <summary>
    ///     Gets the <see cref="IServiceProvider" /> instance to resolve application services.
    /// </summary>
    public IServiceProvider Services { get; }

    private static IServiceProvider ConfigureServices()
    {
        var services = new ServiceCollection();
        var httpClient = new HttpClient
        {
            BaseAddress = new Uri("http://20.4.59.10:9092/"),
            Timeout = TimeSpan.FromSeconds(10)
        };

        services.AddSingleton(httpClient);
        services.AddSingleton<SignalRService>();
        services.AddSingleton<ScannerService>();

        services.AddTransient<PairingViewModel>();
        services.AddTransient<CreatePlantViewModel>();
        services.AddTransient<PlantViewModel>();
        services.AddTransient<DataViewModel>();
        services.AddTransient<LoggersViewsModel>();

        return services.BuildServiceProvider();
    }

    protected override void OnStart()
    {
    }

    protected override void OnSleep()
    {
    }

    protected override void OnResume()
    {
    }
}