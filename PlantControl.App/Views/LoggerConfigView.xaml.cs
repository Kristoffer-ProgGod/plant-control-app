using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PlantControlApp.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PlantControlApp.Views;

[XamlCompilation(XamlCompilationOptions.Compile)]
public partial class LoggerConfigView : ContentPage
{
    public LoggerConfigView()
    {
        InitializeComponent();
        BindingContext = App.Current.Services.GetService<LoggerConfigViewModel>();
    }
}