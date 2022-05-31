using System;
using System.Collections.Generic;
using System.Text;

namespace PlantControl.Xamarin.Models
{
    internal class Pairing
    {
        int Id { get; set; }
        Logger Logger { get; set; }
        Plant Plant { get; set; }
        DateTime CreatedAt { get; set; }
        string Name { get; set; }
    }
}
