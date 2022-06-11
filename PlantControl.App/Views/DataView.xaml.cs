﻿using PlantControlApp.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PlantControlApp.Views;

[XamlCompilation(XamlCompilationOptions.Compile)]
public partial class DataView : ContentPage
{
    public DataView()
    {
        InitializeComponent();
        BindingContext = App.Current.Services.GetService<DataViewModel>();
    }
}