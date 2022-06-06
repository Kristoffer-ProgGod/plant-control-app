using System;

namespace PlantControl.Xamarin.Models
{
    internal class Log
    {
        public string Id { get; set; }
        public int PairingId { get; set; }
        public DateTime Time { get; set; }
        public float Temperature { get; set; }
        public float Humidity { get; set; }
        public float Moisture { get; set; }
    }
}
