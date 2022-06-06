using PlantControl.Xamarin.Models;
using System.Collections.Generic;

namespace PlantControlApp.ViewModels
{
    internal class LogViewModel : Bindable
    {
        List<Log> Logs { get; set; }

        public LogViewModel()
        {

        }
    }
}
