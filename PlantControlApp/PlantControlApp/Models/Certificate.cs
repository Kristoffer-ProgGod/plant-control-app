using System;
using System.Collections.Generic;
using System.Text;

namespace PlantControl.Xamarin.Models
{
    internal class Certificate
    {
        int Id { get; set; }
        DateTime CreatedAt  { get; set; }
        List<Log> Logs { get; set; }
    }
}
