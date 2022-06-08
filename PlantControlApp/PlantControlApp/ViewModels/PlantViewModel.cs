using Microsoft.Extensions.DependencyInjection;
using PlantControl.Models;
using PlantControlApp.Views;
using System.Collections.Generic;
using System.Windows.Input;
using PlantControlApp.Services;
using Xamarin.Forms;

namespace PlantControlApp.ViewModels
{
    internal class PlantViewModel : Bindable
    {
        private readonly SignalRService _signalRService;

        private List<Plant> plants;

        public List<Plant> Plants
        {
            get { return plants; }
            set { plants = value; }
        }

        private ICommand switchViewCommand;

        public ICommand SwitchViewCommand
        {
            get { return switchViewCommand; }
            set { switchViewCommand = value; }
        }

        public PlantViewModel(SignalRService signalRService)
        {
            _signalRService = signalRService;
            SwitchViewCommand = new Command(async () =>
            {
                //var navPage = App.Current.Services.GetService<NavigationPage>();
                //if (navPage != null)
                //await navPage.PushAsync(new CreatePlantView
            });
        }


    }
}
