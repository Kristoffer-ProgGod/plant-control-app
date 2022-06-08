using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using PlantControl.Models;
using Xamarin.Essentials;
using Xamarin.Forms;
using System.Drawing;

namespace PlantControlApp.ViewModels
{
    public class CreatePlantViewModel : Bindable
    {
        private Plant plant;

        public Plant Plant
        {
            get { return plant; }
            set { plant = value; }
        }

        private Image image = new Image();

        public Image Image
        {
            get { return image; }
            set { image = value; }
        }

        private ICommand createPlantCommand;

        public ICommand CreatePlantCommand
        {
            get { return createPlantCommand; }
            set { createPlantCommand = value; }
        }

        private ICommand takePhotoCommand;

        public ICommand TakePhotoCommand
        {
            get { return takePhotoCommand; }
            set { takePhotoCommand = value; }
        }

        public CreatePlantViewModel()
        {
            CreatePlantCommand = new Command(() =>
            {

            });

            TakePhotoCommand = new Command(async() =>
            {
                var photoFile = await MediaPicker.CapturePhotoAsync();
                var photo = await photoFile.OpenReadAsync();
                Image.Source = StreamImageSource.FromStream(() =>
                {
                    return photo;
                });
            });
        }
    }
}
