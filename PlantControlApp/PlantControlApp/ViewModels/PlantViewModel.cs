using Microsoft.Extensions.DependencyInjection;
using PlantControl.Models;
using PlantControlApp.Views;
using System.Collections.Generic;
using System.Windows.Input;
using Xamarin.Forms;

namespace PlantControlApp.ViewModels
{
    internal class PlantViewModel : Bindable
    {

        private NavigationPage _navigationPage;

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

        public PlantViewModel()
        {
            SwitchViewCommand = new Command(async () =>
            {
                var navPage = App.Current.Services.GetService<NavigationPage>();
                await navPage.PushAsync(new CreatePlantView());
            });
        }


    }
}
