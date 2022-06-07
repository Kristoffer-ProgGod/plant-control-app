using Microsoft.Extensions.DependencyInjection;
using PlantControlApp.Views;
using System;
using Xamarin.Forms;

namespace PlantControlApp
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            MainPage = new MainPage();
            
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
