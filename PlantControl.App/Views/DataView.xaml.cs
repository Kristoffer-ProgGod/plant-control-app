using System;
using System.Collections.ObjectModel;
using PlantControl.Models;
using PlantControlApp.ViewModels;
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
    private void NumericalAxis_LabelCreated(object sender, Syncfusion.SfChart.XForms.ChartAxisLabelEventArgs e)
    {
        double value = Convert.ToDouble(e.LabelContent);
            
        //Converting corresponding double value to data time value.
        DateTime date = (new DateTime(1970, 1, 1).AddMilliseconds(value));
        e.LabelContent = String.Format("{0:g}", date);
    }
}