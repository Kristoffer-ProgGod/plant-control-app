﻿using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Net.Http;
using System.Threading.Tasks;
using System.Windows.Input;
using Newtonsoft.Json;
using PlantControl.Models;
using PlantControlApp.Services;
using Xamarin.CommunityToolkit.ObjectModel;
using ObservableObject = CommunityToolkit.Mvvm.ComponentModel.ObservableObject;

namespace PlantControlApp.ViewModels;

internal class PairingViewModel : ObservableObject
{
    private readonly HttpClient _http;
    private readonly ScannerService _scannerService;
    private bool _isRefreshing;

    public bool IsRefreshing
    {
        get => _isRefreshing;
        set => SetProperty(ref _isRefreshing, value);
    }

    public ObservableCollection<Pairing> Pairings { get; }
    public ICommand RefreshCommand { get; }
    public ICommand CreatePairingCommand { get; }


    public PairingViewModel(HttpClient http, ScannerService scannerService)
    {
        _http = http;
        _scannerService = scannerService;

        Pairings = new ObservableCollection<Pairing>();
        RefreshCommand = new AsyncCommand(Refresh);
        CreatePairingCommand = new AsyncCommand(CreatePairing);
    }

    private async Task Refresh()
    {
        IsRefreshing = true;

        try
        {
            var response = await _http.GetStringAsync("pairings");

            if (response == null) return;

            var pairings = JsonConvert.DeserializeObject<Pairing[]>(response);

            if (pairings == null) return;

            Pairings.Clear();

            foreach (var pairing in pairings)
            {
                Pairings.Add(pairing);
            }
        }
        catch (Exception e)
        {
            Debug.WriteLine(e.Message);
        }

        IsRefreshing = false;
    }

    private async Task CreatePairing()
    {
        var plantId = await _scannerService.Scan(bottomText: "Scan the QR code on the plant");
        var loggerId = await _scannerService.Scan(bottomText: "Scan the QR code on the logger");
    }
}