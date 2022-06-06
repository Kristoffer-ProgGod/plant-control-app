namespace PlantControl.Xamarin.Models
{
    internal class Plant
    {
        public string Name { get; set; }
        public string Id { get; set; }
        public Certificate Certificate { get; set; }
        #region Max and minimum values to verify logs against
        public int MaxTemp { get; set; }
        public int MinTemp { get; set; }
        public int MaxHumidity { get; set; }
        public int MinHumidity { get; set; }
        public int MaxMoisture { get; set; }
        public int MinMoisture { get; set; }
        #endregion
    }
}
