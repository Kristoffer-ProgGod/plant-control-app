using PlantControlApp.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using ZXing.Mobile;

namespace PlantControlApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PairingView : ContentPage
    {
        
        public PairingView()
        {
            InitializeComponent();
            BindingContext = new PairingViewModel();
        }
    }
}