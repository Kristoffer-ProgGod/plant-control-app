using System;
using System.Collections.Generic;
using System.Text;

namespace PlantControl.Xamarin.Models
{
    internal class Log
    {
        int Id { get; set; }
        int PairingId { get; set; }
        DateTime Time { get; set; }
        float Temperature { get; set; }
        float Humidity { get; set; }
        float Moisture { get; set; }
    }
}
