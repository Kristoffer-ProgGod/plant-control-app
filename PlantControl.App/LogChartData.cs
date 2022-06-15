using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using Syncfusion.SfChart.XForms;


namespace PlantControlApp;

public partial class LogChartData : ObservableObject
{
    [ObservableProperty] private bool _showLabelAndMarker = false;

    [ObservableProperty] private string _labelFormat = "##.##°C";

    public ObservableCollection<ChartDataPoint> Data { get; set; } = new();

    [ObservableProperty] private double _minValue = 5;

    [ObservableProperty] private double _maxValue = 40;
    

    public int MaxChartValue { get; set; } = 50;

    public int MinChartValue { get; set; } = 0;

    const int StripLineWidth = 25;
    public double MinValueStart => MinValue - StripLineWidth;
    public double MinValueWidth => StripLineWidth;
    public double MaxValueStart => MaxValue;
    public double MaxValueWidth => StripLineWidth;
    public double AcceptedValueStart => MinValue;
    public double AcceptedValueWidth => MaxValue - MinValue;
}