using System.Collections.Generic;
using System.Windows.Input;
using PlantControl.Models;
using Xamarin.Forms;

namespace PlantControlApp.ViewModels;

internal class PlantViewModel : Bindable
{
    public PlantViewModel()
    {
        SwitchViewCommand = new Command(async () =>
        {
            await Shell.Current.GoToAsync("////PlantView/CreatePlantView");
        });
    }

    public List<Plant> Plants { get; set; }

    public ICommand SwitchViewCommand { get; set; }
}