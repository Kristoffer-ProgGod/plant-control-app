using System.Collections.Generic;
using PlantControl.Models;

namespace PlantControlApp.ViewModels;

public class LogViewModel : Bindable
{
    private List<Log> Logs { get; set; }
}