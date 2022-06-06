using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace PlantControlApp.Models
{
    internal class FlyoutPageItem : BindableObject
    {
        public string Title { get; set; }
        public string IconSource { get; set; }

        public object TargetType { get; set; }
    }
}
