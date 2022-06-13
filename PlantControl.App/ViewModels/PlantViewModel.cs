using System.Collections.Generic;
using System.Windows.Input;
using PlantControl.Models;
using Xamarin.CommunityToolkit.ObjectModel;
using Xamarin.Forms;

namespace PlantControlApp.ViewModels;

internal class PlantViewModel
{
    public List<Plant> Plants { get; set; }
    public ICommand SwitchViewCommand { get; set; }

    public PlantViewModel()
    {
        SwitchViewCommand = new AsyncCommand(async () => await Shell.Current.GoToAsync("//PlantView/CreatePlantView"));
    }
}