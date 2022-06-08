using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using PlantControl.Models;
using Xamarin.Essentials;
using Xamarin.Forms;
using System.Net.Http;
using Newtonsoft.Json;
using System.Net.Http.Headers;

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
                var content = GetByteArray(Plant);
                HttpClient httpClient = new HttpClient();
                var response = httpClient.PostAsync("http://40.87.132.220:9092/plants", content).Result;
                Plant plant = (Plant)DeserializeJson(response);
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

        private ByteArrayContent GetByteArray(object serializeObject)
        {
            var objectContent = JsonConvert.SerializeObject(serializeObject);
            var buffer = Encoding.UTF8.GetBytes(objectContent);
            var byteContent = new ByteArrayContent(buffer);
            byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            return byteContent;
        }

        private object DeserializeJson(HttpResponseMessage responseMessage)
        {
            return JsonConvert.DeserializeObject(responseMessage.ToString());
        }
    }
}
