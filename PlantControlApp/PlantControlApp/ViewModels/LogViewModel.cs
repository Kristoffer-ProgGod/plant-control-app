using PlantControl.Models;
using System.Collections.Generic;

namespace PlantControlApp.ViewModels
{
    public class LogViewModel : Bindable
    {
        List<Log> Logs { get; set; }

        public LogViewModel()
        {

        }
    }
}
