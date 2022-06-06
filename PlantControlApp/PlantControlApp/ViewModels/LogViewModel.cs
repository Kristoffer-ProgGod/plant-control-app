using PlantControl.Xamarin.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Input;
using Xamarin.Forms;
using ZXing.Mobile;

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
