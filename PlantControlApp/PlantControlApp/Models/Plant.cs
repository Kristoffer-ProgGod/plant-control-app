using System;
using System.Collections.Generic;
using System.Text;

namespace PlantControl.Xamarin.Models
{
    internal class Plant
    {
        string Name { get; set; }
        int Id { get; set; }
        Certificate Certificate { get; set; }
        #region Max and minimum values to verify logs against
        int MaxTemp { get; set; }
        int MinTemp { get; set; }
        int MaxHumidity { get; set; }
        int MinHumidity { get; set; }
        int MaxMoisture { get; set; }
        int MinMoisture { get; set; }
        #endregion
    }
}
