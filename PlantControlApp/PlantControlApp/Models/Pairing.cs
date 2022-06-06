using System;

namespace PlantControl.Xamarin.Models
{
    internal class Pairing
    {
        public string Id { get; set; }
        public Logger Logger { get; set; }
        public Plant Plant { get; set; }
        public DateTime CreatedAt { get; set; }
        public string Name { get; set; }
    }
}
