using PlantControl.Xamarin.Models;
using System.Collections.Generic;
using System.Windows.Input;

namespace PlantControlApp.ViewModels
{
    internal class PlantViewModel
    {
        private List<Plant> plants;

        public List<Plant> Plants
        {
            get { return plants; }
            set { plants = value; }
        }

        private ICommand switchView;

        public ICommand SwitchView
        {
            get { return switchView; }
            set { switchView = value; }
        }

        public void CreatePlant()
        {

        }



    }
}
