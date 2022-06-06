using System;
using System.Collections.Generic;

namespace PlantControl.Xamarin.Models
{
    internal class Certificate
    {
        public int Id { get; set; }
        public DateTime CreatedAt { get; set; }
        public List<Log> Logs { get; set; }
    }
}
