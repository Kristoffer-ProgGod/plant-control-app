using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PlantControlApp.Views;

[XamlCompilation(XamlCompilationOptions.Compile)]
public partial class SliderCell
{
    public static readonly BindableProperty InfoLabelTextProperty = BindableProperty.Create(nameof(InfoLabelText), typeof(string), typeof(SliderCell));
    public static readonly BindableProperty ValueProperty = BindableProperty.Create(nameof(Value), typeof(double), typeof(SliderCell) ,defaultBindingMode: BindingMode.TwoWay);
    public static readonly BindableProperty MaximumProperty = BindableProperty.Create(nameof(Maximum), typeof(double), typeof(SliderCell),1d);
    public SliderCell()
    {
        InitializeComponent();
        InfoLabel.SetBinding(Label.TextProperty, new Binding(nameof(InfoLabelText), source:this));
        ValueLabel.SetBinding(Label.TextProperty, new Binding(nameof(Value), source:this));
        ValueSlider.SetBinding(Slider.ValueProperty, new Binding(nameof(Value), source:this, mode:BindingMode.TwoWay));
        ValueSlider.SetBinding(Slider.MaximumProperty, new Binding(nameof(Maximum), source:this));
    }
    
    public string InfoLabelText
    {
        get => (string)GetValue(InfoLabelTextProperty);
        set => SetValue(InfoLabelTextProperty, value);
    }
    
    public double Value
    {
        get => (double)GetValue(ValueProperty);
        set => SetValue(ValueProperty, value);
    }
    
    public double Maximum
    {
        get => (double)GetValue(MaximumProperty);
        set => SetValue(MaximumProperty, (double)value);
    }
}